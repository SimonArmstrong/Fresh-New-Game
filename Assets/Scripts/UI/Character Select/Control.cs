using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Control : MonoBehaviour {

    public Color filteredColor;
    public List<PlayerPanel> playerSelectImages = new List<PlayerPanel>();
    public GameObject characterSelectScreen;
    public GameObject mainMenuScreen;
    public int playerCount = 1;

    //flags
    private bool swapToCharacterSelect = false;
    private bool swapToMainMenu = false;

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

        for (int i = playerCount; i < 4; i++) {
            if (Input.GetKeyDown("joystick" + i + "button0")) {
                Debug.Log("joystick" + i + "button0");
                playerCount++;
            }
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button7)) {
            swapToCharacterSelect = true;
            swapToMainMenu = true;
        }

        if (swapToCharacterSelect)
            characterSelectScreen.transform.position = Vector3.Lerp(characterSelectScreen.transform.position, new Vector3(0, 0, 100), Time.deltaTime * 10);

        if (swapToMainMenu)
            mainMenuScreen.transform.position = Vector3.Lerp(mainMenuScreen.transform.position, new Vector3(800, 0, 100), Time.deltaTime * 10);
    }

    IEnumerator Flash(PlayerPanel selectImage) {
        yield return null;
    }
}
