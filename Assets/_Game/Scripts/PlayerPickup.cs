using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public static PlayerPickup Singelton;
    public Transform holdPoint;
    public float pickupRange = 2f;
    public GameObject placeableObj;
    public Transform placeCheckPoint;
    public Transform selectedZoneDebugTransform;
    private PlaceZone currentPlaceZone;
    void Awake()
    {
        Singelton = this;
        selectedZoneDebugTransform.SetParent(null);
    }
    void Update()
    {
        var hits = Physics.OverlapSphere(placeCheckPoint.position, pickupRange);

        if (placeableObj != null)
        {
            foreach (var item in hits)
            {

                if (item.TryGetComponent(out PlaceZone placeZone) && placeZone.isEmpty)
                {
                    currentPlaceZone = placeZone;
                    selectedZoneDebugTransform.position = placeZone.transform.position;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && placeableObj != null)
        {

            if (currentPlaceZone != null && currentPlaceZone.isEmpty)
            {
                float distance = Vector3.Distance(transform.position, currentPlaceZone.transform.position);
                if (distance > 3) return;
                currentPlaceZone.Place();
                placeableObj.transform.position = currentPlaceZone.transform.position;
                placeableObj.transform.SetParent(currentPlaceZone.transform);
                placeableObj.transform.rotation = Quaternion.Euler(Vector3.zero);
                placeableObj.GetComponent<IPlaceable>().OnPlace(currentPlaceZone);
                currentPlaceZone = null;
                placeableObj = null;
                selectedZoneDebugTransform.position = Vector3.zero + Vector3.down * 10;
                return;
            }
        }


        if (Input.GetKeyDown(KeyCode.E) && placeableObj == null)
        {
            foreach (var item in hits)
            {
                if (item.TryGetComponent(out IPlaceable placeable))
                {
                    if (item.transform.parent != null && item.transform.parent.TryGetComponent(out PlaceZone placeZone))
                    {
                        placeZone.UnPlace();
                    }
                    item.transform.position = holdPoint.position;
                    item.transform.SetParent(holdPoint.transform);
                    placeableObj = item.gameObject;
                    placeable.OnTake();
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && placeableObj != null)
        {
            Drop();
        }

    }

    public void Take(GameObject takeObj)
    {
        if (placeableObj == null)
        {
            if (takeObj.TryGetComponent(out IPlaceable placeable))
            {
                if (takeObj.transform.parent != null && takeObj.transform.parent.TryGetComponent(out PlaceZone placeZone))
                {
                    placeZone.UnPlace();
                }
                takeObj.transform.position = holdPoint.position;
                takeObj.transform.SetParent(holdPoint.transform);
                placeableObj = takeObj;
                placeable.OnTake();
            }
        }
    }
    public void Drop()
    {
        Destroy(placeableObj);
        currentPlaceZone = null;
        placeableObj = null;
        selectedZoneDebugTransform.position = Vector3.zero + Vector3.down * 10;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(placeCheckPoint.position, pickupRange);
    }
}
