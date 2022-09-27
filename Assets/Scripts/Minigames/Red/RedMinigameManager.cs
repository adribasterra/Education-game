using UnityEngine;

public class RedMinigameManager : MonoBehaviour
{
    #region Public

    public GameObject oxygenParent;
    public Trajectory trajectory;
    public GameObject oxygenPrefab;
    public Transitions transitor;
    public string onFinishScene = "MainMenu";

    #endregion

    #region Private

    [Header("Trajectory")]
    [SerializeField] private readonly float pushForce = 4f;
    private bool isDragging = false;
    private Vector3 startPoint;
    private Vector3 endPoint;
    private Vector3 direction;
    private Vector3 force;
    private float distance;
    private float elapsedTime = ScoreSystemManager.TotalRedTime;

    [Header("Balls control")]
    private RedMinigamePlayer currentPlayer;
    private int currentPlayerNum;
    private GameObject[] oxygenBalls;
    private RedMinigamePlayer[] oxygenControllers;
    private int timesDraggedPerBall = 0;
    private int numBalls;
    private bool gameOver = false;

    private MinigameCanvasManager canvasManager;

    #endregion

    #region Main functions

    void Start()
    {
        canvasManager = this.GetComponent<MinigameCanvasManager>();
        currentPlayerNum = 0;
        numBalls = ScoreSystemManager.TotalOxygenBalls;
        oxygenControllers = new RedMinigamePlayer[numBalls];
        oxygenBalls = new GameObject[numBalls];
        for (int i = 0; i<numBalls; i++)
        {
            oxygenBalls[i] = Instantiate(oxygenPrefab, oxygenParent.transform);
            oxygenControllers[i] = oxygenBalls[i].GetComponent<RedMinigamePlayer>();
            oxygenBalls[i].SetActive(false);
            oxygenControllers[i].ActivateRb(false);
        }
        currentPlayer = oxygenControllers[currentPlayerNum];
        currentPlayer.gameObject.SetActive(true);
    }

    void Update()
    {
        if (gameOver || !canvasManager.GetHideTutorial()) return;
        elapsedTime += Time.deltaTime;
        //If has reached target or lost out of the bounds of the map
        if (currentPlayer.GetHasArrived() || currentPlayer.GetLostOutOfBounds())
        {
            //Check if more balls are available
            if(currentPlayerNum < numBalls)
            {
                //If has reached target, make times dragged count
                if (currentPlayer.GetHasArrived())
                {
                    ScoreSystemManager.TimesDraggedRedMinigame += timesDraggedPerBall;
                    timesDraggedPerBall = 0;
                    Debug.Log(ScoreSystemManager.TimesDraggedRedMinigame);
                }
                currentPlayer = oxygenControllers[currentPlayerNum++];
                currentPlayer.SetActive(true);
            }
            else
            {
                //Check if drags need to be taken into account
                if(currentPlayer.GetHasArrived()) ScoreSystemManager.TimesDraggedRedMinigame += timesDraggedPerBall;

                //Set variables in ScoreManager
                ScoreSystemManager.TotalRedTime = elapsedTime;
                ScoreSystemManager.MiniGameFinished = true;
                ScoreSystemManager.CalculateFinalRedScore();

                gameOver = true;
                canvasManager.ShowScoreRed();
                return;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!isDragging) timesDraggedPerBall++;
            isDragging = true;
            OnDragStart();
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            OnDragEnd();
        }

        if (isDragging) OnDrag();
    }
    #endregion

    #region Custom Methods

    void OnDragStart()
    {
        currentPlayer.ActivateRb(false);
        Vector3 input = Input.mousePosition;
        input.z = -10;
        startPoint = Camera.main.ScreenToWorldPoint(input);
        startPoint.z = 0;
        trajectory.SetParentActive(true);
    }

    void OnDrag()
    {
        Vector3 input = Input.mousePosition;
        input.z = -10;
        endPoint = Camera.main.ScreenToWorldPoint(input);
        endPoint.z = 0;

        distance = Vector3.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = direction * distance * pushForce;
        trajectory.UpdateDots(currentPlayer.BallPos, force);

        Debug.DrawLine(startPoint, endPoint);
    }

    void OnDragEnd()
    {
        currentPlayer.ActivateRb(true);
        currentPlayer.Push(force);
        //Hide trajectory dots
        trajectory.SetParentActive(false);
    }

    public RedMinigamePlayer GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public void GoLevelsMenu()
    {
        transitor.LoadNextScene(onFinishScene);
    }

    #endregion
}
