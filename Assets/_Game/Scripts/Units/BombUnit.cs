using DG.Tweening;
using UnityEngine;

public class BombUnit : Unit
{
    public ParticleSystem expolsionEffectPrefab;
    public float explosionRadius = 4;
    public float explosionDamage = 100;
    public AudioData explosionAudio;
    public override void OnTake()
    {
        base.OnTake();
    }
    public override void OnPlace(PlaceZone _placeZone)
    {
        base.OnPlace(_placeZone);
        GetComponent<Collider>().enabled = false;
        healthSlider.gameObject.SetActive(false);
        DOVirtual.DelayedCall(2f, delegate
        {
            var expolsionEffect = Instantiate(expolsionEffectPrefab, transform.position, Quaternion.identity);
            expolsionEffect.Play();
            explosionAudio.Play2D(this);
            Destroy(gameObject);
            var hits = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (var item in hits)
            {
                if (item.TryGetComponent(out IDamageable damageable))
                {
                    if (!damageable.IsDead())
                        damageable.TakeDamage(explosionDamage);
                }
            }
        });
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
