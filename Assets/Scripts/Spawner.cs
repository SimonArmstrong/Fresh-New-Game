using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
    public GameObject objectToSpawn;
    public List<GameObject> objectsToSpawn;
    public List<GameObject> relocateList;
    public int maxSpawnAmount = 10;


    void Awake() {
    }

    Vector3 RandomCircle(Vector3 center, int na,  float radius)
    {
        //get a random rotation from in the circle and returns it as a float for location
        float ang = UnityEngine.Random.Range(0,360);
        //get a location between 0 and radius
        radius = Random.Range(0, radius);
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + 0.6f;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }
    

    void Update() {

        Vector3 center = transform.position;
        if (maxSpawnAmount > GameManager.currentSpawned)
        {
            Vector3 pos = RandomCircle(center, GameManager.currentSpawned, gameObject.transform.localScale.x / 2);
            objectsToSpawn.Add(Instantiate(objectToSpawn, pos, Quaternion.identity) as GameObject);
            GameManager.currentSpawned++;
        }
        //when we pick up an orb we need it to respawn to the maxSpawnAmount by spawning 1 orb every few seconds
        //consider making the time that it takes the orb to respawn a Random.range(1-5) meaning it can take between 1-5seconds for another orb to respawn
        for (int i = 0; i < objectsToSpawn.Count; i++)
        {
            if (objectsToSpawn[i].GetComponent<PointOrb>().canSpawn)
            {
                objectsToSpawn[i].SetActive(objectsToSpawn[i].GetComponent<PointOrb>().canSpawn);
            }
            //if objects are not active in the hierarchy then we want to add those to a new list then re-instantiate the objects from the relocate list
            else
            {
                for(int j = 0; j < objectsToSpawn.Count; j++)
                {
                    if (!objectsToSpawn[j].activeSelf)
                    {
                        relocateList.Add(objectsToSpawn[i]);
                        objectsToSpawn.RemoveAt(j);
                        Destroy(relocateList[j]);
                    }
                }
            }
        }
    }
}
