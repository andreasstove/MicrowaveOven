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
        private IOutput output;
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
        //test
        [Test]
        public void Test1()
        {
            power.TurnOn(1);
            StringAssert.Contains("PowerTube works with 1", stringWriter.ToString());
        }
        [Test]
        public void Test2()
        {
            //Hejhej
            Assert.Pass();
        }
        [Test]
        public void Test3()
        {
            Assert.Pass();
        }
        [Test]
        public void Test4()
        {
            var hej = 4;
            Assert.That(hej, Is.EqualTo(4));
        }
    }
}
