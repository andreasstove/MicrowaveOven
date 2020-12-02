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
    class TimeButtonUserInterFace
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

        private System.IO.StringWriter _stringWriter;

        //stubs

        private IDoor _door;
        private IButton _cancelButton;

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
            _cookController = new CookController(_timer, _display, _powerTube);
            _door = Substitute.For<IDoor>();

            _cancelButton = Substitute.For<IButton>();
            _userInterface = new UserInterface(_powerButton, _timeButton, _cancelButton, _door, _display, _light, _cookController);
            _cookController.UI = _userInterface;
            _stringWriter = new System.IO.StringWriter();
            Console.SetOut(_stringWriter);

        }

        [Test]
        public void TimerButton1TimeUserInterFace()
        {
            _door.Opened += Raise.Event();
            _door.Closed += Raise.Event();
            _powerButton.Press();
            _timeButton.Press();
            StringAssert.Contains("01:00", _stringWriter.ToString());
        }

        [Test]
        public void TimerButton5TimeUserInterFace()
        {
            _door.Opened += Raise.Event();
            _door.Closed += Raise.Event();
            _powerButton.Press();
            for (int i = 0; i < 5; i++)
            {
                _timeButton.Press();
            }
            StringAssert.Contains("05:00", _stringWriter.ToString());
        }

        [Test]
        public void TimerButton60TimeUserInterFace()
        {
            _door.Opened += Raise.Event();
            _door.Closed += Raise.Event();
            _powerButton.Press();
            for (int i = 0; i < 60; i++)
            {
                _timeButton.Press();
            }
            StringAssert.Contains("60:00", _stringWriter.ToString());
        }

        [Test]
        public void TimerButton61TimeUserInterFace()
        {
            _door.Opened += Raise.Event();
            _door.Closed += Raise.Event();
            _powerButton.Press();
            for (int i = 0; i < 60; i++)
            {
                _timeButton.Press();
            }
            System.IO.StringWriter _stringWriter1 = new System.IO.StringWriter();
            Console.SetOut(_stringWriter1);
            _timeButton.Press();
            StringAssert.Contains("01:00", _stringWriter1.ToString());
        }
    }
}
