using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;

namespace Microwave.Test.Integration
{
    class PowerTubeAndOutput
    {
        private Output output;
        private PowerTube power;
        private System.IO.StringWriter stringWriter;
        [SetUp]
        public void Setup()
        {
            output = new Output();
            power = new PowerTube(output);
            stringWriter = new System.IO.StringWriter();
            Console.SetOut(stringWriter);
        }

        [Test]
        public void TestForTurnOn()
        {
            power.TurnOn(50);
            StringAssert.Contains("PowerTube works with 50", stringWriter.ToString());
        }
        [Test]
        public void TestForTurnOnTurnOff()
        {
            power.TurnOn(50);
            power.TurnOff();
            StringAssert.Contains("PowerTube turned off", stringWriter.ToString());
        }
        [Test]
        public void TurnOff()
        {
            power.TurnOff();
            StringAssert.DoesNotContain("PowerTube turned off", stringWriter.ToString());
        }
    }
}
