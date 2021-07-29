using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class StatusTests
    {
        // Test whether or not the ArmStatus script references an ArmHandler in the scene
        [Test]
        public void StatusTextDoesReferenceArm()
        {
            // Get arm object in scene
            var armStatus = GameObject.FindObjectOfType<ArmStatus>();

            if(armStatus == null)
            {
                //Fail because there is a null reference of the status itself
                Assert.Fail();
            } else
            {
                //Check if there is an arm referenced in status object
                Assert.AreNotEqual(expected: null, actual: armStatus.arm);
            }
        }
    }
}
