using DataAndSaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlSettingMenuManager : MonoBehaviour
{
    public GameObject optionsMenu;

    bool pYAxisInverted = false;
    public Toggle playerYAxisInvertedToggle;
    bool pControlsPosChanged = false;
    public Toggle playerControlsPosChangedToggle;
    bool snapSpeedJoys = true;
    public Toggle snapSpeedJoystickToggle;
    bool snapMovJoys = false;
    public Toggle snapMovJoystickToggle;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }
    }

    public void SetInvertPlayerYAxis(bool newValue)
    {
        pYAxisInverted = newValue;
        GameData.playerYAxisInverted = pYAxisInverted;
    }

    public void SetChangePlayerControlsPos(bool newValue)
    {
        pControlsPosChanged = newValue;
        GameData.playerControlsPosChanged = pControlsPosChanged;
    }

    public void SetChangeSnapSpeedJoy(bool newValue)
    {
        snapSpeedJoys = newValue;
        GameData.snapSpeedJoystick = snapSpeedJoys;
    }

    public void SetChangeSnapMovJoy(bool newValue)
    {
        snapMovJoys = newValue;
        GameData.snapMovJoystick = snapMovJoys;
    }

    public void setAllFromGameData()
    {
        pYAxisInverted = GameData.playerYAxisInverted;
        pControlsPosChanged = GameData.playerControlsPosChanged;
        snapSpeedJoys = GameData.snapSpeedJoystick;
        snapMovJoys = GameData.snapMovJoystick;

        playerControlsPosChangedToggle.isOn = pControlsPosChanged;
        playerYAxisInvertedToggle.isOn = pYAxisInverted;
        snapSpeedJoystickToggle.isOn = snapSpeedJoys;
        snapMovJoystickToggle.isOn = snapMovJoys;
    }

    public void Back()
    {
        GameData.SaveGame();
        optionsMenu.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
