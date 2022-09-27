using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenIgnoreCollidersMgr : MonoBehaviour
{
    private bool disabledCollisionOxygen = false;

    public void SetDisabledCollisionOxygen(bool newState)
    {
        disabledCollisionOxygen = newState;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collision.collider, this.GetComponent<SphereCollider>());
        }
        if (disabledCollisionOxygen)
        {
            if (collision.gameObject.tag == "Oxygen")
            {
                Physics.IgnoreCollision(collision.collider, this.GetComponent<SphereCollider>());
            }
        }
    }
}
