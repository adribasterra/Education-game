using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBob : MonoBehaviour
{
    #region Attributes
    private float bobPosTimer = 0f;
    [Range(0f, 3f)]
    public float bobSpeed = 1f;
    [Range(0f, 0.05f)]
    public float xIntensity = 0.01f;
    [Range(0f, 0.05f)]
    public float yIntensity = 0.02f;
    [Range(0f, 0.05f)]
    public float zIntensity = 0.01f;

    private Vector3 targetAnimPos = new Vector3();
    private Vector3 initialPos = new Vector3();
    #endregion

    #region Propperties
    #endregion

    #region Main Methods
    // Start is called before the first frame update
    void Start()
    {
        initialPos = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        targetAnimPos = initialPos + new Vector3(Mathf.Cos(bobPosTimer) * xIntensity, Mathf.Sin(bobPosTimer * 2f) * yIntensity, Mathf.Cos(bobPosTimer) * zIntensity);
        this.transform.localPosition = targetAnimPos;
        
        bobPosTimer += Time.deltaTime * bobSpeed;
    }
    #endregion

    #region Custom Methods
    #endregion
}

/*
#region Attributes
#endregion

#region Propperties
#endregion

#region Main Methods
// Start is called before the first frame update
void Start()
{
    
}

// Update is called once per frame
void Update()
{
    
}
#endregion

#region Custom Methods
#endregion
*/