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
        [SetUp]
        public void Setup()
        {
            output = new Output();
            power = new PowerTube(output);
        }
        //test
        [Test]
        public void Test1()
        {
            //var text = new System.IO.TextWriter("Text");
            //string text = "Text";
            var hej = "PowerTube works with 1";
            
            int number = 1;
            power.TurnOn(1);
            output.Received(1).OutputLine(hej);
            //Console.SetOut(System.IO.TextWriter(text));
            //output.Received(1).OutputLine("Display shows:{0}", number);
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
