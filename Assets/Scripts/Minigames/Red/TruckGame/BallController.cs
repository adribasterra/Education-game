using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public BallTextUpdater ballsText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Truck")
        {
            ballsText.recoveredBall();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Truck")
        {
            ballsText.lostBall();
        }
    }
}
