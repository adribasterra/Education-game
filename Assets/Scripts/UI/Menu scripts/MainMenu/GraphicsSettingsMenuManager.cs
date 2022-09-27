using DataAndSaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettingsMenuManager : MonoBehaviour
{

    public GameObject optionsMenu;

    float playerFOV = 80;
    public Text playerFOVText;
    public Slider playerFOVSlider;

    int frameRateLimit = 300;
    public Text frameRateLimitText;
    public Slider frameRateLimitSlider;

    int boidsCount = 30;
    public Text boidsCountText;
    public Slider boidsCountSlider;

    bool vSync = false;
    public Toggle vSyncToggle;

    bool windowed = false;
    public Toggle windowedToggle;

    // Update is called once per frame
    void Update()
    {
        playerFOVText.text = Mathf.Round(playerFOV).ToString();
        frameRateLimitText.text = frameRateLimit.ToString();
        boidsCountText.text = boidsCount.ToString();


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }
    }

    public void Back()
    {
        GameData.SaveGame();
        optionsMenu.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void SetPlayerFOV(float newValue)
    {
        playerFOV = newValue;
        GameData.playerFOV = playerFOV;
    }

    public void SetFrameRateLimit(float newValue)
    {
        frameRateLimit = (int)newValue;
        GameData.frameRateLimiter = frameRateLimit;
    }

    public void SetBoidsLimit(float newValue)
    {
        boidsCount = (int)newValue;
        GameData.boidsCount = boidsCount;
    }

    public void SetVSync(bool newValue)
    {
        vSync = newValue;
        GameData.vSync = vSync;
    }

    public void SetWindowed(bool newValue)
    {
        windowed = newValue;
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor
            || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
        {
            if (windowed)
            {
                GameData.windowed = FullScreenMode.Windowed;
                Screen.fullScreenMode = GameData.windowed;
            }
            else
            {
                GameData.windowed = FullScreenMode.ExclusiveFullScreen;
                Screen.fullScreenMode = GameData.windowed;
            }
        }
    }

    public void setAllFromGameData()
    {
        playerFOV = GameData.playerFOV;
        frameRateLimit = GameData.frameRateLimiter;
        boidsCount = GameData.boidsCount;
        vSync = GameData.vSync;
        if (GameData.windowed == FullScreenMode.ExclusiveFullScreen) windowed = false;
        else windowed = true;

        playerFOVSlider.value = playerFOV;
        frameRateLimitSlider.value = frameRateLimit;
        boidsCountSlider.value = boidsCount;
        vSyncToggle.isOn = vSync;
        windowedToggle.isOn = windowed;
    }
}
