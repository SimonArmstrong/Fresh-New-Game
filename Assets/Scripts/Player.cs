using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float speed;
    public float dashSpeed;
    public float dashDistance;

    public int score;

    public float dashCooldown;
    public float blockTime;

	// Use this for initialization
	void Start () {
        if (speed <= 0) speed = 1;
        if (dashSpeed <= 0) dashSpeed = 10;
        if (dashDistance <= 0) dashDistance = 5;
        score = 0;
        GetComponent<TrailRenderer>().enabled = false;
    }

    public void Dash() {
        GetComponent<TrailRenderer>().enabled = true;
        //transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * dashSpeed * speed * Time.deltaTime;
        speed = speed * 10;

    }

    public void Movement() {
        transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * speed * Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate () {
        Movement();
        dashCooldown -= Time.deltaTime;
        if (Input.GetButtonDown("Jump") && dashCooldown <= 0) {
            Dash();
            dashCooldown = 0.2f;
            dashDistance = .2f;
        }

        if (dashDistance > 0) {
            dashDistance -= Time.deltaTime;
        }

        if (dashDistance <= 0) {
            speed = 5;
        }
	}

}
