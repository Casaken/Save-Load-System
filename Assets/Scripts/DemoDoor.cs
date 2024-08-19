using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoDoor : Interactable, IDataPersistence
{
    [SerializeField] public Animator animator;
    [SerializeField] public InventoryManager inventoryManager;
    [SerializeField] public  string id = Guid.NewGuid().ToString();
    [SerializeField] public Item item;
    [SerializeField] public GameObject handle;
    
    public bool isOpen;

    public bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
    }
    // private void OnEnable()
    // {
    //     DataPersistenceManager.instance.RegisterDataPersistenceObject(this);
    // }
    //
    // private void OnDisable()
    // {
    //     DataPersistenceManager.instance.UnregisterDataPersistenceObject(this);
    // }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInteract()
    {
        if (isOpen)
        {
            animator.SetTrigger("Close");
            // animator.Play("doorClose",0,0);
            isOpen = false;
        }
       
        Item itemInSlot = inventoryManager.GetSelectedItem(false);
        if (itemInSlot == null)
        {
            Debug.Log($"You do not hold anything in your hand let alone A KEY!");
            return;
        }
        if (itemInSlot != null)
        {
            itemInSlot.itemType = inventoryManager.GetSelectedItem(false).itemType;
            if (itemInSlot.itemType == ItemType.Key)
            {
                inventoryManager.GetSelectedItem(true);
                Debug.Log($"You have the key, opening door.");
                // gameObject.SetActive(false);

                if (!isOpen)
                {
                    StartCoroutine(DoorTimerCoroutine());
                }
            }

        }
        
         
        if (itemInSlot == null)
        {
            Debug.Log($"You need a key to Open this door.");
        }

        if (itemInSlot.itemType != ItemType.Key)
        {
            Debug.Log($"This is not a KEY!");
        }

         
        // item.itemType = inventoryManager.GetSelectedItem(false).itemType;
         
    }

    public override void OnFocus()
    {
        
    }

    public override void OnLoseFocus()
    {
        
    }

    public void LoadData(GameData data)
    {
        transform.eulerAngles = data.doorPosition.ToVector3();
        isOpen = data.isOpen;
       
    }

    public void SaveData(GameData data)
    {
        data.doorPosition = new SerializableVector3(transform.eulerAngles);
        data.isOpen = isOpen;
        
    }

    IEnumerator DoorTimerCoroutine()
    {
        // animator.ResetTrigger("doorClose");
        // animator.Play("doorOpen",0,0);
        animator.SetTrigger("Open");
        yield return new WaitForSeconds(1f);
        isOpen = true;
    }
}
