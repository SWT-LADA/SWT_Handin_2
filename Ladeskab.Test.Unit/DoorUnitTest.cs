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
        private Door _uut; 
        [SetUp]
        public void Setup()
        {
            _uut = new Door();
        }

        [TestCase(false, false, false)]
        [TestCase(false, true, true)]
        [TestCase(true, false, false)]
        [TestCase(true, true, true)]
        public void Unit_test_SetDoorState_DifferentDoorStates_DoorStateCorrect(bool originalState, bool newState, bool expectedState) 
        {
            _uut.SetDoorState(originalState);
            Assert.That(_uut.SetDoorState(newState), Is.EqualTo(expectedState));
        }
    }
}
