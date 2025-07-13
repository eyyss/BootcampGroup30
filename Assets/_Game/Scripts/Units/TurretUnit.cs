using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Turret : Unit
{
    public bool canFire = false;
    public float fireRate = 1;
    private float fireTimer;
    public GameObject bulletPrefab;
    public GameObject bulletSpawnPoint;

    public override void Awake()
    {
        base.Awake();
        fireTimer = fireRate;
    }

    public void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer > fireRate && canFire)
        {
            Fire();
            fireTimer = 0;
        }
    }
    private void Fire()
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.identity);

        transform.DOKill();
        Vector3 originalPos = transform.localPosition;

        transform
            .DOLocalMoveZ(originalPos.z - 0.2f, 0.05f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                transform
                    .DOLocalMoveZ(originalPos.z, 0.1f)
                    .SetEase(Ease.InQuad);
            });
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    public override void OnTake()
    {
        base.OnTake();
        canFire = false;
    }

    public override void OnPlace(PlaceZone _placeZone)
    {
        base.OnPlace(_placeZone);
        fireTimer = 0;
        canFire = true;
    }
}
