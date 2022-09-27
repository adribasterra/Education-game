using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCollidersManager : MonoBehaviour
{
    public Transitions transitor;
    [Header("Scene names")]
    private string lobbyExitScene = "FinalGameScene";
    public string redCellMinigame = "RedCellMinigame";
    public string whiteCellMinigame = "WhiteCellMinigame";
    public string plataletMinigame = "PlataletMinigame";

    private void OnTriggerEnter(Collider other)
    {
        //              ELEMENTS OF THE BODY
        /****************************************************/
        if(other.gameObject.tag == "Player")
        {
            if (this.gameObject.name.StartsWith("R-Toe"))
            {
                transitor.LoadNextScene(lobbyExitScene);
                ScoreSystemManager.ResetVariables();
            }
            if (this.gameObject.name.StartsWith("L-Toe"))
            {
                transitor.LoadNextScene(lobbyExitScene);
                ScoreSystemManager.ResetVariables();
            }
            if (this.gameObject.name.StartsWith("R-Hand"))
            {
                transitor.LoadNextScene(lobbyExitScene);
                ScoreSystemManager.ResetVariables();
            }
            if (this.gameObject.name.StartsWith("L-Hand"))
            {
                transitor.LoadNextScene(lobbyExitScene);
                ScoreSystemManager.ResetVariables();
            }
            if (this.gameObject.name.StartsWith("Head"))
            {
                transitor.LoadNextScene(lobbyExitScene);
            }
        }

        //              DESTINATION OF CELLS
        /****************************************************/

        if (this.gameObject.name.StartsWith("RedFinishLine") && other.gameObject.name.StartsWith("RedCell"))
        {
            transitor.LoadNextScene(redCellMinigame);
        }
        if (this.gameObject.name.StartsWith("WhiteFinishLine") && other.gameObject.name.StartsWith("WhiteCell"))
        {
            transitor.LoadNextScene(whiteCellMinigame);
        }
        if (this.gameObject.name.StartsWith("PlataletFinishLine") && other.gameObject.name.StartsWith("Platalet"))
        {
            transitor.LoadNextScene(plataletMinigame);
        }

        //                  LOBBY SCENE
        /****************************************************/

        if (this.gameObject.name.StartsWith("LobbyExit"))
        {
            if (other.gameObject.tag == "Player")
            {
                transitor.LoadNextScene(lobbyExitScene);
            }
        }
    }
}
