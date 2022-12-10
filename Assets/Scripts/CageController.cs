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
    List<GameObject> ChildList;

    [SerializeField]
    bool sender,
        reciver;

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
        if (value == ID)
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

    public void OnBtnClick()
    {
        if (!sender && !reciver)
        {
            Actions.Sender(ID);
        }
        else if (!sender && reciver)
        {
            Debug.Log(SendFrom + " to " + this.ID);
            Debug.Log("Sended");
            Actions.CompleteSending();
        }
        else if (!reciver && sender)
        {
            Actions.CompleteSending();
        }
    }
}
