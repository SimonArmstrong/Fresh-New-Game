using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    public float timeLeft = 300;

    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            GameManager.GameOver();
        }
    }
}
