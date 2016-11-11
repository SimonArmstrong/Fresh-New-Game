using UnityEngine;
using System.Collections;

public class PointOrb : MonoBehaviour {
    public bool canSpawn = true;
    public float hoverSpeed = 2;

    void OnTriggerStay(Collider col) {
        if (col.tag == "Player") {
            //gameObject.transform.position = col.gameObject.GetComponent<Player>().hand.position;
        }
        //gameObject.SetActive(false);
    }
    void Start() {
        //gameObject.SetActive(false);
    }
    float rot = 0;
    void Update() {
        rot += Time.deltaTime * hoverSpeed;
        transform.position = new Vector3(transform.position.x, 1 + Mathf.Sin(rot) / 2, transform.position.z);
    }
}
