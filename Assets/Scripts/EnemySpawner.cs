using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject Target;
    public float InitialSpeed = 1f;
    public float InitialDifficulty = 3f;
    bool canSpawn = true;

    public void SpawnEnemy()
    {
        Instantiate(Enemy, transform.position, transform.rotation).GetComponent<Enemy>().Setup(InitialSpeed);
    }
    public void SpawnTarget()
    {
        Instantiate(Target, transform).GetComponent<Target>().Setup(InitialSpeed);
    }
    void Start()
    {
        SpawnEnemy();
    }
    void Update()
    {
        if (!canSpawn) return;
        StartCoroutine(DelayedSpawn(1));
    }

    private void FixedUpdate()
    {
        InitialSpeed += 0.0005f;
    }

    IEnumerator DelayedSpawn(float seconds)
    {
        canSpawn = false;
        yield return new WaitForSeconds(seconds);
        float i = Random.Range(0f, 5f);
        if (i <= 1.5f)
        {
            SpawnEnemy();
        }
        else
        {
            SpawnTarget();
        }
        canSpawn = true;
    }
}
