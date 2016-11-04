using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
    public GameObject objectToSpawn;
    public List<GameObject> objectsToSpawn;
    public int maxSpawnAmount = 10;


    void Awake() {
        Vector3 center = transform.position;
        for (int i = 0; i < maxSpawnAmount; i++)
        {
            Vector3 pos = RandomCircle(center, GameManager.currentSpawned, gameObject.transform.localScale.x / 2);
            objectsToSpawn.Add(Instantiate(objectToSpawn, pos, Quaternion.identity) as GameObject);
            GameManager.currentSpawned++;
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
    

    void Update() {
        
        for (int i = 0; i < objectsToSpawn.Count; i++)
        {
            if (objectsToSpawn[i].GetComponent<PointOrb>().canSpawn && objectsToSpawn[i] != null)
            {
                objectsToSpawn[i].SetActive(objectsToSpawn[i].GetComponent<PointOrb>().canSpawn);
            }
        }
    }
}
