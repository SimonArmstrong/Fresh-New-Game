﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public static int PLAYER_COUNT;
    public static int currentSpawned;
    public static List<int> playerIDS = new List<int>();
    public static List<GameObject> players = new List<GameObject>();
    public GameObject defaultLight;
    public GameObject playerHUD;
    public float timeLeft = 300;
    private int tempHighScore = 0;

    void Start() {
        for (int i = 0; i < players.Count; i++) {
            players[i].GetComponent<Player>().id = i;
            if (players[i] != null) {
                if (i < playerIDS.Count) {
                    players[i].GetComponent<Player>().inputID = playerIDS[i];
                }
                players[i].GetComponent<Player>().HUD = Instantiate(playerHUD, new Vector3(0, 2, 5 * i), Quaternion.identity) as GameObject;
               
                players[i] = Instantiate(players[i], new Vector3(0, 1, 5 * i), Quaternion.identity) as GameObject;
            }
        }
        //Instantiate(defaultLight, Vector3.zero, Quaternion.LookRotation(new Vector3(45, -50, 45)));
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        for (int i = 0; i < players.Count; i++){
            if(tempHighScore < players[i].GetComponent<Player>().score){
                //tempHighScore is now the highest score
                tempHighScore = players[i].GetComponent<Player>().score;
            }
            if (players[i].GetComponent<Player>().score == tempHighScore) {
                //player at i wins
                //Debug.Log(tempHighScore);
            }
            else{
                //other players don't win
            }
        }
        Debug.Log(tempHighScore);
    }
}
