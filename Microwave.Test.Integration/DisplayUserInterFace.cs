using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using Microwave.Classes.Controllers;

namespace Microwave.Test.Integration
{
    class DisplayUserInterFace
    {
        private Output output;
        private Display display;
        private PowerTube powerTube;
        private UserInterface userInterface;

        //stubs
        private IDoor door;
        private ILight light;
        private ICookController cookController;
        private IButton powerButton;
        private IButton timeButton;
        private IButton cancelButton;
        private System.IO.StringWriter stringWriter;


        [SetUp]
        public void Setup()
        {
            output = new Output();
            display = new Display(output);
            powerTube = new PowerTube(output);
            
            cookController = Substitute.For<ICookController>();
            timeButton = Substitute.For<IButton>();
            cancelButton = Substitute.For<IButton>();
            powerButton = Substitute.For<IButton>();
            door = Substitute.For<IDoor>();
            light = Substitute.For<ILight>();

            stringWriter = new System.IO.StringWriter();
            Console.SetOut(stringWriter);

            userInterface = new UserInterface(powerButton, timeButton, cancelButton, door, display, light, cookController);
        }
        [Test]
        public void TestForDoorOpenReadyAndNothingOnTheDisplay()
        {
            string expectedNot = "50";
            door.Opened += Raise.Event();
            StringAssert.DoesNotContain(expectedNot, stringWriter.ToString());
        }
        [Test]
        public void TestForDoorOpenSetPowerPowerClearDisplay()
        {
            string expected = "Display cleared";
            powerButton.Pressed += Raise.Event();
            System.IO.StringWriter stringWriter1 = new System.IO.StringWriter();
            Console.SetOut(stringWriter1);
            door.Opened += Raise.Event();
            StringAssert.Contains(expected, stringWriter1.ToString());
        }
        [Test]
        public void TestForDoorOpenSetTimeClearDisplay()
        {
            string expected = "Display cleared";
            powerButton.Pressed += Raise.Event();
            timeButton.Pressed += Raise.Event();
            System.IO.StringWriter stringWriter1 = new System.IO.StringWriter();
            Console.SetOut(stringWriter1);
            door.Opened += Raise.Event();
            StringAssert.Contains(expected, stringWriter1.ToString());
        }
        [Test]
        public void TestForDoorOpenCookingClearDisplay()
        {
            string expected = "Display cleared";
            powerButton.Pressed += Raise.Event();
            timeButton.Pressed += Raise.Event();
            cancelButton.Pressed += Raise.Event();
            System.IO.StringWriter stringWriter1 = new System.IO.StringWriter();
            Console.SetOut(stringWriter1);
            door.Opened += Raise.Event();
            StringAssert.Contains(expected, stringWriter1.ToString());
        }
        [Test]
        public void TestForPowerButtunPressedOnTheDisplay()
        {
            string expected = "50";
            powerButton.Pressed += Raise.Event();
            StringAssert.Contains(expected, stringWriter.ToString());
        }
        [Test]
        public void TestForPowerButtun3PressedOnTheDisplay()
        {
            string expected = "150";
            for (int i = 0; i < 3; i++)
            {
                powerButton.Pressed += Raise.Event();
            }
            StringAssert.Contains(expected, stringWriter.ToString());
        }
        [Test]
        public void TestForPowerButtun14PressedOnTheDisplay()
        {
            string expected = "700";
            for (int i = 0; i < 14; i++)
            {
                powerButton.Pressed += Raise.Event();
            }
            StringAssert.Contains(expected, stringWriter.ToString());
        }
        [Test]
        public void TestForPowerButtun15PressedOnTheDisplay()
        {
            string expected = "50";
            for (int i = 0; i < 15; i++)
            {
                powerButton.Pressed += Raise.Event();
            }
            StringAssert.Contains(expected, stringWriter.ToString());
        }
        [Test]
        public void TestForTimeButtunPressedOnTheDisplay()
        {
            string expected = "1";
            powerButton.Pressed += Raise.Event();
            timeButton.Pressed += Raise.Event();
            StringAssert.Contains(expected, stringWriter.ToString());
        }

        [Test]
        public void TestForTimeButtun5PressedOnTheDisplay()
        {
            string expected = "5";
            powerButton.Pressed += Raise.Event();
            for (int i = 0; i < 5; i++)
            {
                timeButton.Pressed += Raise.Event();
            }
            StringAssert.Contains(expected, stringWriter.ToString());
        }

        [Test]
        public void TestForTimeButtun60PressedOnTheDisplay()
        {
            string expected = "60";
            powerButton.Pressed += Raise.Event();
            for (int i = 0; i < 60; i++)
            {
                timeButton.Pressed += Raise.Event();
            }
            StringAssert.Contains(expected, stringWriter.ToString());
        }
        //Den her test er ikke rigtig
        [Test]
        public void TestForTimeButtun61PressedOnTheDisplay()
        {
            string expected = "61";
            powerButton.Pressed += Raise.Event();
            System.IO.StringWriter stringWriter1 = new System.IO.StringWriter();
            Console.SetOut(stringWriter1);
            for (int i = 0; i < 61; i++)
            {
                timeButton.Pressed += Raise.Event();
            }
            StringAssert.DoesNotContain(expected, stringWriter1.ToString());
        }

        [Test]
        public void TestForStartButtonPressedAfterPower()
        {
            string expected = "Display cleared";
            powerButton.Pressed += Raise.Event();
            cancelButton.Pressed += Raise.Event();
            StringAssert.Contains(expected, stringWriter.ToString());
        }

        [Test]
        public void TestForStartButtonPressedBefore()
        {
            string expected = "Display cleared";
            powerButton.Pressed += Raise.Event();
            cancelButton.Pressed += Raise.Event();
            StringAssert.Contains(expected, stringWriter.ToString());
        }


        [Test]
        public void TestForStartButtonPressedAfterCooking()
        {
            string expected = "Display cleared";
            powerButton.Pressed += Raise.Event();
            timeButton.Pressed += Raise.Event();
            cancelButton.Pressed += Raise.Event();
            cancelButton.Pressed += Raise.Event();
            StringAssert.Contains(expected, stringWriter.ToString());
        }

        [Test]
        public void TestForTimerExpired()
        {
            string expected = "Display cleared";
            powerButton.Pressed += Raise.Event();
            timeButton.Pressed += Raise.Event();
            cancelButton.Pressed += Raise.Event();
            userInterface.CookingIsDone();
            StringAssert.Contains(expected, stringWriter.ToString());
        }
    }
}
