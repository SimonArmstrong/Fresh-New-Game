using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterSelect : MonoBehaviour {
    
    void Start() {
       
    }

    void Update() {
        for (int i = 0; i < 3; i++)
        {
            if (Input.GetButtonDown("Jump" + i))
            {
                GameManager.PLAYER_COUNT++;
                Debug.Log(GameManager.PLAYER_COUNT);
                GameManager.players.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
            }
        }
    }
    
}