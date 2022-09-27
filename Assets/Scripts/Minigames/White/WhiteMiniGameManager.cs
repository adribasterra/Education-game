using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteMiniGameManager : MonoBehaviour
{
    #region Public

    [Header("Enemies")]
    public float timeBtwEnemies;
    public GameObject enemyPrefab;
    
    [Header("UI")]
    public Text ammoText;
    public WhitePlayerController player;

    [Header("Transitor")]
    public string onFinishScene = "MainMenu";
    public Transitions transitor;

    #endregion

    #region Private

    private float lastTimeEnemyInstantiated = 0;
    private GameObject enemy;
    
    //Horizontal
    private readonly float minHorizontalRangeAbs = 8;
    private readonly float maxHorizontalRangeAbs = 12;

    //Vertical
    private readonly float minVerticalRangeAbs = 4;
    private readonly float maxVerticalRangeAbs = 8;

    private MinigameCanvasManager canvasManager;
    private bool once = true;

    #endregion

    #region Main Methods

    void Start()
    {
        canvasManager = this.GetComponent<MinigameCanvasManager>();
    }

    void Update()
    {
        if (!canvasManager.GetHideTutorial()) return;
        if (player.GameOver())
        {
            if (once)
            {
                ScoreSystemManager.MiniGameFinished = true;
                ScoreSystemManager.CalculateFinalWhiteScore();
                //Show score canvas
                canvasManager.ShowScoreWhite();
                once = false;
            }
            return;
        }
        //Update ammoText
        //ammoText.text = "Ammo: " + player.GetCurrentAmmo();

        //Instantiate enemies
        lastTimeEnemyInstantiated += Time.deltaTime;
        if (lastTimeEnemyInstantiated > timeBtwEnemies)
        {
            //Instantiate enemy
            float xRandomPos = Random.Range(-maxHorizontalRangeAbs, maxHorizontalRangeAbs);
            float yRandomPos;
            if (Mathf.Abs(xRandomPos) < minHorizontalRangeAbs)  //If x position is inside the frame, y must be at top or buttom
            {
                yRandomPos = Random.Range(minVerticalRangeAbs, maxVerticalRangeAbs);
                if (Random.Range(0, 2) > 1) yRandomPos = -yRandomPos;   //Also take negative values to appear at the buttom part
            }
            else    //If it is outside the frame, can take any value for y axis
            {
                yRandomPos = Random.Range(-maxVerticalRangeAbs, maxVerticalRangeAbs);
            }

            Vector3 randomPos = new Vector3(xRandomPos, yRandomPos, 1);
            enemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity, this.transform);

            //Reset
            lastTimeEnemyInstantiated = 0;
        }
    }

    #endregion

    #region Custom Methods

    public void GoLevelsMenu()
    {
        transitor.LoadNextScene(onFinishScene);
    }

    #endregion
};
