using UnityEngine;
using System.Collections;

public class PointOrb : MonoBehaviour {
    public bool canSpawn = true;

    void OnTriggerEnter(Collider col) {
        if (col.tag != "Player") {
            canSpawn = false;
        }
        gameObject.SetActive(false);
    }
    void Start() {
        //gameObject.SetActive(false);
    }
}
