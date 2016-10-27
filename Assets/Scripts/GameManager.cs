using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public static int PLAYER_COUNT;
    public static List<GameObject> players = new List<GameObject>();

    public static void LoadMain() {
        if (players.Count == 2) {
            players[0].GetComponent<Player>().cam.rect = new Rect(0, 0.5f, 1, 1);
            players[1].GetComponent<Player>().cam.rect = new Rect(0, -0.5f, 1, 1);
        }
        else if(players.Count == 3) {
            players[0].GetComponent<Player>().cam.rect = new Rect(0, 0.5f, 1, 1);
            players[1].GetComponent<Player>().cam.rect = new Rect(0, -0.5f, 1, 1);
            players[2].GetComponent<Player>().cam.rect = new Rect(-0.5f, -0.5f, 1, 1);
        }
        else if (players.Count == 4)
        {
            players[0].GetComponent<Player>().cam.rect = new Rect(-.5f,   0.5f, 1, 1);
            players[1].GetComponent<Player>().cam.rect = new Rect( .5f,    .5f, 1, 1);
            players[2].GetComponent<Player>().cam.rect = new Rect(-0.5f, -0.5f, 1, 1);
            players[3].GetComponent<Player>().cam.rect = new Rect( 0.5f, -0.5f, 1, 1);
        }
        for (int i = 0; i < players.Count; i++) {
            players[i].GetComponent<Player>().cam.rect = new Rect(0, 0, 1, 1);
            Instantiate(players[i], new Vector3(0, 10, 5 * i), Quaternion.identity);
        }

    }
}
