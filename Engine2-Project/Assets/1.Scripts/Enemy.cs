using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyHP = 5f;
    public float moveSpeed = 2f;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(player.position);
    }

    public void TakeDamage(float damage)
    {
        enemyHP -= damage;

        if(enemyHP <= 0)
        {
            Destroy(gameObject);
            Debug.Log("»ç¸Á");
        }
    }
}
