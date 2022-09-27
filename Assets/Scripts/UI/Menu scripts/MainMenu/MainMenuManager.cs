using DataAndSaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    public GameObject playText;
    public GameObject optionsText;
    public GameObject playMenu;
    public GameObject optionsMenu;
    public GameObject exit;
    public GameObject bestScore;
    public GameObject levelMenu;
    public AudioMixer masterMixer;

    // Start is called before the first frame update
    void Start()
    {
        if (ScoreSystemManager.MiniGameFinished)
        {
            ScoreSystemManager.MiniGameFinished = false;
            this.gameObject.SetActive(false);
            levelMenu.SetActive(true);
        }
        GameData.LoadGame();
        masterMixer.SetFloat("Master", - ((1.0f - GameData.Volume_Main / 100.0f) * 80.0f));
        masterMixer.SetFloat("Voice", -((1.0f - GameData.Volume_Voice / 100.0f) * 80.0f));
        masterMixer.SetFloat("Effects", -((1.0f - GameData.Volume_Effects / 100.0f) * 80.0f));
        masterMixer.SetFloat("Ambient", -((1.0f - GameData.Volume_Ambient / 100.0f) * 80.0f));
        masterMixer.SetFloat("Music", -((1.0f - GameData.Volume_Music / 100.0f) * 80.0f));
    }

    // Update is called once per frame
    void Update()
    {
        //bestScore.GetComponent<TextMesh>().text = "Current best score: " + GameData.bestScore;

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameData.ResetCfgComponentsFunction();
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //Entrar al menu de salir del juego
            if (hit.transform.name == "PlayText")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    playMenu.gameObject.SetActive(true);
                    this.gameObject.SetActive(false);
                }

                hit.transform.GetComponent<TextMesh>().color = Color.yellow;

            }

            else
            {
                playText.GetComponent<TextMesh>().color = Color.white;
            }

            if (hit.transform.name == "ExitText")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GameData.SaveGame();
                    Application.Quit();
                    Debug.Log("Game closing...");
                }

                hit.transform.GetComponent<TextMesh>().color = Color.yellow;

            }

            else
            {
                exit.GetComponent<TextMesh>().color = Color.white;
            }

            if (hit.transform.name == "OptionsText")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    optionsMenu.gameObject.SetActive(true);
                    this.gameObject.SetActive(false);
                }

                hit.transform.GetComponent<TextMesh>().color = Color.yellow;

            }
            else
            {
                optionsText.GetComponent<TextMesh>().color = Color.white;
            }
        }

        else
        {
            playText.GetComponent<TextMesh>().color = Color.white;
            optionsText.GetComponent<TextMesh>().color = Color.white;
            exit.GetComponent<TextMesh>().color = Color.white;
        }
    }
}
