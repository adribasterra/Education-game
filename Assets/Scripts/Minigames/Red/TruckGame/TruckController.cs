using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    #region Attributtes
    [Header("Main Attributes")]
    public float wheelsTorque = 1000f;
    public float sidesAccel = 3f;
    public Joystick velJoystick;
    public Joystick movJoystick;
    public WheelCollider[] wheels;
    #endregion

    #region Main Methods
    void Start()
    {
        foreach (WheelCollider wheel in wheels)
        {
            wheel.motorTorque = 0.000000000001f;
        }
    }

    private void FixedUpdate()
    {
        foreach(WheelCollider wheel in wheels)
        {
            wheel.motorTorque = velJoystick.Horizontal * wheelsTorque;
            //Debug.Log("Aplying " + wheel.motorTorque + " to " + wheel.gameObject.name);
        }

        this.gameObject.GetComponent<Rigidbody>().AddTorque(transform.right * movJoystick.Horizontal * sidesAccel, ForceMode.Acceleration);

        
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor
            || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)
            || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {
                pcControls();
            }
            
        }
    }
    #endregion

    #region Custom Methods
    private void pcControls()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            foreach (WheelCollider wheel in wheels)
            {
                wheel.motorTorque = wheelsTorque;
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            foreach (WheelCollider wheel in wheels)
            {
                wheel.motorTorque = -wheelsTorque;
            }
        }
        /*else
        {
            foreach (WheelCollider wheel in wheels)
            {
                wheel.motorTorque = 0.000000000001f;
            }
        }*/
        if (Input.GetKey(KeyCode.D))
        {
            this.gameObject.GetComponent<Rigidbody>().AddTorque(transform.right * sidesAccel, ForceMode.Acceleration);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            this.gameObject.GetComponent<Rigidbody>().AddTorque(transform.right * -sidesAccel, ForceMode.Acceleration);
        }
    }
    #endregion
}
