using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Designates transform values for a attachable and detachable objects.
/// </summary>
public class AttachPoint
{
    public AttachPoint(Attachable attachable)
    {
        attachPos = attachable.transform.localPosition;
        attachmentTransform = attachable.transform.parent;
    }

    public Transform attachmentTransform { get; private set; } = default;
    public Vector3 attachPos { get; private set; } = default;

    /// <summary>
    /// Get attachment position in world space.
    /// </summary>
    /// <returns></returns>
    public Vector3 AttachmentPositionInWorldSpace()
    {
        if(attachmentTransform)
        {
            return attachmentTransform.TransformPoint(attachPos);
        } else
        {
            return attachPos;
        }
    }
}
