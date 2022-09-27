using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsCanvasMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject audioMenu;
    public GameObject graphicsMenu;
    public GameObject controlsMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            goPauseMenu();
        }
    }

    public void goPauseMenu()
    {
        pauseMenu.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void goAudioMenu()
    {
        audioMenu.gameObject.SetActive(true);
        audioMenu.GetComponent<AudioSettingsMenuManager>().setAllFromGameData();
        this.gameObject.SetActive(false);
    }

    public void goGraphicsMenu()
    {
        graphicsMenu.gameObject.SetActive(true);
        graphicsMenu.GetComponent<GraphicsSettingsMenuManager>().setAllFromGameData();
        this.gameObject.SetActive(false);
    }

    public void goControlsMenu()
    {
        controlsMenu.gameObject.SetActive(true);
        controlsMenu.GetComponent<ControlSettingMenuManager>().setAllFromGameData();
        this.gameObject.SetActive(false);
    }
}
