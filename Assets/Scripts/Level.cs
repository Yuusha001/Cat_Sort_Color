using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level")]
public class Level : ScriptableObject
{
    public int numberOfCages;

    public List<gameData> gameDatas;
}

[System.Serializable]
public class gameData
{
    public List<Cats> Type;
}

public enum Cats
{
    Cat0,
    Cat1,
    Cat2,
    Cat3
}
