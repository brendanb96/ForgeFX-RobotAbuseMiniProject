using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmHandler : MonoBehaviour
{
    /// <summary>
    /// POSITIONING PROPERTIES
    /// </summary>
    ///
    #region
    //Default local position for arm
    private Vector3 basePos;

    //Drag start
    private Vector3 dragOffset;

    //Whether or not arm is being dragging
    private bool isDragging = false;

    //Arm distance from body
    public float ArmDistance
    {
        get
        {
            return Vector3.Distance(transform.localPosition, basePos);
        }
    }

    //Get adjusted input mouse position for world position
    private Vector3 CurrentMousePosition
    {
        get
        {
            //Current mouse position
            Vector3 mousePos = Input.mousePosition; //Get from input
            mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z; //Adjust z-position to reset depth positioning

            return Camera.main.ScreenToWorldPoint(mousePos);
        }
    }
    #endregion

    /// <summary>
    /// SNAPPING PROPERTIES
    /// </summary>
    ///
    #region
    //Tolerance/threshold for snapping
    [Range(0F, 0.2F)]
    public float snapThreshold = 0.1F;

    //Whether or not it is detached
    public bool IsAttached
    {
        get
        {
            return Mathf.Approximately(ArmDistance, 0F);
        }
    }

    #endregion

    /// <summary>
    /// RENDERING
    /// </summary>
    ///
    #region
    //Renderer reference
    private Renderer[] armRends;

    //Default color for arm
    private Color baseClr;

    //Highlight color value
    public Color highlightClr = Color.blue;

    //Current color value
    public Color CurrentColor
    {
        get
        {
            //Check null reference for renderer
            if(armRends == null)
            {
                return baseClr;
            }

            //Check arm rend
            if(armRends.Length < 1)
            {
                return baseClr;
            }

            //Check arm rend
            if (armRends[0] == null)
            {
                return baseClr;
            }

            //Get current renderer color
            return armRends[0].material.color;
        }

        set
        {
            //Check null reference for renderer
            if (armRends != null)
            {
                foreach(Renderer rend in armRends)
                {
                    rend.material.color = value;
                }
            }
        }
    }
    #endregion

    //On object awoken
    private void Awake()
    {
        //Initialize
        Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Initialize base/default values
    private void Init()
    {
        //Set default base position based on local position
        basePos = transform.localPosition;

        //Get renderer reference
        armRends = GetComponentsInChildren<Renderer>();

        //Set default color for arm
        baseClr = armRends[0].material.color;
    }

    /// <summary>
    /// HIGHLIGHTING
    /// </summary>
    ///
    #region
    //Whether or not to highlight arm
    public void Highlight(bool isOn)
    {
        //Check whether or not it should be highlighted, and apply the corresponding color
        if (isOn)
        {
            CurrentColor = highlightClr;
        }
        else
        {
            CurrentColor = baseClr;
        }
    }
    #endregion

    /// <summary>
    /// DRAGGING
    /// </summary>
    ///
    #region
    //Drag arm
    public void DragArm()
    {
        //Clamp position
        transform.position = CurrentMousePosition + dragOffset;
    }

    //Snap arm back in place
    public void SnapInPlace()
    {
        //Set local position to base position
        transform.localPosition = basePos;
    }
    #endregion

    /// <summary>
    /// BASIC EVENT HANDLING
    /// </summary>
    /// 
    #region
    //On mouse over arm
    private void OnMouseEnter()
    {
        //Highlight arm
        Highlight(true);
    }

    //On mouse click arm
    private void OnMouseDown()
    {
        //Drag start position
        Vector3 dragStart = CurrentMousePosition;

        //Get vector from drag start to current arm position
        dragOffset = transform.position - dragStart;

        //Set dragging propery
        isDragging = true;
    }

    //On mouse drag / pull arm
    private void OnMouseDrag()
    {
        //Drag arm
        DragArm();
    }

    //On mouse end drag / release arm
    private void OnMouseUp()
    {
        //Check distance of arm to determine if it should snap back in place
        if(ArmDistance <= snapThreshold)
        {
            SnapInPlace();
        }

        //Reset dragging propery
        isDragging = false;
    }

    //On mouse exit arm
    private void OnMouseExit()
    {
        //Unhighlight arm based on whether or not is is being dragged (no dragging -> don't highlight; is dragging -> highlighting persists)
        Highlight(isDragging);
    }
    #endregion
}
