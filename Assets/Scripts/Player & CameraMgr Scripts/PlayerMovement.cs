using UnityEngine;
using UnityEngine.SceneManagement;
using DataAndSaveSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    #region Attributtes

    [Header("Main Attributes")]
    public Text mapData;
    public Transitions transitor;
    public float defaultSidesAccel = 6.0f;
    public float turboAccelOffset = 2f;
    public float startSpeed = 15f;
    public float maxSpeed = 20f;
    public float maxSpeedTurbo = 25f;
    public float smoothSpeedCoef = 0.5f;
    public Joystick movJoystick;
    public Joystick velJoystick;
    private Vector3 movement = new Vector3();
    private float speedFront = 0;
    private Vector3 rbSpeed = new Vector3();
    private Vector3 rbTargetSpeed = new Vector3();
    private Timer timer;
    private bool isSceneLobby = false;

    [Header("Lobby Exit Animation")]
    public GameObject finishLine;

    [Header("Lobby Exit Animation")]
    [Range(0f, 1f)]
    public float rotationSpeedLerp = 0.125f;
    public Transform exitPoint;
    public float movementSpeed;
    public float launchSpeed;

    private bool lobbyAnimationRun = false;
    private bool arrived = false, looked = false;

    [Header("Visual Rotation Animation")]
    public GameObject playerVisuals;
    [Range(0f, 1f)]
    public float animationSmoothSpeed = 0.125f;
    private Quaternion destinationRotation;

    [Header("Map Forces")]
    public float forceMagApplied = 4f;
    public Vector3 mapForce;
    public float collisionForceMult = 10f;
    private bool insideMapForce = false;

    [Header("Boids things")]
    public GameObject boidTarget;
    private Vector3 initialBoidTargetLocalPos;

    [Header("Oxygen")]
    public OxygenController oxygenController;
    private readonly string onOxygenExhaustedScene = "FinalGameScene";
    private int numOxygen;
    public OxygenManagerUI oxygenManagerUI;

    [Header("Audio")]
    public AudioSource crashSound;

    #endregion

    #region Properties

    public void SetLobbyAnimationRun(bool run)
    {
        this.lobbyAnimationRun = run;
    }

    #endregion

    #region Main Methods
    void Start()
    {
        if (SceneManager.GetActiveScene().name.StartsWith("Lobby"))
        {
            defaultSidesAccel = 1.0f;
            turboAccelOffset = 0.5f;
            isSceneLobby = true;
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
        else timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        numOxygen = 10;
        if (SceneElementsController.PlayingCell == (int)SceneElementsController.Cells.white)
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce((boidTarget.transform.position - playerVisuals.transform.position).normalized * startSpeed, ForceMode.VelocityChange);
        }
        else this.gameObject.GetComponent<Rigidbody>().AddForce(playerVisuals.transform.forward * startSpeed , ForceMode.VelocityChange);
        initialBoidTargetLocalPos = boidTarget.transform.localPosition;

        if (finishLine != null) finishLine.SetActive(true);

        crashSound = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        CheckNumOxygen();
    }

    private void FixedUpdate()
    {
        if(GameData.playerYAxisInverted) movement = movJoystick.Horizontal * playerVisuals.transform.right + (movJoystick.Vertical * -1.0f) * playerVisuals.transform.up;
        else movement = movJoystick.Horizontal * playerVisuals.transform.right + movJoystick.Vertical * playerVisuals.transform.up;
        
        speedFront = velJoystick.Vertical * turboAccelOffset;

        if(!isSceneLobby) this.gameObject.GetComponent<Rigidbody>().AddForce(movement * defaultSidesAccel + playerVisuals.transform.forward * speedFront
                                                            + mapForce * forceMagApplied, ForceMode.Acceleration);
        else this.gameObject.GetComponent<Rigidbody>().AddForce(movement * defaultSidesAccel + playerVisuals.transform.forward * speedFront, ForceMode.Acceleration);

        if (velJoystick.Vertical > 0.1f)
        {
            if (this.gameObject.GetComponent<Rigidbody>().velocity.magnitude > maxSpeedTurbo)
            {
                rbSpeed = this.gameObject.GetComponent<Rigidbody>().velocity;
                rbTargetSpeed = rbSpeed.normalized * maxSpeedTurbo;
                this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.Lerp(rbSpeed, rbTargetSpeed, smoothSpeedCoef);
                //Debug.Log(rbSpeed + " (" + rbSpeed.magnitude + ") -> " + rbTargetSpeed + " (" + rbTargetSpeed.magnitude + ")"
                //            + "  -> Final: " + this.gameObject.GetComponent<Rigidbody>().velocity.magnitude);
            }
        }
        else if (this.gameObject.GetComponent<Rigidbody>().velocity.magnitude > maxSpeed)
        {
            rbSpeed = this.gameObject.GetComponent<Rigidbody>().velocity;
            rbTargetSpeed = rbSpeed.normalized * maxSpeed;
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.Lerp(rbSpeed, rbTargetSpeed, smoothSpeedCoef);
        }
        //Debug.Log("Sticks (movH, movV, vel): " + new Vector3(movJoystick.Horizontal, movJoystick.Vertical, velJoystick.Vertical)
        //            + " || Vel:" + this.gameObject.GetComponent<Rigidbody>().velocity.magnitude);

        SetBoidTargetPos();
        if(!isSceneLobby) PlayerLookAnimation();

        if (lobbyAnimationRun) LobbyAnimation();
    }

    #endregion

    #region Custom Methods

    public void LobbyAnimation()
    {
        if (this.gameObject.tag == "Player")
        {
            if (!arrived && !looked)
            {
                //Smooth rotation to look at exitPoint
                var targetRotation = Quaternion.LookRotation(exitPoint.position - this.transform.position);
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, rotationSpeedLerp);
                if (this.transform.rotation == targetRotation) looked = true;
            }
            else if (!arrived)
            {
                transform.position = Vector3.MoveTowards(this.transform.position, exitPoint.position, movementSpeed * Time.deltaTime);
                if (this.transform.position != exitPoint.position) arrived = true;
            }
            if (arrived) this.transform.position += Vector3.forward * launchSpeed * Time.deltaTime;
        }
    }

    private void PlayerLookAnimation()
    {
        Vector3 movement = this.gameObject.GetComponent<Rigidbody>().velocity;
        if (movement != Vector3.zero)
        {
            destinationRotation = Quaternion.LookRotation(movement, Vector3.up);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, destinationRotation, animationSmoothSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            if (this.gameObject.name.StartsWith("Red"))
            {
                ScoreSystemManager.TotalRedTime = timer.GetTotalTime();
                ScoreSystemManager.TotalOxygenBalls = oxygenManagerUI.NumCargo;
                Debug.Log($"Time: {ScoreSystemManager.TotalRedTime} + oxygens: {ScoreSystemManager.TotalOxygenBalls}");
            }
            else if (this.gameObject.name.StartsWith("White"))
            {
                ScoreSystemManager.TotalWhiteTime = timer.GetTotalTime();
                ScoreSystemManager.TotalAmmo = oxygenManagerUI.NumCargo;
                Debug.Log($"Time: {ScoreSystemManager.TotalWhiteTime} + ammo: {ScoreSystemManager.TotalAmmo}");
            }
            else if (this.gameObject.name.StartsWith("Platalet"))
            {
                ScoreSystemManager.TotalPlataletTime = timer.GetTotalTime();
                ScoreSystemManager.NumPlatalets = oxygenManagerUI.NumCargo;
                Debug.Log($"Time: {ScoreSystemManager.TotalPlataletTime} + platalets: {ScoreSystemManager.NumPlatalets}");
            }
        }
        if(other.tag == "Data")
        {
            //Show them in canvas
            mapData.text = other.name;
            //Debug.Log(mapData.text);
            //Remove the _0
            if (other.name.Contains("_")) mapData.text = other.name.Substring(0, other.name.Length - 2);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "ForceIndicator")
        {
            insideMapForce = true;
            mapForce = other.GetComponent<ForceIndicatorScript>().GetForceDirection();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "ForceIndicator")
        {
            insideMapForce = false;
        }
    }

    //                      Oxygen funcs
    /*************************************************************/
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.StartsWith("Map"))
        {
            crashSound.Play();
            oxygenManagerUI.NumCargo = --numOxygen;
            if(this.gameObject.name.StartsWith("Red")) oxygenController.OnCollision();

            //Calculate points depending on the collision force
            ScoreSystemManager.OxygenFall(collision.impulse.magnitude, timer.GetTotalTime());
        }
    }

    private void CheckNumOxygen()
    {
        if (numOxygen > 0) return;

        ScoreSystemManager.ResetRedVariables();
        transitor.LoadNextScene(onOxygenExhaustedScene);
    }

    //                      Boid funcs
    /*************************************************************/
    private void SetBoidTargetPos()
    {
        if(insideMapForce)
        {
            boidTarget.transform.position = transform.position + mapForce.normalized * initialBoidTargetLocalPos.magnitude;
        }
        else
        {
            boidTarget.transform.localPosition = initialBoidTargetLocalPos;
        }
        Debug.DrawLine(playerVisuals.transform.position, boidTarget.transform.position);
    }

    #endregion
}