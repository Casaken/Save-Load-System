using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoDoor : Interactable, IDataPersistence
{
    [SerializeField] public InventoryManager inventoryManager;
    [SerializeField] public  string id = Guid.NewGuid().ToString();
    [SerializeField] public Item item;

    public bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // private void OnEnable()
    // {
    //     DataPersistenceManager.instance.RegisterDataPersistenceObject(this);
    // }
    //
    // private void OnDisable()
    // {
    //     DataPersistenceManager.instance.UnregisterDataPersistenceObject(this);
    // }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInteract()
    {
         Item itemInSlot = inventoryManager.GetSelectedItem(false);
         if (itemInSlot == null)
         {
             Debug.Log($"You do not hold anything in your hand let alone A KEY!");
             return;
         }
         if (itemInSlot != null)
         {
             itemInSlot.itemType = inventoryManager.GetSelectedItem(false).itemType;
             if (itemInSlot.itemType == ItemType.Key)
             {
                 inventoryManager.GetSelectedItem(true);
                 Debug.Log($"You have the key, opening door.");
                 gameObject.SetActive(false);
             }
             
         }

         if (itemInSlot == null)
         {
             Debug.Log($"You need a key to Open this door.");
         }

         if (itemInSlot.itemType != ItemType.Key)
         {
             Debug.Log($"This is not a KEY!");
         }

         
         // item.itemType = inventoryManager.GetSelectedItem(false).itemType;
         
    }

    public override void OnFocus()
    {
        
    }

    public override void OnLoseFocus()
    {
        
    }

    public void LoadData(GameData data)
    {
        
    }

    public void SaveData(GameData data)
    {
        // Debug.Log($"SAVING {this} STATUS");
        // SerializableGameObject serializableObject = new SerializableGameObject
        // {
        //     //first _id is the SerializableGameObject's id.
        //     _id = id,
        //     _position = new SerializableVector3(transform.position),
        //     _rotation = new SerializableVector3(transform.eulerAngles),
        //     isActive = gameObject.activeSelf
        // };
        // //making sure list exists...
        // if (data.gameObjects == null)
        // {
        //     data.gameObjects = new List<SerializableGameObject>();
        // }
        //
        // //This here checks if the item is already in the save list.
        // var existingObject = data.gameObjects.Find(existingObject => existingObject._id == id);
        // if (existingObject != null)
        // {
        //     //if yes just delete it.
        //     data.gameObjects.Remove(existingObject);  
        // }
        // //if not add the object.
        // data.gameObjects.Add(serializableObject);  
    }
}
