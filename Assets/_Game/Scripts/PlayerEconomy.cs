using TMPro;
using UnityEngine;

public class PlayerEconomy : MonoBehaviour
{
    public static PlayerEconomy Singelton;
    public int money = 0;
    public TextMeshProUGUI moneyText;
    public float moneyPickupRange = 2f;
    public LayerMask moneyLayer;
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private GameObject collectEffectPrefab;
    void Awake()
    {
        Singelton = this;
        moneyText.text = "Money: " + money.ToString();
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            money += 100;
            moneyText.text = "Money: " + money.ToString();
        }
#endif

        Collider[] hits = Physics.OverlapSphere(transform.position, moneyPickupRange, moneyLayer);
        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out Money moneyComp))
            {
                moneyComp.CollectWorld(transform.position + Vector3.up * 0.5f, () =>
                {
                    money += moneyComp.value;
                    moneyText.text = "Money: " + money.ToString();

                    if (collectSound != null)
                        AudioSource.PlayClipAtPoint(collectSound, transform.position);

                    if (collectEffectPrefab != null)
                        Instantiate(collectEffectPrefab, transform.position + Vector3.up * 1f, Quaternion.identity);
                });
            }
        }
    }
    public bool TryBuy(UnitCardData data)
    {
        if (data.price > money) return false;
        money -= (int)data.price;
        moneyText.text = "Money: " + money.ToString();
        return true;
    }
}
