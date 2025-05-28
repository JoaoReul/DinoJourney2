using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;                         // Referência ao personagem
    public Vector3 offset = new Vector3(0, 5, -7);    // Posição da câmera em relação ao personagem
    public float smoothSpeed = 10f;                  // Suavidade da posição
    public float rotationSmoothSpeed = 5f;           // Suavidade da rotação

    void LateUpdate()
    {
        if (target == null) return;

        // Posição desejada
        Vector3 desiredPosition = target.position + offset;

        // Movimento suave da posição
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Rotação desejada
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);

        // Rotação suave
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSmoothSpeed * Time.deltaTime);
    }
}
