using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI Elements")]
    [SerializeField]
    Button plusOneBtn;

    [SerializeField]
    Button nextLvBtn;

    [SerializeField]
    Button redoBtn;

    [Header("Gameplay Elements")]
    [SerializeField]
    int currentLv;

    [SerializeField]
    List<CageController> AllCages;

    [Header("Level Elements")]
    [SerializeField]
    List<Level> Levels;

    [SerializeField]
    int numberOfCages;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpStage(currentLv);
    }

    void SetUpStage(int Lv)
    {
        this.numberOfCages = Levels[Lv].numberOfCages;
        foreach (var item in AllCages)
        {
            item.gameObject.SetActive(item.cageID < numberOfCages);
            if (item.cageID < numberOfCages)
            {
                var data = Levels[Lv].gameDatas[item.cageID].Type;
                Cats[] temp = new Cats[data.Count];
                data.CopyTo(temp);
                item.CageData = temp.ToList();
            }
        }
    }

    // Update is called once per frame
    void Update() { }

    public void plusOneBtnClick()
    {
        plusOneBtn.gameObject.SetActive(false);
        foreach (var item in AllCages)
        {
            if (item.cageID == numberOfCages)
                item.gameObject.SetActive(true);
        }
    }

    public CageController getCagebyID(int ID)
    {
        foreach (var item in AllCages)
        {
            if (item.cageID == ID)
            {
                return item;
            }
        }
        return null;
    }
}

public enum GameState
{
    Prepare,
    Playing,
    Complete
}

public enum ProgressingAction
{
    Matching,
    NotMatch
}

public class Actions
{
    public static Action<int> Sender;
    public static Action CompleteSending;
}
