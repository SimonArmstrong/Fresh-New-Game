using UnityEngine;
using System.Collections;

public class SimpleLookAt : MonoBehaviour {
    public GameObject target;

    void Start() {
        if (target == null) target = GameObject.FindWithTag("pointOrb");
    }

	// Update is called once per frame
	void Update () {
        if(target != null) transform.LookAt(target.transform);
	}
}
