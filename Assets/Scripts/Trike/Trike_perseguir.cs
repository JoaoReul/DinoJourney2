using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class TrikePerseguirController : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public float detectionRange = 15f;

    float attackingcool = 0f;

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

        // Fugir durante o cooldown
        if (attackingcool > 0f)
        {
            attackingcool -= Time.deltaTime;

            Vector3 direction = (transform.position - player.position).normalized;
            direction.y = 0;

            animator.SetBool("Andando", true);

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            controller.Move(direction * speed * Time.deltaTime);
            return; // Impede que execute o resto do código enquanto foge
        }

        // Ataca se estiver bem perto
        if (distance <= 2f)
        {
            animator.SetBool("Andando", false); // Para de andar para atacar
            animator.SetTrigger("Atacar");

            attackingcool = 3f;
            hpPlayer.TakeDamage(1);
        }
        // Persegue se estiver dentro do alcance
        else if (distance <= detectionRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0;

            animator.SetBool("Andando", true);

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            controller.Move(direction * speed * Time.deltaTime);
        }
        // Fora do alcance: para de andar
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
