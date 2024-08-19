using System.Collections;  
using System.Collections.Generic;  
using System.Linq;  
using UnityEngine;  
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour, IDataPersistence, ISerializationCallbackReceiver  
{  
    public ItemDatabase database;  
  
    //when saving just make the InventoryItem list's i.tem = ItemData.i.item maybe??  
    [SerializeField]public List<InventoryData> Container = new List<InventoryData>();  
    [SerializeField] public List <Item> items;    
    public GameObject inventoryItemPrefab;  
    [SerializeField] public GameObject playerHandPosition;  
    [SerializeField] public MyFirstPersonController player;

    [SerializeField] public GameObject keyItem;
    [SerializeField] public GameObject miscItem;
    [SerializeField] public GameObject weapon1Item;
    [SerializeField] public GameObject weapon2Item;
    [SerializeField] public GameObject thrashItem;
    [SerializeField] public GameObject foodItem;
    
    [SerializeField] public List<GameObject> interactables;
               

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
            GetSelectedItemData(itemInSlot, Container);  
            if(use){  
                Destroy(itemInSlot.gameObject);
                if (itemInSlot.item.itemType == ItemType.Key)
                {
                    keyItem.SetActive(false);
                }
                if (itemInSlot.item.itemType == ItemType.Misc)
                {
                    miscItem.SetActive(false);
                }
                if (itemInSlot.item.itemType == ItemType.Weapon)
                {
                    weapon1Item.SetActive(false);
                }

                if (itemInSlot.item.itemType == ItemType.Thrash)
                {
                    thrashItem.SetActive(false);
                }

                if (itemInSlot.item.itemType == ItemType.Food)
                {
                    foodItem.SetActive(false);
                }
                
                Container.Remove(GetSelectedItemData(itemInSlot, Container));  
            }  
            return item;  
        }  
        return null;  
  
    }

  
     
    //basically takes in an item and spits out the InventoryData version. It is needed to remove it from the Container, which consists of InventoryData.  
    public InventoryData GetSelectedItemData(InventoryItem item, List<InventoryData> container)  
    {  
        int itemId = item.item.id;  
        ItemType itemType = item.item.itemType;  
  
        InventoryData result = container.FirstOrDefault(data => data.item.id == itemId && data.item.itemType == itemType);  
        return result;  
    }

    public void ResetScene()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Container.Clear();
        ClearInventory();
        keyItem.gameObject.SetActive(false);
        miscItem.gameObject.SetActive(false);
        weapon1Item.gameObject.SetActive(false);
        thrashItem.gameObject.SetActive(false);
        foodItem.gameObject.SetActive(false);
        for (int i = 0; i < interactables.Count; i++)
        {
            interactables[i].SetActive(true);
        }
    }
  
    public void DropItem()  
    {  
        InventorySlot slot = inventorySlots[selectedSlot];  
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();  
        if (itemInSlot != null)  
        {
           
            if (itemInSlot.item.itemType == ItemType.Key)
            {
                keyItem.SetActive(false);                
            }
            if (itemInSlot.item.itemType == ItemType.Misc)
            {
                miscItem.SetActive(false);                
            }
            if (itemInSlot.item.itemType == ItemType.Weapon)
            {
                weapon1Item.SetActive(false);                
            }

            if (itemInSlot.item.itemType == ItemType.Thrash)
            {
                thrashItem.SetActive(false);
            }

            if (itemInSlot.item.itemType == ItemType.Food)
            {
                foodItem.SetActive(false);
            }
            Destroy(itemInSlot.gameObject);
            Debug.Log($"Dropped {itemInSlot.item}");
            Container.Remove(GetSelectedItemData(itemInSlot, Container));  
        }  
    }    private void Update() {  
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
        if(selectedSlot >= 0)  
        {  
            InventorySlot currentSlot = inventorySlots[selectedSlot];  
            InventoryItem itemInCurrentSlot = currentSlot.GetComponentInChildren<InventoryItem>();

            if (itemInCurrentSlot != null)
            {
                //TODO hide the selected item.
                if (itemInCurrentSlot.item.itemType == ItemType.Key)
                {
                    keyItem.SetActive(false);
                }
                if (itemInCurrentSlot.item.itemType == ItemType.Misc)
                {
                    miscItem.SetActive(false);
                }
                if (itemInCurrentSlot.item.itemType == ItemType.Weapon)
                {
                    weapon1Item.SetActive(false);
                }
                if (itemInCurrentSlot.item.itemType == ItemType.Thrash)
                {
                    thrashItem.SetActive(false);
                }

                if (itemInCurrentSlot.item.itemType == ItemType.Food)
                {
                    foodItem.SetActive(false);  
                }
            }
              
            inventorySlots[selectedSlot].Deselect();  
        }  
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
        InventoryItem newSelectedItem = inventorySlots[selectedSlot].GetComponentInChildren<InventoryItem>();
        if (newSelectedItem != null)
        {
            if (newSelectedItem.item.itemType == ItemType.Key)
            {
                keyItem.SetActive(true);
            }
            if (GetSelectedItem(false).itemType == ItemType.Misc)
            {
                miscItem.SetActive(true);
            }
            if (GetSelectedItem(false).itemType == ItemType.Weapon)
            {
                weapon1Item.SetActive(true);
            }

            if (GetSelectedItem(false).itemType == ItemType.Thrash)
            {
                thrashItem.SetActive(true);
            }

            if (GetSelectedItem(false).itemType == ItemType.Food)
            {
                foodItem.SetActive(true);
            }
        }
        
    }  
    public GameObject GetSelectedItemGameObject(InventorySlot slot)  
    {  
        slot = inventorySlots[selectedSlot];  
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();  
        if (itemInSlot.inventoryItemGameObject != null)  
        {  
            GameObject itemGameObject = itemInSlot.inventoryItemGameObject;  
            return itemGameObject;  
        }  
  
        return null;  
    }  
  
  
    public InventorySlot GetSelectedSlot()  
    {  
        InventorySlot slot = inventorySlots[selectedSlot];  
        return slot;  
    }  
   public bool AddItem(Item item){  
    for (int i = 0; i < inventorySlots.Length; i++)  
    {  
        InventorySlot slot = inventorySlots[i];  
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        ChangeSelectedSlot(i);
        if(itemInSlot == null){  
            SpawnNewItem(item, slot);
            
            return true;  
        }     
    }  
    return false;  
   }  
   public void ClearInventory()  
   {  
       Container.Clear();  
       DeactivateAllItems();
       inventorySlots[selectedSlot].Deselect();  
       for (int i = 0; i < inventorySlots.Length; i++)  
       {  
           InventorySlot slot = inventorySlots[i];  
           InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();  
           if (itemInSlot != null)  
           {  
               Destroy(itemInSlot.gameObject);                  
           }     
           
       }
       
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
        inventoryItem.InitializeSceneObject(item.itemGameObject);  
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

        public void DeactivateAllItems()
        {
            ChangeSelectedSlot(0);
            keyItem.SetActive(false);
            miscItem.SetActive(false);
            weapon1Item.SetActive(false);
            thrashItem.SetActive(false);
            foodItem.SetActive(false);
        }
  
  
    public void LoadData(GameData data)  
    {  
        StartCoroutine(LoadDataCoroutine(data));  
          
    }  
    public IEnumerator LoadDataCoroutine(GameData data)  
    {  
         ClearInventory();  
         yield return new WaitForSeconds(0.2f);  
        Container = data.items;  
        for (int i = 0; i < data.items.Count; i++)  
        {  
            Debug.Log($"Index {i} - setting container with ID {Container[i].Id} to {database.GetItem[Container[i].Id]}");  
            //I can get the data set here without a problem.  
            Container[i].item = database.GetItem[Container[i].Id];  
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