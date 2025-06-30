using UnityEngine;

public class RexChase2 : MonoBehaviour
{
    private float distance = 20f;
    private float speed = 5f;
    private Rigidbody rb;
    private Transform PlayerTransform;
    private GameObject playerObject;

    [SerializeField] private Animator animator; // << Adiciona o Animator aqui

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (animator == null)
        {
            animator = GetComponent<Animator>(); // Tenta pegar automaticamente
        }
    }

    void Update()
    {
        if (playerObject == null)
        {
            playerObject = GameObject.FindGameObjectWithTag("Player");
        }
        if (playerObject != null && PlayerTransform == null)
        {
            PlayerTransform = playerObject.transform;
        }

        bool andando = false;

        if (PlayerTransform != null)
        {
            float dist = Vector3.Distance(transform.position, PlayerTransform.position);
            if (dist <= distance)
            {
                // Move o Trex em direo ao player

                
                
                    // Calcula a direo e move o Trex
                    Vector3 direction = (PlayerTransform.position - transform.position).normalized;
                    Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;
                rb.MovePosition(newPosition);
                
               

                andando = true;


            }


            // Se o Trex estiver muito prximo do player, para de se mover   
            if (dist < 1f)
            {
                rb.linearVelocity = Vector3.zero; // Para o Trex
                andando = false; // Para a animao de andar

                //insira aqui codigo pra atacar o player


            }

            // Rota o Trex para olhar para o jogador
            transform.LookAt(new Vector3(PlayerTransform.position.x, transform.position.y, PlayerTransform.position.z));
            //lock trex rotation y on 0
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }

        // ANIMAO
        if (animator != null)
        {
            animator.SetBool("Andando", andando); // Ativa ou desativa a animao
        }
    }
}
