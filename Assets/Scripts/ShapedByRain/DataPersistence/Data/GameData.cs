using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData 
{
    public SerializableVector3 playerPosition;
    public List<InventoryData> items;
    public List<SerializableGameObject> gameObjects;
    public GameData(){
        playerPosition = new SerializableVector3();
        items = new List<InventoryData>();
        gameObjects = new List<SerializableGameObject>();
    }

}

