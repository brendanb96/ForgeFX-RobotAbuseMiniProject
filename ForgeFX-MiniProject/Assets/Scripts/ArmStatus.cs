using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmStatus : MonoBehaviour
{
    /// <summary>
    /// OBJECT REFERENCES
    /// </summary>
    ///
    #region
    [Header("REFERENCES")]
    //Associated arm
    public ArmHandler arm;

    //Text mesh reference
    public Text tm;
    #endregion

    /// <summary>
    /// STATUS TEXTS
    /// </summary>
    ///
    #region
    [Header("TEXT")]
    //Attached text
    public string attached = "ATTACHED";

    //Detached text
    public string dettached = "DETTACHED";
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check current status
        CheckStatus();
    }

    //Check arm status based on referenced arm
    public void CheckStatus()
    {
        //Check current "attached" value
        if(arm.IsAttached)
        {
            tm.text = attached;
        } else
        {
            tm.text = dettached;
        }
    }
}
