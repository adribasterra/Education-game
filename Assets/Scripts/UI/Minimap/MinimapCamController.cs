using UnityEngine;
using System.Collections;

public class MinimapCamController : MonoBehaviour {
    
	public Transform targets;
    private Transform target;

    private void Start()
    {
        target = targets.GetChild(SceneElementsController.PlayingCell);
    }

    void FixedUpdate ()
    {
		transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
		//transform.eulerAngles = new Vector3(transform.eulerAngles.x, target.eulerAngles.y, transform.eulerAngles.z);
	}
}