using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class links this object to another.
/// </summary>
public class LinkObject : MonoBehaviour
{
    // Transform of object to replace and link.
    [SerializeField] private Transform objectToLink;

    /// <summary>
    /// On object awake.
    /// </summary>
    private void Awake()
    {
        if(objectToLink)
        {
            ApplyTransformFrom(objectToLink);
        }
    }

    /// <summary>
    /// Apply transform values based off a specified transform. Link specified transform as new child object.
    /// </summary>
    /// <param name="obj">Transform to link.</param>
    private void ApplyTransformFrom(Transform obj)
    {
        Transform parent = obj.parent;

        Vector3 localPos = obj.localPosition;
        Quaternion localRot = obj.localRotation;

        // Assign new parent to obj and reset local transform values.
        obj.SetParent(transform);
        obj.localPosition = Vector3.zero;
        obj.localRotation = Quaternion.identity;

        // Change parent of this object to old obj parent.
        transform.SetParent(parent);
        transform.localPosition = localPos;
        transform.localRotation = localRot;
    }
}
