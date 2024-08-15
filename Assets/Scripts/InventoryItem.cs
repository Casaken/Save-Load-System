using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;


//can make this a plain c# class
public class InventoryItem : MonoBehaviour
{
    public int Id;
    [HideInInspector]public Item item;
    [HideInInspector]public GameObject inventoryItemGameObject;
    [Header("UI")]
    public Image image;

    //use this as a reference when saving and loading.
    // public InventoryData itemData;


    //this may be the constructor if we're to make it a c# class?
    public void InitializeItem( int _id,Item newItem){
        Id = _id;
        item = newItem;
        image.sprite = newItem.image;
    }
    public void InitializeItem(Item newItem){

        Debug.Log($" New Item is {newItem}");
        Debug.Log($"Image is {image}");
        item = newItem;
        image.sprite = newItem.image;

    }

    public void InitializeSceneObject(GameObject sceneGameObject)
    {
        inventoryItemGameObject = sceneGameObject;
    }

    // public void InitializeItem(Item newItem, GameObject gameObjectInScene)
    // {
    //     item = newItem;
    //     image.sprite = newItem.image;
    //     itemGameObject = gameObjectInScene;
    // }


    // //call this when loading inventory instead of initializing I guess...
    // public void LoadItems( int _id,Item loadedItem){
    //     Id = _id;
    //     item = loadedItem;
    //     image.sprite = loadedItem.image;
    // }

}

