using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour, ISpawner
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform target;
    [SerializeField] private float spawnTime;
    [SerializeField] private Vector3 spawnOffset = Vector3.zero;
    private Coroutine spawnCoroutine;

    private int prefabCount;
    private void Start()
    {
        prefabCount = prefabs.Length;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetSpawnTime(float time)
    {
        spawnTime = time;
    }

    public void Spawn()
    {
        var createObject = Instantiate(prefabs[Random.Range(0, prefabCount)]); ;
        createObject.transform.position = spawnPoint.position + spawnOffset;
        createObject.GetComponent<EnemyController>().SetTarget(target);
        createObject.GetComponent<EntityStatus>().deathEvnet.AddListener(gameManager.AddScore);

    }
    public void StartSpawn()
    {
        spawnCoroutine = StartCoroutine(CoSpawn());
    }

    public void StopSpawn()
    {
        StopCoroutine(spawnCoroutine);
        spawnCoroutine = null;
    }

    private IEnumerator CoSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            Spawn();
        }
    }
}
