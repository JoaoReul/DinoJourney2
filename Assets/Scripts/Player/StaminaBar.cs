using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    
    [SerializeField] Slider Slider;
    PlayerWalk player;
    [SerializeField] GameObject Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Player.GetComponent<PlayerWalk>();
    }

    // Update is called once per frame
    void Update()
    {
        Slider.value = player.CurrentStamina;
    }
}
