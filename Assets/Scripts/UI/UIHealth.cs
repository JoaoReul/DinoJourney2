using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    [SerializeField] GameObject player;
    float currenthealth;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currenthealth = player.GetComponent<PlayerWalk>().CurrentHealth;
        if (currenthealth == 3)
        {
            RawImage healthImage = GetComponent<RawImage>();
            healthImage.color = new Color(0, 1, 0, 1); // Verde
        }
        if (currenthealth ==2)
        {
            RawImage healthImage = GetComponent<RawImage>();
            healthImage.color = new Color(1, 1, 0, 1); // Amarelo
        }
        if (currenthealth == 1)
        {
            RawImage healthImage = GetComponent<RawImage>();
            healthImage.color = new Color(1, 0, 0, 1); // Vermelho
        }
        if (currenthealth == 0)
        {
            RawImage healthImage = GetComponent<RawImage>();
            healthImage.color = new Color(0, 0, 0, 1); // Preto
        }
    }
}
