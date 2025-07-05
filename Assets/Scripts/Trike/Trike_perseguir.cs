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

        if (distance <= detectionRange && attackingcool <= 0f)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0; // Remove inclinação

            animator.SetBool("Andando", true); // Ativa andar

            // Rotaciona suavemente
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            // Move
            controller.Move(direction * speed * Time.deltaTime);
        }
        if(distance <= 2f && attackingcool <= 0f)
        {
            animator.SetTrigger("Atacar"); // Ataca quando perto
            //insira aqui codigo pra ligar overlay do ataque
            attackingcool = 3f; // Cooldown de ataque
            hpPlayer.TakeDamage(1); // Dano ao jogador

        }
        else
        {
            animator.SetBool("Andando", false); // Para andar quando estiver longe
        }

        if (attackingcool > 0f)
        {
            attackingcool -= Time.deltaTime; // Reduz cooldown
            Vector3 direction = (transform.position - player.position).normalized;
            direction.y = 0; // Remove inclinação

            animator.SetBool("Andando", true); // Ativa andar

            // Rotaciona suavemente
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            // Move
            controller.Move(direction * speed * Time.deltaTime);
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
