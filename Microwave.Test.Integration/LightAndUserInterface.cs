﻿using System;
using System.Collections.Generic;
using System.Text;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;
namespace Microwave.Test.Integration
{
    class LightAndUserInterface
    {
        //real
        private IOutput _output;
        private IPowerTube _powerTube;
        private IDisplay _display;
        private ILight _light;
        private IUserInterface _userInterface;

        private System.IO.StringWriter _stringWriter;

        //stubs
        private ICookController _cookController;
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

            _cookController = Substitute.For<ICookController>();
            _door = Substitute.For<IDoor>();
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _cancelButton = Substitute.For<IButton>();
            _userInterface = new UserInterface(_powerButton, _timeButton, _cancelButton, _door, _display, _light, _cookController);

        }
        [Test]
        public void OnStartCancelPressed_caseSetPower_TurnOff()
        {
            string expected = "Light is turned off";
            _powerButton.Pressed += Raise.Event();
            _light.TurnOn();
            _stringWriter = new System.IO.StringWriter();
            Console.SetOut(_stringWriter);
            _cancelButton.Pressed += Raise.Event();
              
            StringAssert.Contains(expected, _stringWriter.ToString());
        }
        [Test]
        public void OnStartCancelPressed_caseSetTime_TurnOn()
        {
            string expected = "Light is turned on";
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _stringWriter = new System.IO.StringWriter();
            Console.SetOut(_stringWriter);
            _cancelButton.Pressed += Raise.Event();

            StringAssert.Contains(expected, _stringWriter.ToString());
        }
        [Test]
        public void OnStartCancelPressed_caseCooking_TurnOff()
        {
            string expected = "Light is turned off";
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _cancelButton.Pressed += Raise.Event();
            _stringWriter = new System.IO.StringWriter();
            Console.SetOut(_stringWriter);
            _cancelButton.Pressed += Raise.Event();

            StringAssert.Contains(expected, _stringWriter.ToString());
        }
        [Test]
        public void OnDoorOpened_caseReady_TurnOn()
        {
            string expected = "Light is turned on";
            _stringWriter = new System.IO.StringWriter();
            Console.SetOut(_stringWriter);
            _door.Opened += Raise.Event();

            StringAssert.Contains(expected, _stringWriter.ToString());
        }
        [Test]
        public void OnDoorOpened_caseSetPower_TurnOn()
        {
            string expected = "Light is turned on";
            _powerButton.Pressed += Raise.Event();
            _stringWriter = new System.IO.StringWriter();
            Console.SetOut(_stringWriter);
            _door.Opened += Raise.Event();

            StringAssert.Contains(expected, _stringWriter.ToString());
        }
        [Test]
        public void OnDoorOpened_caseSetTime_TurnOn()
        {
            string expected = "Light is turned on";
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _stringWriter = new System.IO.StringWriter();
            Console.SetOut(_stringWriter);
            _door.Opened += Raise.Event();

            StringAssert.Contains(expected, _stringWriter.ToString());
        }
        [Test]
        public void OnDoorClosed_caseDoorOpen_TurnOff()
        {
            string expected = "Light is turned off";
            _door.Opened += Raise.Event();
            _stringWriter = new System.IO.StringWriter();
            Console.SetOut(_stringWriter);
            _door.Closed += Raise.Event();

            StringAssert.Contains(expected, _stringWriter.ToString());
        }

        [Test]
        public void CookingIsDone_caseCooking_TurnOff()
        {
            string expected = "Light is turned off";
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _cancelButton.Pressed += Raise.Event();
            _stringWriter = new System.IO.StringWriter();
            Console.SetOut(_stringWriter);
            _userInterface.CookingIsDone();

            StringAssert.Contains(expected, _stringWriter.ToString());
        }


    }
}
