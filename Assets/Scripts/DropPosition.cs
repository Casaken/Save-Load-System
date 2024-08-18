using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.8f);
        
        Gizmos.DrawCube(transform.position, new Vector3(.4f, .4f, .4f));
        
    }

    
}
