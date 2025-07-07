using System.Collections;
using UnityEngine;

public class Hunger : MonoBehaviour
{
    PlayerWalk walk;
    private float hungertimer = 0f;
    public int hunger = 5;
    [SerializeField] GameObject Hunger1;
    [SerializeField] GameObject Hunger2;
    [SerializeField] GameObject Hunger3;
    [SerializeField] GameObject Hunger4;
    [SerializeField] GameObject Hunger5;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hungertimer += Time.deltaTime;
        if (hungertimer >= 30f) // Every 30 seconds
        {
            hunger--;
            hungertimer = 0f;
            UpdateHunger();
        }
    }


    private void UpdateHunger()
    {
        Hunger1.SetActive(hunger >= 1);
        Hunger2.SetActive(hunger >= 2);
        Hunger3.SetActive(hunger >= 3);
        Hunger4.SetActive(hunger >= 4);
        Hunger5.SetActive(hunger >= 5);


    }

    public void Eat()
    {
        if (hunger < 5) // Assuming 5 is the max hunger level
        {
            hunger++;
            UpdateHunger();
            hungertimer = 0f; // Reset the hunger timer after eating
        }
    }
}
