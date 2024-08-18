using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscItem : MonoBehaviour, IDataPersistence
{
    
    [SerializeField] public  string id = Guid.NewGuid().ToString();
    public GameObject miscItemInteractable;
    public GameObject dropPosition;
    public InventoryManager inventoryManager;
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
                
                miscItemInteractable.SetActive(true);
                miscItemInteractable.transform.position = dropPosition.transform.position;
                miscItemInteractable.transform.rotation = dropPosition.transform.rotation;
                inventoryManager.DropItem();
                
                
            }
        
    }

    public void LoadData(GameData data)
    {
       
    }

    public void SaveData(GameData data)
    {
        //
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
