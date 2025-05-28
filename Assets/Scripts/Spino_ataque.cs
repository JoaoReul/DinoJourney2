using UnityEngine;

public class SpinoAttack : MonoBehaviour
{
    public GameObject biteHitbox; // arraste o cubo mordida aqui no Inspector

    void Start()
    {
        if (biteHitbox != null)
            biteHitbox.SetActive(false);
    }

    // Chamado pela Animation Event no começo do ataque
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

    // Método para finalizar o ataque, pode ser chamado no fim da animação ou no script principal
    public void EndAttack()
    {
        DeactivateBite();
        // Aqui você pode colocar outras lógicas de fim de ataque, se precisar
    }
}
