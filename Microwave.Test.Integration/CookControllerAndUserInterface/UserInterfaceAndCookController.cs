using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Microwave.Test.Integration
{
    class UserInterfaceAndCookController
    {
        //real
        private Output _output;
        private PowerTube _powerTube;
        private Display _display;
        private Light _light;
        private UserInterface _userInterface;
        private CookController _cookController;
        private Classes.Boundary.Timer _timer;

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
            _timer = new Classes.Boundary.Timer();
            _cookController = new CookController(_timer, _display, _powerTube);
            _door = Substitute.For<IDoor>();
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _cancelButton = Substitute.For<IButton>();
            _userInterface = new UserInterface(_powerButton, _timeButton, _cancelButton, _door, _display, _light, _cookController);

            _cookController.UI = _userInterface;

            _stringWriter = new System.IO.StringWriter();
            Console.SetOut(_stringWriter);
        }

        [Test]
        public void OnTimerExpired_IsNotCalled_CookingIsDone()
        {
            int power = 100;
            int time = 2;
            int sleepingTime = 3000;
            string expected = $"Display cleared";

            _cookController.StartCooking(power, time);
                       
            
            Thread.Sleep(sleepingTime);

            StringAssert.DoesNotContain(expected, _stringWriter.ToString());
        }
        [Test]
        public void OnTimerExpired_IsCalled_CookingIsDone()
        {
            int sleepingTime = 61000;
            string expected = $"Display cleared";

            _door.Opened += Raise.Event();
            _door.Closed += Raise.Event();
            _powerButton.Pressed += Raise.Event();
            _timeButton.Pressed += Raise.Event();
            _cancelButton.Pressed += Raise.Event();

            Thread.Sleep(sleepingTime);

            StringAssert.Contains(expected, _stringWriter.ToString());
        }
    }
}
