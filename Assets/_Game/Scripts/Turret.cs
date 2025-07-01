using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour, IPlayerDamageable
{
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
        if (fireTimer > fireRate)
        {
            Fire();
            fireTimer = 0;
        }
    }
    private void Fire()
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.identity);
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
}
