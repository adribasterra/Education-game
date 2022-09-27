using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    #region Public

    public float maxTime;
    public Text timeValue;
    public GameObject transitor;

    #endregion

    #region Private

    private string timeOutScene = "FinalGameScene";
    private bool isCounting;
    private bool isFirstTime;
    private float timeCount = 0;

    #endregion

    #region Main Methods

    void Start()
    {
        isCounting = true;
        //isFirstTime = true;
    }

    void Update()
    {
        if (false /*insert condition*/ && isFirstTime)
        {
            isCounting = true;
            isFirstTime = false;
        }

        if (isCounting)
        {
            timeCount += Time.deltaTime;
        }

        MinAndSecTimer();

        if (timeCount > maxTime)
        {
            if (transitor != null) transitor.GetComponent<Transitions>().LoadNextScene(timeOutScene);
        }
    }

    #endregion

    #region Custom Methods

    public bool GetIsFirstTime()
    {
        return isFirstTime;
    }

    public void SetTimeInScoreManager()
    {
        int playingCell = SceneElementsController.PlayingCell;

        if (playingCell == (int)SceneElementsController.Cells.red)
        {
            ScoreSystemManager.TotalRedTime = timeCount;
        }
        else if(playingCell == (int)SceneElementsController.Cells.white)
        {
            ScoreSystemManager.TotalWhiteTime = timeCount;
        }
        else if(playingCell == (int)SceneElementsController.Cells.platalet)
        {
            ScoreSystemManager.TotalPlataletTime = timeCount;
        }
    }

    public void SetCounting(bool isCounting)
    {
        this.isCounting = isCounting;
    }

    public float GetTotalTime()
    {
        return timeCount;
    }

    void MinAndSecTimer()
    {
        float minutes = Mathf.Floor(timeCount / 60);
        float seconds = timeCount % 60;

        string secondsString;
      
        if (seconds < 10) secondsString = "0" + Mathf.FloorToInt(seconds).ToString();
        else secondsString = "" + Mathf.FloorToInt(seconds).ToString();

        if (SceneManager.GetActiveScene().name.Contains("Minigame")) ;
        else timeValue.text = minutes + ":" + secondsString;
    }

    #endregion
}
