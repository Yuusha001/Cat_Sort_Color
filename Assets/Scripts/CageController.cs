using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageController : MonoBehaviour
{
    [Header("Cage Elements")]
    [SerializeField]
    int ID,
        SendFrom;

    [SerializeField]
    List<Cats> cageData;

    [SerializeField]
    List<GameObject> ChildList;

    [SerializeField]
    bool sender,
        reciver;

    public int cageID
    {
        get => ID;
        set => ID = value;
    }
    public List<Cats> CageData
    {
        get => cageData;
        set => cageData = value;
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    private void OnEnable()
    {
        Actions.Sender += SenderHandle;
        Actions.CompleteSending += CompleteSendingHandle;
    }

    private void OnDisable()
    {
        Actions.Sender -= SenderHandle;
        Actions.CompleteSending -= CompleteSendingHandle;
    }

    void CompleteSendingHandle()
    {
        sender = false;
        reciver = false;
    }

    void SenderHandle(int value)
    {
        SendFrom = value;
        if (value == cageID)
        {
            sender = true;
            reciver = false;
        }
        else
        {
            reciver = true;
            sender = false;
        }
    }

    private bool Movable(List<Cats> prev, List<Cats> next)
    {
        if (prev.Count == 0)
            return false;
        else if (next.Count >= 4)
            return false;
        else if (prev.Count > 0 && next.Count == 0)
            return true;
        else if (prev[prev.Count - 1] == next[next.Count - 1])
            return true;
        else
            return false;
    }

    public ProgressingAction PlayerAction()
    {
        var count = 0;
        foreach (var item in cageData)
        {
            if (item == cageData[0])
            {
                count++;
            }
        }
        if (count == 4)
            return ProgressingAction.Matching;
        else
            count = 0;
        return ProgressingAction.NotMatch;
    }

    private int availableCount(List<Cats> prev, List<Cats> next)
    {
        int countAnimal = 0;
        int totalPrev = prev.Count;
        int totalNext = next.Count;
        for (int i = prev.Count - 1; i >= 0; i--)
        {
            if (prev[i] == prev[prev.Count - 1])
                countAnimal++;
            else
                break;
        }

        if (countAnimal + totalNext > 4)
            return 4 - totalNext;
        return countAnimal;
    }

    void SendData(List<Cats> prev, List<Cats> next, int totalMove)
    {
        var Last = prev.Count - 1;
        for (int i = Last; i > Last - totalMove; i--)
        {
            Debug.Log(i);
            next.Add(prev[i]);
            prev.RemoveAt(i);
        }
    }

    void SendProgressing(int prev, int next)
    {
        CageController nextCage = GameManager.instance.getCagebyID(next);
        CageController prevCage = GameManager.instance.getCagebyID(prev);
        var prevCageData = prevCage.cageData;
        var nextCageData = nextCage.cageData;
        Debug.Log("Prev : " + prevCageData.Count);
        Debug.Log("Next : " + nextCageData.Count);

        if (Movable(prevCageData, nextCageData))
        {
            var totalMove = availableCount(prevCageData, nextCageData);
            Debug.Log(SendFrom + " to " + this.cageID);
            Debug.Log("Send : " + totalMove);
            SendData(prevCageData, nextCageData, totalMove);
            var nextCageAction = nextCage.PlayerAction();
            Steps.instance.AddList(
                prev,
                next,
                nextCageAction,
                nextCageData[nextCageData.Count - 1],
                totalMove
            );
            if (nextCageAction == ProgressingAction.Matching)
                cageData.Clear();
        }
        else
        {
            Debug.Log("Not send able");
        }
    }

    public void OnBtnClick()
    {
        if (!sender && !reciver)
        {
            Actions.Sender(cageID);
        }
        else if (!sender && reciver)
        {
            SendProgressing(SendFrom, this.cageID);
            Actions.CompleteSending();
        }
        else if (!reciver && sender)
        {
            Actions.CompleteSending();
        }
    }
}
