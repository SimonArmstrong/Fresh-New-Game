using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
    public List<GameObject> objectsToSpawn;
    public int maxSpawnAmount = 10;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Vector3 center = transform.position;
        for (int i = 0; i < maxSpawnAmount; i++)
        {
            Vector3 pos = RandomCircle(center, i, gameObject.transform.localScale.x/2);
            Instantiate(objectsToSpawn[objectsToSpawn.Count - 1], pos, Quaternion.identity);
        }
    }

    Vector3 RandomCircle(Vector3 center, int na,  float radius)
    {
        float ang = UnityEngine.Random.Range(0,360);
        radius = Random.Range(0, radius);
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + 0.6f;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }
    

    void FixedUpdate()
    {
        GameManager.currentSpawned = maxSpawnAmount;
    }
}
