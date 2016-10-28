﻿using UnityEngine;
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

    public Camera cam;
    public GameObject shield;
    public GameObject camFollowPoint;
    public Animator animator;
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
        if (col.tag != "Player" && col.tag != "dropZone" && col.tag != "pointOrb") {
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
        if (col.tag != "Player" && dashing && col.tag != "pointOrb") {
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
        AnimationManager.OnBeginIdle(gameObject);
        cam.GetComponent<MouseAimCamera>().target = gameObject;
        Instantiate(cam, new Vector3(transform.position.x, transform.position.y + 8, transform.position.z - 15), Quaternion.FromToRotation(cam.transform.position, transform.position));
        if (GameManager.players.Count == 2)
        {
            GameManager.players[0].GetComponent<Player>().cam.rect = new Rect(0, 0.5f, 1, 1);
            GameManager.players[1].GetComponent<Player>().cam.rect = new Rect(0, -0.5f, 1, 1);
        }
        else if (GameManager.players.Count == 3)
        {
            GameManager.players[0].GetComponent<Player>().cam.rect = new Rect(0, 0.5f, 1, 1);
            GameManager.players[1].GetComponent<Player>().cam.rect = new Rect(0, -0.5f, 1, 1);
            GameManager.players[2].GetComponent<Player>().cam.rect = new Rect(-0.5f, -0.5f, 1, 1);
        }
        else if (GameManager.players.Count == 4)
        {
            GameManager.players[0].GetComponent<Player>().cam.rect = new Rect(-.5f, 0.5f, 1, 1);
            GameManager.players[1].GetComponent<Player>().cam.rect = new Rect(.5f, .5f, 1, 1);
            GameManager.players[2].GetComponent<Player>().cam.rect = new Rect(-0.5f, -0.5f, 1, 1);
            GameManager.players[3].GetComponent<Player>().cam.rect = new Rect(0.5f, -0.5f, 1, 1);
        }


        controls.moveX = "Horizontal" + id;
        controls.moveY = "Vertical" + id;
        controls.block = "Block" + id;
        controls.dash  = "Dash" + id;
    }

    public void Dash() {
        dashCooldownTimer -= Time.deltaTime;
        if (Input.GetButtonDown(controls.dash) && dashCooldownTimer <= 0 && !nearWall || Input.GetButtonDown(controls.dash) && dashCooldownTimer <= 0 && !nearWall) {
            AnimationManager.OnBeginDash(gameObject);
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
        else { AnimationManager.OnDashing(gameObject); }
    }
    public void Movement() {
        Vector3 axisMovement = new Vector3(Input.GetAxis(controls.moveX), 0, Input.GetAxis(controls.moveY)).normalized * moveSpeed * Time.deltaTime;
        axisMovement = Camera.allCameras[id].transform.TransformDirection(axisMovement);

        transform.position += new Vector3(axisMovement.x, 0, axisMovement.z);


        if (axisMovement.magnitude > 0) { AnimationManager.OnRun(gameObject); }
        else { AnimationManager.OnBeginIdle(gameObject); }
        // NOTE: Consider Revising the use of LookAt();
        //Vector3 direction = transform.position + axisMovement;
        //transform.LookAt(direction);
    }
    public void Block() {

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown(controls.block)) {
            AnimationManager.OnBeginBlock(gameObject);
            blocking = true;
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetButton(controls.block)) {
            AnimationManager.OnBlocking(gameObject);
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