using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFog : MonoBehaviour
{
    #region Attributes

    //Apply fog
    public bool AllowFog = false;

    //Save the settings value
    private bool SettingFogOn;

    #endregion

    #region Main Methods

    private void OnPreRender()
    {
        /*if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor
            || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
        {*/
            SettingFogOn = RenderSettings.fog;
            RenderSettings.fog = AllowFog;
        /*}*/
    }

    private void OnPostRender()
    {
        /*if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor
            || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
        {*/
            RenderSettings.fog = SettingFogOn;
        /*}*/
    }

    #endregion
}