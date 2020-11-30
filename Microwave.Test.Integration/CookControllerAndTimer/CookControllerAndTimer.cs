using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using System.Threading;
using NSubstitute;

namespace Microwave.Test.Integration
{
    class CookControllerAndTimer
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
        public void StartCooking_Called_Start()
        {
            int power = 50;
            int time = 1000;
            _cookController.StartCooking(power, time);
            Assert.That(_timer.TimeRemaining, Is.EqualTo(time));
        }
        [Test]
        public void Stop_Called_Stop()
        {
            int power = 100;
            int time = 5000;
            int sleepingTime = 2100;
            _cookController.StartCooking(power, time);
            Thread.Sleep(sleepingTime);
            _cookController.Stop();
            Thread.Sleep(21000);
            int expected = time-(sleepingTime / 1000);
            Assert.That(_timer.TimeRemaining, Is.EqualTo(expected));
        }
    }
}
