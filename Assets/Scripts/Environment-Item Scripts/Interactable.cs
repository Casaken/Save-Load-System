using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //message displayed to player.
    public string promptMessage;
    public void Awake()
    {
        gameObject.layer = 7;
    }

    public abstract void OnInteract();
    public abstract void OnFocus();
    public abstract void OnLoseFocus();
}
