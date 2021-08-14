using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class adds dragging functionality to an arm.
/// </summary>
public class ArmMove : MonoBehaviour
{
    private Vector3 dragOffset;
    [HideInInspector] public bool isDragging = false;

    // Starting local position of transform.
    private Vector3 basePos;
    public float ArmDistance
    {
        get
        {
            return Vector3.Distance(transform.localPosition, basePos);
        }
    }

    [Range(0F, 0.2F)]
    [SerializeField] private float snapThreshold = 0.1F;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        ResetBasePosition();
    }

    /// <summary>
    /// Set the base / starting position for this arm.
    /// </summary>
    public void ResetBasePosition()
    {
        basePos = transform.localPosition;
    }

    /// <summary>
    /// Start dragging arm from a specified position.
    /// </summary>
    /// <param name="origin">The drag origin position</param>
    public void StartDragHere(Vector3 origin)
    {
        dragOffset = transform.position - origin;

        isDragging = true;
    }

    /// <summary>
    /// Drag arm along a certain position.
    /// </summary>
    /// <param name="dragPosition">Position to drag arm to.</param>
    public void DragArm(Vector3 dragPosition)
    {
        //Clamp position
        transform.position = dragPosition + dragOffset;
    }

    /// <summary>
    /// Stop dragging arm and snap in place (if necessary).
    /// </summary>
    public void StopDrag()
    {
        if (ArmDistance <= snapThreshold)
        {
            SnapInPlace();
        }

        isDragging = false;
    }

    /// <summary>
    /// Snap arm back to body based off the snap threshold.
    /// </summary>
    public void SnapInPlace()
    {
        transform.localPosition = basePos;
    }
}
