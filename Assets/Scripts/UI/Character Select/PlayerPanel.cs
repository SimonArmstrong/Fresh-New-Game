using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerPanel : MonoBehaviour {
    public bool on;
    public int num;
    public int currentSelectedCharacter;
    public Image image;
    public List<Sprite> sprites = new List<Sprite>();

    public string ownerController;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();        
        currentSelectedCharacter = 0;
	}
	
	// Update is called once per frame
	void Update () {
        //image.sprite = sprites[currentSelectedCharacter];
    }
}
