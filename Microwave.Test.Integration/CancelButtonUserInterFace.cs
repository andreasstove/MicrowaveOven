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
    class CancelButtonUserInterFace
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
        private System.IO.StringWriter _stringWriter;

        //stubs

        private IDoor _door;


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
            _cookController = new CookController(_timer, _display, _powerTube);
            _door = Substitute.For<IDoor>();


            _userInterface = new UserInterface(_powerButton, _timeButton, _cancelButton, _door, _display, _light, _cookController);
            _cookController.UI = _userInterface;
            _stringWriter = new System.IO.StringWriter();
            Console.SetOut(_stringWriter);

        }


        [Test]
        public void CancelButtonInSetPowerStateUserInterFace()
        {
            _door.Opened += Raise.Event();
            _door.Closed += Raise.Event();
            _powerButton.Press();
            _cancelButton.Press();
            StringAssert.Contains("Display cleared", _stringWriter.ToString());
        }

        [Test]
        public void CancelButtonInSetTimeStateUserInterFace()
        {
            _door.Opened += Raise.Event();
            _door.Closed += Raise.Event();
            _powerButton.Press();
            _timeButton.Press();
            _cancelButton.Press();
            StringAssert.Contains("50 W 1:00", _stringWriter.ToString());
        }

        [Test]
        public void CancelButtonInSetCookingStateUserInterFace()
        {
            _door.Opened += Raise.Event();
            _door.Closed += Raise.Event();
            _powerButton.Press();
            _timeButton.Press();
            _cancelButton.Press();
            _cancelButton.Press();
            StringAssert.Contains("Display cleared", _stringWriter.ToString());
        }

        [Test]
        public void NotATestJustForHelp()
        {
            string expected = "Light is turned off";
            _powerButton.Pressed += Raise.Event();
            _light.TurnOn();
            _stringWriter = new System.IO.StringWriter();
            Console.SetOut(_stringWriter);
            _cancelButton.Pressed += Raise.Event();

            StringAssert.Contains(expected, _stringWriter.ToString());
        }
    }
}
