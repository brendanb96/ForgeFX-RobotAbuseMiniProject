using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class aallows an object to be grabbed.
/// </summary>
///
[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour
{
    private Vector3 dragOffset;
    public bool isDragging { get; private set; } = false;

    public bool IsGrabbed => CurrentGrabber != null;
    [HideInInspector] public Grabber CurrentGrabber;

    /// <summary>
    /// Start dragging arm from a specified position.
    /// </summary>
    /// <param name="origin">The drag origin position</param>
    public void Grab(Vector3 origin)
    {
        dragOffset = transform.position - origin;
        isDragging = true;
    }

    /// <summary>
    /// Drag arm along a certain position.
    /// </summary>
    /// <param name="dragPosition">Position to drag arm to.</param>
    public void Drag(Vector3 dragPosition)
    {
        //Clamp position
        transform.position = dragPosition + dragOffset;
    }

    /// <summary>
    /// Stop dragging arm and snap in place (if necessary).
    /// </summary>
    public void Release()
    {
        CurrentGrabber = null;
        isDragging = false;
    }
}
