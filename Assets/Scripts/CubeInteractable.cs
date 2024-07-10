using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CubeInteractable : Interactable
{
    // [SerializeField] private InventoryManager inventoryManager;

    // [FormerlySerializedAs("itemToPickup")] public Item item;
    private bool isInHand = false;
    [SerializeField] private Transform ground;
    [SerializeField] private Transform handPosition;
    // public InventoryObject inventory;
    // private item Item;
    private MyFirstPersonController player;
    private Collider _collider;

    [SerializeField] private LayerMask groundLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        // Item = GetComponent<item>();
        player = FindObjectOfType<MyFirstPersonController>().GetComponent<MyFirstPersonController>();
        _collider = GetComponent<Collider>();
    }


    // Update is called once per frame
    void Update()
    {
        bool canDrop =Physics.Raycast(transform.position, Vector3.down,out RaycastHit hit, 10f);
        // if (isInHand && Input.GetKeyDown(KeyCode.Q) && canDrop)
        // {
        //     item = inventoryManager.GetSelectedItem(true);
        //     transform.SetParent(null);
        //     transform.position = new Vector3(hit.point.x, hit.point.y +_collider.bounds.extents.y/2 , hit.point.z);
        //     transform.rotation =Quaternion.identity;
        //     inventoryManager.DropItem(item);
        // }

        // foreach (var slot in inventory.Container)
        // {
        //     Debug.Log(slot.ID);
        // }
       
    }

    public override void OnInteract()
    {
            // inventoryManager.AddItem(item);
            // Debug.Log($"Item {item} added");


            // if (player)
            // {
            //     inventory.AddItem(Item.Item,1 );
            // }
            
            
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
