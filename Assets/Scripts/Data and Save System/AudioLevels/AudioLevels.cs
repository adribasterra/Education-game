using DataAndSaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


namespace DataAndSaveSystem
{
    public class AudioLevels : MonoBehaviour
    {
        public AudioMixer myMixer;

        // Start is called before the first frame update
        void Start()
        {
            myMixer.SetFloat("Master", 0f - ((100f - GameData.Volume_Main) * 0.8f));
            myMixer.SetFloat("Voice", 0f - ((100f - GameData.Volume_Voice) * 0.8f));
            myMixer.SetFloat("Effects", 0f - ((100f - GameData.Volume_Effects) * 0.8f));
            myMixer.SetFloat("Ambient", 0f - ((100f - GameData.Volume_Ambient) * 0.8f));
            myMixer.SetFloat("Music", 0f - ((100f - GameData.Volume_Music) * 0.8f));
        }

        // Update is called once per frame
        void Update()
        {
            if(Time.timeScale == 0f)
            {
                myMixer.SetFloat("Master", 0f - ((100f - GameData.Volume_Main) * 0.8f));
                myMixer.SetFloat("Voice", 0f - ((100f - GameData.Volume_Voice) * 0.8f));
                myMixer.SetFloat("Effects", 0f - ((100f - GameData.Volume_Effects) * 0.8f));
                myMixer.SetFloat("Ambient", 0f - ((100f - GameData.Volume_Ambient) * 0.8f));
                myMixer.SetFloat("Music", 0f - ((100f - GameData.Volume_Music) * 0.8f));
            }
        }
    }
}
