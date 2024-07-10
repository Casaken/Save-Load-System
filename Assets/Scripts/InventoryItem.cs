using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//can make this a plain c# class
public class InventoryItem : MonoBehaviour
{
    
    [HideInInspector]public Item item;

    [Header("UI")]
    public Image image;


    //this may be the constructor if we're to make it a c# class?
    public void InitializeItem(Item newItem){
        item = newItem;
        image.sprite = newItem.image;
    }

}
