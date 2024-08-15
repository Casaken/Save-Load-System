// using UnityEngine;
// using Vector3 = UnityEngine.Vector3;
//
// public class CubeInteractable : Interactable
// {
//     [SerializeField] private InventoryManager inventoryManager;
//
//     // [FormerlySerializedAs("itemToPickup")] public Item item;
//     private bool isInHand = false;
//     // [SerializeField] private GameObject prefab;
//     [SerializeField] private Transform ground;
//     [SerializeField] private Transform handPosition;
//     [SerializeField] public Item item;
//     public GameObject keyGameObject;
//     [SerializeField] public InventoryItem inventoryItem;
//     private MyFirstPersonController player;
//     private Collider _collider;
//
//     [SerializeField] private LayerMask groundLayerMask;
//     // Start is called before the first frame update
//     void Start()
//     {
//         player = FindObjectOfType<MyFirstPersonController>().GetComponent<MyFirstPersonController>();
//         _collider = GetComponent<Collider>();
//     }
//
//
//     // Update is called once per frame
//     void Update()
//     {
//         bool canDrop =Physics.Raycast(transform.position, Vector3.down,out RaycastHit hit, 10f);
//         if (isInHand && Input.GetKeyDown(KeyCode.Q) && canDrop)
//         {
//             // item = inventoryManager.GetSelectedItem(true);
//             transform.SetParent(null);
//             transform.position = new Vector3(hit.point.x, hit.point.y +_collider.bounds.extents.y/2 , hit.point.z);
//             transform.rotation =Quaternion.identity;
//             // inventoryManager.GetSelectedItem(true);
//             inventoryManager.DropItem();
//         }
//
//        
//     }
//
//     public override void OnInteract()
//     {
//             Destroy(gameObject);
//             inventoryManager.AddItem(item);
//             keyGameObject.gameObject.SetActive(true);
//             // GameObject instantiatedObject = Instantiate(prefab, handPosition);
//             // inventoryItem.InitializeSceneObject(instantiatedObject);
//             // instantiatedObject.gameObject.SetActive(true);
//             Debug.Log($"Item {item} added");
//             // instantiatedObject.transform.localPosition = handPosition.transform.localPosition;
//             // instantiatedObject.transform.SetParent(handPosition.transform,false);
//             // instantiatedObject.transform.localPosition = new Vector3(0, 0, 0);
//             // gameObject.transform.localPosition = handPosition.transform.localPosition;
//             // gameObject.transform.SetParent(handPosition.transform,false);
//             // gameObject.transform.localPosition = new Vector3(0, 0, 0);
//             isInHand = true;
//     }
//
//     public override void OnFocus()
//     {
//         Debug.Log($"Focused on {gameObject}");
//     }
//
//     public override void OnLoseFocus()
//     {
//         Debug.Log($"Lost focus on {gameObject}");
//     }
//    
// }
