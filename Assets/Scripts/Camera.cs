using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;                         // Refer�ncia ao personagem
    public Vector3 offset = new Vector3(0, 4, -6);    // Posi��o em rela��o ao personagem
    public float positionSmoothTime = 0.1f;          // Suaviza��o do movimento
    public float rotationSmoothTime = 0.1f;          // Suaviza��o da rota��o

    private Vector3 currentVelocity;

    void LateUpdate()
    {
        if (target == null) return;

        // Calcula a posi��o desejada baseada na rota��o do personagem
        Vector3 desiredPosition = target.position + target.rotation * offset;

        // Suaviza a posi��o da c�mera
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, positionSmoothTime);

        // Gira suavemente para olhar para o personagem
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothTime);
    }
}
