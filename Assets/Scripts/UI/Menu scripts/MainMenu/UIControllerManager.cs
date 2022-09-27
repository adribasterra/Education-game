using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControllerManager : MonoBehaviour
{
    #region Attributtes
    public GameObject pauseMenu;
    public bool cursorVisibleEnabledOnPlay = true;
    #endregion

    #region Main Methods
    // Start is called before the first frame update
    void Start()
    {
        if (!cursorVisibleEnabledOnPlay) Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != 0f)
        {
            Cursor.lockState = CursorLockMode.None;
            pauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0f;
            //If the player presses the Esc key, the cursor is released
            if (!cursorVisibleEnabledOnPlay) Cursor.visible = true;
        }
    }
    #endregion
}
