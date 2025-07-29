using System.Collections.Generic;
using HeneGames.DialogueSystem;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop Singelton;
    public CinemachineCamera gameplayCamera, shopCamera;
    public GameObject shopCanvas;
    public List<UnitCardData> cardDatas;
    public UnitCard cardPrefab;
    public Transform unitPanel;
    private float shopTimer;
    public Button shopButton;
    void Awake()
    {
        Singelton = this;
    }
    private void Start()
    {
        DialogueUI.onDialogueStart.AddListener(delegate
        {
            shopButton.interactable = false;
            if (unitPanel.gameObject.activeSelf) ExitShop();
        });
        DialogueUI.onDialogueEnd.AddListener(delegate
        {
            shopButton.interactable = true;
        });

        foreach (var item in cardDatas)
        {
            UnitCard unitCard = Instantiate(cardPrefab, unitPanel);
            unitCard.Initialize(item);
        }

    }
    public void ShopButtonOnClick()
    {
        if (shopCanvas.activeSelf) ExitShop();
        else EnterShop();
    }
    public void EnterShop()
    {
        //gameplayCamera.gameObject.SetActive(false);
        //shopCamera.gameObject.SetActive(true);
        shopTimer = 0;
        shopCanvas.SetActive(true);
        PlayerMovement.Singelton.SetMove(false);
        //PlayerMovement.Singelton.visual.SetActive(false);
    }
    public void ExitShop()
    {
        //gameplayCamera.gameObject.SetActive(true);
        //shopCamera.gameObject.SetActive(false);
        shopTimer = 0;
        shopCanvas.SetActive(false);
        PlayerMovement.Singelton.SetMove(true);
        //PlayerMovement.Singelton.visual.SetActive(true);
    }



    void Update()
    {
        if (!shopButton.interactable) return;

        if (Input.GetKeyDown(KeyCode.B) && !shopCanvas.activeSelf)
        {
            EnterShop();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitShop();
        }

        if (shopCanvas.activeSelf)
            shopTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.B) && shopTimer > 0.1f)
        {
            ExitShop();
        }
    }
}
