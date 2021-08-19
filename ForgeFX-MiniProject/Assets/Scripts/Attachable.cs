using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Grabbable))]
/// <summary>
/// Applies attachable/detachable functionality to a grabbable object.
/// </summary>
public class Attachable : MonoBehaviour
{
    public delegate void OnAttachmentChange();
    public OnAttachmentChange onChanged;

    private Grabbable grabbable;
    private AttachPoint attachPoint;

    public bool isAttached { get; private set; } = true;
    [Range(0F, 0.2F)]
    [SerializeField] private float detachThreshold = 0.1F;
    private float attachThreshold => detachThreshold * 0.5F;
    private Vector3 tension;

    // On object awake
    private void Awake()
    {
        grabbable = GetComponent<Grabbable>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetAttachPosition();
        Attach();
    }

    // Update is called once per frame
    void Update()
    {
        if(grabbable.IsGrabbed)
        {
            if(isAttached)
            {
                CheckDetachTension();
            } else if(WithinAttachRange())
            {
                Attach();
            }
        } else
        {
            tension = Vector3.zero;
        }
    }

    /// <summary>
    /// Whether or not the current position of attachable is within attachment range.
    /// </summary>
    /// <returns></returns>
    private bool WithinAttachRange()
    {
        return Vector3.Distance(transform.position, attachPoint.AttachmentPositionInWorldSpace()) < attachThreshold;
    }

    /// <summary>
    /// Checks the detachment tension to process whether or not it should detach.
    /// </summary>
    private void CheckDetachTension()
    {
        if (transform.position != attachPoint.AttachmentPositionInWorldSpace())
        {
            tension += transform.position - attachPoint.AttachmentPositionInWorldSpace();
        }

        if(tension.magnitude > detachThreshold)
        {
            Detach();
        } else
        {
            transform.position = attachPoint.AttachmentPositionInWorldSpace();
        }
    }

    /// <summary>
    /// Set the attachPoint for this attachable.
    /// </summary>
    public void ResetAttachPosition()
    {
        attachPoint = new AttachPoint(this);
    }

    /// <summary>
    /// Connect attachable back to its attachment point.
    /// </summary>
    public void Attach()
    {
        if(grabbable.IsGrabbed)
        {
            grabbable.CurrentGrabber.ReleaseGrabbable();
        }

        isAttached = true;
        SnapToAttachPoint();
        onChanged();
    }

    /// <summary>
    /// Disconnect attachable from its attachment point.
    /// </summary>
    public void Detach()
    {
        grabbable.transform.position += tension;
        tension = Vector3.zero;
        isAttached = false;
        transform.parent = null;
        onChanged();
    }

    /// <summary>
    /// Set attachable to its original attachment point.
    /// </summary>
    public void SnapToAttachPoint()
    {
        transform.SetParent(attachPoint.attachmentTransform);
        transform.localPosition = attachPoint.attachPos;
    }
}
