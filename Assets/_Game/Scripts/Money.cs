using UnityEngine;
using System.Collections;

public class Money : MonoBehaviour
{
    public int value = 1;
    private bool collected = false;
    private Collider coll;
    private Rigidbody rb;
    private Transform target;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    public void CollectWorld(Vector3 worldTarget, System.Action onCollected = null)
    {
        if (collected) return;
        collected = true;
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        coll.enabled = false;
        StartCoroutine(MoveToWorldPointCoroutine(worldTarget, onCollected));
    }

    private IEnumerator MoveToWorldPointCoroutine(Vector3 worldTarget, System.Action onCollected)
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, worldTarget, 15f * Time.deltaTime);
            float distance = Vector3.Distance(transform.position, worldTarget);
            float scale = Mathf.Lerp(0.2f, 1f, distance / 3f);
            transform.localScale = Vector3.one * scale;

            if (distance < 0.05f)
            {
                onCollected?.Invoke();
                Destroy(gameObject);
                yield break;
            }

            yield return null;
        }
    }
}
