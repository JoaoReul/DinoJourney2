using UnityEngine;

public class SpinoAttack : MonoBehaviour
{
    public GameObject biteHitbox; // arraste o cubo mordida aqui no Inspector

    void Start()
    {
        if (biteHitbox != null)
            biteHitbox.SetActive(false);
    }

    // Chamado pela Animation Event no come�o do ataque
    public void ActivateBite()
    {
        if (biteHitbox != null)
            biteHitbox.SetActive(true);
    }

    // Chamado pela Animation Event no fim do ataque
    public void DeactivateBite()
    {
        if (biteHitbox != null)
            biteHitbox.SetActive(false);
    }

    // M�todo para finalizar o ataque, pode ser chamado no fim da anima��o ou no script principal
    public void EndAttack()
    {
        DeactivateBite();
        // Aqui voc� pode colocar outras l�gicas de fim de ataque, se precisar
    }
}
