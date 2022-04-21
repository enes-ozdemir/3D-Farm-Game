using Farm.Entity;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public List<GameObject> seedPrefabs = new List<GameObject>();

#nullable enable
    public GameObject? GetPrefabyByName(string name)
    {
        return seedPrefabs.Find(prefab =>
        {
            IEntity entity = prefab.GetComponent<IEntity>();
            if (entity != null)
            {
                return entity.Name == name;
            }
            return false;
        });
    }
#nullable disable
}
