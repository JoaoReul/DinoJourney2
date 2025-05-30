using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 10f;
    public float speed = 5f;
    public float gravity = -9.81f;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundMask;

    public float attackCooldown = 2f;

    public GameObject biteEffectObject;
    public Transform biteSpawnPoint;

    private CharacterController controller;
    private Animator animator;

    private Vector3 velocity;
    private bool isGrounded;

    private float attackTimer = 0f;
    private bool isAttacking = false;

    // Suavização do movimento
    private Vector3 smoothMoveDirection = Vector3.zero;
    public float movementSmoothTime = 0.1f; // quanto menor, mais responsivo; quanto maior, mais suave

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if (biteEffectObject != null)
            biteEffectObject.SetActive(false);
    }

    void Update()
    {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;

        Vector3 origin = transform.position + Vector3.up * 0.1f;
        isGrounded = Physics.Raycast(origin, Vector3.down, groundCheckDistance + 0.1f, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (!isAttacking)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            // Direção alvo baseada no input
            Vector3 targetDirection = (transform.forward * v + transform.right * h).normalized;

            // Suaviza o movimento
            smoothMoveDirection = Vector3.Lerp(smoothMoveDirection, targetDirection, Time.deltaTime / movementSmoothTime);

            // Define a animação
            animator.SetBool("Andando", smoothMoveDirection.magnitude > 0.3f);

            // Roda o personagem suavemente
            if (smoothMoveDirection.magnitude >= 0.1f)
            {
                Quaternion toRotation = Quaternion.LookRotation(smoothMoveDirection, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 2.5f); // suavidade da rotação
            }

            velocity.y += gravity * Time.deltaTime;
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : speed;
            Vector3 finalMove = smoothMoveDirection * currentSpeed + velocity;
            controller.Move(finalMove * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Andando", false);
        }

        if (Input.GetKeyDown(KeyCode.J) && attackTimer <= 0f)
        {
            animator.SetTrigger("Atacando");
            attackTimer = attackCooldown;
            isAttacking = true;
        }
    }

    // Animation Event
    public void EndAttack()
    {
        isAttacking = false;
        SpawnBiteEffect();
    }

    private void SpawnBiteEffect()
    {
        if (biteEffectObject != null && biteSpawnPoint != null)
        {
            biteEffectObject.transform.position = biteSpawnPoint.position;
            biteEffectObject.transform.rotation = biteSpawnPoint.rotation;
            biteEffectObject.SetActive(true);
            Invoke("biteEffectObjectt", 0.3f);
        }
    }

    private void biteEffectObjectt()
    {
        if (biteEffectObject != null)
        {
            biteEffectObject.SetActive(false);
        }
    }
}
