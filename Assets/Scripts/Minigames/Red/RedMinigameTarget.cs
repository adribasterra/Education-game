using UnityEngine;

public class RedMinigameTarget : MonoBehaviour
{
    #region Private

    private GameObject particleSyst;

    #endregion

    #region Main functions

    private void Start()
    {
        particleSyst = this.transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Play particle systems
            particleSyst.SetActive(true);
            for (int i = 0; i < particleSyst.transform.childCount; i++)
            {
                particleSyst.transform.GetChild(i).GetComponent<ParticleSystem>().Play();
            }

            //Deactivate oxygenBall
            other.GetComponent<RedMinigamePlayer>().SetActive(false);
            other.GetComponent<RedMinigamePlayer>().SetHasArrived();
            //Increase scored balls
            ScoreSystemManager.NumOxygenBallsScored++;
        }
    }

    #endregion
}
