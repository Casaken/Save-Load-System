using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject /Item")]
public class Item : ScriptableObject
{
    // public GameObject prefab;
    public int id;
    public Sprite image;
    public GameObject itemGameObject;

    public ItemType itemType;
}


public enum ItemType{
    Misc,
    Key,
    Weapon,

}
