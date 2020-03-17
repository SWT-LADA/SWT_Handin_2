using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.DoorEvent;
using NUnit.Framework;

namespace Ladeskab.Test.Unit
{
    [TestFixture]
    class DoorUnitTest
    {
        private Door door; 
        [SetUp]
        public void Setup()
        {
            door = new Door();
        }

        public void SetDoorState_true_returntrue() //Denne er ikke testet færdig - den giver succes lige meget hvad - kan ikke debugge? 
        {
            Assert.That(door.SetDoorState(true), Is.EqualTo(true));
        }
    }
}
