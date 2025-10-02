using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState { Idle, Trace, Attack, Runaway }
    public EnemyState state = EnemyState.Idle;


    public float moveSpeed = 2f;
    public float traceRange = 3f;
    public float attackRange = 3f;
    public float attackCooldown = 1.5f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float lastAttackTime;

    public int maxHP = 5;
    public int currentHP;
    private bool run = true;
    private Transform player;
    public Transform headAnchor; 
    private EnemyHealthBarUI healthBarUI;

    public float minScale = 0.5f;
    public float maxScale = 1.0f;
    public float minDistance = 5f;
    public float maxDistance = 20f;

    private void Start()
    {
        currentHP = maxHP;
        GameObject canvas = GameObject.Find("Canvas"); 
        GameObject healthBarPrefab = Resources.Load<GameObject>("EnemyHP"); 
        GameObject healthBarObj = Instantiate(healthBarPrefab, canvas.transform);

        // 연결
        healthBarUI = healthBarObj.GetComponent<EnemyHealthBarUI>();
        healthBarUI.target = headAnchor;
        healthBarUI.SetMaxHealth(maxHP);
        healthBarUI.SetHealth(currentHP);


        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastAttackTime = -attackCooldown;
    }

    private void Update()
    {
        if (player == null) return;
        float dist = Vector3.Distance(player.position, transform.position);

        if(currentHP <= maxHP / 5 && run)
        {
            state = EnemyState.Runaway;
        }


        #region State

        switch (state)
        {
            case EnemyState.Idle:
                if (dist < traceRange)
                    state = EnemyState.Trace;
                break;

            case EnemyState.Attack:
                if (dist > attackRange)
                    state = EnemyState.Trace;
                else
                    AttackPlayer();
                break;

            case EnemyState.Trace:
                if (dist < attackRange)
                    state = EnemyState.Attack;
                else if (dist > traceRange)
                    state = EnemyState.Idle;
                else
                    TracePlayer();
                break;
            case EnemyState.Runaway:
                Runaway();

                break;

        }
        #endregion
    }

    void TracePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(player.position);
    }

    void AttackPlayer()
    {
        if(Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            Shoot();
        }
    }

    void Shoot()
    {
        if(bulletPrefab != null && firePoint != null)
        {
            transform.LookAt(player.position);
            GameObject proj = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            EnemyProjectile ep = proj.GetComponent<EnemyProjectile>();
            if(ep != null)
            {
                Vector3 dir = (player.position - transform.position).normalized;
                ep.SetDirection(dir);
            }
        }
    }

    void Runaway()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position -= direction * moveSpeed * Time.deltaTime;
        Invoke("SetIdle",3f);
    }

    void SetIdle()
    {
        state = EnemyState.Idle;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log($"받은 데미지 :{damage} ");
        healthBarUI.SetHealth(currentHP);

        if (currentHP <= 0)
        {
            Destroy(gameObject);
            Debug.Log("사망");
        }
    }
}
