using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBase : MonoBehaviour, IPlayerDamageable
{
    private float health;
    public float maxHealth = 100;
    public Slider healthSlider;
    private bool isDead = false;
    public List<GameObject> baseVisuals;
    private void Awake()
    {
        foreach (var item in baseVisuals)
        {
            item.SetActive(false);
        }
        baseVisuals[ChapterController.Singelton.currentChapterIndex].SetActive(true);

        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthSlider.value = health;
        if (health <= 0 && !isDead)
        {
            isDead = true;
            UIManager.Singelton.OpenDefeatPanel();
            Debug.Log("Kaybettin");
        }
    }
}
