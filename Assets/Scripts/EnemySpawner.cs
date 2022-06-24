using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Input Objects")]
    [SerializeField] GameObject[] SpawnableObjects;

    [Header("Spawn Settings")]
    [SerializeField][Range(0.01f, 4f), InspectorName("Minimum Delay")] float delayMin = 3f;
    [SerializeField][Range(0.01f, 4f), InspectorName("Maximum Delay")] float delayMax = 4f;
    [SerializeField] float deltaDelay = .0001f;
    [Space(10)]
    [SerializeField][InspectorName("Speed Change")][Tooltip("Speed change between physics updates (50/s)")] float deltaVel = 1f;
    // [SerializeField][InspectorName("Speed Change")][Tooltip("Speed increase per physics tick")] float deltaDeltaVel = 0.001f;
    [Space(10)]
    [Header("Hit Settings")]
    [SerializeField][InspectorName("Minimum Hittable X")] float minHittableX;
    [SerializeField][InspectorName("Maximum Hittable X")] float maxHittableX;
    // Private:
    bool CanSpawn = true;
    List<GameObject> spawnedObjects = new List<GameObject>();
    Player player;

    private void Start()
    {
        // Assert correct delay range
        if (delayMax < delayMin)
        {
            throw new System.Exception("Spawner: Minimum delay is larger than maximum delay");
        }
        // Assert spawnable array isn't empty
        if (SpawnableObjects.Length <= 0)
        {
            throw new System.Exception("Spawner: Spawnable objects are empty");
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        if (delayMin > .3)
        {
            delayMin -= deltaDelay;
            delayMax -= deltaDelay;
        }
        else
        {
            deltaVel += deltaDelay;
            // delayMax = delayMin;
        }
        foreach (GameObject obj in spawnedObjects)
        {
            if (!obj) { continue; }
            obj.transform.position += obj.transform.TransformDirection(Vector2.right * deltaVel * Time.deltaTime * 10);
            if (obj.transform.position.x > 5f)
            {
                if(obj.transform.CompareTag("Target")){
                    player.RemoveScore(10);
                }
                Destroy(obj);
            }
        }

        if (CanSpawn) StartCoroutine(Delay(Random.Range(delayMin, delayMax)));
    }

    private IEnumerator Delay(float delay)
    {
        CanSpawn = false;
        _Spawn(__PickRandom(SpawnableObjects.Length));
        yield return new WaitForSeconds(delay);
        CanSpawn = true;
    }

    private void _Spawn(int index)
    {
        GameObject spawnedObj = Instantiate(SpawnableObjects[index], transform.position, transform.rotation);
        spawnedObjects.Add(spawnedObj);
    }

    private int __PickRandom(int size)
    {
        return Random.Range(0, size);
    }

#nullable enable
    public GameObject? Hit()
    {
        foreach (GameObject hittable in spawnedObjects)
        {
            if (!hittable) { continue; }
            if (Enumerable.Range((int)minHittableX, (int)maxHittableX).Contains((int)hittable.transform.position.x))
            {
                return hittable;
            }
        }
        return null;
    }
#nullable disable
}
