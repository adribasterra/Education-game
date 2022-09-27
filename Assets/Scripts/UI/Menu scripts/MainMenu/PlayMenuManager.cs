using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DataAndSaveSystem;

public class PlayMenuManager : MonoBehaviour
{
    public GameObject gameMenu;
    public TorrenteCanvasMenu canvasCells;
    public GameObject NewGame;
    public GameObject LoadGame;
    public GameObject Back;
    public GameObject MainMenu;
    public GameObject transitor;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainMenu.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //Entrar al menu de salir del juego
            if (hit.transform.name == "NewGameText")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GameData.ResetGameFunction();
                    NewGame.transform.GetComponent<TextMesh>().color = Color.red;
                    gameMenu.SetActive(true);
                    foreach (GameObject go in canvasCells.thingsGame)
                    {
                        go.SetActive(false);
                    }
                    this.gameObject.SetActive(false);
                }
                
                hit.transform.GetComponent<TextMesh>().color = Color.yellow;
            }
            else
            {
                NewGame.GetComponent<TextMesh>().color = Color.white;
            }

            if (hit.transform.name == "BackText")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    MainMenu.gameObject.SetActive(true);
                    this.gameObject.SetActive(false);
                }

                hit.transform.GetComponent<TextMesh>().color = Color.yellow;
            }
            else
            {
                Back.GetComponent<TextMesh>().color = Color.white;
            }

            if (hit.transform.name == "LoadGameText")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    NewGame.transform.GetComponent<TextMesh>().color = Color.red;
                    if (GameData.LoadGame() != null)
                    {
                        gameMenu.SetActive(true);
                        foreach (GameObject go in canvasCells.thingsGame)
                        {
                            go.SetActive(false);
                        }
                        this.gameObject.SetActive(false);
                    }
                }

                hit.transform.GetComponent<TextMesh>().color = Color.yellow;
            }

            else
            {
                LoadGame.GetComponent<TextMesh>().color = Color.white;
            }

        }

        else
        {
            NewGame.GetComponent<TextMesh>().color = Color.white;
            LoadGame.GetComponent<TextMesh>().color = Color.white;
            Back.GetComponent<TextMesh>().color = Color.white;
        }
    }
}
