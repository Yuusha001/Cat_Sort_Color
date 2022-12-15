using System.Collections.Generic;
using UnityEngine;

public class ObjPooling : MonoBehaviour
{
    public static ObjPooling SharedInstance;

    [Header("Obj pooling elemets")]
    public Dictionary<string, List<GameObject>> ListObjPooling;

    void Awake()
    {
        SharedInstance = this;
        ListObjPooling = new Dictionary<string, List<GameObject>>();
    }

    void Start()
    {
        // objectToPool = Resources.Load("FX/Bullet_BOSS") as GameObject;
    }

    public void CreatePool(string keyName, GameObject go, Transform par, int amountToPool)
    {
        List<GameObject> tempList = new List<GameObject>();
        for (var i = 0; i < amountToPool; i++)
        {
            var temp = Instantiate(go, par);
            tempList.Add(temp);
            temp.SetActive(false);
        }
        //Debug.Log(tempList.Count);
        ListObjPooling.Add(keyName, tempList);
    }

    public GameObject GetPooledObject(string keyName)
    {
        return ListObjPooling[keyName].Find(item => !item.activeInHierarchy);
        // foreach (var item in ListObjPooling)
        // {
        //     if (item.Key == keyName && item.Value.Count > 0)
        //     {
        //         for (int i = 0; i < item.Value.Count; i++)
        //         {
        //             if (!item.Value[i].activeInHierarchy)
        //             {
        //                 return item.Value[i];
        //             }
        //         }
        //     }
        // }
        // return null;
    }

    public void DisableListItems(string keyName)
    {
        foreach (var item in ListObjPooling[keyName])
        {
            item.SetActive(false);
        }
    }
}
