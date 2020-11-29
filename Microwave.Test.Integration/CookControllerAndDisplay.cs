﻿using System;
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
        private IOutput _output;
        private IPowerTube _powerTube;
        private IDisplay _display;
        private ICookController _cookController;
      
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
            string expected = "Display shows: 00:00";
            int power = 20;
            int time = 11;

            _cookController.StartCooking(power, time);
           
            _stringWriter = new System.IO.StringWriter();
            Console.SetOut(_stringWriter);
            _timer.TimerTick += Raise.Event();
            StringAssert.Contains(expected, _stringWriter.ToString());
        }
       
    }
}