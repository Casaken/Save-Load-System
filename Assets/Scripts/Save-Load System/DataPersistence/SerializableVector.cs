using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableVector3
{
    public float x;
    public float y;
    public float z;

    public SerializableVector3() {}

    //Since I had problems serializing the default Vector3s I had to do a workaround like this. Basically it takes vector3 axis and just copies them in itself
    public SerializableVector3(float rX, float rY, float rZ)
    {
        x = rX;
        y = rY;
        z = rZ;
    }

    public SerializableVector3(Vector3 v3)
    {
        x = v3.x;
        y = v3.y;
        z = v3.z;
    }

    //To be able to write to Vector3s its values. I made a simple method to copy its values.       
    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z); }
    
}

