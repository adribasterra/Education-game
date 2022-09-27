using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataAndSaveSystem;

public class OptionsMenuManager : MonoBehaviour
{
    public GameObject AudioSettings;
    public GameObject GraphicsSettings;
    public GameObject ControlsSettings;
    public GameObject Back;
    public GameObject MainMenu;
    public GameObject AudioMenu;
    public GameObject GraphicsMenu;
    public GameObject ControlsMenu;

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
            if (hit.transform.name == "AudioSettingsText")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    AudioMenu.gameObject.SetActive(true);
                    AudioMenu.GetComponent<AudioSettingsMenuManager>().setAllFromGameData();
                    this.gameObject.SetActive(false);
                }
                hit.transform.GetComponent<TextMesh>().color = Color.yellow;
            }

            else
            {
                AudioSettings.GetComponent<TextMesh>().color = Color.white;
            }

            if (hit.transform.name == "BackText")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GameData.SaveGame();
                    MainMenu.gameObject.SetActive(true);
                    this.gameObject.SetActive(false);
                }
                hit.transform.GetComponent<TextMesh>().color = Color.yellow;
            }

            else
            {
                Back.GetComponent<TextMesh>().color = Color.white;
            }

            if (hit.transform.name == "GraphicsSettingsText")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GraphicsMenu.gameObject.SetActive(true);
                    GraphicsMenu.GetComponent<GraphicsSettingsMenuManager>().setAllFromGameData();
                    this.gameObject.SetActive(false);
                }
                     
                hit.transform.GetComponent<TextMesh>().color = Color.yellow;
            }

            else
            {
                GraphicsSettings.GetComponent<TextMesh>().color = Color.white;
            }

            if (hit.transform.name == "ControlsSettingsText")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ControlsMenu.gameObject.SetActive(true);
                    ControlsMenu.GetComponent<ControlSettingMenuManager>().setAllFromGameData();
                    this.gameObject.SetActive(false);
                }
                  
                hit.transform.GetComponent<TextMesh>().color = Color.yellow;
            }

            else
            {
                ControlsSettings.GetComponent<TextMesh>().color = Color.white;
            }
        }

        else
        {
            AudioSettings.GetComponent<TextMesh>().color = Color.white;
            GraphicsSettings.GetComponent<TextMesh>().color = Color.white;
            ControlsSettings.GetComponent<TextMesh>().color = Color.white;
            Back.GetComponent<TextMesh>().color = Color.white;
        }
    }
}
