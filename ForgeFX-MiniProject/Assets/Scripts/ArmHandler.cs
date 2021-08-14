using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class takes input and applies them to an arm.
/// </summary>
public class ArmHandler : MonoBehaviour
{
    public delegate void OnAttachmentChange();
    public OnAttachmentChange onAttached;
    public OnAttachmentChange onDetached;

    [SerializeField] private ArmHighlight highlight;
    [SerializeField] private ArmMove movement;

    // Get adjusted input mouse position for world position.
    private Vector3 CurrentMousePosition
    {
        get
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;

            return Camera.main.ScreenToWorldPoint(mousePos);
        }
    }

    [HideInInspector] public bool isAttached = true;

    /// <summary>
    /// On mouse over arm.
    /// </summary>
    private void OnMouseEnter()
    {
        //Highlight arm
        highlight.Highlight(true);
    }

    /// <summary>
    /// On mouse click arm.
    /// </summary>
    private void OnMouseDown()
    {
        //Drag start position
        movement.StartDragHere(CurrentMousePosition);
    }

    /// <summary>
    /// On mouse drag / pull arm.
    /// </summary>
    private void OnMouseDrag()
    {
        //Drag arm
        movement.DragArm(CurrentMousePosition);

        CheckAttachment();
    }

    /// <summary>
    /// On mouse release arm.
    /// </summary>
    private void OnMouseUp()
    {
        movement.StopDrag();

        CheckAttachment();
    }

    /// <summary>
    /// On mouse exit arm.
    /// </summary>
    private void OnMouseExit()
    {
        //Unhighlight arm based on whether or not is is being dragged (no dragging -> don't highlight; is dragging -> highlighting persists)
        highlight.Highlight(movement.isDragging);
    }

    /// <summary>
    /// Check whether or not the arm is still attached. Invoke corresponding delegates.
    /// </summary>
    public void CheckAttachment()
    {
        bool armCloseToBody = Mathf.Approximately(movement.ArmDistance, 0F);

        if (isAttached != armCloseToBody)
        {
            isAttached = armCloseToBody;

            if (isAttached)
            {
                onAttached();
            }
            else
            {
                onDetached();
            }
        }
    }
}
