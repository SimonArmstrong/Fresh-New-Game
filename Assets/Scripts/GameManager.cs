using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public static int PLAYER_COUNT;
    public static List<GameObject> players = new List<GameObject>();
    public GameObject defaultLight;

    public void Awake() {
        for (int i = 0; i < players.Count; i++) {
            players[i].GetComponent<Player>().cam.rect = new Rect(0, 0, 1, 1);
            players[i].GetComponent<Player>().id = i;
            Instantiate(players[i], new Vector3(0, 1, 5 * i), Quaternion.identity);
        }
        Instantiate(defaultLight, Vector3.zero, Quaternion.LookRotation(new Vector3(45, -50, 45)));
    }
}
