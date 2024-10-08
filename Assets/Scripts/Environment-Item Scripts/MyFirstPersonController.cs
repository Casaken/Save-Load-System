using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;


[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class MyFirstPersonController : MonoBehaviour, IDataPersistence
    {
        public bool isMenuEnabled;
        public bool isLoading;
        [SerializeField] public GameData playerData;
        // public InventoryObject inventory;
        public void LoadData(GameData data)
        {
            StartCoroutine(DisableMovements());
            transform.position = data.playerPosition.ToVector3();
            Debug.Log("Loaded player position");

        }

        public void SaveData(GameData data)
        {
            StartCoroutine(DisableMovements());
            data.playerPosition = new SerializableVector3(transform.position);
        }
        
        IEnumerator DisableMovements()
        {
            isLoading = true;
            yield return new WaitForSeconds(1);
            isLoading = false; 
        }


        public bool CanMove { get; private set; } = true;
        private bool IsSprinting => canSprint && Input.GetKey(sprintKey);

        [Header("Functional Options")] [SerializeField]
        private bool canSprint = true;

        [SerializeField] private bool _willSlideOnSlopes = true;
        [SerializeField] private bool canInteract = true;
        [SerializeField] private bool useFootsteps = true;
        [SerializeField] public bool canMouseLook = true;

        [Header("Controls")] [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
        [SerializeField] private KeyCode interactKey = KeyCode.E;

        [FormerlySerializedAs("walkSpeed")] [Header("Movement Parameters")] [SerializeField]
        private float _walkSpeed = 3f;

        [SerializeField] private float _sprintSpeed = 6f;
        [SerializeField] private float gravity = 30f;
        [SerializeField] private float _slopeSpeed = 8f;

        [Header("Look Parameters")] [SerializeField, Range(1, 10)]
        private float lookSpeedX = 2f;

        [SerializeField, Range(1, 10)] private float lookSpeedY = 2f;
        [SerializeField, Range(1, 180)] private float upperLookLimit = 80f;
        [SerializeField, Range(1, 180)] private float lowerLookLimit = 80f;
        [SerializeField] GameObject _cameraRoot;
        private Quaternion _characterTargetRotation;
        private Quaternion _cameraTargetRotation;

        // [Header("PlayerUI")] private PlayerUI _playerUI;


        //SLIDING PARAMETERS
        private Vector3 _hitPointNormal;
        [SerializeField] public InventoryManager inventoryManager;
        



        private bool IsSliding
        {
            get
            {
                if (_characterController.isGrounded &&
                    Physics.Raycast(transform.position, Vector3.down, out RaycastHit slopeHit, 2f))
                {
                    _hitPointNormal = slopeHit.normal;
                    return Vector3.Angle(_hitPointNormal, Vector3.up) >= _characterController.slopeLimit;
                }
                else
                {
                    return false;
                }

            }
        }

 

    [Header("Interaction")] [SerializeField]
        private Vector3 interactionRayPoint = default;

        [SerializeField] private float interactionDistance = default;
        [SerializeField] private LayerMask interactionLayer = default;
        private Interactable currentInteractable;

        [Header("Footstep Parameters")] [SerializeField]
        private float baseStepSpeed = .2f;

        [SerializeField] private AudioSource footstepAudioSource = default;
        [SerializeField] private AudioClip[] indoorClips = default;
        [SerializeField] private AudioClip[] forestClips = default;
        [SerializeField] private AudioClip[] streetClips = default;
        [SerializeField] private AudioClip[] defaultClips = default;
        private float footstepTimer = 0;


        private Camera _playerCamera;
        private CharacterController _characterController;

        private Vector3 _moveDirection;
        private Vector2 _currentInput;

        public float rotationX = 0;

        private void Awake()
        {

            _playerCamera = FindObjectOfType<Camera>().GetComponent<Camera>();
            
            _characterController = GetComponent<CharacterController>();
            
        }

        private void Start()
        {
            // _playerUI.UpdateText(string.Empty);
           
        }

        // Update is called once per frame
        void Update()
        {


                if (CanMove&& !isLoading &&!isMenuEnabled)
                {
                    HandleMovementInput();
                    if (canMouseLook)
                    {
                        HandleMouseLook();
                    }

                    if (useFootsteps)
                    {
                        // HandleFootsteps();
                    }

                    if (canInteract)
                    {
                        //this is gonna keep firing raycast.
                        HandleInteractionCheck();
                        //this gonna check the key for interaction.
                        HandleInteractionInput();
                    }

                    ApplyFinalMovements();
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    isMenuEnabled = true;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    
                }

                if (Input.GetKeyDown(KeyCode.Tab) && isMenuEnabled)
                {
                    isMenuEnabled = false;
                }
                


                if (Input.GetKeyDown(KeyCode.Space))
                {

                    DataPersistenceManager.instance.SaveGame();
                }

                if (Input.GetKeyDown(KeyCode.C))
                {

                    DataPersistenceManager.instance.LoadGame();
                }

                if (Input.GetKeyDown(KeyCode.X))
                {
                    Debug.Log("Clearing Inventory");
                    inventoryManager.ResetScene();
                    
                    
                }

                if (Input.GetKeyDown(KeyCode.Z))
                {
                    inventoryManager.ClearInventory();
                }

                if (Input.GetKeyDown(KeyCode.N))
                {
                    Debug.Log("IsLoading, stopping playermovement");
                    isLoading = true;
                }
                
                if (Input.GetKeyDown(KeyCode.M))
                {
                    Debug.Log("IsLoading false, enabling movement");
                    isLoading = false;
                }
                
        }


        private void HandleMovementInput()
        {
            _currentInput = new Vector2((IsSprinting ? _sprintSpeed : _walkSpeed) * Input.GetAxis("Vertical"),
                (IsSprinting ? _sprintSpeed : _walkSpeed) * Input.GetAxis("Horizontal"));

            float moveDirectionY = _moveDirection.y;
            _moveDirection = (transform.TransformDirection(Vector3.forward) * _currentInput.x) +
                             (transform.TransformDirection(Vector3.right) * _currentInput.y);
            _moveDirection = _moveDirection.normalized * Mathf.Clamp(_moveDirection.magnitude, 0, _walkSpeed);
            _moveDirection.y = moveDirectionY;

        }

        private void HandleMouseLook()
        {
            //this here is for rotating the camera. We set up a cinemachine camera root. Then we rotate the camera root instead.
            rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
            rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
            // _playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            _cameraRoot.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            //------------
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
        }

        private void HandleInteractionCheck()
        {
            if (Physics.Raycast(_playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit,
                    interactionDistance) && hit.collider.gameObject.layer == 7)
            {
                if (hit.collider.gameObject.layer == 7 && (currentInteractable == null ||
                                                           hit.collider.gameObject.GetInstanceID() !=
                                                           currentInteractable.gameObject.GetInstanceID()))
                {
                    hit.collider.TryGetComponent(out currentInteractable);
                    if (currentInteractable)
                    {
                        currentInteractable.OnFocus();
                    }
                }
            }
            else if (currentInteractable)
            {

                currentInteractable.OnLoseFocus();
                currentInteractable = null;
            }
        }

        private void HandleInteractionInput()
        {
            if (Input.GetKeyDown(interactKey) && currentInteractable != null && Physics.Raycast(
                    _playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance,
                    interactionLayer))
            {
                currentInteractable.OnInteract();
            }
        }

        private void ApplyFinalMovements()
        {
            if (!_characterController.isGrounded)
            {
                _moveDirection.y -= gravity * Time.deltaTime;
            }

            if (_willSlideOnSlopes && IsSliding)
            {
                _moveDirection += new Vector3(_hitPointNormal.x, -_hitPointNormal.y, _hitPointNormal.z) * _slopeSpeed;
            }

            _characterController.Move(_moveDirection * Time.deltaTime);
        }

        public void MouseLookInit()
        {
            rotationX = 0;
        }

}

