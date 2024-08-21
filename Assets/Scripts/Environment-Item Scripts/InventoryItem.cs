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


}

