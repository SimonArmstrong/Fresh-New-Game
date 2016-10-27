using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Control : MonoBehaviour {

    public Color filteredColor;
    public List<PlayerPanel> playerSelectImages = new List<PlayerPanel>();
    public GameObject characterSelectScreen;
    public GameObject mainMenuScreen;
    public GameObject readyScreen;
    public GameObject defaultPlayer;
    public int playerCount = 1;

    public List<GameObject> menuContext = new List<GameObject>();
    public GameObject highlightSprite;
    public int selectedIndex;

    public int controllerCount;

    // 350 is joy1, 20 nums between each joy // -DUALSHOCK-
    // 350 is joy1, 20 nums between each joy // -DIRECT X -

    //flags
    private bool   swapToCharacterSelect = false;
    private bool   swapToMainMenu = false;
    private bool   isPlaystation = true;
    private bool   nextPress = true;
    private bool   canReady = false;
    public string contextOrientation = "Vertical";

    void Start() {
        selectedIndex = 0;
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
        for (int i = controllerCount; i < 4; i++)
        {
            if (!isPlaystation) //-DIRECT X-
            {
                if (Input.GetKeyDown((KeyCode)(350 + (i * 20) + 7)))
                {
                    swapToCharacterSelect = true;
                    swapToMainMenu = false;
                    controllerCount++;
                    playerSelectImages[i].on = true;
                }
            }
            else                //-DUALSHOCK-
            {
                if (Input.GetKeyDown(KeyCode.Joystick1Button1)) {
                    swapToCharacterSelect = true;
                    swapToMainMenu = false;
                }
                if (Input.GetKeyDown((KeyCode)(350 + (i * 20) + 1))) {
                    controllerCount++;
                    playerSelectImages[i].on = true;
                }
                if (canReady && Input.GetKeyDown((KeyCode)359)) {
                    SceneManager.LoadScene(1);

                    for(int j = 0; j < controllerCount; j++) {
                        GameManager.players.Add(Instantiate(defaultPlayer) as GameObject);
                        GameManager.players[i].GetComponent<Player>().controls = new Player.Controller();
                        //GameManager.players[i].GetComponent<Player>().controls.dash
                    }
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
