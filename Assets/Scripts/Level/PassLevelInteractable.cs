using UnityEngine;
using UnityEngine.SceneManagement;

public class PassLevelInteractable : InteractableObject
{
    protected override void OnInteract()
    {
        // Registra la interacción en la clase base
        base.OnInteract();

        // Verifica que la interacción fue registrada y procede
        if (z_Interacted)
        {
            Debug.Log("Interact With..."+name);
            Debug.Log("Pasando de nivel....");
            //SceneManager.LoadScene("Level2");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            Debug.LogWarning("La interacción no fue registrada correctamente.");
        }
    }
}
