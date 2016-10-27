using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public static int PLAYER_COUNT;
    public static List<GameObject> players = new List<GameObject>();

    public void Start() {

        for (int i = 0; i < players.Count; i++) {
            players[i].GetComponent<Player>().cam.rect = new Rect(0, 0, 1, 1);
            players[i].GetComponent<Player>().id = i;
            Instantiate(players[i], new Vector3(0, 10, 5 * i), Quaternion.identity);
        }
    }
}
