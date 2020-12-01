using System.Collections.Generic;
using UnityEngine;

//TODO: I'm doing this on the assumption that we won't have time to develop buff/debuff items.
public class ItemManager : MonoBehaviour
{
    [Tooltip("Time between spawning items")]
    [Range(1f, 10f)]
    public float spawnTime = 3f;

    [Tooltip("Cannot instantiate if spawn point is overlapped by gameobject in the layers.")]
    public LayerMask doesntSpawnLayer;

    public List<Transform> spawnPoints;

    public List<Item> items;

    private float _timer;

    private void Awake()
    {
        if (spawnPoints == null || spawnPoints.Count == 0)
            Debug.LogError("Missing spawn points.");

        if (items == null || items.Count == 0)
            Debug.LogError("Missing items.");
    }

    private void Update()
    {
        if (CanSpawn())
        {
            SpawnItem();
        }
    }

    //TODO: A wild feature/bug appeared. If all spawn point are overlaped, soon as a item is picked up,
    //      another will spawn at the same point.
    private bool CanSpawn()
    {
        _timer += Time.deltaTime;

        if (_timer >= spawnTime)
        {
            _timer = 0;
            if (HasEmptySpawnPoint())
            {
                return true;
            }
        }

        return false;
    }

    private void SpawnItem()
    {
        // Choose item randomly
        int indexItem = Random.Range(0, items.Count);

        int indexPos;
        // Prevents the spawn of more than one item in position during gameplay
        do
        {
            indexPos = Random.Range(0, spawnPoints.Count);
        }
        while (!IsSpawnPointValid(spawnPoints[indexPos]));

        // Instantiate item in spawn point position
        Instantiate(items[indexItem].gameObject, 
            spawnPoints[indexPos].position, items[indexItem].transform.rotation);
    }
    
    private bool IsSpawnPointValid(Transform point)
    {
        // If there is no item or player overlaping the spawn point
        if (point.gameObject.activeSelf && Physics2D.OverlapPoint(point.position, doesntSpawnLayer) == null)
            return true;

        return false;
    }

    private bool HasEmptySpawnPoint()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (IsSpawnPointValid(spawnPoints[i])) {
                return true;
            }
        }
        return false;
    }
}