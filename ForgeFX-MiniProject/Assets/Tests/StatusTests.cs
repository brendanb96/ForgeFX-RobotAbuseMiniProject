using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    /// <summary>
    /// Test for ArmStatus class.
    /// </summary>
    public class StatusTests
    {
        /// <summary>
        /// Test for whether or not the ArmStatus script references an ArmHandler in the scene.
        /// </summary>
        [Test]
        public void GrabberStatusTextHasReference()
        {
            // Get arm object in scene
            var grabberStatus = GameObject.FindObjectOfType<GrabberStatus>();

            if (grabberStatus == null)
            {
                //Fail because there is a null reference of the status itself
                Assert.Fail();
            }
            else
            {
                //Check if there is an arm referenced in status object
                Assert.AreNotEqual(expected: null, actual: grabberStatus.ActiveGrabber);
            }
        }
    }
}
