using UnityEngine;
using System.Collections;

public class SinWave : MonoBehaviour {
    public float max;
    public float min;

    public float speed;
    public float currentPosition;    

	// Update is called once per frame
	void Update () {
        currentPosition += Time.deltaTime;
	    transform.localPosition += new Vector3(Mathf.Sin(currentPosition) * speed, 0, 0);
	}
}
