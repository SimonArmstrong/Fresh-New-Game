using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Control : MonoBehaviour {

    public Color filteredColor;
    public List<PlayerPanel> playerSelectImages = new List<PlayerPanel>(); 

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
	}

    IEnumerator Flash(PlayerPanel selectImage) {
        yield return null;
    }
}
