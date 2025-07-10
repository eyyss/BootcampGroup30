using System;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour, IPlayerDamageable, IPlaceable
{
    [NonSerialized] public float health;
    public float maxHealth = 100;
    public Slider healthSlider;
    public virtual void Awake()
    {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }



    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        healthSlider.value = health;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public virtual void OnTake()
    {
        healthSlider.gameObject.SetActive(false);
    }
    public virtual void OnPlace()
    {
        healthSlider.gameObject.SetActive(true);
        transform.DoSpawnTween();
    }


}