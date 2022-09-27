using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenController : MonoBehaviour
{
    public float speed = 100.0f;
    public GameObject[] oxygen;
    public float force = 5.0f;
    public int numOxygenFallenWhenColliding = 2;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < oxygen.Length; i++)
        {
            oxygen[i].GetComponent<Rigidbody>().AddForce((this.transform.position - oxygen[i].transform.position).normalized * speed, ForceMode.Acceleration);
        }
    }

    public void OnCollision()
    {
        GameObject[] fallingOxigen = new GameObject[numOxygenFallenWhenColliding];
        if(oxygen.Length - numOxygenFallenWhenColliding > 0)
        {
            GameObject[] aux = new GameObject[oxygen.Length - numOxygenFallenWhenColliding];
            for (int i = 0; i < oxygen.Length; i++)
            {
                if (i < oxygen.Length - numOxygenFallenWhenColliding)
                {
                    aux[i] = oxygen[i];
                }
                else
                {
                    fallingOxigen[i - (oxygen.Length - numOxygenFallenWhenColliding)] = oxygen[i];
                }
            }
            oxygen = aux;
            for (int i = 0; i < fallingOxigen.Length; i++)
            {
                foreach (GameObject oxy in oxygen)
                {
                    Physics.IgnoreCollision(oxy.GetComponent<SphereCollider>(), fallingOxigen[i].GetComponent<SphereCollider>());
                }
                fallingOxigen[i].GetComponent<Rigidbody>().drag = 0.0f;
                fallingOxigen[i].GetComponent<Rigidbody>().angularDrag = 0.0f;
                fallingOxigen[i].GetComponent<Rigidbody>().AddExplosionForce(force, this.transform.position, 2.0f);
            }
        }
    }
}
