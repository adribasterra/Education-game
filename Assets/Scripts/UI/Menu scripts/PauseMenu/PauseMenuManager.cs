using DataAndSaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public UIControllerManager UiController;
    public GameObject OptionsMenu;
    public GameObject transitor;
    public string mainMenuName = "MainMenu";

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            goContinue();
        }
    }

    public void goOptions()
    {
        OptionsMenu.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void goContinue()
    {
        GameData.SaveGame();
        Time.timeScale = 1.0f;
        if(!UiController.cursorVisibleEnabledOnPlay) Cursor.visible = false;
        this.gameObject.SetActive(false);
        Debug.Log("Disabled");
    }

    public void goMainMenuScene()
    {
        Time.timeScale = 1f;
        transitor.GetComponent<Transitions>().LoadNextScene(mainMenuName);
    }
}
