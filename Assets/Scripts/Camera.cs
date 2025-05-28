using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;                         // Refer�ncia ao personagem
    public Vector3 offset = new Vector3(0, 5, -7);    // Posi��o da c�mera em rela��o ao personagem
    public float smoothSpeed = 10f;                  // Suavidade da posi��o
    public float rotationSmoothSpeed = 5f;           // Suavidade da rota��o

    void LateUpdate()
    {
        if (target == null) return;

        // Posi��o desejada
        Vector3 desiredPosition = target.position + offset;

        // Movimento suave da posi��o
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Rota��o desejada
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);

        // Rota��o suave
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSmoothSpeed * Time.deltaTime);
    }
}
