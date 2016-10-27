using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player : MonoBehaviour {
    [System.Serializable]
    public struct Controller {
        public string dash;
        public string block;
        public string pause;
        public string moveX;
        public string moveY;
        public string rotX;
        public string rotY;
    }

    public float speed;
    public float dashSpeed;
    public float dashCooldown;

    public GameObject shield;
    public Transform wallSpawnPosition;
    public List<GameObject> dashWallObject = new List<GameObject>();
    public float shieldGrowSpeed;
    public float wallPlaceDistance;
    public Controller controls;

    public int score;
    public int currentHeld;

    private float dashCooldownTimer;
    private float dashDistance;
    private float blockTime;
    private float placementSkin;

    public bool blocking;
    public bool dashing;
    public bool holdingOrb;

    private float moveSpeed;
    private Collider radialDetection;
    private bool nearWall;

    public ParticleSystem collisionParticle;

    public int id;
    public string joystickName;

    public bool dashMode = false;

    private void OnTriggerStay(Collider col) {
        if (col.tag != "Player" || col.tag != "dropZone") {
            nearWall = true;
            moveSpeed = speed;
        }
        //increments a players score when you drop off orbs
        if (col.tag == "dropZone")
        {
            score += currentHeld;
            currentHeld -= currentHeld;
        }
    }

    private void OnTriggerEnter(Collider col) {
        //when the player picks up the orb
        if (col.tag == "pointOrb")
        {
            Destroy(col.gameObject);
            currentHeld++;
        }
        //where the player recieves the effect of the powerup
        if (col.tag == "powerup")
        {
            Destroy(col.gameObject);
            //powerupy stuff;
        }
        if (col.tag != "Player" && dashing) {
            collisionParticle.Play();
        }
    }

    // Use this for initialization
    void Start () {
        //if (speed <= 0) speed = 1;
        //if (dashSpeed <= 0) dashSpeed = 10;
        //if (dashDistance <= 0) dashDistance = 5;
        score = 0;
        GetComponent<TrailRenderer>().enabled = false;
        blocking = false;
        placementSkin = wallPlaceDistance;
        holdingOrb = false;
        joystickName = "joystick " + (id + 1);
    }

    public void Dash() {
        dashCooldownTimer -= Time.deltaTime;
        if (Input.GetAxis("Jump" + id) < 0 && dashCooldownTimer <= 0 && !nearWall) {
            AnimationManager.OnBeginDash();
            GetComponent<TrailRenderer>().enabled = true; 
            moveSpeed = moveSpeed * dashSpeed;
            dashCooldownTimer = dashCooldown;
            dashDistance = .2f;
            dashing = true;
        }

        if (dashDistance > 0) {
            dashDistance -= Time.deltaTime;
            if (dashMode) {
                placementSkin -= Time.deltaTime;
                // Place walls behind player
                if (placementSkin <= 0)
                {
                    int r = UnityEngine.Random.Range(0, dashWallObject.Count);  // Random integer to access different wall pieces
                                                                                // Instantiate random wall pieces behund the player
                                                                                // Determine the behind direction of the player
                    Instantiate(dashWallObject[r],
                        wallSpawnPosition.position,
                        Quaternion.identity);

                    placementSkin = wallPlaceDistance;
                    GetComponent<TexturePlace>().Place(transform.position, Vector3.down);
                }
            }
            dashing = true;
        }

        if (dashDistance <= 0) {
            moveSpeed = speed;
            dashing = false;
            GetComponent<TrailRenderer>().enabled = false;
        }
        else { AnimationManager.OnDashing(); }
    }
    public void Movement() {
        Vector3 axisMovement = new Vector3(Input.GetAxis("Horizontal" + id), 0, Input.GetAxis("Vertical" + id)).normalized * moveSpeed * Time.deltaTime;
        axisMovement = Camera.main.transform.TransformDirection(axisMovement);

        transform.position += new Vector3(axisMovement.x, 0, axisMovement.z);

        // NOTE: Consider Revising the use of LookAt();
        //Vector3 direction = transform.position + axisMovement;
        //transform.LookAt(direction);
    }
    public void Block() {

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetAxis("Block" + id) > -1) {
        }
        else  {
            AnimationManager.OnBeginBlock();
            blocking = true;
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetAxis("Block" + id) > -1) {
            shield.transform.localScale = Vector3.Lerp(shield.transform.localScale, new Vector3(0, 0, 0), Time.deltaTime * shieldGrowSpeed);
            if (shield.transform.localScale.x <= .2f)
                shield.SetActive(false); blocking = false;
        }
        else {
            AnimationManager.OnBlocking();
            moveSpeed = 0;
            shield.SetActive(true);
            shield.transform.localScale = Vector3.Lerp(shield.transform.localScale, new Vector3(2, 2, 2), Time.deltaTime * shieldGrowSpeed);
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        Dash();
        Block();
        Movement();

        if (!dashMode) {
            if (Input.GetKeyDown(KeyCode.E)) {

            }
        }

        if (nearWall) {
            nearWall = false;
        }

        GameObject hasOrbSprite = gameObject.transform.GetChild(5).gameObject;
        //returns true if your score is greater than 0
        holdingOrb = currentHeld > 0;

        //displays whether or not a player is holding an orb
        if (holdingOrb == true)
        {
            hasOrbSprite.SetActive(true);
        }
        else
        {
            hasOrbSprite.SetActive(false);
        }
    }
}