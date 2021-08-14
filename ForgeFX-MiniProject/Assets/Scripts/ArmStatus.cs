using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class updates the UI based of the status of an ArmHandler.
/// </summary>
public class ArmStatus : MonoBehaviour
{
    public ArmHandler arm;
    [SerializeField] private Text tm;

    [Header("Attachment Text")]
    //Attached text
    public string attached = "ATTACHED";

    [Header("Detachment Text")]
    //Detached text
    public string dettached = "DETACHED";

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
        if(arm)
        {
            if (shouldSubscribe)
            {
                arm.onAttached += SendArmAttachedMessage;
                arm.onDetached += SendArmDetachedMessage;
            }
            else
            {
                arm.onAttached -= SendArmAttachedMessage;
                arm.onDetached -= SendArmDetachedMessage;
            }
        }
    }

    /// <summary>
    /// Change text (if necessary) to text stating it IS attached.
    /// </summary>
    private void SendArmAttachedMessage()
    {
        if(tm.text != attached)
        {
            tm.text = attached;
        }
    }

    /// <summary>
    /// Change text (if necessary) to text stating it IS NOT attached.
    /// </summary>
    private void SendArmDetachedMessage()
    {
        if (tm.text != dettached)
        {
            tm.text = dettached;
        }
    }
}
