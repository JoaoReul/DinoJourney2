using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float E_health = 3f; // Enemy health variable
    [SerializeField] GameObject meat; // Reference to the meat GameObject

    SphereCollider col;

    void Start()
    {
      col = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerAttack")) // Check if the collider belongs to the player attack
        {
            TakeDamage(1f); // Call TakeDamage method with a damage value of 1
        }
    }

    public void TakeDamage(float damage)
    {
        E_health -= damage; // Reduce health by damage amount
        if (E_health <= 0f)
        {
            Die(); // Call Die method if health is zero or less
        }
    }

   private void Die()
    {

        GameObject.Instantiate(meat, transform.position, Quaternion.identity);
        Debug.Log("FoodDropped"); // Log food drop

        Object.Destroy(gameObject); // Destroy the enemy game object
        Debug.Log("Enemy died"); // Log enemy death
    }
}
