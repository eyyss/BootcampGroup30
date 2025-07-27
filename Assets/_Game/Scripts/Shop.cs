using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour, IInteractable
{
    public static Shop Singelton;
    public CinemachineCamera gameplayCamera, shopCamera;
    public GameObject shopCanvas;
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
        //gameplayCamera.gameObject.SetActive(false);
        //shopCamera.gameObject.SetActive(true);
        shopCanvas.SetActive(true);
        PlayerMovement.Singelton.CanMove = false;
        //PlayerMovement.Singelton.visual.SetActive(false);
    }
    public void ExitShop()
    {
        //gameplayCamera.gameObject.SetActive(true);
        //shopCamera.gameObject.SetActive(false);
        shopCanvas.SetActive(false);
        PlayerMovement.Singelton.CanMove = true;
        //PlayerMovement.Singelton.visual.SetActive(true);
    }

    public void Enter()
    {

    }

    public void Exit()
    {

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
