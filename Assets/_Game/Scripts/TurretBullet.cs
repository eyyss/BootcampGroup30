using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    public float moveSpeed = 50;
    public float damage = 20;
    void Start()
    {
        Destroy(gameObject, 6);
    }
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out IDamageable damageable))
        {
            Destroy(gameObject);
            damageable.TakeDamage(damage);
        }
    }
}
