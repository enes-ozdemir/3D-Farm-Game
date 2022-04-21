using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [SerializeField] string id;
    [Range(1,16)]
    public int maximumStacks = 1;
    public string ID
    {
        get { return id; }
    }
    public string itemName;
    public Sprite icon;
    public int value;

    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }

    public virtual Item GetCopy()
    {
        return this;
    }  
    public virtual void Destroy()
    {
        
    }
}
