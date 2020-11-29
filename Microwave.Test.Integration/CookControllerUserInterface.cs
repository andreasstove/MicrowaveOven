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
    class CookControllerUserInterface
    {

        //real
        private Output _output;
        private PowerTube _powerTube;
        private Display _display;
        private Light _light;
        private UserInterface _userInterface;
        private CookController _cookController;
        private Timer _timer;

        private System.IO.StringWriter _stringWriter;

        //stubs

        private IDoor _door;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _cancelButton;

        [SetUp]
        public void Setup()
        {
            _output = new Output();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _light = new Light(_output);
            _timer = new Timer();

            _cookController = new CookController(_timer, _display, _powerTube);
            _door = Substitute.For<IDoor>();
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _cancelButton = Substitute.For<IButton>();
            _userInterface = new UserInterface(_powerButton, _timeButton, _cancelButton, _door, _display, _light, _cookController);
            _stringWriter = new System.IO.StringWriter();
            Console.SetOut(_stringWriter);

        }

        [Test]
        public void OnStartCancelPressed_caseSetTime_TurnOff()
        {
            
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _cancelButton.Pressed += Raise.Event();

            StringAssert.Contains("PowerTube works with", _stringWriter.ToString());
        }
        [Test]
        public void OnStartCancelPressed_caseSetTimeWithMorePower_TurnOff()
        {

            _powerButton.Pressed += Raise.Event();
            _powerButton.Pressed += Raise.Event();
            _powerButton.Pressed += Raise.Event();
            _powerButton.Pressed += Raise.Event();
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _cancelButton.Pressed += Raise.Event();

            StringAssert.Contains("PowerTube works with", _stringWriter.ToString());
        }

        [Test]
        public void OnStartCancelPressed_caseCooking_TurnOff()
        {

            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _cancelButton.Pressed += Raise.Event();
            _cancelButton.Pressed += Raise.Event();
            StringAssert.Contains("PowerTube turned off", _stringWriter.ToString());
        }

        [Test]
        public void OnDoorOpenedFromCaseCooking()
        {

            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _cancelButton.Pressed += Raise.Event();
            _door.Opened += Raise.Event();
            StringAssert.Contains("PowerTube turned off", _stringWriter.ToString());
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
