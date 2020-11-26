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
    [TestFixture]
    class PowerTubeAndCookController
    {
        private ICookController _cookController;
        private IPowerTube _powerTube;
        private IDisplay _display;
        private ITimer _timer;
        private IOutput _output;
        private System.IO.StringWriter _stringWriter;
        [SetUp]
        public void Setup()
        {
            _timer = Substitute.For<ITimer>();
            _output = new Output();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _cookController = new CookController(_timer, _display, _powerTube);
            _stringWriter = new System.IO.StringWriter();
            Console.SetOut(_stringWriter);
        }
        //test
        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        public void StartCooking_Called_TurnOn(int power)
        {
            int time = 30;
            _cookController.StartCooking(power, time);
            string expected = ($"PowerTube works with {power}");
            StringAssert.Contains(expected, _stringWriter.ToString());
        }
    }
}
