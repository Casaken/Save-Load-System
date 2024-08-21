using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteractable : Interactable ,IDataPersistence
{
    [SerializeField] public  string id = Guid.NewGuid().ToString();
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
    private void OnEnable()
    {
        // DataPersistenceManager.instance.RegisterDataPersistenceObject(this);
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

    public void LoadData(GameData data)
    {
        // // Find the saved data for this KeyItem using its ID
        var gameObj = data.gameObjects.Find(obj => obj._id == id);
        //
        if (item != null)
        {
            
            transform.localPosition = gameObj._position.ToVector3();
            transform.eulerAngles = gameObj._rotation.ToVector3();
        
            
            gameObject.SetActive(gameObj.isActive);
        
            
        }
    }

    public void SaveData(GameData data)
    {
        
        SerializableGameObject serializableObject = new SerializableGameObject
        {
            //first _id is the SerializableGameObject's id.
            _id = id,
            _position = new SerializableVector3(transform.position),
            _rotation = new SerializableVector3(transform.eulerAngles),
            isActive = gameObject.activeSelf
        };
        
        //I can maybe add the objects to the list on OnEnable instead of doing this?
        if (data.gameObjects == null)
        {
            data.gameObjects = new List<SerializableGameObject>();
        }
        
        
        //This here checks if the item is already in the save list.
        //by checking for the id of the object. The ID I set myself.
        var existingObject = data.gameObjects.Find(existingObject => existingObject._id == id);
        if (existingObject != null)
        {
            //if yes just delete it.
            data.gameObjects.Remove(existingObject);  
        }
        //if not add the object.
        data.gameObjects.Add(serializableObject);  
    }
}
