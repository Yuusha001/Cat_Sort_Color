using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level")]
public class Level : ScriptableObject
{
    public int numberOfCages;
    public string levelName = "";

    public List<gameData> gameDatas;
}

[System.Serializable]
public class gameData
{
    public List<Cats> Type;
}

public enum Cats
{
    Cat_0,
    Cat_1,
    Cat_2,
    Cat_3,
    Cat_4,
    Cat_5,
    Cat_6
}
