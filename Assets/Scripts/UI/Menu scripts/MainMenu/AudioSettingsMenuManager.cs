using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataAndSaveSystem;
using UnityEngine.Audio;

public class AudioSettingsMenuManager : MonoBehaviour
{

    public GameObject optionsMenu;
    float mainVolume = 50;
    public Text mainVolumeText;
    public Slider mainVolumeSlider;

    float voiceVolume = 50;
    public Text ballisticsVolumeText;
    public Slider ballisticsVolumeSlider;

    float effectsVolume = 50;
    public Text planeEngineVolumeText;
    public Slider planeEngineVolumeSlider;

    float ambientVolume = 50;
    public Text ambientVolumeText;
    public Slider ambientVolumeSlider;

    float musicVolume = 50;
    public Text musicVolumeText;
    public Slider musicVolumeSlider;

    public AudioMixer masterMixer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mainVolumeText.text = "" +Mathf.Round(mainVolume) + " %";
        ballisticsVolumeText.text = "" + Mathf.Round(voiceVolume) + " %";
        planeEngineVolumeText.text = "" + Mathf.Round(effectsVolume) + " %";
        ambientVolumeText.text = "" + Mathf.Round(ambientVolume) + " %";
        musicVolumeText.text = "" + Mathf.Round(musicVolume) + " %";

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }
    }

    public void SetMainVolume(float newVolume)
    {
        mainVolume = newVolume;
        GameData.Volume_Main = mainVolume;
        setAudioLevels();
    }

    public void SetVoiceVolume(float newVolume)
    {
        voiceVolume = newVolume;
        GameData.Volume_Voice = voiceVolume;
        setAudioLevels();
    }

    public void SetEffectsVolume(float newVolume)
    {
        effectsVolume = newVolume;
        GameData.Volume_Effects = effectsVolume;
        setAudioLevels();
    }

    public void SetAmbientVolume(float newVolume)
    {
        ambientVolume = newVolume;
        GameData.Volume_Ambient = ambientVolume;
        setAudioLevels();
    }

    public void SetMusicVolume(float newVolume)
    {
        musicVolume = newVolume;
        GameData.Volume_Music = musicVolume;
        setAudioLevels();
    }

    public void Back()
    {
        GameData.SaveGame();
        setAudioLevels();
        optionsMenu.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void setAllFromGameData()
    {
        mainVolume = GameData.Volume_Main;
        voiceVolume = GameData.Volume_Voice;
        effectsVolume = GameData.Volume_Effects;
        ambientVolume = GameData.Volume_Ambient;
        musicVolume = GameData.Volume_Music;

        mainVolumeSlider.value = mainVolume;
        ballisticsVolumeSlider.value = voiceVolume;
        planeEngineVolumeSlider.value = effectsVolume;
        ambientVolumeSlider.value = ambientVolume;
        musicVolumeSlider.value = musicVolume;
    }

    public void setAudioLevels()
    {
        masterMixer.SetFloat("Master", -((1.0f - GameData.Volume_Main / 100.0f) * 80.0f));
        masterMixer.SetFloat("Voice", -((1.0f - GameData.Volume_Voice / 100.0f) * 80.0f));
        masterMixer.SetFloat("Effects", -((1.0f - GameData.Volume_Effects / 100.0f) * 80.0f));
        masterMixer.SetFloat("Ambient", -((1.0f - GameData.Volume_Ambient / 100.0f) * 80.0f));
        masterMixer.SetFloat("Music", -((1.0f - GameData.Volume_Music / 100.0f) * 80.0f));
    }
}
