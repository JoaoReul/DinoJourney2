using UnityEngine;
using UnityEngine.InputSystem.XR;

public class RexChase : MonoBehaviour
{

    public Transform player;
    public float detectionRange = 20f;
    public float dangerZone = 5f;
    public float chaseSpeed = 3f;
    public float reducedChaseSpeed = 4f;
    public float rotationSpeed = 5f;

    public float gravity = -9.81f;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundMask;

    public int maxHealth = 3;
    private int currentHealth;

    private Animator animator;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    private bool isDead = false;
    private bool isAttacking = false;
    private int hitsTaken = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Verifica se está no chão
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        isGrounded = Physics.Raycast(origin, Vector3.down, groundCheckDistance + 0.1f, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < detectionRange)
        {
            float currentSpeed = (distance < dangerZone) ? reducedChaseSpeed : chaseSpeed;

            Vector3 directionChase = player.position - transform.position;
            directionChase.y = 0f;
            directionChase = directionChase.normalized;

            Vector3 move = directionChase * currentSpeed;

            velocity.y += gravity * Time.deltaTime;

            controller.Move((move + velocity) * Time.deltaTime);

            if (directionChase != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionChase);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            animator.SetBool("Correndo", true);
        }
        else
        {
            animator.SetBool("Correndo", false);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }


        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isAttacking)
            {
                isAttacking = true;
                animator.SetTrigger("Atacar");

            }
        }
    }
}











