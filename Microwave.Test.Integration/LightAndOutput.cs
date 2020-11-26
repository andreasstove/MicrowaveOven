using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;

namespace Microwave.Test.Integration
{
    class OutputLight
    {
        private IOutput output;
        private Light light;
        private System.IO.StringWriter stringWriter;
        [SetUp]
        public void Setup()
        {
            output = new Output();
            light = new Light(output);
            stringWriter = new System.IO.StringWriter();
            Console.SetOut(stringWriter);
        }
        //test
        [Test]
        public void TestForTurnOn()
        {
            light.TurnOn();
            //power.TurnOn(1);
           
            StringAssert.Contains("Light is turned on", stringWriter.ToString());
        }
        [Test]
        public void TestForTurnOnAndOff()
        {
            light.TurnOn();
            light.TurnOff();
            StringAssert.Contains("Light is turned off", stringWriter.ToString());
        }
        [Test]
        public void TurnOnTwiceForOneInput()
        {
            light.TurnOn();
            System.IO.StringWriter stringWriter1 = new System.IO.StringWriter();
            Console.SetOut(stringWriter1);
            light.TurnOn();
            StringAssert.DoesNotContain("Light is turned off", stringWriter.ToString());
        }
        [Test]
        public void TurnOffFromTheStart()
        {
            light.TurnOff();
            StringAssert.DoesNotContain("Light is turned off", stringWriter.ToString());
        }
    }
}
