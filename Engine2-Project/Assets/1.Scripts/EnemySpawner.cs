using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EneyPrefab;
    public float spawninterval = 3f;
    public float spawnRange = 5f;
    private float timer = 5f;

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawninterval)
        {
            Vector3 spawnPos = new Vector3(
                transform.position.x + Random.Range(-spawnRange, spawnRange),
                transform.position.y,
                transform.position.z + Random.Range(-spawnRange, spawnRange)
                );

            Instantiate(EneyPrefab, spawnPos, Quaternion.identity);
            timer = 0f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnRange * 2, 0.1f, spawnRange * 2));
    }



}
