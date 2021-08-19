using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class updates the UI based of the status of a Grabber.
/// </summary>
public class GrabberStatus : MonoBehaviour
{
    [SerializeField] private Text statusText;
    [SerializeField] private Grabber _grabber;
    public Grabber ActiveGrabber
    {
        get
        {
            return _grabber;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetStatusTextFromGrabber();
    }

    // Update is called once per frame
    void Update()
    {
        if (_grabber.CurrentSelection)
        {
            SetStatusTextFromGrabber();
        } else
        {
            if(statusText.text != "")
            {
                statusText.text = "";
            }
        }
    }

    /// <summary>
    /// Change text (if necessary) to text based off currently selected grabber item.
    /// </summary>
    private void SetStatusTextFromGrabber()
    {
        if (statusText.text == "")
        {
            statusText.text = _grabber.CurrentSelection.name;
        }
    }
}
