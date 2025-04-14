using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public float timeTillSpawn;
    public GameObject[] prefabList;
    public float minTime = 1.0f;
    public float maxTime = 5.0f;

    float currentTime;
    Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        timeTillSpawn = Random.Range(minTime, maxTime);

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        spawnPosition = new Vector3(Random.Range(-3.2f, 3.2f), transform.position.y, 0f);



        if (currentTime > timeTillSpawn)
        {
            Instantiate();
        }

    }

    void Instantiate()
    {
        Instantiate(prefabList[UnityEngine.Random.Range(0, prefabList.Length)], spawnPosition, Quaternion.identity);
        timeTillSpawn = Random.Range(minTime, maxTime);
        currentTime = 0;
    }
}
