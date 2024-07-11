using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class InventoryManager : MonoBehaviour, IDataPersistence, ISerializationCallbackReceiver
{
    public ItemDatabase database;

    //when saving just make the InventoryItem list's i.tem = ItemData.i.item maybe??
    [SerializeField]public List<InventoryData> Container = new List<InventoryData>();
    public List <Item> items;  
    public GameObject inventoryItemPrefab;
    // public InventoryData itemData;
    private int selectedSlot = -1;

    //I can maybe define the database and dictionaries here.
    [JsonIgnore]public InventorySlot[] inventorySlots;


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
                // InventoryItemContainer.Remove(itemInSlot);
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

   public bool LoadItem(Item item){
     for (int i = 0; i < inventorySlots.Length; i++)
    {
        InventorySlot slot = inventorySlots[i];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if(itemInSlot == null){
           SpawnLoadedItem(item,slot);
           return true;
        }   
    }
    return false;
   }

   public void SpawnNewItem(Item item, InventorySlot slot){
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
        Container.Add(new InventoryData(database.GetId[item],item, item.itemType));
        
   }
    public void AddItemData(Item item){
    Container.Add(new InventoryData(database.GetId[item],item, item.itemType));
   }

   public void SpawnLoadedItem(Item item, InventorySlot slot){
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
   }


    public void LoadData(GameData data)
    {
        Debug.Log("Loaded");
        
        Container = data.items;
        for (int i = 0; i < data.items.Count; i++)
        {
            Debug.Log($"Index {i} - setting container with ID {Container[i].Id} to {database.GetItem[Container[i].Id]}");
            //I can get the data set here without a problem.
            Container[i].item = database.GetItem[Container[i].Id];
            
            //TODO I just need to find a way to SpawnItems. Items that I get from the Database after loading.
        }
        for (int x = 0; x < Container.Count; x++)
        {
                var item = Container[x].item;
                if (item == null) {
                Debug.Log($"Item at {x} was null");
                }
                LoadItem(Container[x].item);
        }

    }

    public void SaveData(GameData data)
    {
       Debug.Log("Saved game");
       data.items = this.Container;
       Debug.Log($"Data item count is {data.items.Count}");
       for (int i = 0; i < Container.Count; i++)
       {
            data.items[i].item = this.Container[i].item;
       }
       
    }

    public void OnBeforeSerialize()
    {
        
    }

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Container.Count; i++)
        {
            Container[i].item = database.GetItem[Container[i].Id];
            Container[i].itemType = database.GetItem[Container[i].Id].itemType;
        }
    }

    private void OnApplicationQuit() {
        Container.Clear();
    }
}

[System.Serializable]
public class InventoryData{
    public int Id;
    [JsonIgnore]public Item item;
    public ItemType itemType;
    

    public InventoryData(int _id, Item _item, ItemType _itemType)
    {
        Id = _id;
        item = _item;
        itemType = _itemType;

    }

    public InventoryData(){

    }
    
}



