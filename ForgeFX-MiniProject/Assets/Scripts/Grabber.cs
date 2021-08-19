using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class takes input and applies them to an arm.
/// </summary>
public class Grabber : MonoBehaviour
{
    public LayerMask grabbableLayer;
    public float grabDistance = 10F;

    private Grabbable _currentSelection;
    public Grabbable CurrentSelection
    {
        get
        {
            return _currentSelection;
        }
    }
    private Grabbable currentItemGrabbed;

    [SerializeField] private Material highlightMaterial = default;
    private Highlight highlight = default;

    // On awake
    private void Awake()
    {
        highlight = new Highlight(highlightMaterial);
        currentItemGrabbed = null;
    }

    // On update
    private void Update()
    {
        if (currentItemGrabbed)
        {
            DragGrabbable();
            CheckReleaseInput();
        } else
        {
            CheckGrabInput();
        }
    }

    /// <summary>
    /// Check the input conditions for grabbing an object.
    /// </summary>
    private void CheckGrabInput()
    {
        if (FindGrabbableItem(out _currentSelection))
        {
            if (Input.GetMouseButtonDown(0))
            {
                TryToGrab(_currentSelection);
            } else
            {
                HighlightGrabbable(_currentSelection);
            }
        } else
        {
            ClearHighlights();
        }

    }

    /// <summary>
    /// Highlight a grabbable object.
    /// </summary>
    /// <param name="_grabbable">Grabbable object to highlight.</param>
    private void HighlightGrabbable(Grabbable _grabbable)
    {
        highlight.HighlightRenderers(_grabbable.gameObject.GetComponentsInChildren<Renderer>());
    }

    /// <summary>
    /// Clear any active highlights.
    /// </summary>
    private void ClearHighlights()
    {
        highlight.ClearHighlights();
    }

    /// <summary>
    /// Check for the input for releasing grabbable.
    /// </summary>
    private void CheckReleaseInput()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ReleaseGrabbable();
        }
    }

    /// <summary>
    /// Find grabbable objecct within the scope of this grabber.
    /// </summary>
    /// <param name="_potentialGrabbable">Grabbable that may be potentially found.</param>
    /// <returns></returns>
    public bool FindGrabbableItem(out Grabbable _potentialGrabbable)
    {
        RaycastHit hit;

        if (Physics.Raycast(GetRayFromMouse(), out hit, grabDistance, grabbableLayer) && hit.rigidbody)
        {
            return hit.rigidbody.TryGetComponent<Grabbable>(out _potentialGrabbable);
        } else
        {
            _potentialGrabbable = null;
            return false;
        }
    }

    // Grab item
    /// <summary>
    /// Attempt to grab a grabbable object.
    /// </summary>
    /// <param name="_grabbable">Grabbable object that is desired to grab.</param>
    public void TryToGrab(Grabbable _grabbable)
    {
        if (!currentItemGrabbed && !_grabbable.IsGrabbed)
        {
            currentItemGrabbed = _grabbable;
            currentItemGrabbed.Grab(GetWorldPositionFromMouse());
            currentItemGrabbed.CurrentGrabber = this;
            ClearHighlights();
        }
    }

    /// <summary>
    /// Drag any currently grabbed object.
    /// </summary>
    public void DragGrabbable()
    {
        if(currentItemGrabbed)
        {
            currentItemGrabbed.Drag(GetWorldPositionFromMouse());
        }
    }

    /// <summary>
    /// Release any currently grabbed object.
    /// </summary>
    public void ReleaseGrabbable()
    {
        if(currentItemGrabbed)
        {
            currentItemGrabbed.Release();
            currentItemGrabbed = null;
        }

        _currentSelection = null;
    }

    /// <summary>
    /// Get Ray from mouse input position
    /// </summary>
    /// <returns></returns>
    public Ray GetRayFromMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;

        return Camera.main.ScreenPointToRay(mousePos);
    }

    /// <summary>
    /// Get world space position from mouse input position
    /// </summary>
    /// <returns></returns>
    public Vector3 GetWorldPositionFromMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;

        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
