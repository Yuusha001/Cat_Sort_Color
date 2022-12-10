using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField]
    Button plusOneBtn;

    [SerializeField]
    Button nextLvBtn;

    [SerializeField]
    Button redoBtn;

    [Header("Gameplay Elements")]
    [SerializeField]
    List<CageController> RightCages;

    [SerializeField]
    List<CageController> LeftCages;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}

public enum GameState
{
    Prepare,
    Playing,
    Complete
}

public class Actions
{
    public static Action<int> Sender;
    public static Action CompleteSending;
}
