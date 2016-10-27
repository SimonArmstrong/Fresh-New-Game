using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlinkOnAwake : MonoBehaviour {
    public float dampening;
    private bool awake;

    void Awake() {
        awake = true;
    }

    void Update() {
        if (awake) {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(1, 1), Time.deltaTime * dampening);
        }
    }
}
