using DataAndSaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicLevels : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = GameData.frameRateLimiter;
        if (GameData.vSync)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0f)
        {
            Application.targetFrameRate = GameData.frameRateLimiter;
            if(GameData.vSync)
            {
                QualitySettings.vSyncCount = 1;
            }
            else
            {
                QualitySettings.vSyncCount = 0;
            }
        }
    }
}
