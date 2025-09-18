using Cinemachine;
using UnityEngine;

public class CinemachineSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera cam;

    public CinemachineFreeLook freeCam;

    public bool isFree = false;

    private void Start()
    {
        cam.Priority = 10;
        freeCam.Priority = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isFree = !isFree;
            if (isFree)
            {
                PlayerController.Instance.isStop = true;
                freeCam.Priority = 20;
                cam.Priority = 0;
            }
            else
            {
                PlayerController.Instance.isStop = false;
                cam.Priority = 20;
                freeCam.Priority = 0;
            }
        }
    }
}
