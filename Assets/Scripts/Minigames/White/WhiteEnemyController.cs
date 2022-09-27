using UnityEngine;

public class WhiteEnemyController : MonoBehaviour
{
    #region Public

    public GameObject explosionPrefab;

    public float speed;
    public float scaleReduction = 0.05f;

    #endregion

    #region Private

    private GameObject explosion;
    private Transform woundsParent;
    private WhitePlayerController player;

    #endregion

    #region Main functions

    void Start()
    {
        woundsParent = GameObject.FindGameObjectWithTag("Marker").gameObject.transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<WhitePlayerController>();
    }

    void Update()
    {
        if (player.GameOver()) return;

        float prevDistance = 1000f;
        int chasingChild = 0;
        for(int i = 0; i<woundsParent.childCount; i++)
        {
            float distance = Vector3.Distance(transform.position, woundsParent.GetChild(i).transform.position);
            if (distance < prevDistance)
            {
                prevDistance = distance;
                chasingChild = i;
            }
            else
            {
                chasingChild = i - 1;
            }
        }
        Vector3 woundPos = woundsParent.GetChild(chasingChild).transform.position;
        woundPos.z = this.transform.position.z;
        transform.position = Vector3.MoveTowards(transform.position, woundPos, speed * Time.deltaTime);

        if(this.transform.position == woundPos)
        {
            if (this.transform.localScale.x > 0)
            {
                this.transform.localScale -= new Vector3(scaleReduction, scaleReduction, scaleReduction) * Time.deltaTime;
                //this.GetComponent<BoxCollider>().enabled = false;   //If it's getting into de wound it may be to late to kill it
            }
            else
            {
                this.gameObject.SetActive(false);
                ScoreSystemManager.NumCellsInWound++;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (player.GetHasArrived())
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    #endregion

    #region Custom Methods

    public void HitByRay()
    {
        explosion = Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
        explosion.transform.GetChild(0).GetComponent<Animator>().Play("Explosion");
        this.gameObject.SetActive(false);
        ScoreSystemManager.NumShootsScored++;
    }

    #endregion
}
