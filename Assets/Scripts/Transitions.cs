using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transitions : MonoBehaviour
{
    public Animator transition;
    public float duration = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextScene(String sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    private IEnumerator LoadScene(String sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(duration);

        SceneManager.LoadScene(sceneName);
    }
}
