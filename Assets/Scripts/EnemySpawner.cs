using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject Target;

    [SerializeField, Tooltip("Starting fish per second")] private float InitialSpawnRate = 1f;
    [SerializeField, Tooltip("Rate change per tick")] private float DeltaSpawnRate = 0.01f;
    private float currentSpawnRate;

    [SerializeField, InspectorName("Speed")] private float ObjectSpeed = 1f;

    private List<GameObject> spawnedObjects = new();
    private int PHYSICS_ENGINE_TPS;

    [SerializeField] private float BOUND = 7f;
    [SerializeField] private Player player;

    [SerializeField, InspectorName("Bad vs good boys"), Range(0f, 100f)] private float BIAS = 50f;

    [SerializeField] private float UpperBound;
    [SerializeField] private float LowerBound;

    private float RateToDelay(float r)
    {
        return PHYSICS_ENGINE_TPS / (r / 20);
    }

    private void Awake()
    {
        PHYSICS_ENGINE_TPS = 50;
    }

    private void Start()
    {
        currentSpawnRate = InitialSpawnRate;
    }

    int i = 0;
    private void FixedUpdate()
    {
        i++;
        // Higher spawn rate each frame
        currentSpawnRate += DeltaSpawnRate;

        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
            {
                if (obj.transform.position.x > BOUND)
                {
                    if (obj.transform.CompareTag("Target"))
                    {
                        player.RemoveScore(5);
                    }
                    Destroy(obj);
                    continue;
                }
                obj.transform.position += transform.TransformDirection(10 * ObjectSpeed * Time.fixedDeltaTime * Vector2.right);
            }
        }

        // Check if enough time has passed since the last spawn

        if (i > RateToDelay(currentSpawnRate))
        {
            Spawn();
            i = 0;
        }
    }

    private void Spawn()
    {
        if (DecideRandomSpawnObject(BIAS))
        {
            GameObject enemy = Instantiate(Enemy, transform.position, transform.rotation);
            spawnedObjects.Add(enemy);
        }
        else
        {
            GameObject target = Instantiate(Target, transform.position, transform.rotation);
            spawnedObjects.Add(target);
        }
    }

    /// <summary>
    /// Returns a random boolean using the provided bias
    /// </summary>
    /// <param name="bias">Value between 0f and 100f, greater value -> greater probability of returning true</param>
    /// <returns>boolean</returns>
    private bool DecideRandomSpawnObject(float bias)
    {
        float i = Random.Range(0f, 100f);
        if (i >= 100f - bias)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public string HitTarget()
    {
        foreach (GameObject go in spawnedObjects)
        {
            if (go != null)
            {
                if (go.transform.position.x <= UpperBound && go.transform.position.x >= LowerBound)
                {
                    if (go.transform.CompareTag("Enemy"))
                    {
                        Destroy(go);
                        return "Enemy";
                    }
                    else if (go.transform.CompareTag("Target"))
                    {
                        Destroy(go);
                        return "Target";
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    continue;
                }
            }
        }
        return "null";
    }
}
