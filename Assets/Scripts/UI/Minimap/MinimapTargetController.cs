using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapTargetController : MonoBehaviour
{
    public Transform target;
    public float maxDistance;

    private SpriteRenderer sRenderer;
    private Color color;
    private Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        sRenderer = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        color = sRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        destination = new Vector3(target.position.x, this.transform.position.y, target.position.z);
        this.transform.LookAt(destination);

        float distance = Vector3.Distance(this.transform.position, destination);
        //Debug.Log($"dist: {distance}");

        if (distance < maxDistance)
        {
            distance /= maxDistance;
            color.a = Mathf.Clamp(distance, 0, 1);
            sRenderer.color = color;
            //Debug.Log($"sRenderer color: {sRenderer.color}");
        }
    }
}
