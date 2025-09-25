using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject[] bulletPrefab;
    private int bullectIndex = 0;
    public Transform firePoint;
    Camera cam;

    public float coolTime = 0.5f;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && coolTime <= 0)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            bullectIndex += 1;
        }

        coolTime -= Time.deltaTime;
    }

    void Shoot()
    {
        coolTime = 0.5f;
        Ray r = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint;
        targetPoint = r.GetPoint(50f);
        Vector3 direction = (targetPoint - firePoint.position).normalized;

        int bullectIndex_ = bullectIndex % 2;

        GameObject proj = Instantiate(bulletPrefab[bullectIndex_], firePoint.position, Quaternion.LookRotation(direction));
    }
}
