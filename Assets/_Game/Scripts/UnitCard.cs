using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitCard : MonoBehaviour
{
    public UnitCardData data;
    public Image iconImage;
    public TextMeshProUGUI nameText, descriptionText, priceText;
    public Button buyButton;
    public void Initialize(UnitCardData data)
    {
        this.data = data;
        iconImage.sprite = data.icon;
        nameText.text = data.unitName;
        descriptionText.text = data.description;
        priceText.text = data.price.ToString();
        buyButton.onClick.AddListener(delegate
        {
            if (PlayerPickup.Singelton.placeableObj == null)
            {
                var spawnedObj = Instantiate(data.prefab);
                PlayerPickup.Singelton.Take(spawnedObj);
                Shop.Singelton.ExitShop();
            }
        });
    }
}
