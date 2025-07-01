using UnityEngine;
using UnityEngine.UI;

public class PlayerBase : MonoBehaviour, IPlayerDamageable
{
    private float health;
    public float maxHealth = 100;
    public Slider healthSlider;
    private void Awake()
    {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthSlider.value = health;
        if (health <= 0)
        {
            Debug.Log("Kaybettin");
        }
    }
}
