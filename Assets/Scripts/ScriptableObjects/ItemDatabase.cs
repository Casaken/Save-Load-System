using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item Database", menuName ="Inventory System/Database")]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
   public Item[] items;
   public Dictionary<Item, int> GetId = new Dictionary<Item, int>();
   public Dictionary <int, Item> GetItem = new Dictionary<int, Item>();

//    public List<int> ids = new List<int>();
//    public List<Item> Items = new List<Item>();

//    public List<int> GetItemIds(List<Item> items){
        
//         for (int i = 0; i < items.Count; i++)
//         {
            
//             ids.Add(items[i].id);
            
//         }
//         return ids;
//    }

//    public List<Item> GetItemsById(List<int> itemIds){
//     return Items;

//    }
    public void OnAfterDeserialize()
    {
        for (int i = 0; i < items.Length; i++)
        {
            GetId.Add(items[i], i);
            GetItem.Add(i , items[i]);
           foreach (var item in GetId)
           {
                GetId.TryGetValue(items[i], out int Id);
                Debug.Log(Id);    
           }
        }
    }

    public void OnBeforeSerialize()
    {
        
    }
}
