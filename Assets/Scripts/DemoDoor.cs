using System;
using System.Collections;
using UnityEngine;

public class DemoDoor : Interactable, IDataPersistence
{
    [SerializeField] public Animator animator;
    [SerializeField] public InventoryManager inventoryManager;
    [SerializeField] public  string id = Guid.NewGuid().ToString();
    [SerializeField] public Item item;
    [SerializeField] public Transform handle;
    public bool isUnlocked;
    
    public bool isOpenState;
    public bool isClosedState;
    public bool isIdleState;
    public bool isOpen;

    public bool isActive;


    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        isIdleState = animatorStateInfo.shortNameHash == Animator.StringToHash("Idle");
        Debug.Log($"{isIdleState} IS IDLE?");
        isOpen = false;
        isUnlocked = false;

    }
    
    // Update is called once per frame
    void Update()
    {
         
    }

    public override void OnInteract()
    {
        
        if (isOpen && isUnlocked)
        {
            
            StartCoroutine(CloseDoorAnimation());
        }

        if (!isOpen && isUnlocked)
        {
            
            StartCoroutine(OpenDoorAnimation());
        }
       
        Item itemInSlot = inventoryManager.GetSelectedItem(false);
        if (itemInSlot == null && !isUnlocked)
        {
            Debug.Log($"You do not hold anything in your hand let alone A KEY!");
            return;
        }
        if (itemInSlot != null && !isUnlocked && !isOpen)
        {
            if (itemInSlot.itemType != ItemType.Key)
            {
                Debug.Log($"This is not a KEY!");
            }
            itemInSlot.itemType = inventoryManager.GetSelectedItem(false).itemType;
            if (itemInSlot.itemType == ItemType.Key)
            {
                inventoryManager.GetSelectedItem(true);
                Debug.Log($"You have the key, opening door.");
                
                StartCoroutine(OpenDoorAnimation());
                
                isUnlocked = true;
                
            }

        }
         
        if (itemInSlot == null && !isUnlocked && !isOpen)
        {
            Debug.Log($"You need a key to Open this door.");
        }

        

         
        
         
    }

    public override void OnFocus()
    {
        
    }

    public override void OnLoseFocus()
    {
        
    }

    public void SaveData(GameData data)
    {
        animator.enabled = false;
        AnimatorStateInfo currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        isClosedState = currentStateInfo.shortNameHash == Animator.StringToHash("doorClose");
        isIdleState = currentStateInfo.shortNameHash == Animator.StringToHash("Idle");
        isOpenState = currentStateInfo.shortNameHash == Animator.StringToHash("doorOpen");
        
        data.isUnlocked = isUnlocked;
        data.isOpenState = isOpenState;
        data.isClosedState = isClosedState;
        data.isIdleState = isIdleState;
        data.handleRotation = new SerializableQuaternion(handle.transform.rotation);
        data.isOpen = isOpen;
        Debug.Log($"Saved Rotation: {handle.transform.rotation}");
        
        StartCoroutine(AnimatorActivationRoutine());
    }

    public void LoadData(GameData data)
    {
        
        handle.transform.rotation = data.handleRotation.ToQuaternion();
        Debug.Log($"Loaded Rotation: {handle.transform.rotation}");
        AnimatorStateInfo currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        isClosedState = currentStateInfo.shortNameHash == Animator.StringToHash("doorClose");
        isIdleState = currentStateInfo.shortNameHash == Animator.StringToHash("Idle");
        isOpenState = currentStateInfo.shortNameHash == Animator.StringToHash("doorOpen");

       
        
        isClosedState = data.isClosedState;
        isIdleState = data.isIdleState;
        isOpenState = data.isOpenState;
        isOpen = data.isOpen;
        isUnlocked = data.isUnlocked;
        
        if (isOpenState)
        {
            
            
            animator.Play("doorOpen",0 ,0);
            
            
        }
        else if (isClosedState)
        {
            animator.Play("doorClose", 0, 0);
        }
        
        animator.enabled = true;
        
        
    }


    IEnumerator AnimatorActivationRoutine()
    {
        
        animator.enabled = true;
        yield return null;
    }

    public IEnumerator OpenDoorAnimation()
    {
        animator.SetTrigger("Open");
        yield return new WaitForSeconds(1f);
        AnimatorStateInfo currentStateInfo = animator.GetCurrentAnimatorStateInfo(0); 
        isOpenState = currentStateInfo.shortNameHash == Animator.StringToHash("doorOpen");
        Debug.Log($"{isOpenState} = DOOR IS OPEN?");
        yield return null;
        isOpen = true;
    }

    public IEnumerator CloseDoorAnimation()
    {
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(1f);
        AnimatorStateInfo currentStateInfo = animator.GetCurrentAnimatorStateInfo(0); 
        //if its in openState it takes the currentState's hash compares it to doorOpen string's hash. Returns true or false accordingly. I guess?
        isClosedState = currentStateInfo.shortNameHash == Animator.StringToHash("doorClose");
        Debug.Log($"{isOpenState} = DOOR IS CLOSED");
        yield return null;
        isOpen = false;
    }
}