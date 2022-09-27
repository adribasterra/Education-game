using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallTextUpdater : MonoBehaviour
{
    #region Attrubutes
    private int currentBalls = 0;
    #endregion

    #region Main Methods

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = currentBalls.ToString();
    }
    #endregion

    #region Custom Methods
    public void lostBall()
    {
        currentBalls--;
    }

    public void recoveredBall()
    {
        currentBalls++;
    }
    #endregion
}
