using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData 
{
    public SerializableVector3 playerPosition;
    public List<InventoryData> items;
    public GameData(){
        playerPosition = new SerializableVector3();
        items = new List<InventoryData>();
    }

}

