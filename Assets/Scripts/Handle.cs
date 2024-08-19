using UnityEngine;

public class Handle : MonoBehaviour,IDataPersistence
{
    [SerializeField] public Transform childDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadData(GameData data)
    {
        
        // Debug.Log($"Loading values for HANDLE = {data.handleRotation.ToVector3()}");
        // transform.eulerAngles = data.handleRotation.ToVector3();
        
        
    }
    
    public void SaveData(GameData data)
    {
        // Debug.Log($" Saving values for HANDLE = {transform.eulerAngles}");
        // //Converting this gameObject's rotation to SerializableVector3 here.
        // data.handleRotation = new SerializableVector3(transform.rotation.eulerAngles);
        
        
    }
}
