using UnityEngine;

public class SpawnerSystem : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Spawner[] spawners;
    [SerializeField] private Transform target;

    private void Start()
    {
        var targetEntity = target.GetComponent<EntityStatus>();

        foreach (var spawner in spawners)
        {
            spawner.SetTarget(target);
            spawner.StartSpawn();
            targetEntity.deathEvnet.AddListener(spawner.StopSpawn);
        }
    }

    public void SetSpawnTime(float time)
    {
        foreach (var spawner in spawners)
        {
            spawner.SetSpawnTime(time);
        }
    }
}
