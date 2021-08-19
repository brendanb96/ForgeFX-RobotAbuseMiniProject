using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class updates the UI based of the status of an Attachable.
/// </summary>
public class AttachableStatus : MonoBehaviour
{
    [SerializeField] Attachable attachable;
    [SerializeField] private Text statusText;

    [Header("Attachment Text")]
    private string attachedSuffix = "ATTACHED";

    [Header("Detachment Text")]
    private string detachedSuffix = "DETACHED";

    private bool isAttached;

    /// <summary>
    /// On object enabled.
    /// </summary>
    private void OnEnable()
    {
        Subscribe(true);
    }

    /// <summary>
    /// On object disabled.
    /// </summary>
    private void OnDisable()
    {
        Subscribe(false);
    }

    /// <summary>
    /// Subscribe to ArmHandler events.
    /// </summary>
    /// <param name="shouldSubscribe">Should this subscribe or not (unsubscribe)?</param>
    private void Subscribe(bool shouldSubscribe)
    {
        if (attachable)
        {
            if (shouldSubscribe)
            {
                attachable.onChanged += SetStatusTextFromAttachable;
            }
            else
            {
                attachable.onChanged -= SetStatusTextFromAttachable;
            }
        }
    }

    /// <summary>
    /// Get attachment text based off a specified attachment value.
    /// </summary>
    /// <param name="attachmentValue">Whether or not attachable is attached.</param>
    /// <returns></returns>
    private string GetAttachmentText(bool attachmentValue)
    {
        if(attachable)
        {
            if(attachmentValue)
            {
                return string.Format("{0} {1}", attachable.name, attachedSuffix);
            } else
            {
                return string.Format("{0} {1}", attachable.name, detachedSuffix);
            } 
        } else
        {
            return "";
        }
    }

    /// <summary>
    /// Change text (if necessary) to text based off whether or not Attachable is attached.
    /// </summary>
    private void SetStatusTextFromAttachable()
    {
        if (isAttached != attachable.isAttached)
        {
            isAttached = attachable.isAttached;

            if (statusText.text != GetAttachmentText(isAttached))
            {
                statusText.text = GetAttachmentText(isAttached);
            }
        }
    }
}
