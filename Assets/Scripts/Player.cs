using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Player : MonoBehaviour
{
    [System.Serializable]
    public struct Controller
    {
        public string dash;
        public string block;
        public string pause;
        public string moveX;
        public string moveY;
        public string rotX;
        public string rotY;
    }

    [System.Serializable]
    public struct ArmourSet
    {
        public GameObject head;
        public GameObject lShoulder;
        public GameObject rShoulder;
        public GameObject torso;
        public GameObject lArm;
        public GameObject rArm;
        public GameObject lKnee;
        public GameObject rKnee;
    }
    public ArmourSet armour;
    public Transform hand;

    public Transform head;
    public Transform lShoulder;
    public Transform rShoulder;
    public Transform torso;
    public Transform lArm;
    public Transform rArm;
    public Transform lKnee;
    public Transform rKnee;

    public float speed;
    public float dashSpeed;
    public float dashCooldown;

    public float stunTime;
    private float stunTimer;
    public float blockTime;
    private float blockTimer;

    public GameObject targetPointer;

    public Camera cam;
    public GameObject shield;
    public GameObject camFollowPoint;
    public GameObject mesh;
    public BoxCollider dashCollision;
    public Animator animator;
    public Transform wallSpawnPosition;
    public List<GameObject> dashWallObject = new List<GameObject>();
    public float shieldGrowSpeed;
    public float scaleDashCollision;
    public float wallPlaceDistance;
    public Controller controls;
    public GameObject HUD;
    public Image dashIcon;
    public Text dashTmerText;
    public Image winImage;
    public Image loseImage;
    public GameObject dashFeedback;
    public GameObject scoreFeedback;

    public int score;
    public int currentHeld;

    private float dashCooldownTimer;
    private float dashDistance;
    private float placementSkin;

    public bool blocking;
    public bool dashing;
    public bool stunned;
    public bool holdingOrb;
    public bool screenMode;

    private float moveSpeed;
    private Collider radialDetection;
    private bool nearWall;

    public ParticleSystem collisionParticle;

    public int id;
    public int inputID;

    public string joystickName;

    public bool dashMode = false;
    private GameObject heldOrb;

    public bool win = false;

    private void OnTriggerStay(Collider col)
    {
        if (!screenMode)
        {
            if (col.tag != "Player" && col.tag != "dropZone" && col.tag != "pointOrb")
            {
                nearWall = true;
                moveSpeed = speed;
            }
            //increments a players score when you drop off orbs
            if (col.tag == "dropZone")
            {
                if (heldOrb != null)
                {
                    scoreFeedback.SetActive(true);
                    Destroy(heldOrb);
                    heldOrb = null;
                    score++;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!screenMode)
        {
            //when the player picks up the orb
            if (col.tag == "pointOrb")
            {
                heldOrb = col.gameObject;
                col.enabled = false;
            }
            //where the player recieves the effect of the powerup
            if (col.tag == "powerup")
            {
                Destroy(col.gameObject);
                //powerupy stuff;
            }
            if (col.tag != "Player" && dashing && col.tag != "pointOrb")
            {
                collisionParticle.Play();
            }

            if (col.tag == "Player")
            {
                if (col.gameObject.GetComponent<Player>().inputID != inputID && col.gameObject.GetComponent<Player>().dashing && !blocking)
                {
                    stunTimer = stunTime;
                    moveSpeed = 0;
                    AnimationManager.OnGetStunned(gameObject);
                    if (heldOrb != null)
                    {
                        col.GetComponent<Player>().heldOrb = heldOrb;
                        if (heldOrb.GetComponent<BoxCollider>() != null)
                            heldOrb.GetComponent<BoxCollider>().enabled = true;
                        heldOrb = null;
                    }
                }
                else if (col.gameObject.GetComponent<Player>().inputID != inputID && col.gameObject.GetComponent<Player>().dashing && blocking)
                {
                    dashDistance = 0;
                }
            }
        }
        
    }

    void LoadArmour()
    {
        if (armour.head != null) (armour.head = Instantiate(armour.head, head.position, Quaternion.identity) as GameObject).transform.SetParent(head);
        if (armour.lShoulder != null) (armour.lShoulder = Instantiate(armour.lShoulder, lShoulder.position, Quaternion.identity) as GameObject).transform.SetParent(lShoulder);
        if (armour.rShoulder != null) (armour.rShoulder = Instantiate(armour.rShoulder, rShoulder.position, Quaternion.identity) as GameObject).transform.SetParent(rShoulder);
        if (armour.torso != null) (armour.torso = Instantiate(armour.torso, torso.position, Quaternion.identity) as GameObject).transform.SetParent(torso);
        if (armour.lArm != null) (armour.lArm = Instantiate(armour.lArm, lArm.position, Quaternion.identity) as GameObject).transform.SetParent(lArm);
        if (armour.rArm != null) (armour.rArm = Instantiate(armour.rArm, rArm.position, Quaternion.identity) as GameObject).transform.SetParent(rArm);
        if (armour.lKnee != null) (armour.lKnee = Instantiate(armour.lKnee, lKnee.position, Quaternion.identity) as GameObject).transform.SetParent(lKnee);
        if (armour.rKnee != null) (armour.rKnee = Instantiate(armour.rKnee, rKnee.position, Quaternion.identity) as GameObject).transform.SetParent(rKnee);

    }

    // Use this for initialization
    void Start()
    {
        //if (speed <= 0) speed = 1;
        //if (dashSpeed <= 0) dashSpeed = 10;
        //if (dashDistance <= 0) dashDistance = 5;
        score = 0;
        blockTimer = blockTime;
        GetComponent<TrailRenderer>().enabled = false;
        dashCollision = GetComponent<BoxCollider>();
        blocking = false;
        placementSkin = wallPlaceDistance;
        holdingOrb = false;
        joystickName = "joystick " + (inputID + 1);
        AnimationManager.OnBeginIdle(gameObject);
        if (!screenMode)
        {
            cam.GetComponent<MouseAimCamera>().target = gameObject;

            if (GameManager.players.Count == 1)
            {
                if (id == 0) cam.rect = new Rect(0, 0, 1, 1);
            }
            else if (GameManager.players.Count == 2)
            {
                if (id == 1) cam.rect = new Rect(0, 0.5f, 1, 1);
                else if (id == 0) cam.rect = new Rect(0, -0.5f, 1, 1);
            }
            else if (GameManager.players.Count == 3)
            {
                if (id == 0) cam.rect = new Rect(0, 0.5f, 1, 1);
                else if (id == 1) cam.rect = new Rect(0.5f, -0.5f, 1, 1);
                else if (id == 2) cam.rect = new Rect(-0.5f, -0.5f, 1, 1);
            }
            else if (GameManager.players.Count == 4)
            {
                if (id == 0) cam.rect = new Rect(-.5f, 0.5f, 1, 1);
                else if (id == 1) cam.rect = new Rect(.5f, .5f, 1, 1);
                else if (id == 2) cam.rect = new Rect(-0.5f, -0.5f, 1, 1);
                else if (id == 3) cam.rect = new Rect(0.5f, -0.5f, 1, 1);
            }

            Instantiate(cam, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z - 8), Quaternion.identity);
            controls.moveX = "Horizontal" + inputID;
            controls.moveY = "Vertical" + inputID;
            controls.block = "Block" + inputID;
            controls.dash = "Dash" + inputID;
            //stunTimer = stunTime
            HUD.GetComponent<Canvas>().worldCamera = Camera.allCameras[id];
            HUD.GetComponent<Canvas>().planeDistance = .5f;
            //HUD.transform.localScale = new Vector2(cam.pixelWidth, cam.pixelHeight); 
            Camera.allCameras[id].GetComponent<MouseAimCamera>().scoreText = HUD.GetComponentsInChildren<Text>()[1];
            winImage = HUD.GetComponentsInChildren<Image>()[2];
            loseImage = HUD.GetComponentsInChildren<Image>()[3];
            dashIcon = HUD.GetComponentsInChildren<Image>()[1];
            dashTmerText = dashIcon.gameObject.GetComponentsInChildren<Text>()[0];
        }
        LoadArmour();
    }

    public void Dash()
    {
        dashCooldownTimer -= Time.deltaTime * GameManager.gameSpeed;
        if (stunTimer <= 0)
        {
            if (Input.GetButtonDown(controls.dash) && dashCooldownTimer <= 0 && !nearWall || Input.GetButtonDown(controls.dash) && dashCooldownTimer <= 0 && !nearWall)
            {
                AnimationManager.OnBeginDash(gameObject);
                GetComponent<TrailRenderer>().enabled = true;
                moveSpeed = moveSpeed * dashSpeed;
                dashCooldownTimer = dashCooldown;
                dashDistance = .2f;
                dashing = true;
            }

            if (dashDistance > 0)
            {
                dashDistance -= Time.deltaTime * GameManager.gameSpeed;
                if (dashMode)
                {
                    placementSkin -= Time.deltaTime * GameManager.gameSpeed;
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
                //scaleDashCollision = 1.5f;
            }
            stunTimer = 0;

            if (dashDistance <= 0)
            {
                moveSpeed = speed;
                dashing = false;
                scaleDashCollision = 1;
                GetComponent<TrailRenderer>().enabled = false;
                AnimationManager.OnEndDash(gameObject);
            }
            else { AnimationManager.OnDashing(gameObject); }
        }

        if (dashCooldownTimer <= 0)
        {
            dashFeedback.SetActive(true);
        }
        else
        {
            dashFeedback.SetActive(false);
        }
    }
    public void Movement()
    {
        if (stunTimer <= 0)
        {
            Vector3 axisMovement = new Vector3(Input.GetAxis(controls.moveX), 0, Input.GetAxis(controls.moveY)).normalized * moveSpeed * Time.deltaTime * GameManager.gameSpeed;
            axisMovement = Camera.allCameras[id].transform.TransformDirection(axisMovement);

            transform.position += new Vector3(axisMovement.x, 0, axisMovement.z);

            if (axisMovement.magnitude > 0) { AnimationManager.OnRun(gameObject); }
            else { AnimationManager.OnBeginIdle(gameObject); }
            // NOTE: Consider Revising the use of LookAt();
            Vector3 direction = transform.position + axisMovement;
            direction = new Vector3(direction.x, 0, direction.z);
            if (direction.magnitude > .4f) mesh.transform.LookAt(direction);
            stunTimer = 0;
        }
    }
    public void Block()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown(controls.block) && blockTimer >= 1.5f)
        {
            blocking = true;
            AnimationManager.OnBeginBlock(gameObject);
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetButton(controls.block) && blockTimer > 0 && blocking == true)
        {
            moveSpeed = 0;
            blockTimer -= Time.deltaTime * GameManager.gameSpeed;
            shield.SetActive(true);
            shield.transform.localScale = Vector3.Lerp(shield.transform.localScale, new Vector3(.5f, .5f, .5f) * blockTimer, Time.deltaTime * GameManager.gameSpeed * shieldGrowSpeed);
        }
        else
        {
            shield.transform.localScale = Vector3.Lerp(shield.transform.localScale, new Vector3(0, 0, 0), Time.deltaTime * GameManager.gameSpeed * shieldGrowSpeed);

            if (shield.transform.localScale.x <= .2f)
            {
                shield.SetActive(false);
                blocking = false;
                AnimationManager.OnEndBlock(gameObject);
            }
            if (blockTimer < blockTime) blockTimer += Time.deltaTime * GameManager.gameSpeed;
        }
    }

    void OnGUI()
    {
        //GUI.DrawTexture(new Rect(0, 10, 100, 60), orbSprite);
        //GUI.Box(new Rect(50, 10, 60, 60), "X " + score.ToString(), GUIStyle.none);
        //if (dashCooldownTimer > 0) {
        //    GUI.Box(new Rect(Screen.width - 10, 10, (-dashCooldownTimer * 300 / 100) * 20, 20), "", guiStyle);
        //}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!screenMode)
        {
            if (GameManager.gameEnd)
            {
                if (win)
                {
                    winImage.enabled = true;
                    loseImage.enabled = false;
                }
                else
                {
                    winImage.enabled = false;
                    loseImage.enabled = true;

                    stunned = true;
                }
            }

            dashCollision.size = new Vector3(2.51f, 0.97f, 1.3f) * scaleDashCollision;
            stunTimer -= Time.deltaTime * GameManager.gameSpeed;
            if (stunTimer > 0 && !blocking) moveSpeed = speed;

            Dash();
            Block();
            Movement();

            if (nearWall)
            {
                nearWall = false;
            }

            //returns true if your score is greater than 0
            holdingOrb = currentHeld > 0;

            if (dashCooldownTimer > 0)
            {
                dashIcon.color = new Color(1, 1, 1, .3f);
                //dashTmerText.enabled = true;
                dashTmerText.text = ((int)dashCooldownTimer + 1).ToString();
            }
            else
            {
                dashIcon.color = new Color(1, 1, 1, 1);
                //dashTmerText.enabled = false;
                dashTmerText.text = "";
            }

            if (heldOrb != null)
            {
                heldOrb.transform.position = new Vector3(head.position.x, head.position.y + 2, head.position.z);
                targetPointer.transform.LookAt(new Vector3(0, targetPointer.transform.position.y, 0));
            }
            else
            {
                if (GameObject.FindWithTag("pointOrb") != null)
                {
                    Transform orb = GameObject.FindWithTag("pointOrb").transform;
                    Vector3 lookAtTarget = new Vector3(orb.position.x, 0 + targetPointer.transform.position.y, orb.position.z);
                    targetPointer.transform.LookAt(lookAtTarget);
                }
            }

            mesh.GetComponent<Animator>().SetFloat("gameSpeed", GameManager.gameSpeed);
            Camera.allCameras[id].GetComponent<MouseAimCamera>().scoreText.text = score.ToString();
        }
    }
}
