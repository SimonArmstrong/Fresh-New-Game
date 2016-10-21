using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Control : MonoBehaviour {

    public Color filteredColor;
    public List<PlayerPanel> playerSelectImages = new List<PlayerPanel>(); 

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 4; i++) {
            if (!playerSelectImages[i].on) {
                playerSelectImages[i].image.color = filteredColor;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
