using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterDB : MonoBehaviour {
    public List<GameObject> characters = new List<GameObject>();
    public List<Transform> spawnPositions = new List<Transform>();
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (!characters[i].GetComponent<Player>().screenMode) characters[i].GetComponent<Player>().screenMode = true;
        }
        for (int i = 0; i < Control1.controllerCount; i++)
        {
            Instantiate(characters[i], spawnPositions[i].position, Quaternion.identity);
        }
    }
	
	// Update is called once per frame
	void Update () {
	}
}
