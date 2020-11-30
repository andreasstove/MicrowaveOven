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
        //test
        [Test]
        public void TestForDoorOpenReadyAndNothingOnTheDisplay()
        {
            door.Opened += Raise.Event();
            StringAssert.DoesNotContain("50", stringWriter.ToString());
        }
        [Test]
        public void TestForDoorOpenSetPowerPowerClearDisplay()
        {
            powerButton.Pressed += Raise.Event();
            System.IO.StringWriter stringWriter1 = new System.IO.StringWriter();
            Console.SetOut(stringWriter1);
            door.Opened += Raise.Event();
            //brug cleared når det er clear display
            StringAssert.Contains("cleared", stringWriter1.ToString());
        }
        [Test]
        public void TestForDoorOpenSetTimeClearDisplay()
        {
            powerButton.Pressed += Raise.Event();
            timeButton.Pressed += Raise.Event();
            System.IO.StringWriter stringWriter1 = new System.IO.StringWriter();
            Console.SetOut(stringWriter1);
            door.Opened += Raise.Event();
            //brug cleared når det er clear display
            StringAssert.Contains("cleared", stringWriter1.ToString());
        }
        [Test]
        public void TestForDoorOpenCookingClearDisplay()
        {
            powerButton.Pressed += Raise.Event();
            timeButton.Pressed += Raise.Event();
            cancelButton.Pressed += Raise.Event();
            System.IO.StringWriter stringWriter1 = new System.IO.StringWriter();
            Console.SetOut(stringWriter1);
            door.Opened += Raise.Event();
            //brug cleared når det er clear display
            StringAssert.Contains("cleared", stringWriter1.ToString());
        }
        [Test]
        public void TestForDoorOpenReady()
        {
            powerButton.Pressed += Raise.Event();
            timeButton.Pressed += Raise.Event();
            cancelButton.Pressed += Raise.Event();
            System.IO.StringWriter stringWriter1 = new System.IO.StringWriter();
            Console.SetOut(stringWriter1);
            door.Opened += Raise.Event();
            //brug cleared når det er clear display
            StringAssert.Contains("cleared", stringWriter1.ToString());
        }



        [Test]
        public void TestForPowerButtunPressedOnTheDisplay()
        {
            powerButton.Pressed += Raise.Event();
            StringAssert.Contains("50", stringWriter.ToString());
        }
        [Test]
        public void TestForPowerButtun3PressedOnTheDisplay()
        {
            for (int i = 0; i < 3; i++)
            {
                powerButton.Pressed += Raise.Event();
            }
            StringAssert.Contains("150", stringWriter.ToString());
        }
        [Test]
        public void TestForPowerButtun14PressedOnTheDisplay()
        {
            for (int i = 0; i < 14; i++)
            {
                powerButton.Pressed += Raise.Event();
            }
     
            StringAssert.Contains("700", stringWriter.ToString());
        }
        [Test]
        public void TestForPowerButtun15PressedOnTheDisplay()
        {
            for (int i = 0; i < 15; i++)
            {
                powerButton.Pressed += Raise.Event();
            }

            StringAssert.Contains("50", stringWriter.ToString());
        }


        [Test]
        public void TestForTimeButtunPressedOnTheDisplay()
        {
            powerButton.Pressed += Raise.Event();
            timeButton.Pressed += Raise.Event();


            StringAssert.Contains("1", stringWriter.ToString());
        }

        [Test]
        public void TestForTimeButtun5PressedOnTheDisplay()
        {
            powerButton.Pressed += Raise.Event();
            for (int i = 0; i < 5; i++)
            {
                timeButton.Pressed += Raise.Event();
            }

            StringAssert.Contains("5", stringWriter.ToString());
        }

        [Test]
        public void TestForTimeButtun60PressedOnTheDisplay()
        {
            powerButton.Pressed += Raise.Event();
            for (int i = 0; i < 60; i++)
            {
                timeButton.Pressed += Raise.Event();
            }

            StringAssert.Contains("60", stringWriter.ToString());
        }

        [Test]
        public void TestForTimeButtun61PressedOnTheDisplay()
        {
            powerButton.Pressed += Raise.Event();
            for (int i = 0; i < 61; i++)
            {
                timeButton.Pressed += Raise.Event();
            }
            //Her viser den 61 istedet for at gå til 1 minut
            StringAssert.DoesNotContain("61", stringWriter.ToString());
        }

        [Test]
        public void TestForStartButtonPressedAfterPower()
        {
            powerButton.Pressed += Raise.Event();

            cancelButton.Pressed += Raise.Event();
            //brug cleared når det er clear display
            StringAssert.Contains("cleared", stringWriter.ToString());
        }

        [Test]
        public void TestForStartButtonPressedBefore()
        {
            powerButton.Pressed += Raise.Event();

            cancelButton.Pressed += Raise.Event();
            //brug cleared når det er clear display
            StringAssert.Contains("cleared", stringWriter.ToString());
        }


        [Test]
        public void TestForStartButtonPressedAfterCooking()
        {
            powerButton.Pressed += Raise.Event();
            timeButton.Pressed += Raise.Event();
            cancelButton.Pressed += Raise.Event();
            cancelButton.Pressed += Raise.Event();
            //brug cleared når det er clear display
            StringAssert.Contains("cleared", stringWriter.ToString());
        }

        [Test]
        public void TestForTimerExpired()
        {
            powerButton.Pressed += Raise.Event();
            timeButton.Pressed += Raise.Event();
            cancelButton.Pressed += Raise.Event();
            userInterface.CookingIsDone();
            //brug cleared når det er clear display
            StringAssert.Contains("cleared", stringWriter.ToString());
        }

    }
}
