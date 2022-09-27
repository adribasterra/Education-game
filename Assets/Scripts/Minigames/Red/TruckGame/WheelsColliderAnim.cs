using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsColliderAnim : MonoBehaviour
{
    #region Attributes
    public WheelCollider wheelC;

    private Vector3 wheelCCenter;
    private RaycastHit hit;
    #endregion

    #region Main Attributes
    private void Start()
    {
        if (wheelC)
        {
            //wheelC.motorTorque = 0.0000000000001f;        //makes the wheels rotate and not get stuck when the plane starts moving
        }
    }

    void FixedUpdate()
    {
        wheelCCenter = wheelC.transform.TransformPoint(wheelC.center);

        Debug.DrawRay(wheelCCenter, -wheelC.transform.right);

        if (Physics.Raycast(wheelCCenter, -wheelC.transform.right, out hit, wheelC.suspensionDistance + wheelC.radius))
        {
            transform.position = hit.point + (wheelC.transform.right * wheelC.radius);
        }
        else
        {
            transform.position = wheelCCenter - (wheelC.transform.right * wheelC.suspensionDistance);
        }
    }
    #endregion
}


/* Versión de internet con rotación de ruedas y más funcionalidad, no hace falta aqui
using UnityEngine;
using System.Collections;
 
public class WheelLagCompensation : MonoBehaviour
{
 
    public WheelCollider wheelC;
    private Vector3 wheelCCenter;
    private Quaternion wheelCForward;
    private RaycastHit hit;
    float rotation;
 
    void LateUpdate()
    {
        wheelCCenter = wheelC.transform.TransformPoint(wheelC.center);
        wheelCForward = wheelC.transform.rotation;
        float steerAngle = wheelC.steerAngle;
        rotation += Mathf.Rad2Deg*(2*Mathf.PI*wheelC.rpm/60*Time.deltaTime);
        Quaternion rot = new Quaternion();
        rot = Quaternion.Euler(rotation, steerAngle, 0);
 
        if (Physics.Raycast(wheelCCenter, -wheelC.transform.up, out hit, wheelC.suspensionDistance + wheelC.radius))
        {
            transform.position = hit.point + (wheelC.transform.up * wheelC.radius);
            transform.rotation = wheelCForward * rot;
        }
        else
        {
            transform.position = wheelCCenter - (wheelC.transform.up * wheelC.suspensionDistance);
            transform.rotation = wheelCForward * rot;
        }
    }
}
*/