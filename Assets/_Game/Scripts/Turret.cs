using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour, IPlayerDamageable, IPlaceable
{
    public bool canFire = false;
    public float fireRate = 1;
    private float fireTimer;
    public GameObject bulletPrefab;
    public GameObject bulletSpawnPoint;
    private float health;
    public float maxHealth = 100;
    public Slider healthSlider;
    void Awake()
    {
        fireTimer = fireRate;
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
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

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthSlider.value = health;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTake()
    {
        healthSlider.gameObject.SetActive(false);
        canFire = false;
    }

    public void OnPlace()
    {
        fireTimer = 0;
        healthSlider.gameObject.SetActive(true);
        canFire = true;


        Vector3 startPos = transform.position + Vector3.up * 0.5f;
        transform.position = startPos;
        transform.DOMoveY(transform.position.y - 0.5f, 0.4f)
            .SetEase(Ease.OutBounce);

        transform.DOScale(Vector3.one * 1.1f, 0.1f)
            .SetLoops(2, LoopType.Yoyo);

        transform.DORotate(Vector3.zero, 0.3f).SetEase(Ease.InOutSine);
    }
}
