using UnityEngine;

public interface IPlaceable
{
    public void OnTake();
    public void OnPlace(PlaceZone _placeZone);
}