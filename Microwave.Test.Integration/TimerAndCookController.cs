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
    class TimerAndCookController
    {
        private Output _output;
        private PowerTube _powerTube;
        private Display _display;
        private CookController _cookController;
        private Classes.Boundary.Timer _timer;
        private IUserInterface _userInterface;

        private System.IO.StringWriter _stringWriter;

        [SetUp]
        public void Setup()
        {
            _userInterface = Substitute.For<IUserInterface>();
            _output = new Output();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _timer = new Classes.Boundary.Timer();
            _cookController = new CookController(_timer, _display, _powerTube);
            _cookController = new CookController(_timer, _display, _powerTube, _userInterface);
            _stringWriter = new System.IO.StringWriter();
            Console.SetOut(_stringWriter);

        }

        [Test]
        public void OnTimerTick_Called_ShowTime()
        {
            int power = 100;
            int time = 10000;
            int sleepingTime = 4000;
            string expected = $"Display shows: {(time-(sleepingTime/1000)+1) / 60:D2}:{(time - (sleepingTime/1000)+1) % 60:D2}";

            _cookController.StartCooking(power, time);
            Thread.Sleep(sleepingTime);
            StringAssert.Contains(expected, _stringWriter.ToString());
        }
        [Test]
        public void OnTimerExpired_Called_TurnOff()
        {
            int power = 100;
            int time = 6000;
            int sleepingTime = 7000;
            string expected = "PowerTube turned off";

            _cookController.StartCooking(power, time);
            Thread.Sleep(sleepingTime);

            StringAssert.Contains(expected, _stringWriter.ToString());
        }
    }
}
