using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscInteractable : Interactable
{
    [SerializeField] public  string id = Guid.NewGuid().ToString();
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
        bool result = inventoryManager.AddItem(item);
        if (!result)
        {
            Debug.Log($"Inventory full");
        }
        else
        {
            gameObject.SetActive(false);
            miscGameObject.SetActive(true);
            Debug.Log($"Added {item} to Inventory");
            // inventoryManager.AddItem(item);
            
        }
    }

    public override void OnFocus()
    {
        
    }

    public override void OnLoseFocus()
    {
        
    }
}
