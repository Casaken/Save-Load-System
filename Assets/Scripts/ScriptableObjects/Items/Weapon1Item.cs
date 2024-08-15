using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1Item : MonoBehaviour
{
    public Item item;
    public bool isInHand;
    [SerializeField] public InventoryManager inventoryManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            isInHand = true;
            if (isInHand && Input.GetKeyDown(KeyCode.E))
            {
                inventoryManager.GetSelectedItem(true);
                Debug.Log($"Used {item}");
            }
        }
    }
    
    
}
