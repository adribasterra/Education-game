using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameCanvasManager : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject score;
    public GameObject scorePivot;
    public float speed;

    private float factor = 0;
    private float pointsFactor;
    private bool animate;

    private bool hideTutorial = false;

    // Start is called before the first frame update
    void Start()
    {
        tutorial.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (animate)
        {
            factor = Mathf.Lerp(factor, pointsFactor, speed * Time.deltaTime);
            factor = factor > 1 ? 1 : factor;
            scorePivot.transform.localScale = new Vector3(scorePivot.transform.localScale.x, factor, scorePivot.transform.localScale.z);
        }
    }

    public void HideTutorial()
    {
        hideTutorial = true;
        tutorial.SetActive(false);
    }

    public bool GetHideTutorial()
    {
        return hideTutorial;
    }

    public void ShowScoreRed()
    {
        float points = ScoreSystemManager.TotalRedScore;
        float max = ScoreSystemManager.MaxPuntuation();
        pointsFactor = Mathf.Clamp01(points/max);
        Debug.Log($"pointsFactor: {pointsFactor}, points: {points}, max: {max}, totalredscore: {ScoreSystemManager.TotalRedScore}, maxpunct: {ScoreSystemManager.MaxPuntuation()}");
        score.SetActive(true);
        //Animar hasta arriba la barra
        animate = true;
    }

    public void ShowScoreWhite()
    {
        float points = ScoreSystemManager.TotalWhiteScore;
        float max = ScoreSystemManager.MaxPuntuation();
        pointsFactor = Mathf.Clamp01(points / max);
        Debug.Log($"pointsFactor: {pointsFactor}, points: {points}, max: {max}, totalredscore: {ScoreSystemManager.TotalWhiteScore}, maxpunct: {ScoreSystemManager.MaxPuntuation()}");
        score.SetActive(true);
        //Animar hasta arriba la barra
        animate = true;
    }
}
