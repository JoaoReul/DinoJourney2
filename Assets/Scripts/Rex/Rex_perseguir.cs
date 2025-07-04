using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RexPerseguirController : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public float detectionRange = 15f;

    private CharacterController controller;
    private Animator animator;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
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
        else
        {
            animator.SetBool("Andando", false); // Para andar quando estiver longe
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
