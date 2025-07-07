using UnityEngine;

public class Meat : MonoBehaviour
{
    [SerializeField] SphereCollider col;
    [SerializeField] GameObject player;
    Hunger nhom;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player GameObject by tag
        nhom = player.GetComponent<Hunger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            nhom.Eat(); // Call the Eat method from the Hunger script
            Destroy(gameObject); // Destroy the meat object after pickup
        }
    }
 
}
