using UnityEngine;
using System.Collections;

public class ORBSpawn : MonoBehaviour {
    public Vector3 pointR;
    public GameObject Orb;
    public GameObject orbInstance;
    public float spawnDelay = 3f;

    public float innerOffset;
    public float outerOffset; 

    public Vector3 GetRandomPointInRadius(float radius) {
        Vector3 point = new Vector3();
        point.x = Random.Range(-100, 100);                          // Between -100 and 100 for float conversion
        point.z = Random.Range(-100, 100);
        point /= 100;                                               // Get point's values between -1 and 1 with no truncation
        point.Normalize();                                          // Unit Circle
        point *= Random.Range(innerOffset, radius - outerOffset);   // Multiply magnitude between the innerOffset and radius - outerOffset
        Vector3.ClampMagnitude(point, radius);                      // Ensure the spawn point doesn't exceed the bounds of our given radius
        point.y = 1f;                                               // The height to spawn the orb
        return point;
    }

    // Use this for initialization
    void Start () {

    }

	// Update is called once per frame
	void Update () {
        if (orbInstance == null) {
            Vector3 spawnPosition = GetRandomPointInRadius(40);
            GameObject spawnCheck = new GameObject("Spawn Check", System.Type.GetType("CollisionCheck"));
            spawnCheck.AddComponent<BoxCollider>();
            spawnCheck.transform.position = spawnPosition;
            if (!spawnCheck.GetComponent<CollisionCheck>().colliding) {
                Destroy(spawnCheck);
                spawnDelay -= Time.deltaTime;
                if (spawnDelay <= 0) {
                    orbInstance = Instantiate(Orb, spawnPosition, Quaternion.identity) as GameObject;
                }
            }
            else { spawnPosition = GetRandomPointInRadius(40); }
        }        
	}
}