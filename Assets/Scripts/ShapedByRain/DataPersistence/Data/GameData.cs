using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData 
{
    // public List<InventoryItem> inventoryItems;
    public int PlayerId;
    public SerializableVector3 playerPosition;
    public List<InventoryItem> inventoryItems;

    public List<InventoryData> items;
    

    public GameData(){
        this.playerPosition = new SerializableVector3();
        items = new List<InventoryData>();
    }

}

