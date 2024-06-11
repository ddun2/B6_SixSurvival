using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawn : MonoBehaviour
{
    public GameObject resourcePrefab;
    public float respawnTime = 5f;
    public Vector3 spawnPosition;

    void Start()
    {
        ReSpawn();
    }

    public void ReSpawn()
    {
        GameObject resource = Instantiate(resourcePrefab, spawnPosition, Quaternion.identity);
        Resource resourceScript = resource.GetComponent<Resource>();
        resourceScript.spawn = this;
    }

    public IEnumerator SpawnCoroutine(Vector3 position)
    {
        yield return new WaitForSeconds(respawnTime);

        ReSpawn();
    }
}
