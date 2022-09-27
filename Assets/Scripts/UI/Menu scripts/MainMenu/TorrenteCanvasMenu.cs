using UnityEngine;

public class TorrenteCanvasMenu : MonoBehaviour
{
    #region Public

    public GameObject mainMenu;
    public GameObject redCell;
    public GameObject platalet;
    public GameObject whiteCell;
    public Transitions transitor;
    public GameObject[] thingsGame;

    #endregion

    #region Private

    private string levelSceneName = "LobbyScene";

    #endregion

    #region Main Methods

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoMainMenu();
        }
    }

    #endregion

    #region Custom Methods

    public void GoMainMenu()
    {
        mainMenu.gameObject.SetActive(true);
        foreach(GameObject go in thingsGame)
        {
            go.SetActive(true);
        }
        this.transform.parent.parent.gameObject.SetActive(false);
    }

    public void GoRedCellLevel()
    {
        Time.timeScale = 1f;
        SceneElementsController.PlayingCell = (int)SceneElementsController.Cells.red;
        transitor.LoadNextScene(levelSceneName);
    }

    public void GoWhiteCellLevel()
    {
        //Check if achieved minimum score to unlock
        if (ScoreSystemManager.MaxRedScore >= ScoreSystemManager.MinRedScoreToUnlock)
        {
            Time.timeScale = 1f;
            SceneElementsController.PlayingCell = (int)SceneElementsController.Cells.white;
            transitor.LoadNextScene(levelSceneName);
        }
    }

    public void GoPlataletLevel()
    {
        //Check if achieved minimum score to unlock
        if (ScoreSystemManager.MaxWhiteScore >= ScoreSystemManager.MinWhiteScoreToUnlock)
        {
            Time.timeScale = 1f;
            SceneElementsController.PlayingCell = (int)SceneElementsController.Cells.platalet;
            transitor.LoadNextScene(levelSceneName);
        }
    }

    #endregion
}
