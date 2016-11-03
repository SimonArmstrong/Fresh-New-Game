using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Control : MonoBehaviour {

    public struct Controller {
        public int joyNum;
    }

    public Color filteredColor;
    public List<PlayerPanel> playerSelectImages = new List<PlayerPanel>();
    public GameObject characterSelectScreen;
    public GameObject mainMenuScreen;
    public GameObject readyScreen;
    public GameObject defaultPlayer;
    public GameObject defaultLight;
    public int playerCount = 1;

    public List<GameObject> menuContext = new List<GameObject>();
    public GameObject highlightSprite;
    public int selectedIndex;

    public int controllerCount;

    public List<Controller> controllers = new List<Controller>();

    // 350 is joy1, 20 nums between each joy // -DUALSHOCK-
    // 350 is joy1, 20 nums between each joy // -DIRECT X -

    //flags
    private bool   swapToCharacterSelect = false;
    private bool   swapToMainMenu = false;
    private bool   isPlaystation = false;
    private bool   nextPress = true;
    private bool   canReady = false;
    public string contextOrientation = "Vertical";

    void Start() {
        selectedIndex = 0;
        controllerCount = 0;
    }

    void Update () {
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
            if (Input.GetKeyDown((KeyCode)(350))) {
                swapToCharacterSelect = true;
                swapToMainMenu = false;
            }
            if (swapToCharacterSelect)
            {
                for (int i = controllerCount; i < 4; i++)
                {
                    if (Input.GetKeyDown((KeyCode)(350 + (controllerCount * 20) + 0)))
                    {
                        playerSelectImages[controllerCount].on = true;
                        controllerCount++;
                    }
                }
                if (Input.GetKeyDown((KeyCode)357))
                {
                    SceneManager.LoadScene(1);

                    for (int j = 0; j < controllerCount; j++)
                    {
                        GameManager.players.Add(defaultPlayer);
                        //GameManager.players[i].GetComponent<Player>().controls = new Player.Controller();
                        //GameManager.players[i].GetComponent<Player>().controls.dash
                    }
                }
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
                    GameManager.players.Add(defaultPlayer);
                    //GameManager.players[i].GetComponent<Player>().controls = new Player.Controller();
                    //GameManager.players[i].GetComponent<Player>().controls.dash
                }
            }
        }

        if (controllerCount > 1) {
            canReady = true;
        }

        if (canReady) {
            readyScreen.SetActive(true);
        }

        if (selectedIndex > menuContext.Count) {
            selectedIndex = 0;
        }

        if (selectedIndex < 0) {
            selectedIndex = menuContext.Count;
        }


        if (swapToCharacterSelect) {
            characterSelectScreen.transform.position = Vector3.Lerp(characterSelectScreen.transform.position, new Vector3(0, 0, 100), Time.deltaTime * 10);
            mainMenuScreen.transform.position = Vector3.Lerp(mainMenuScreen.transform.position, new Vector3(800, 0, 100), Time.deltaTime * 10);
        }
        if (swapToMainMenu) {
            characterSelectScreen.transform.position = Vector3.Lerp(characterSelectScreen.transform.position, new Vector3(0, 0, 100), Time.deltaTime * 10);
            mainMenuScreen.transform.position = Vector3.Lerp(mainMenuScreen.transform.position, new Vector3(800, 0, 100), Time.deltaTime * 10);
        }
            
    }

    IEnumerator Flash(PlayerPanel selectImage) {
        yield return null;
    }
}
