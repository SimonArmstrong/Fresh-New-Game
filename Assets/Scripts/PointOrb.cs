using UnityEngine;
using System.Collections;

public class PointOrb : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
        }
    }
}
