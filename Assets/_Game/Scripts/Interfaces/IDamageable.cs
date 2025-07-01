using UnityEngine;

public interface IDamageable
{
    public bool IsDead();
    public void TakeDamage(float damage);
}
