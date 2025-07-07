using UnityEngine;
[CreateAssetMenu(menuName = "UnitCardData")]
public class UnitCardData : ScriptableObject
{
    public string unitName;
    public string description;
    public float price;
    public Sprite icon;
    public GameObject prefab;
}