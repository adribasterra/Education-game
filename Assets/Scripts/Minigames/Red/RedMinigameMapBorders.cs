using UnityEngine;

public class RedMinigameMapBorders : MonoBehaviour
{
    private RedMinigameManager minigameManager;

    void Start()
    {
        minigameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<RedMinigameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && minigameManager != null)
        {
            other.GetComponent<RedMinigamePlayer>().SetLostOutOfBounds();
            other.gameObject.SetActive(false);
        }
    }
}
