using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    public static ConfigManager instance { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    public Sprite LoadCatsImagebyCodeName(Cats codeName)
    {
        var link = "Textures/Cats/" + codeName;
        var img = Resources.Load<Sprite>(link);
        return img;
    }

    public Sprite LoadShipsImagebyCodeName(SpaceLifts codeName)
    {
        var link = "Textures/SpaceLifts/" + codeName;
        var img = Resources.Load<Sprite>(link);
        return img;
    }
}
