﻿using System;
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
        private Output _output;
        private System.IO.StringWriter _stringWriter;
        [SetUp]
        public void Setup()
        {
            _output = new Output();
            _display = new Display(_output);
            _stringWriter = new System.IO.StringWriter();
            Console.SetOut(_stringWriter);
        }
        [Test]
        public void ShowTime_Called_OutputLine()
        {
            int min = 10;
            int sec = 5;
            var expected = "Display shows: 10:05";
            _display.ShowTime(min, sec);
            StringAssert.Contains(expected, _stringWriter.ToString());
        }
        [TestCase(50)]
        [TestCase(500)]
        [TestCase(700)]

        public void ShowPower_Called_OutputLine(int power)
        {
            var expected = $"Display shows: {power} W";
            _display.ShowPower(power);
            StringAssert.Contains(expected, _stringWriter.ToString());
        }
        [Test]
        public void Clear_Called_OutputLine()
        {
            var expected = "Display cleared";
            _display.Clear();
            StringAssert.Contains(expected, _stringWriter.ToString());
        }
    }
}
