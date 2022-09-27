using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhitePlayerController : MonoBehaviour
{
    #region Public

    [Header("Player")]
    public float timeBtwShoots;
    public float aimingSpeed;

    [Header("Line Renderers")]
    public GameObject top;
    public GameObject left;

    #endregion

    #region Private

    private int totalAmmo;
    private float elapsedTime;
    private float lastTimeShot = 0;
    private Vector3 worldPos;
    private bool hasArrivedToDestination = false;
    private readonly float mapVerticalLimitAbs = 2f;
    private readonly float mapHorizontalLimitAbs = 4.8f;
    private bool gameOver = false;

    private LineRenderer topLine;
    private LineRenderer leftLine;

    #endregion

    #region Main functions

    void Start()
    {
        totalAmmo = ScoreSystemManager.TotalAmmo;
        elapsedTime = ScoreSystemManager.TotalWhiteTime;
        Debug.Log($"totalAmmo: {totalAmmo}, time: {elapsedTime}");
        topLine = top.GetComponent<LineRenderer>();
        leftLine = left.GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (gameOver) return;
        if (totalAmmo <= 0)
        {
            gameOver = true;
            ScoreSystemManager.TotalWhiteTime = elapsedTime;
            return;
        }

        //Aim & shoot
        lastTimeShot += Time.deltaTime;
        elapsedTime += Time.deltaTime;
        if (lastTimeShot >= timeBtwShoots)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Get position
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 5;
                worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                worldPos.z = this.transform.position.z;
                hasArrivedToDestination = false;

                //Check map limits
                worldPos.x = Mathf.Abs(worldPos.x) > mapHorizontalLimitAbs ? Mathf.Sign(worldPos.x) * mapHorizontalLimitAbs : worldPos.x;
                worldPos.y = Mathf.Abs(worldPos.y) > mapVerticalLimitAbs ? Mathf.Sign(worldPos.y) * mapVerticalLimitAbs : worldPos.y;

                //Debug.Log($"worldpos x: {worldPos.x}, y: {worldPos.y}");

                //Reset
                lastTimeShot = 0;
            }
            this.transform.position = Vector3.MoveTowards(this.transform.position, worldPos, aimingSpeed * Time.deltaTime);

            if(this.transform.position == worldPos && !hasArrivedToDestination)
            {
                hasArrivedToDestination = true;
                totalAmmo--;
                if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit))
                {
                    if (hit.transform.name.StartsWith("Enemy"))
                    {
                        hit.collider.transform.GetComponent<WhiteEnemyController>().HitByRay();
                    }
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                }
            }

            //Update positions
            top.transform.position = new Vector3(this.transform.position.x, top.transform.position.y, 0);
            left.transform.position = new Vector3(left.transform.position.x, this.transform.position.y, 0);

            //Update line Renderers
            UpdateLineRenderers();
        }
    }

    #endregion

    #region Methods

    private void UpdateLineRenderers()
    {
        //Vertical line
        Vector3 finalTopPos = top.transform.position;
        finalTopPos.y = -10;
        topLine.SetPosition(0, top.transform.position);
        topLine.SetPosition(1, finalTopPos);

        //Horizontal line
        Vector3 finalLeftPos = left.transform.position;
        finalLeftPos.x = 20;
        leftLine.SetPosition(0, left.transform.position);
        leftLine.SetPosition(1, finalLeftPos);
    }

    public bool GetHasArrived()
    {
        return hasArrivedToDestination;
    }

    public int GetCurrentAmmo()
    {
        return totalAmmo;
    }

    public bool GameOver()
    {
        return gameOver;
    }

    #endregion
}
