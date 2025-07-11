using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Singelton;
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Transform cam;
    private Vector3 velocity;
    private bool isGrounded;
    public bool CanMove = true;
    public GameObject visual;
    void Awake()
    {
        Singelton = this;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 move = (camForward * inputZ + camRight * inputX).normalized;

        if (CanMove)
            controller.Move(move * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (move != Vector3.zero && CanMove)
        {
            transform.forward = move;
        }
    }
}
