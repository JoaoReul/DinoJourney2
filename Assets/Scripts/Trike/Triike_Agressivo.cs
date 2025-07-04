using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class TrikeAgressivo : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 10f;        // Distância para começar a perseguir
    public float attackRange = 2f;        // Distância para atacar
    public float moveSpeed = 3.5f;
    public float rotationSpeed = 5f;
    public float gravity = -9.81f;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundMask;

    private Animator animator;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isAttacking = false;
    private bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (isDead || player == null)
            return;

        // Checagem do chão
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        isGrounded = Physics.Raycast(origin, Vector3.down, groundCheckDistance + 0.1f, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < attackRange && !isAttacking)
        {
            // Ataca
            StartCoroutine(Attack());
        }
        else if (distance < chaseRange && !isAttacking)
        {
            // Persegue
            Vector3 direction = player.position - transform.position;
            direction.y = 0f;
            direction = direction.normalized;

            Vector3 move = direction * moveSpeed;
            velocity.y += gravity * Time.deltaTime;

            controller.Move((move + velocity) * Time.deltaTime);

            // Rotação suave
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            animator.SetBool("walk", true);
            animator.SetBool("atacando", false);
        }
        else
        {
            // Parado
            animator.SetBool("walk", false);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;

        // Para de andar
        animator.SetBool("walk", false);

        // Olha para o jogador
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = lookRotation;
        }

        animator.SetBool("atacando", true);

        yield return new WaitForSeconds(1.5f); // tempo da animação de ataque

        animator.SetBool("atacando", false);
        isAttacking = false;
    }

    // Caso queira uma função de dano
    public void TakeDamage(int damage = 1)
    {
        // Aqui pode adicionar lógica de vida se quiser
    }
}
