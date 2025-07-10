using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // DOTween'i unutma

public class EconomyUnit : Unit
{
    private bool canSpawn = false;
    public GameObject moneyPrefab;
    private float spawnTimer;
    public float spawnTime = 5f;

    public override void OnPlace()
    {
        base.OnPlace();
        canSpawn = true;
    }

    public override void OnTake()
    {
        base.OnTake();
        canSpawn = false;
    }

    void Update()
    {
        if (canSpawn)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer > spawnTime)
            {
                spawnTimer = 0;
                Vector3 pos = transform.position - transform.forward + transform.up;
                Instantiate(moneyPrefab, pos, Quaternion.identity);

                Sequence seq = DOTween.Sequence();
                seq.Append(transform.DOMoveY(transform.position.y + 0.3f, 0.2f).SetEase(Ease.OutQuad));
                seq.Join(transform.DOScale(Vector3.one * 1.1f, 0.2f).SetEase(Ease.OutQuad));
                seq.Append(transform.DOMoveY(transform.position.y, 0.2f).SetEase(Ease.InQuad));
                seq.Join(transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InQuad));
            }
        }
    }

}
