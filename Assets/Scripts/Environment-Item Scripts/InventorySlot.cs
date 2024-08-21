using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image image;
    public Color selectedColor , notSelectedColor;
    public InventorySlot slot;
    private void Awake()
    {
        Deselect();
    }
    public void Select()
    {
        image.color = selectedColor;
    }

    public void Deselect(){
        
        image.color = notSelectedColor;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
