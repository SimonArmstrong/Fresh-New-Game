using UnityEngine;
using System.Collections;

public class PointOrb : MonoBehaviour {
    public bool canSpawn = true;

    void OnTriggerEnter(Collider col) {
        Debug.Log("HIT");
        if (col.tag == "dropZone")
        {
            canSpawn = false;
        }
        gameObject.SetActive(false);
    }
    void Start()
    {
        gameObject.SetActive(false);
    }
}
