using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableRotation
{
    public float x;
    public float y;
    public float z;

    public SerializableRotation() {}

    public SerializableRotation(float rX, float rY, float rZ)
    {
        x = rX;
        y = rY;
        z = rZ;
    }

    public SerializableRotation(Vector3 v3)
    {
        x = v3.x;
        y = v3.y;
        z = v3.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3 (x, y, z); 
    }
    
}


