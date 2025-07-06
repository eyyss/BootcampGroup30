using Unity.Cinemachine;
using UnityEngine;

public class Shop : MonoBehaviour, IInteractable
{
    public GameObject interactInputInfo;
    public CinemachineCamera gameplayCamera, shopCamera;

    public void Enter()
    {
        if (interactInputInfo)
            interactInputInfo.SetActive(true);
    }

    public void Exit()
    {
        if (interactInputInfo)
            interactInputInfo.SetActive(false);
    }

    public void Tick()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactInputInfo.gameObject.SetActive(false);
            gameplayCamera.gameObject.SetActive(false);
            shopCamera.gameObject.SetActive(true);
            PlayerMovement.Singelton.CanMove = false;
        }
    }
}
