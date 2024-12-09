using UnityEngine;

public class InteractableObject : CollidableObject
{
    protected bool z_Interacted = false; 

    protected override void OnCollided(GameObject collidedObject)
    {
        if (Input.GetKey(KeyCode.E))
        {
            OnInteract();
        }
    }

    protected virtual void OnInteract()
    {
        if (!z_Interacted)
        {
            Debug.Log("Interaccion registrada en "+name);
            z_Interacted = true;
        }
        else
        {
            Debug.Log("Ya interactuaste con "+ name);
        }
    }
}
