using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData 
{
    //Classes that are called Serializable are just modified for being able to save them as json.
    public SerializableVector3 playerPosition;
    public List<InventoryData> items;
    public List<SerializableGameObject> gameObjects;
    public SerializableQuaternion handleRotation;
    public bool isOpenState;
    public bool isIdleState;
    public bool isClosedState;
    public bool isUnlocked;
    
    public bool isOpen;
    public GameData(){
        //Creating new data here.
        playerPosition = new SerializableVector3();
        items = new List<InventoryData>();
        gameObjects = new List<SerializableGameObject>();
    }

}

