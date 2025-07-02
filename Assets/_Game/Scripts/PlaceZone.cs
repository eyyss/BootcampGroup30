using UnityEngine;

public class PlaceZone : MonoBehaviour
{
    public bool isEmpty = true;
    public void Place()
    {
        isEmpty = false;
    }
    public void UnPlace()
    {
        isEmpty = true;
    }
}