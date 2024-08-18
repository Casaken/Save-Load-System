using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour, IDataPersistence
{
    
    [SerializeField] public  string id = Guid.NewGuid().ToString();
    public GameObject dropPosition;
    public GameObject keyItemInteractable;
    public InventoryManager inventoryManager;

    public bool isActive;
    // public Item item;
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
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            
            keyItemInteractable.SetActive(true);
            keyItemInteractable.transform.position = dropPosition.transform.position;
            keyItemInteractable.transform.rotation = dropPosition.transform.rotation;
            inventoryManager.DropItem();   
            
        }
    }

    public void LoadData(GameData data)
    {
        // // Find the saved data for this KeyItem using its ID
        // var item = data.gameObjects.Find(obj => obj._id == id);
        //
        // if (item != null)
        // {
        //     // Apply the saved position and rotation
        //     transform.position = item._position.ToVector3();
        //     transform.eulerAngles = item._rotation.ToVector3();
        //
        //     // Set the active state
        //     gameObject.SetActive(item.isActive);
        //
        //     // If the item was initially inactive, you might need to manage its interactions
        //     if (!item.isActive)
        //     {
        //         // Handle initialization or state when inactive
        //         // For example, you might need to set up keyItemInteractable or other references
        //         keyItemInteractable.SetActive(false); // Ensure it matches saved state
        //     }
        // }
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
        // if (data.gameObjects == null)
        // {
        //     data.gameObjects = new List<SerializableGameObject>();
        // }
        //
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
