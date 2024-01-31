using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [SerializeField] private string id;
    [SerializeField] private string itemName;
    [SerializeField] private Sprite icon;
    [Range(1, 999)]
    [SerializeField] private int maximumStacks = 1;
    [SerializeField] private int price;

    public string ID { get { return id; } }
    public string ItemName { get { return itemName; } }
    public Sprite Icon { get { return icon; } }
    public int MaximumStacks { get { return maximumStacks; } }
    public int Price { get { return price; } }

#if UNITY_EDITOR
    public void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }
#endif

    public virtual Item GetCopy()
    {
        return this;
    }

    public virtual void Destroy()
    {

    }
}