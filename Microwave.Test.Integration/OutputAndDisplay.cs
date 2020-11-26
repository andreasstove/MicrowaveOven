using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Microwave.Classes.Interfaces;
using Microwave.Classes.Boundary;
namespace Microwave.Test.Integration
{
    [TestFixture]
    class OutputAndDisplay
    {
        private Display _display;
        private IOutput _output;
        private System.IO.StringWriter _text;
        [SetUp]
        public void Setup()
        {
            _output = new Output();
            _display = new Display(_output);
            _text = new System.IO.StringWriter();
            Console.SetOut(_text);
        }
        //test
        [Test]
        public void ShowTime_Called_OutputLine()
        {
            int min = 10;
            int sec = 5;
            var expected = "Display shows: 10:05";
            _display.ShowTime(min, sec);
            StringAssert.Contains(expected, _text.ToString());
        }
        [Test]
        public void ShowPower_Called_OutputLine()
        {
            int power = 20;
            var expected = "Display shows: 20 W";
            _display.ShowPower(power);
            StringAssert.Contains(expected, _text.ToString());
        }
        [Test]
        public void Clear_Called_OutputLine()
        {
            var expected = "Display cleared";
            _display.Clear();
            StringAssert.Contains(expected, _text.ToString());
        }
    }
}
