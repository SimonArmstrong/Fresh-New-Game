using UnityEngine;
using System.Collections;

public class BasicFollow : MonoBehaviour {
    public GameObject target;
    public Vector3 offset;

    // Update is called once per frame
    void Update () {
        Vector3 targetPos = new Vector3(target.transform.position.x + offset.x, target.transform.position.y + offset.y, target.transform.position.z + offset.z);
        transform.position = targetPos;
	}
}
