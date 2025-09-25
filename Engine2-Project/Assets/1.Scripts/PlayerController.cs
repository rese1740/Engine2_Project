using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public float speed = 5f;
    public bool isRunning = false;
    public float jumpPower = 5f;
    public float gravity = -9.81f;
    public bool isStop = false;

    public CinemachineVirtualCamera cam;
    public float rotationSpeed;
    private CinemachinePOV pov;
    public float originalFOV;
    public float runningFOV = 20;
    private CharacterController controller;
    private Vector3 velocity;
    public bool isGrounded;

    private void Start()
    {
        Instance = this;
        controller = GetComponent<CharacterController>();
        pov = cam.GetCinemachineComponent<CinemachinePOV>();
    }

    private void Update()
    {
        if (isStop)
            return;

        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 8f;
            cam.m_Lens.FieldOfView = Mathf.Lerp(cam.m_Lens.FieldOfView, runningFOV, Time.deltaTime * 1.5f);
        }
        else
        {
            speed = 5f;
            cam.m_Lens.FieldOfView = Mathf.Lerp(cam.m_Lens.FieldOfView, originalFOV, Time.deltaTime * 1.5f);
        }

        #region 시네머신
        Vector3 camFoward = cam.transform.forward;
        camFoward.y = 0;
        camFoward.Normalize();

        Vector3 camRight = cam.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 move = (camFoward * z + camRight * x).normalized;
        controller.Move(move * speed * Time.deltaTime);

        float cameraYaw = pov.m_HorizontalAxis.Value;
        Quaternion targetRot = Quaternion.Euler(0f, cameraYaw, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        #endregion

        #region 점프
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpPower;
        }
        #endregion
    }

}
