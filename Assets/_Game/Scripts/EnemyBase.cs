using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        rb.linearVelocity = transform.forward * moveSpeed * Time.deltaTime;
    }
}