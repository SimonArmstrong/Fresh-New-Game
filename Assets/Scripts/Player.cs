using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player : MonoBehaviour {
    public float speed;
    public float dashSpeed;
    public float dashCooldown;

    public GameObject shield;
    public Transform wallSpawnPosition;
    public List<GameObject> dashWallObject = new List<GameObject>();
    public float shieldGrowSpeed;
    public float wallPlaceDistance;

    public int score;

    private float dashCooldownTimer;
    private float dashDistance;
    private float blockTime;
    private float placementSkin;

    public bool blocking;
    public bool dashing;

    private float moveSpeed;
    private Collider radialDetection;
    private bool nearWall;

    public ParticleSystem collisionParticle;

    public int id;
    public string joystickName;

    public bool dashMode = false;

    private void OnTriggerStay(Collider col) {
        if (col.tag != "Player") {
            nearWall = true;
            moveSpeed = speed;
        }
    }

    private void OnTriggerEnter(Collider col) {
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

        joystickName = "joystick " + (id + 1);
    }

    public void Dash() {
        dashCooldownTimer -= Time.deltaTime;
        if (Input.GetButtonDown("Jump" + id) && dashCooldownTimer <= 0 && !nearWall) {
            AnimationManager.OnBeginDash();
            GetComponent<TrailRenderer>().enabled = true; 
            moveSpeed = moveSpeed * dashSpeed;
            dashCooldownTimer = dashCooldown;
            dashDistance = .2f;
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
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal" + id), 0, Input.GetAxis("Vertical" + id)).normalized * moveSpeed * Time.deltaTime;
        transform.position += movement;

        // NOTE: Consider Revising the use of LookAt();
        Vector3 direction = transform.position + movement;
        transform.LookAt(direction);
    }
    public void Block() {

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetAxis("Block" + id) < 0) {
            AnimationManager.OnBeginBlock();
            blocking = true;
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetAxis("Block" + id) < 0) {
            AnimationManager.OnBlocking();
            moveSpeed = 0;
            shield.SetActive(true);
            shield.transform.localScale = Vector3.Lerp(shield.transform.localScale, new Vector3(2, 2, 2), Time.deltaTime * shieldGrowSpeed);

        }
        else {
            shield.transform.localScale = Vector3.Lerp(shield.transform.localScale, new Vector3(0, 0, 0), Time.deltaTime * shieldGrowSpeed);
            if (shield.transform.localScale.x <= .2f)
                shield.SetActive(false); blocking = false;
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
	}
}