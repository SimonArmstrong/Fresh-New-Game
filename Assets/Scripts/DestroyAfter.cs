using UnityEngine;
using System.Collections;

public class DestroyAfter : MonoBehaviour {

    public float lifetime;
    public float riseSpeed;
    public float maxHeight;
	
	// Update is called once per frame
	void Update () {
        if(transform.position.y <= maxHeight)
            transform.Translate(Vector3.up * Time.deltaTime * riseSpeed);

        lifetime -= Time.deltaTime;
        if(lifetime <= 0)
            Destroy(gameObject);
	}
}
