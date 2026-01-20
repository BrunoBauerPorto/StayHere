using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    

    public void BaseInteract()
    {
        interact();
    }

    protected virtual void interact()
    {

    }
}
