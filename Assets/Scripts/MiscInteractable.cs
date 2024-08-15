using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscInteractable : Interactable
{
    [SerializeField] public GameObject miscGameObject;
    [SerializeField] public Item item;

    [SerializeField] public InventoryManager inventoryManager;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInteract()
    {
        Destroy(gameObject);
        inventoryManager.AddItem(item);
        miscGameObject.SetActive(true);
    }

    public override void OnFocus()
    {
        
    }

    public override void OnLoseFocus()
    {
        
    }
}
