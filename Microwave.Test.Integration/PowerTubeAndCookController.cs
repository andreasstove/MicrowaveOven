using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using Microwave.Classes.Boundary;
using NSubstitute;
namespace Microwave.Test.Integration
{
    //Done
    [TestFixture]
    class PowerTubeAndCookController
    {
        private Output _output;
        private CookController _cookController;
        private PowerTube _powerTube;
        private Display _display;
        private ITimer _timer;
        private IUserInterface _userInterface;
        private System.IO.StringWriter _stringWriter;
        [SetUp]
        public void Setup()
        {
            _timer = Substitute.For<ITimer>();
            _userInterface = Substitute.For<IUserInterface>();
            _output = new Output();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _cookController = new CookController(_timer, _display, _powerTube, _userInterface);
            
        }
        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        public void StartCooking_Called_TurnOn(int power)
        {
            _stringWriter = new System.IO.StringWriter();
            Console.SetOut(_stringWriter);
            int time = 30;
            _cookController.StartCooking(power, time);
            string expected = ($"PowerTube works with {power}");
            StringAssert.Contains(expected, _stringWriter.ToString());
        }

        [Test]
        public void Stop_Called_TurnOff()
        {
            int time = 30;
            int power = 50;
            string expected = "PowerTube turned off";
            _cookController.StartCooking(power, time);
            _stringWriter = new System.IO.StringWriter();
            Console.SetOut(_stringWriter);
            _cookController.Stop();
            StringAssert.Contains(expected, _stringWriter.ToString());
        }

        [Test]
        public void OnTimerExpired_Called_TurnOff()
        {
            int time = 30;
            int power = 50;
            string expected = "PowerTube turned off";
            _cookController.StartCooking(power, time);
            _stringWriter = new System.IO.StringWriter();
            Console.SetOut(_stringWriter);
            _timer.Expired += Raise.Event();
            StringAssert.Contains(expected, _stringWriter.ToString());
        }


    }
}
