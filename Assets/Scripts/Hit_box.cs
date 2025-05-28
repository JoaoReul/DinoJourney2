using UnityEngine;

public class BiteHitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bronto"))
        {
            BrontoFugindo bronto = other.GetComponent<BrontoFugindo>();
            if (bronto != null)
            {
                bronto.TakeDamage(1);
            }
        }
    }
}
