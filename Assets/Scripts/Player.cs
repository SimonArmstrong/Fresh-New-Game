using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player : MonoBehaviour {
    public float speed;
    public float dashSpeed;

    public GameObject shield;
    public List<GameObject> dashWallObject = new List<GameObject>();
    public float shieldGrowSpeed;
    public float wallPlaceDistance;

    public int score;

    private float dashCooldown;
    private float dashDistance;
    private float blockTime;
    private float placementSkin;

    public bool blocking;

    private float moveSpeed;

	// Use this for initialization
	void Start () {
        //if (speed <= 0) speed = 1;
        //if (dashSpeed <= 0) dashSpeed = 10;
        //if (dashDistance <= 0) dashDistance = 5;
        score = 0;
        GetComponent<TrailRenderer>().enabled = false;
        blocking = false;
        placementSkin = wallPlaceDistance;
    }

    public void Dash() {
        dashCooldown -= Time.deltaTime;
        if (Input.GetButtonDown("Jump") && dashCooldown <= 0) {
            AnimationManager.OnBeginDash();
            GetComponent<TrailRenderer>().enabled = true;
            //transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * dashSpeed * speed * Time.deltaTime;
            moveSpeed = moveSpeed * dashSpeed;
            dashCooldown = 0.2f;
            dashDistance = .2f;
        }

        if (dashDistance > 0) {
            dashDistance -= Time.deltaTime;
            placementSkin -= Time.deltaTime;
            // Place walls behind player
            if (placementSkin <= 0) {
                int r = UnityEngine.Random.Range(0, dashWallObject.Count);
                Instantiate(dashWallObject[r],
                    new Vector3(transform.position.x, transform.position.y - 4, transform.position.z - 2),
                    Quaternion.identity);

                placementSkin = wallPlaceDistance;
                GetComponent<TexturePlace>().Place(transform.position, Vector3.down);
            }
        }

        if (dashDistance <= 0) {
            moveSpeed = speed;
        }
        else { AnimationManager.OnDashing(); }
    }
    public void Movement() {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * moveSpeed * Time.deltaTime;
        transform.position += movement;
        //transform.rotation = 
    }
    public void Block() {

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetAxis("Block") < 0) {
            AnimationManager.OnBeginBlock();
            blocking = true;
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetAxis("Block") < 0) {
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


        blocking = true ? false : true;
    }

    // Update is called once per frame
    void FixedUpdate () {
        Block();
        Movement();
        Dash();
	}
}