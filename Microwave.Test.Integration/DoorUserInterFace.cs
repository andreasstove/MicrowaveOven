using System;
using System.Collections.Generic;
using System.Text;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;


namespace Microwave.Test.Integration
{
    class DoorUserInterFace
    {
        //real
        private Output _output;
        private PowerTube _powerTube;
        private Display _display;
        private Light _light;
        private UserInterface _userInterface;
        private CookController _cookController;
        private Timer _timer;
        private Button _powerButton;
        private Button _timeButton;
        private Button _cancelButton;
        private Door _door;
        private System.IO.StringWriter _stringWriter;

        //stubs




        [SetUp]
        public void Setup()
        {
            _output = new Output();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _light = new Light(_output);
            _timer = new Timer();
            _powerButton = new Button();
            _timeButton = new Button();
            _cancelButton = new Button();
            _door = new Door();
            _cookController = new CookController(_timer, _display, _powerTube);


            _userInterface = new UserInterface(_powerButton, _timeButton, _cancelButton, _door, _display, _light, _cookController);
            _cookController.UI = _userInterface;
            _stringWriter = new System.IO.StringWriter();
            Console.SetOut(_stringWriter);

        }

        [Test]
        public void DoorOpened()
        {
            _door.Open();
            StringAssert.Contains("Light is turned on", _stringWriter.ToString());
        }
        [Test]
        public void DoorOpenedClosed()
        {
            string expected = "Light is turned off";
            _door.Open();
            System.IO.StringWriter _stringWriter1 = new System.IO.StringWriter();
            Console.SetOut(_stringWriter1);
            _door.Close();
            StringAssert.Contains(expected, _stringWriter1.ToString());
        }

        [Test]
        public void DoorOpenedInSetPowerState()
        {
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _door.Open();
            StringAssert.Contains("Display cleared", _stringWriter.ToString());
        }

        [Test]
        public void DoorOpenedInSetTimeState()
        {
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _door.Open();
            StringAssert.Contains("Display cleared", _stringWriter.ToString());
        }

        [Test]
        public void DoorOpenedInCookingState()
        {
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _cancelButton.Press();
            _door.Open();
            StringAssert.Contains("Display cleared", _stringWriter.ToString());
        }
        
        
        
        [Test]
        public void DoorOpenedAfterCookingIsDone()
        {
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _cancelButton.Press();
            System.Threading.Thread.Sleep(65000);
            System.IO.StringWriter stringWriter1 = new System.IO.StringWriter();
            Console.SetOut(stringWriter1);
            _door.Open();
            StringAssert.Contains("Light is turned off", _stringWriter.ToString());
        }


        [Test]
        public void DoorClosedAfterCookingIsDone()
        {
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _cancelButton.Press();
            System.Threading.Thread.Sleep(65000);
            System.IO.StringWriter stringWriter1 = new System.IO.StringWriter();
            Console.SetOut(stringWriter1);
            _door.Open();
            _door.Close();
            StringAssert.Contains("Light is turned off", _stringWriter.ToString());
        }
    }
}
