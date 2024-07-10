using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject /Item")]
public class Item : ScriptableObject
{
    public int id;
    public Sprite image;

    public ItemType itemType;
}


public enum ItemType{
    Misc,
    Key,
    Weapon,

}
