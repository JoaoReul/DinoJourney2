using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RexPerseguirController : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public float detectionRange = 15f;

    float attackingcool = 0f;
    bool isAttacking = false;

    private CharacterController controller;
    private Animator animator;

    PlayerWalk hpPlayer;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        hpPlayer = player.GetComponent<PlayerWalk>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // Se estiver em cooldown de ataque
        if (attackingcool > 0f)
        {
            attackingcool -= Time.deltaTime;
            animator.SetBool("Andando", false);
            return;
        }

        if (distance <= detectionRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0;

            // Rotaciona suavemente
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            // Se perto o suficiente, ataca
            if (distance <= 2f)
            {
                animator.SetBool("Andando", false);
                animator.SetTrigger("Atacar");
                attackingcool = 3f; // Tempo de cooldown
                hpPlayer.TakeDamage(1);
            }
            else
            {
                // Anda se não estiver perto demais
                animator.SetBool("Andando", true);
                controller.Move(direction * speed * Time.deltaTime);
            }
        }
        else
        {
            animator.SetBool("Andando", false);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
