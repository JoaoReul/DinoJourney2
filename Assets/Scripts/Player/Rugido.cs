using JetBrains.Annotations;
using UnityEngine;

public class Rugido : MonoBehaviour
{
    private Animator animator; // agora dentro da classe
    private bool isRugindo = false;

    void Start()
    {
        animator = GetComponent<Animator>(); // sem declarar de novo
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            animator.SetTrigger("Rugir"); // usa a variável
            isRugindo = true; // ativa flag

        }

        if (isRugindo)
        {
        }

        }
        public void TerminarRugido()
    {
        isRugindo = false;
            }
            
                    
 }

   