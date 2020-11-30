
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
    class LightUserInterFace
    {
        private Output output;
        private Display display;
        private PowerTube powerTube;
        private UserInterface userInterface;
        private Light light;

        //Stubs
        private IDoor door;
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
            light = new Light(output);

            cookController = Substitute.For<ICookController>();
            timeButton = Substitute.For<IButton>();
            cancelButton = Substitute.For<IButton>();
            powerButton = Substitute.For<IButton>();
            door = Substitute.For<IDoor>();

            stringWriter = new System.IO.StringWriter();
            Console.SetOut(stringWriter);
            userInterface = new UserInterface(powerButton, timeButton, cancelButton, door, display, light, cookController);
        }
        [Test]
        public void TestForDontDoAnyThingForTurnOffInOnStartSetPower()
        {
            //Det her bør aldrig kunne ske da den ikke kan være tændt i den state.
            powerButton.Pressed += Raise.Event();
            cancelButton.Pressed += Raise.Event();
            StringAssert.DoesNotContain("Light is turned off", stringWriter.ToString());
        }

        [Test]
        public void TestForTurnLightOnSettimeStartPressed()
        {
            powerButton.Pressed += Raise.Event();
            timeButton.Pressed += Raise.Event();
            cancelButton.Pressed += Raise.Event();
            StringAssert.Contains("Light is turned on", stringWriter.ToString());
        }

        [Test]
        public void TestForTurnLightOffCookingStartPressed()
        {
            powerButton.Pressed += Raise.Event();
            timeButton.Pressed += Raise.Event();
            cancelButton.Pressed += Raise.Event();
            cancelButton.Pressed += Raise.Event();
            StringAssert.Contains("Light is turned off", stringWriter.ToString());
        }

        [Test]
        public void TestForTurnLightOnReadyDoorOpened()
        {
            door.Opened += Raise.Event();
            StringAssert.Contains("Light is turned on", stringWriter.ToString());
        }
        [Test]
        public void TestForTurnLightOnSetpowerDoorOpened()
        {
            powerButton.Pressed += Raise.Event();
            door.Opened += Raise.Event();
            StringAssert.Contains("Light is turned on", stringWriter.ToString());
        }

        [Test]
        public void TestForTurnLightOnSettimeDoorOpened()
        {
            powerButton.Pressed += Raise.Event();
            timeButton.Pressed += Raise.Event();
            door.Opened += Raise.Event();
            StringAssert.Contains("Light is turned on", stringWriter.ToString());
        }

        [Test]
        public void TestForTurnLightOffDoorClosed()
        {

            door.Opened += Raise.Event();
            door.Closed += Raise.Event();
            StringAssert.Contains("Light is turned off", stringWriter.ToString());
        }

        [Test]
        public void TestForTurnLightOffDoorClosedButIsClosed()
        {


            door.Closed += Raise.Event();
            StringAssert.DoesNotContain("Light is turned off", stringWriter.ToString());
        }

        [Test]
        public void TestForTurnLightOffCookingIsDone()
        {
            powerButton.Pressed += Raise.Event();
            timeButton.Pressed += Raise.Event();
            cancelButton.Pressed += Raise.Event();

            userInterface.CookingIsDone();
            StringAssert.Contains("Light is turned off", stringWriter.ToString());
        }
    }
}

