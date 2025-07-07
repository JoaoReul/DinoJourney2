using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerWalk : MonoBehaviour
{
    public float CurrentHealth = 3f;
    public float MaxHealth = 3f;
    public float MaxStamina = 10f;
    public float CurrentStamina = 10f;

    public float runSpeed = 10f;
    private float runningCooldown = 0f;
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
    private bool isRunning = false;
    private bool isRugindo = false;

    // Suaviza??o do movimento
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

        if (Input.GetKeyUp(KeyCode.K))
        {
            animator.SetTrigger("Rugir");
            isRugindo = true;
        }


        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (!isAttacking)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if (!isAttacking && !isRugindo)
{

                if (!isAttacking && !isRugindo)
                {
                    // Pode andar normalmente
                }
                else
                {
                    animator.SetBool("Andando", false);
                }

                // Pode andar normalmente
            }
            else
            {
              animator.SetBool("Andando", false);
            }


            // Dire??o alvo baseada no input
            Vector3 targetDirection = (transform.forward * v + transform.right * h).normalized;

            // Suaviza o movimento
            smoothMoveDirection = Vector3.Lerp(smoothMoveDirection, targetDirection, Time.deltaTime / movementSmoothTime);

            // Define a anima??o
            animator.SetBool("Andando", smoothMoveDirection.magnitude > 0.3f);

            // Roda o personagem suavemente
            if (smoothMoveDirection.magnitude >= 0.1f)
            {
                Quaternion toRotation = Quaternion.LookRotation(smoothMoveDirection, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 2.5f); // suavidade da rota??o
            }
            float currentSpeed = 0f;
            velocity.y += gravity * Time.deltaTime;
            if (Input.GetKey(KeyCode.LeftShift) && CurrentStamina > 0f && runningCooldown <= 0f)
            {
                isRunning = true;
                CurrentStamina -= Time.deltaTime * 2; // Consome stamina ao correr
                currentSpeed = runSpeed;
                if (CurrentStamina <= 0f && runningCooldown <= 0f)
                {
                    CurrentStamina = 0f; // Impede que a stamina fique negativa
                    isRunning = false; // Para de correr se a stamina acabar
                    runningCooldown = 5f; // Tempo de recarga para correr novamente
                }
            }
            else
            {
                currentSpeed = speed;
                isRunning = false;
                if (CurrentStamina < MaxStamina)
                {
                    CurrentStamina += Time.deltaTime; // Regenera stamina quando n??o est?? correndo
                }
                if (runningCooldown > 0f)
                {
                    runningCooldown -= Time.deltaTime; // Diminui o tempo de recarga

                }
            }
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

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("Morrer");
        // Aqui voc?? pode adicionar l??gica de morte, como desativar o personagem ou reiniciar o jogo
        Debug.Log("Player morreu!");
    }

    public void Rugir()
    {
        if (isRugindo)
        {
            isRugindo = false;
            animator.SetBool("Rugindo", false);
        }
        else
        {
            animator.SetBool("Rugindo", true);
            isRugindo = true;
        }
    }

    public void Eat()
    {
        CurrentHealth = Mathf.Min(CurrentHealth + 1f, MaxHealth);
    }
}
