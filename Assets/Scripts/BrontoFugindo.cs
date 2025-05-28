using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class BrontoFugindo : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 5f;
    public float dangerZone = 2f;
    public float fleeSpeed = 3f;
    public float reducedFleeSpeed = 1f;
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

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead || player == null || isAttacking)
            return;

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
            float currentSpeed = (distance < dangerZone) ? reducedFleeSpeed : fleeSpeed;

            Vector3 directionAway = transform.position - player.position;
            directionAway.y = 0f;
            directionAway = directionAway.normalized;

            Vector3 move = directionAway * currentSpeed;

            velocity.y += gravity * Time.deltaTime;

            controller.Move((move + velocity) * Time.deltaTime);

            if (directionAway != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionAway);
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

    public void TakeDamage(int damage = 1)
    {
        if (isDead)
            return;

        currentHealth -= damage;
        hitsTaken++;

        if (currentHealth <= 0)
        {
            Die();
        }
        else if (hitsTaken == 2)
        {
            StartCoroutine(ReactWithAttack());
        }
    }

    private IEnumerator ReactWithAttack()
    {
        isAttacking = true;
        animator.SetBool("Correndo", false);

        // Olha para o jogador
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = lookRotation;
        }

        animator.SetTrigger("Atacar");

        yield return new WaitForSeconds(1.5f); // tempo da animação de ataque

        isAttacking = false;

        // Verifica se o player ainda está por perto e reativa a animação de correr
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < detectionRange)
        {
            animator.SetBool("Correndo", true);
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Morrer");
        animator.SetBool("Correndo", false);
        Destroy(gameObject, 3f);
    }
}
