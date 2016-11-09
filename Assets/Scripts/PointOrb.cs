using UnityEngine;
using System.Collections;

public class PointOrb : MonoBehaviour {
    public bool canSpawn = true;

    void OnTriggerStay(Collider col) {
        if (col.tag == "Player") {
            //gameObject.transform.position = col.gameObject.GetComponent<Player>().hand.position;
        }
        //gameObject.SetActive(false);
    }
    void Start() {
        //gameObject.SetActive(false);
    }
}
