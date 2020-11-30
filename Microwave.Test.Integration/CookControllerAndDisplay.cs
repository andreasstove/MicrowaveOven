using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using Microwave.Classes.Controllers; 
using NSubstitute;

namespace Microwave.Test.Integration
{
    class CookControllerAndDisplay
    {
        private Output _output;
        private PowerTube _powerTube;
        private Display _display;
        private CookController _cookController;
      
        private System.IO.StringWriter _stringWriter;

        //stubs
        private ITimer _timer;
        [SetUp]
        public void Setup()
        {
            _output = new Output();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _timer = Substitute.For<ITimer>();
            _cookController = new CookController(_timer, _display, _powerTube);

            
        }
        //test
        [Test]
        public void OnTimerTick_called_myDisplay()
        {
            string expected = "Display shows: 02: 00";
            int power = 20;
            int time = 12090;
            _cookController.StartCooking(power, time);
            _stringWriter = new System.IO.StringWriter();
            Console.SetOut(_stringWriter);
            _timer.TimerTick += Raise.Event();
            StringAssert.Contains(expected, _stringWriter.ToString());
        }
       
    }
}
