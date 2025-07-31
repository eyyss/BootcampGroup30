using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitCard : MonoBehaviour
{
    public UnitCardData data;
    public Image iconImage;
    public TextMeshProUGUI nameText, descriptionText, priceText, orderText;
    public Button buyButton;
    public void Initialize(UnitCardData data, int order)
    {
        this.data = data;
        iconImage.sprite = data.icon;
        nameText.text = data.unitName;
        descriptionText.text = data.description;
        priceText.text = "Fiyat: " + data.price.ToString();
        orderText.text = "(" + order.ToString() + ")";
        buyButton.onClick.AddListener(delegate
        {
            Shop.Singelton.Buy(data);
        });
    }
}
