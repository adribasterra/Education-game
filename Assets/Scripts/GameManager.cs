using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Lobby")]
    public GameObject players;

    [Header("Game")]
    public GameObject finishLines;
    public GameObject cargos;
    public GameObject forceIndicators;
    public GameObject dataIndicators;

    private int player;

    private bool miniGameFinished;

    // Start is called before the first frame update
    void Start()
    {        
        player = SceneElementsController.PlayingCell;

        //Activate the current playing cell & it's stuff
        players.transform.GetChild(player).gameObject.SetActive(true);
        if (this.gameObject.name.StartsWith("Lobby"))
        {
            //Nothing else
        }
        else if (this.gameObject.name.StartsWith("Game"))
        {
            finishLines.transform.GetChild(player).gameObject.SetActive(true);      //Finish line or destination
            cargos.transform.GetChild(player).gameObject.SetActive(true);           //Corresponding UI
            dataIndicators.transform.GetChild(player).gameObject.SetActive(true);   //Corresponding map data

            //Depending on the cell, change the force indicators' direction / orientation
            if (player == 1) forceIndicators.transform.GetChild(player).gameObject.SetActive(true);
            else forceIndicators.transform.GetChild(0).gameObject.SetActive(true);

        }
    }
}