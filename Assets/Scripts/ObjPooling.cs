using System.Collections.Generic;
using UnityEngine;

public class ObjPooling : MonoBehaviour
{
    public static ObjPooling SharedInstance;

    [Header("Obj pooling elemets")]
    public Dictionary<GameObject, List<GameObject>> ListObjPooling;
    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        // objectToPool = Resources.Load("FX/Bullet_BOSS") as GameObject;
        foreach (var item in ListObjPooling)
        {
            for (var i = 0; i < amountToPool; i++)
            {
                var tmp = Instantiate(item.Key);
                tmp.gameObject.SetActive(false);
                item.Value.Add(tmp);
                tmp.transform.parent = this.transform;
            }
        }
    }

    public GameObject GetPooledObject(GameObject key)
    {
        foreach (var item in ListObjPooling)
        {
            if (item.Key == key)
            {
                if (item.Value.Count > 0)
                {
                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        if (!item.Value[i].activeInHierarchy)
                        {
                            return item.Value[i];
                        }
                    }
                }
            }
        }
        return null;
    }
}
