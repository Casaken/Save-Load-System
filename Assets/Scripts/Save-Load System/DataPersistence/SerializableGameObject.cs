using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableGameObject
{

    //This is a class to be able to save the GameObject's with specified id rotation and position in world. So whenever we load back into our game. They will remember where they were.

    public string _id;
    public SerializableVector3 _position;
    public SerializableVector3 _rotation;
    public bool isActive;
    

}
