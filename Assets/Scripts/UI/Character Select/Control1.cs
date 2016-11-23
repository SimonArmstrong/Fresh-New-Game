using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Control1 : MonoBehaviour {

    public struct Controller {
        public int joyNum;
    }

    public bool keyboard;

    public Color filteredColor;
    public Menu mainMenu = new Menu();
    public List<PlayerPanel> playerSelectImages = new List<PlayerPanel>();
    public GameObject characterSelectScreen;
    public GameObject mainMenuScreen;
    public GameObject readyScreen;
    public List<GameObject> defaultPlayer = new List<GameObject>();
    public GameObject defaultLight;
    public int playerCount = 0;
    public List<Transform> camPositions = new List<Transform>();

    public List<GameObject> menuContext = new List<GameObject>();
    public GameObject highlightSprite;
    public int selectedIndex;

    public int controllerCount;

    public List<Controller> controllers = new List<Controller>();

    // 350 is joy1, 20 nums between each joy // -DUALSHOCK-
    // 350 is joy1, 20 nums between each joy // -DIRECT X -

    //flags
    private bool   swapToCharacterSelect    = false;
    private bool   swapToMainMenu           = false;
    private bool   swapToOptions            = false;
    private bool   isPlaystation            = false;
    private bool   nextPress                = true;
    private bool   canReady                 = false;
    private bool   cycleReady               = false;
    private bool[] p_cycleReady             = null;
    public string  contextOrientation       = "Vertical";

    private Vector3 menuStartPosition;

    void Start() {
        menuStartPosition = mainMenuScreen.transform.position;
        selectedIndex = 0;
        for (int j = 0; j < 4; j++) {
            for (int i = 0; i < 4; i++) {
                if (i < Input.GetJoystickNames().Length && j < Input.GetJoystickNames().Length) {
                    if (!ReferenceEquals(Input.GetJoystickNames()[i], Input.GetJoystickNames()[j])) {
                        controllerCount++;
                    }
                }
            }
        }
        controllerCount = Mathf.Clamp(controllerCount, 0, 4);

        mainMenu.elements.Add(new Menu.Item("Play", menuContext[0].transform));
        mainMenu.elements.Add(new Menu.Item("Options", menuContext[1].transform));
        mainMenu.elements.Add(new Menu.Item("Quit", menuContext[2].transform));

        mainMenu.element("Play").execute = Play;
        mainMenu.element("Options").execute = Options;
        mainMenu.element("Quit").execute = Quit;

        if(keyboard)
            p_cycleReady = new bool[1];
        else
            p_cycleReady = new bool[controllerCount];
    }

    void Play() {
        swapToCharacterSelect = true;
        swapToMainMenu = false;
    }
    void Options() {
        swapToCharacterSelect = false;
        swapToOptions = true;
        swapToMainMenu = false;
    }
    void Quit() {
        Application.Quit();
    }

    void Update () {
        if (controllerCount == 0) controllerCount = 1;
        for (int i = 0; i < 4; i++) {
            if (!playerSelectImages[i].on) {
                playerSelectImages[i].image.color = filteredColor;
            }
            else {
                StartCoroutine(Flash(playerSelectImages[i]));
                playerSelectImages[i].image.color = Color.white;
            }
        }

        if (!isPlaystation) //-DIRECT X-
        {
            //if (!keyboard)
            {
                if (!swapToCharacterSelect)
                {
                    if ((int)Input.GetAxis(contextOrientation + 0) != 0 && cycleReady)
                    {
                        selectedIndex -= (int)Input.GetAxis(contextOrientation + 0);
                        cycleReady = false;
                    }
                    if ((int)Input.GetAxis(contextOrientation + 0) == 0)
                    {
                        cycleReady = true;
                    }
                }
                if (Input.GetButtonDown("Cancel" + 0)) {
                    swapToCharacterSelect = false;
                    swapToOptions = false;
                    swapToMainMenu = true;
                }
                for (int i = 0; i < controllerCount; i++)
                {
                    if (Input.GetButtonDown("Submit" + i))
                    {
                        mainMenu.elements[selectedIndex].execute();
                    }
                    if (swapToCharacterSelect)
                    {
                        if (Input.GetButtonDown("Cancel" + i) && playerSelectImages[i].on == true)
                        {
                            playerSelectImages[i].on = false;
                            playerCount--;
                        }

                        if (Input.GetButtonDown("Submit" + i) && playerSelectImages[i].on == false)
                        {
                            playerSelectImages[i].on = true;
                            playerCount++;
                        }

                        if ((int)Input.GetAxis(contextOrientation + i) != 0 && p_cycleReady[i])
                        {
                            playerSelectImages[i].currentSelectedCharacter -= (int)Input.GetAxis(contextOrientation + 0);
                            p_cycleReady[i] = false;
                        }
                        if ((int)Input.GetAxis(contextOrientation + i) == 0)
                        {
                            p_cycleReady[i] = true;
                        }
                    }

                }
            }
            if (Input.GetButtonDown("Pause" + 0) && swapToCharacterSelect && playerCount > 0)
            {
                GameManager.playerIDS.Clear();
                for (int j = 0; j < 4; j++)
                {
                    if (playerSelectImages[j].on)
                    {
                        GameManager.playerIDS.Add(playerSelectImages[j].num);
                    }
                    //defaultPlayer.GetComponent<Player>().id = playerNum[j];
                    //GameManager.players[i].GetComponent<Player>().controls = new Player.Controller();
                    //GameManager.players[i].GetComponent<Player>().controls.dash
                }
                GameManager.players.Clear();
                for (int j = 0; j < playerCount; j++) {
                    defaultPlayer[j].GetComponent<Player>().screenMode = false;
                    GameManager.players.Add(defaultPlayer[j]);
                }

                SceneManager.LoadScene(1);
            }
        }
        else                //-DUALSHOCK-
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button1)) {
                swapToCharacterSelect = true;
                swapToMainMenu = false;
            }
            for (int i = controllerCount; i < 4; i++) {
                if (Input.GetKeyDown((KeyCode)(350 + (i * 20) + 1))) {
                    controllerCount++;
                    playerSelectImages[i].on = true;
                }
            }
            if (canReady && Input.GetKeyDown((KeyCode)359)) { 
                SceneManager.LoadScene(1);
                    
                for(int j = 0; j < controllerCount; j++) {
                    // GameManager.players.Add(playerSelectImages[i].selectedCharacter);
                    GameManager.players.Add(defaultPlayer[j]);
                    //GameManager.players[i].GetComponent<Player>().controls = new Player.Controller();
                    //GameManager.players[i].GetComponent<Player>().controls.dash
                }
            }
        }

        if (controllerCount > 1) {
            //canReady = true;
        }

        if (canReady) {
            readyScreen.SetActive(true);
        }

        if (selectedIndex > menuContext.Count - 1) {
            selectedIndex = 0;
        }

        if (selectedIndex <= -1) {
            selectedIndex = menuContext.Count - 1;
        }        

        if (swapToCharacterSelect) {
            //characterSelectScreen.transform.position = Vector3.Lerp(characterSelectScreen.transform.position, new Vector3(0, 0, 100), Time.deltaTime * 10);
            mainMenuScreen.transform.position = Vector3.Lerp(mainMenuScreen.transform.position, mainMenuScreen.transform.position + Vector3.up * 80, Time.deltaTime * 10);
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, camPositions[2].position, Time.deltaTime * 10);
        }
        if (swapToMainMenu) {
            //characterSelectScreen.transform.position = Vector3.Lerp(characterSelectScreen.transform.position, new Vector3(800, 0, 100), Time.deltaTime * 10);
            mainMenuScreen.transform.position = Vector3.Lerp(mainMenuScreen.transform.position, menuStartPosition, Time.deltaTime * 10);
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, camPositions[0].position, Time.deltaTime * 10);
        }
        if (swapToOptions) {
            mainMenuScreen.transform.position = Vector3.Lerp(mainMenuScreen.transform.position, mainMenuScreen.transform.position + Vector3.up * 80, Time.deltaTime * 10);
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, camPositions[1].position, Time.deltaTime * 10);
        }

        highlightSprite.transform.position = menuContext[selectedIndex].transform.position;
        GameObject.FindObjectOfType<CharacterDB>().controllerCount = controllerCount;
    }



    IEnumerator Flash(PlayerPanel selectImage) {
        yield return null;
    }
}
