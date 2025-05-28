using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour

{
    public float runSpeed = 10f; // velocidade ao correr
    public float speed = 5f;
    public float rotationSmoothTime = 0.1f;
    public float gravity = -9.81f;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundMask;

    public float attackCooldown = 2f;

    public GameObject biteEffectObject; // <- objeto da mordida presente na cena
    public Transform biteSpawnPoint;    // <- ponto na frente da boca

    private CharacterController controller;
    private Animator animator;

    private Vector3 velocity;
    private bool isGrounded;

    private float currentAngle;
    private float rotationVelocity;

    private float attackTimer = 0f;
    private bool isAttacking = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if (biteEffectObject != null)
            biteEffectObject.SetActive(false); // garantir que comece invisível
    }

    void Update()
    {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;

        // Verifica se está no chão
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
            Vector3 inputDirection = new Vector3(h, 0, v).normalized;

            animator.SetBool("Andando", inputDirection.magnitude >= 0.1f);

            Vector3 moveDirection = Vector3.zero;

            if (inputDirection.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
                currentAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, rotationSmoothTime);
                transform.rotation = Quaternion.Euler(0, currentAngle, 0);

                moveDirection = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward;
            }

            velocity.y += gravity * Time.deltaTime;
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : speed;
            Vector3 finalMove = moveDirection * currentSpeed + velocity;
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

    // Chamada por Animation Event no final da animação de ataque
    public void EndAttack()
    {
        isAttacking = false;
        SpawnBiteEffect(); // <- ativa o sprite temporariamente
    }

    private void SpawnBiteEffect()
    {
        if (biteEffectObject != null && biteSpawnPoint != null)
        {
            biteEffectObject.transform.position = biteSpawnPoint.position;
            biteEffectObject.transform.rotation = biteSpawnPoint.rotation;
            biteEffectObject.SetActive(true);
            Invoke("biteEffectObject", 0.3f); // tempo em que o efeito aparece
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