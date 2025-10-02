using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage = 1;
    public float speed = 20f;
    public float lifeTime = 2f;
    public Vector3 moveDir; 

    public void SetDirection(Vector3 dir)
    {
        moveDir = dir.normalized;
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.position += moveDir * speed * Time.deltaTime;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¥Í¿Ω2");
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDmaage(damage);
            }
            Destroy(gameObject);
        }
    }
}
