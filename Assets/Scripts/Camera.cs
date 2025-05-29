using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;                         // Referência ao personagem
    public Vector3 offset = new Vector3(0, 4, -6);    // Posição em relação ao personagem
    public float positionSmoothTime = 0.1f;          // Suavização do movimento
    public float rotationSmoothTime = 0.1f;          // Suavização da rotação

    private Vector3 currentVelocity;

    void LateUpdate()
    {
        if (target == null) return;

        // Calcula a posição desejada baseada na rotação do personagem
        Vector3 desiredPosition = target.position + target.rotation * offset;

        // Suaviza a posição da câmera
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, positionSmoothTime);

        // Gira suavemente para olhar para o personagem
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothTime);
    }
}
