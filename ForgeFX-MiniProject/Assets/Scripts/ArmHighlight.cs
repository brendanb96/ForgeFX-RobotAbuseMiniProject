using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class adds highlighting functionality to an arm.
/// </summary>
public class ArmHighlight : MonoBehaviour
{
    private Renderer[] armRends;

    private Color baseClr;

    [SerializeField] private Color highlightClr = Color.blue;
    public Color CurrentColor
    {
        get
        {
            if (armRends == null)
            {
                return baseClr;
            }

            if (armRends.Length < 1)
            {
                return baseClr;
            }

            if (armRends[0] == null)
            {
                return baseClr;
            }

            // Get current renderer color from first renderer.
            return armRends[0].material.color;
        }

        set
        {
            if (armRends != null)
            {
                foreach (Renderer rend in armRends)
                {
                    rend.material.color = value;
                }
            }
        }
    }

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        armRends = transform.GetComponentsInChildren<Renderer>();

        if(armRends[0])
        {
            baseClr = armRends[0].material.color;
        }
        
    }

    /// <summary>
    /// Change renderer material color based off whether or not to highlight arm.
    /// </summary>
    /// <param name="isOn">Should this arm be highlighted?</param>
    public void Highlight(bool isOn)
    {
        if (isOn)
        {
            CurrentColor = highlightClr;
        }
        else
        {
            CurrentColor = baseClr;
        }
    }
}
