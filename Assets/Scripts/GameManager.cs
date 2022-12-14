using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Newtonsoft.Json;

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

    [SerializeField]
    TMP_Text levelTxt;

    [SerializeField]
    GameObject shopBtn,
        PopupShop;

    [Header("Gameplay Elements")]
    [SerializeField]
    int currentLv;

    [SerializeField]
    PlayerData playerData { get; set; }

    [SerializeField]
    List<CageController> AllCages;

    [Header("Level Elements")]
    [SerializeField]
    List<Level> Levels;

    [SerializeField]
    int numberOfCages;

    [HideInInspector]
    public CageController nextCage,
        prevCage;

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
        LoadPlayerDatas();

        SetUpStage(currentLv);
    }

    // Update is called once per frame
    void Update() { }

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

#region PlayerData
    void LoadPlayerDatas()
    {
        var playerDataTxt = PlayerPrefs.GetString("PlayerData");
        if (string.IsNullOrEmpty(playerDataTxt))
        {
            this.playerData = new PlayerData();
            this.playerData.currentLevel = 0;
            this.playerData.UnlockedCharactors = new Dictionary<Cats, bool>();
            this.playerData.UnlockedSpaceLifts = new Dictionary<SpaceLifts, bool>();
        }
        else
        {
            this.playerData = JsonConvert.DeserializeObject<PlayerData>(playerDataTxt);
            currentLv = this.playerData.currentLevel;
        }
    }

    public void SavePlayerDatas()
    {
        //Set level up
        playerData.currentLevel = currentLv;
        var PlayerDataTxt = JsonConvert.SerializeObject(playerData);
        PlayerPrefs.SetString("PlayerData", PlayerDataTxt);
    }

#endregion

#region Gameplay
    void SetUpStage(int Lv)
    {
        this.numberOfCages = Levels[Lv].numberOfCages;
        this.levelTxt.text = Levels[Lv].name == "" ? "Level " + Lv : Levels[Lv].name;
        var data = Levels[Lv].gameDatas;
        for (var i = 0; i < data.Count; i++)
        {
            var Item = AllCages.Find(b => b.cageID == i);
            foreach (var item in data[Item.cageID].Type)
            {
                Item.CageData.Add(item);
            }
        }
        foreach (var item in AllCages)
        {
            item.gameObject.SetActive(item.cageID < numberOfCages);
            item.LoadImage();
        }
    }

    void CleanUp()
    {
        Steps.instance.PlayerAction1.Clear();
        foreach (var item in AllCages)
        {
            item.CageData.Clear();
            item.LoadImage();
        }
    }

    public void StageComplete()
    {
        var count = 0;
        var activeCages = 0;
        foreach (var item in AllCages)
        {
            if (item.gameObject.activeSelf)
            {
                activeCages++;
            }
        }
        foreach (var item in AllCages)
        {
            if (item.CageData.Count == 0 && item.gameObject.activeSelf)
            {
                count++;
            }
        }
        if (count == activeCages)
        {
            Debug.Log("Stage Clear");
            Invoke(nameof(LoadNextLevel), 2);
        }
    }

    public void LoadNextLevel()
    {
        CleanUp();
        var Lv = currentLv + 1;
        if (Lv > Levels.Count - 1)
        {
            Lv = 0;
        }
        SetUpStage(Lv);
        currentLv = Lv;
        SavePlayerDatas();
    }

    public void plusOneBtnClick()
    {
        plusOneBtn.gameObject.SetActive(false);
        foreach (var item in AllCages)
        {
            if (item.cageID == numberOfCages)
                item.gameObject.SetActive(true);
        }
    }

    public void RollBackBtn()
    {
        if (Steps.instance.PlayerAction1.Count == 0)
            return;
        int LastIndex_Next = Steps.instance.PlayerAction1[Steps.instance.GetLast()].next;
        int LastIndex_Prev = Steps.instance.PlayerAction1[Steps.instance.GetLast()].prev;
        nextCage = getCagebyID(LastIndex_Next);
        prevCage = getCagebyID(LastIndex_Prev);
        var prevCageData = prevCage.CageData;
        var nextCageData = nextCage.CageData;
        if (Steps.instance.PlayerAction1.Count > 0)
        {
            if (
                Steps.instance.PlayerAction1[Steps.instance.GetLast()].action
                == ProgressingAction.NotMatch
            )
            {
                RollBackFunc(prevCageData, nextCageData);
            }
            else
            {
                Cats destroyPrefab = Steps.instance.PlayerAction1[Steps.instance.GetLast()].name;
                for (int i = 0; i < 4; i++)
                    nextCageData.Add(destroyPrefab);
                RollBackFunc(prevCageData, nextCageData);
            }
        }
        prevCage.LoadImage();
        nextCage.LoadImage();
    }

    private void RollBackFunc(List<Cats> prevCageData, List<Cats> nextCageData)
    {
        for (
            int i = 0;
            i < Steps.instance.PlayerAction1[Steps.instance.GetLast()].totalAnimals;
            i++
        )
        {
            //Swich Position
            prevCageData.Add(nextCageData[nextCageData.Count - 1]);

            //Remove Last
            nextCageData.RemoveAt(nextCageData.Count - 1);
        }

        //Remove Action
        Steps.instance.RemoveList();
    }

    public void OpenShop(bool value)
    {
        PopupShop.SetActive(value);
    }
    #endregion
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
