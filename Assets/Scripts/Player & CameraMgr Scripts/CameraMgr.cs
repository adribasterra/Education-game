using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataAndSaveSystem;

public class CameraMgr : MonoBehaviour
{
    #region Attributes
    public GameObject players;
    public Transform positionMarker;
    [Range(0f, 1f)]
    public float cameraRotationSmoothSpeed = 0.125f;
    [Range(0f, 1f)]
    public float cameraPositionSmoothSpeed = 0.95f;
    public Vector3 offsetPos = Vector3.zero;
    public bool lerpPos = false;
    public bool freezeheight = false;
    public float freezeHeightPos = 0f;
    #endregion

    #region Propperties
    #endregion

    #region Main Methods

    private void Start()
    {
        if(positionMarker == null) positionMarker = players.transform.GetChild(SceneElementsController.PlayingCell).GetChild(0).Find("CameraTarget");
    }

    void FixedUpdate()
    {
        if (lerpPos) transform.position = Vector3.Lerp(transform.position, positionMarker.position + offsetPos, cameraPositionSmoothSpeed);
        else transform.position = positionMarker.position + offsetPos;
        transform.rotation = Quaternion.Lerp(transform.rotation, positionMarker.rotation, cameraRotationSmoothSpeed);
        if (freezeheight) transform.position = new Vector3 (transform.position.x , freezeHeightPos, transform.position.z);
    }

    void Update()
    {
        if(Time.timeScale == 0.0f) this.GetComponent<Camera>().fieldOfView = GameData.playerFOV;
    }
    #endregion

    #region Custom Methods
    #endregion
}
