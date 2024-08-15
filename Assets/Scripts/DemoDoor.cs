using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoDoor : Interactable
{
    [SerializeField] public InventoryManager inventoryManager;

    [SerializeField] public Item item;
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
         item = inventoryManager.GetSelectedItem(false);
         item.itemType = inventoryManager.GetSelectedItem(false).itemType;
         if (item.itemType == ItemType.Key)
         {
             inventoryManager.GetSelectedItem(true);
             Debug.Log($"You have the key, opening door.");
             gameObject.SetActive(false);
         }
         else
         {
             Debug.Log($"You need a key to open this door.");
         }
    }

    public override void OnFocus()
    {
        
    }

    public override void OnLoseFocus()
    {
        
    }
}
