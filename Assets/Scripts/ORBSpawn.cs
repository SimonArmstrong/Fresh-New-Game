using UnityEngine;
using System.Collections;

public class ORBSpawn : MonoBehaviour {
    public Vector3 pointR;
    public GameObject Orb;
    public GameObject orbInstance;

    public float innerOffset;
    public float outerOffset; 

    public Vector3 GetRandomPointInRadius(float radius) {
        Vector3 point = new Vector3();
        point.x = Random.Range(-100, 100);                          // Between -100 and 100 for float conversion
        point.y = 1;                                                // The height to spawn the orb
        point.z = Random.Range(-100, 100);
        point /= 100;                                               // Get point's values between -1 and 1 with no truncation
        point.Normalize();                                          // Unit Circle
        point *= Random.Range(innerOffset, radius - outerOffset);   // Multiply magnitude between the innerOffset and radius - outerOffset
        Vector3.ClampMagnitude(point, radius);                      // Ensure the spawn point doesn't exceed the bounds of our given radius
        return point;
    }

    // Use this for initialization
    void Start () {

    }

	// Update is called once per frame
	void Update () {
        if (orbInstance == null) {
            Vector3 spawnPosition = GetRandomPointInRadius(40);
            GameObject spawnCheck = new GameObject("Spawn Check", System.Type.GetType("BoxCollider"), System.Type.GetType("CollisionCheck"));
            Instantiate(spawnCheck, spawnPosition, Quaternion.identity);
            orbInstance = Instantiate(Orb, spawnPosition, Quaternion.identity) as GameObject;
        }        
	}
}
