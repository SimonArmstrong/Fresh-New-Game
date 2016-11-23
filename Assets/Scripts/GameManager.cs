using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public enum GameMode {
        TimeAttack,
        FirstTo,
        Oddball
    }

    public static int PLAYER_COUNT;
    public static GameMode GAME_MODE = GameMode.FirstTo;
    public static float gameSpeed = 1;
    public static int currentSpawned;
    public static List<int> playerIDS = new List<int>();
    public static List<GameObject> players = new List<GameObject>();
    public GameObject defaultLight;
    public GameObject playerHUD;
    public Text timerText;
    public Image timerImage;
    public float gameOverScreenTime = 5;
    public static float timeLeft = 60;
    private int tempHighScore = 0;
    public int scoreToWin = 10;
    public static bool gameEnd = false;

    void Start() {
        PLAYER_COUNT = 0;
                
        for (int i = 0; i < players.Count; i++) {
            players[i].GetComponent<Player>().id = i;
            if (players[i] != null) {
                if (i < playerIDS.Count) {
                    players[i].GetComponent<Player>().inputID = playerIDS[i];
                }
                players[i].GetComponent<Player>().HUD = Instantiate(playerHUD, new Vector3(0, 0, 5 * i), Quaternion.identity) as GameObject;
               
                players[i] = Instantiate(players[i], new Vector3(0, 0, 5 * i), Quaternion.identity) as GameObject;
            }
        }
        //Instantiate(defaultLight, Vector3.zero, Quaternion.LookRotation(new Vector3(45, -50, 45)));
    }

    void Update()
    {
        if (GAME_MODE == GameMode.TimeAttack)
        {
            timeLeft -= Time.deltaTime * GameManager.gameSpeed;
            timerText.text = ((int)timeLeft + 1).ToString();
            if (timeLeft < 0)
            {
                timerText.text = 0.ToString();
                GameOver();
                gameSpeed = 0;
            }
        }
        else if (GAME_MODE == GameMode.Oddball) { }
        else if (GAME_MODE == GameMode.FirstTo) {
            timerImage.enabled = false;
            timerText.enabled = false;
            for (int i = 0; i < players.Count; i++) {
                if (players[i].GetComponent<Player>().score == scoreToWin) {
                    gameSpeed = Mathf.Lerp(gameSpeed, .05f, Time.deltaTime * 2);
                    GameOver();
                }
            }
        }
    }

    public void GameOver()
    {
        gameEnd = true;
        for (int i = 0; i < players.Count; i++){
            if(tempHighScore < players[i].GetComponent<Player>().score){
                tempHighScore = players[i].GetComponent<Player>().score;
            }
            if (players[i].GetComponent<Player>().score == tempHighScore) {
                players[i].GetComponent<Player>().win = true;
            }
            else{
                players[i].GetComponent<Player>().win = false;
            }
        }
        gameOverScreenTime -= Time.deltaTime;
        if (gameOverScreenTime <= 0)
        {
            OnSceneUnload();
            SceneManager.LoadScene(0);
            SceneManager.UnloadScene(1);
        }
    }
    void OnSceneUnload()
    {
        for (int i = 0; i < players.Count; i++) {
            players[i].GetComponent<Player>().score = 0;
            Destroy(players[i].GetComponent<Player>().cam);
        }
        gameEnd = false;
        gameSpeed = 1;
        timeLeft = 60;
        gameOverScreenTime = 5;
        tempHighScore = 0;
        timerText = null;
        playerHUD = null;
        players.Clear();
        playerIDS.Clear();
    }
}
