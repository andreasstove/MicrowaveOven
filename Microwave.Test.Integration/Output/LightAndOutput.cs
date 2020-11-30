using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;

namespace Microwave.Test.Integration
{
    //Done
    class OutputLight
    {
        private Output output;
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
            string expected = "Light is turned on";
            light.TurnOn();
            StringAssert.Contains(expected, stringWriter.ToString());
        }
        [Test]
        public void TestForTurnOnAndOff()
        {
            string expected = "Light is turned off";
            light.TurnOn();
            light.TurnOff();
            StringAssert.Contains(expected, stringWriter.ToString());
        }
        [Test]
        public void TurnOnTwiceForOneInput()
        {
            string expected = "Light is turned off";
            light.TurnOn();
            System.IO.StringWriter stringWriter1 = new System.IO.StringWriter();
            Console.SetOut(stringWriter1);
            light.TurnOn();
            StringAssert.DoesNotContain(expected, stringWriter.ToString());
        }
        [Test]
        public void TurnOffFromTheStart()
        {
            string expected = "Light is turned off";
            light.TurnOff();
            StringAssert.DoesNotContain(expected, stringWriter.ToString());
        }
    }
}
