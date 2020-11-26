using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Microwave.Classes.Interfaces;
using Microwave.Classes.Boundary;
namespace Microwave.Test.Integration
{
    class OutputAndDisplay
    {
        IDisplay display;
        IOutput output;
        [SetUp]
        public void Setup()
        {

        }
        //test
        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}
