using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour, IDamageable
{
    public float moveSpeed;
    private bool isDead = false;
    private float health;
    public float maxHealth = 100;
    public Slider healthSlider;
    public LayerMask obstacleLayer;
    public float obstacleCheckDistance = 1f;
    public GameObject frontGO;
    public float damage = 5;
    public float attackRate = 1;
    private float attackTimer;
    public Animator animator;

    public bool IsDead()
    {
        return isDead;
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

    void Awake()
    {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
        attackTimer = attackRate;
    }
    void Update()
    {

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, obstacleCheckDistance, obstacleLayer))
        {
            if (hit.collider != null) frontGO = hit.collider.gameObject;
            else frontGO = null;
        }
        else frontGO = null;

        //move
        if (frontGO == null) 
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            animator.SetBool("IsMove", true);
        } 
        else//attack
        {
            animator.SetBool("IsMove", false);
            attackTimer += Time.deltaTime;
            if (attackTimer > attackRate && frontGO.TryGetComponent(out IPlayerDamageable damageable))
            {
                attackTimer = 0;
                Attack(damageable);
            }
        }

    }
    private void Attack(IPlayerDamageable damageable)
    {
        damageable.TakeDamage(damage);
        animator.SetTrigger("Attack");
    }
    void OnDrawGizmos()
    {

        Gizmos.DrawRay(transform.position, transform.forward * obstacleCheckDistance);
    }
}