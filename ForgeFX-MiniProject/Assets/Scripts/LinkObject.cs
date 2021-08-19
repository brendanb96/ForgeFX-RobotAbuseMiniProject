using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Injects an object into the transform heirarchy in place of another object. Makes replaced object child of this current one.
/// </summary>
public class LinkObject : MonoBehaviour
{
    // Transform of object to replace and link.
    [SerializeField] private Transform linkTarget;

    // On object awake
    private void Awake()
    {
        if(linkTarget)
        {
            InsertAsParent(linkTarget);
        }
    }

    /// <summary>
    /// Insert this object as a parent of another specified object.
    /// </summary>
    /// <param name="target">Transform to replace.</param>
    private void InsertAsParent(Transform target)
    {
        transform.SetParent(target.parent);
        transform.SetPositionAndRotation(target.position, target.rotation);
        target.SetParent(transform);
    }
}
