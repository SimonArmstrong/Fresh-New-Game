using UnityEngine;
using System.Collections;

public class CollisionCheck : MonoBehaviour {
    public bool colliding = false;
    void OnCollisionStay(Collision col) {
        colliding = true;
    }
}
