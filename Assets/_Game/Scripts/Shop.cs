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
    public List<ChapterCardData> chapterCardData;
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

        List<UnitCardData> cardDatas = chapterCardData[ChapterController.Singelton.currentChapterIndex].unitCardDatas;
        for (int i = 0; i < cardDatas.Count; i++)
        {
            UnitCard unitCard = Instantiate(cardPrefab, unitPanel);
            unitCard.Initialize(cardDatas[i], i + 1);
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

        if (Input.GetKeyDown(KeyCode.Q) && !shopCanvas.activeSelf)
        {
            EnterShop();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitShop();
        }

        if (shopCanvas.activeSelf)
            shopTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Q) && shopTimer > 0.1f)
        {
            ExitShop();
        }

        List<UnitCardData> cardDatas = chapterCardData[ChapterController.Singelton.currentChapterIndex].unitCardDatas;

        if (!shopCanvas.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Buy(cardDatas[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Buy(cardDatas[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && cardDatas.Count > 2)
        {
            Buy(cardDatas[2]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && cardDatas.Count > 3)
        {
            Buy(cardDatas[3]);
        }

    }
    public void Buy(UnitCardData data)
    {
        if (!PlayerEconomy.Singelton.TryBuy(data)) return;

        if (PlayerPickup.Singelton.placeableObj == null)
        {
            var spawnedObj = Instantiate(data.prefab);
            PlayerPickup.Singelton.Take(spawnedObj);
            ExitShop();
        }
    }

    [System.Serializable]
    public class ChapterCardData
    {
        public List<UnitCardData> unitCardDatas;
    }
}
