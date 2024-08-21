using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereInteractable : Interactable
{
    [SerializeField] private InventoryManager inventoryManager;

    
    private bool isInHand = false;
    [SerializeField] private Transform ground;
    [SerializeField] private Transform handPosition;
    [SerializeField] public Item item;
    private MyFirstPersonController player;
    private Collider _collider;
    [SerializeField] public GameObject prefab;

    [SerializeField] private LayerMask groundLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<MyFirstPersonController>().GetComponent<MyFirstPersonController>();
        _collider = GetComponent<Collider>();
    }


    // Update is called once per frame
    void Update()
    {
        bool canDrop =Physics.Raycast(transform.position, Vector3.down,out RaycastHit hit, 10f);
        if (isInHand && Input.GetKeyDown(KeyCode.Q) && canDrop)
        {
            
            transform.SetParent(null);
            transform.position = new Vector3(hit.point.x, hit.point.y +_collider.bounds.extents.y/2 , hit.point.z);
            transform.rotation =Quaternion.identity;
            inventoryManager.DropItem();
        }

       
    }

    public override void OnInteract()
    {
        inventoryManager.AddItem(item);
        Debug.Log($"Item {item} added");
        gameObject.transform.localPosition = handPosition.transform.localPosition;
        gameObject.transform.SetParent(handPosition.transform,false);
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
        isInHand = true;
    }

    public override void OnFocus()
    {
        Debug.Log($"Focused on {gameObject}");
    }

    public override void OnLoseFocus()
    {
        Debug.Log($"Lost focus on {gameObject}");
    }
}
