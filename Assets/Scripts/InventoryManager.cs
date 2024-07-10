using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public GameObject inventoryItemPrefab;
    private int selectedSlot = -1;

    //I can maybe define the database and dictionaries here.
    public InventorySlot[] inventorySlots;

    private void Start() {
        ChangeSelectedSlot(0);
    }

    public Item GetSelectedItem(bool use){
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if(itemInSlot != null){
            Item item = itemInSlot.item;
            if(use){
                Destroy(itemInSlot.gameObject);
            }
            return item;
        }
        return null;

    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            ChangeSelectedSlot(0);
        }else if(Input.GetKeyDown(KeyCode.Alpha2)){
            ChangeSelectedSlot(1);
        }else if(Input.GetKeyDown(KeyCode.Alpha3)){
            ChangeSelectedSlot(2);
        }else if(Input.GetKeyDown(KeyCode.Alpha4)){
            ChangeSelectedSlot(3);
        }
    }
    public void ChangeSelectedSlot(int newValue){
        if(selectedSlot >= 0){
            inventorySlots[selectedSlot].Deselect();
        }
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }
   public bool AddItem(Item item){
    for (int i = 0; i < inventorySlots.Length; i++)
    {
        InventorySlot slot = inventorySlots[i];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if(itemInSlot == null){
            SpawnNewItem(item, slot);
            return true;
        }   
    }
    return false;
   }

   public void SpawnNewItem(Item item, InventorySlot slot){
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
   }

}
