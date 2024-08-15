using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInteractable : Interactable
{
    public bool isInHand;
    [SerializeField] public Item item;
    [SerializeField] public GameObject weaponGameObject;
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
        gameObject.SetActive(false);
        weaponGameObject.SetActive(true);
        inventoryManager.AddItem(item);
        Debug.Log($"Added {item} to Inventory");
        
    }

    public override void OnFocus()
    {
        
    }

    public override void OnLoseFocus()
    {
        
    }
}
