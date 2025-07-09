using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour, IInteractable
{
    public static Shop Singelton;
    public GameObject interactInputInfo;
    public CinemachineCamera gameplayCamera, shopCamera;
    public List<UnitCardData> cardDatas;
    public UnitCard cardPrefab;
    public Transform unitPanel;
    public Button exitButton;
    void Awake()
    {
        Singelton = this;
    }
    private void Start()
    {
        foreach (var item in cardDatas)
        {
            UnitCard unitCard = Instantiate(cardPrefab, unitPanel);
            unitCard.Initialize(item);
        }

        exitButton.onClick.AddListener(ExitShop);
    }
    public void EnterShop()
    {
        interactInputInfo.gameObject.SetActive(false);
        gameplayCamera.gameObject.SetActive(false);
        shopCamera.gameObject.SetActive(true);
        PlayerMovement.Singelton.gameObject.SetActive(false);
    }
    public void ExitShop()
    {
        interactInputInfo.gameObject.SetActive(false);
        gameplayCamera.gameObject.SetActive(true);
        shopCamera.gameObject.SetActive(false);
        PlayerMovement.Singelton.gameObject.SetActive(true);
    }

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
        if (Input.GetKeyDown(KeyCode.E) && !shopCamera.gameObject.activeSelf)
        {
            EnterShop();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && shopCamera.gameObject.activeInHierarchy)
        {
            ExitShop();
        }
    }
}
