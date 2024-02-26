using UnityEngine;
using System.Collections.Generic;

public class PoolManager : Singleton<PoolManager>
{
    Dictionary<string, List<GameObject>> pool;
    Transform poolParent;

    private void Awake()
    {
        //Inicializamos la pool
        pool = new Dictionary<string, List<GameObject>>();
        poolParent = new GameObject("Pool Parent").transform;
    }

    public void Load(GameObject prefab, int quantity = 1)
    {
        var goName = prefab.name;
        if (!pool.ContainsKey(goName))
        {
            pool[goName] = new List<GameObject>();
        }
        for (int i = 0; i < quantity; i++)
        {
            var go = Instantiate(prefab);
            go.name = prefab.name;
            go.transform.SetParent(poolParent);
            go.SetActive(false);
            pool[goName].Add(go);
        }
    }

    public GameObject Spawn(GameObject prefab)
    {
        if (!pool.ContainsKey(prefab.name) || pool[prefab.name].Count == 0)
        {
            Load(prefab);
        }
        var l = pool[prefab.name];
        var go = l[0];
        l.RemoveAt(0);
        go.SetActive(true);
        go.transform.SetParent(null);
        return go;
    }

    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
       var go = Spawn(prefab); 
       var t = go.transform;
       t.position = position;
       t.rotation = rotation;

       return go;
    }

    public void Despawn(GameObject go)
    {
        if (!pool.ContainsKey(go.name))
        {
            pool[go.name] = new List<GameObject>();
        }
        go.SetActive(false);
        go.transform.SetParent(poolParent);
        pool[go.name].Add(go);
    }
}