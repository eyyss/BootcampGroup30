using DG.Tweening;
using HeneGames.DialogueSystem;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Singelton;
    public float moveSpeed = 5f;
    public float rotateSpeed = 7f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Transform cam;
    private Vector3 velocity;
    private bool isGrounded;
    public bool CanMove = true;
    public Animator animator;
    public Renderer visual;
    public AudioData dissolveAudio;
    public GameObject torch;
    void Awake()
    {
        Singelton = this;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;

        DialogueUI.onDialogueStart.AddListener(delegate
        {
            SetMove(false);
        });
        DialogueUI.onDialogueEnd.AddListener(delegate
        {
            SetMove(true);
        });

        if (ChapterController.Singelton.currentChapterIndex == 2)// son chapter
        {
            torch.SetActive(true);
            animator.SetBool("IsTorch", true);
        }

        dissolveAudio.Play2D(this);
        visual.material.SetVector("_DissolveOffest", new Vector3(0, 2, 0));
        visual.material.DOVector(new Vector3(0, -2, 0), "_DissolveOffest", 2);
    }

    public void SetMove(bool state)
    {
        CanMove = state;
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
            transform.forward = Vector3.Slerp(transform.forward, move, Time.deltaTime * rotateSpeed);
        }

        if (move.magnitude > 0.1 && CanMove)
        {
            animator.SetBool("IsMove", true);
        }
        else
        {
            animator.SetBool("IsMove", false);
        }
    }
}
