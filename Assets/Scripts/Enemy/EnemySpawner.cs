using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float enemys;
    [SerializeField] private string[] enemyTags;

    private ObjectPool pool;

    private Vector3 randomPoint;

    private int randomNum;

    private void Awake()
    {
        pool = GameManager.Instance.GetComponent<ObjectPool>();
    }

    private void Start()
    {
        InitializeSpawner();
    }

    public void InitializeSpawner()
    {
        GameObject obj;

        for (int i = 0; i < enemys;)
        {
            randomPoint = transform.position + Random.insideUnitSphere * range;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
            {
                obj = pool.SpawnFromPool(enemyTags[Random.Range(0, 2)]);

                obj.transform.position = hit.position;

                Enemy enemy = obj.GetComponent<Enemy>();
                enemy.InitializeEnemy();

                ++i;
            }
        }
    }
}
