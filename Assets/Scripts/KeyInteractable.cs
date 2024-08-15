using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteractable : Interactable
{
    [SerializeField] public Item item;
    [SerializeField] public GameObject keyGameObject;
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
        keyGameObject.SetActive(true);
        inventoryManager.AddItem(item);
    }

    public override void OnFocus()
    {

    }

    public override void OnLoseFocus()
    {

    }
}
