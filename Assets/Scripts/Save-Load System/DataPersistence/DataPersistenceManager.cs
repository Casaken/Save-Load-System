using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataPersistenceManager : MonoBehaviour
{
    private GameData gameData;
    [SerializeField] private string fileName;
    private FileDataHandler dataHandler;
    private List<IDataPersistence> dataPersistenceObjects;
   public static DataPersistenceManager instance {get ; private set;}


   private void Start() {
    this.dataHandler = new FileDataHandler(Application.persistentDataPath,fileName);
    this.dataPersistenceObjects = FindAllDataPersistenceObjects();
    Debug.Log("Starting!!");
    NewGame();
    }

    
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        //this here finds the inactive objects. Thats why I replaced the previous one.
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistence>();
        // Find all MonoBehaviour objects that implement IDataPersistence
        // IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        // Find all ScriptableObject instances that implement IDataPersistence
        IEnumerable<IDataPersistence> scriptableObjects = Resources.FindObjectsOfTypeAll<ScriptableObject>().OfType<IDataPersistence>();

        // Combine both collections
        dataPersistenceObjects = dataPersistenceObjects.Concat(scriptableObjects);

        // Return the combined collection as a List
        return dataPersistenceObjects.ToList();
    }


    private void Awake() {
    if(instance != null){
        Debug.LogError("Found more than one Data Persistence Manager in the scene");
        Destroy(gameObject);
    }
    instance = this;
   }


   public void NewGame(){
        this.gameData = new GameData();
   }

   public void LoadGame()
   {
    this.gameData = dataHandler.Load();
    Debug.Log("Loaded Game");
    if(this.gameData == null){
        Debug.Log("No data was found. Initializing data to defaults");
        NewGame();
    }

    foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
    {
        dataPersistenceObj.LoadData(gameData);
    }

   }
   

   public void SaveGame(){
       Debug.Log("Saved Game");
    foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
    {
        dataPersistenceObj.SaveData(gameData);
    }

    dataHandler.Save(gameData);
   }
   
   public void RegisterDataPersistenceObject(IDataPersistence obj)
   {
       if (!dataPersistenceObjects.Contains(obj))
       {
           dataPersistenceObjects.Add(obj);
       }
       
   }
   
}

