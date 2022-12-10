using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RollBack
{
    public ProgressingAction action;
    public int prev;
    public int next;
    public Cats name;
    public int totalAnimals;

    public RollBack(ProgressingAction action, int prev, int next, Cats name, int totalAnimals)
    {
        this.action = action;
        this.prev = prev;
        this.next = next;
        this.name = name;
        this.totalAnimals = totalAnimals;
    }
}

public class Steps : MonoBehaviour
{
    [SerializeField]
    private List<RollBack> PlayerAction;
    public static Steps instance;

    public List<RollBack> PlayerAction1
    {
        get => PlayerAction;
        set => PlayerAction = value;
    }

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

    public void AddList(int prev, int next, ProgressingAction action, Cats name, int totalAnimals)
    {
        RollBack temp = new RollBack(action, prev, next, name, totalAnimals);
        PlayerAction1.Add(temp);
    }

    public void RemoveList()
    {
        if (PlayerAction1.Count > 0)
            PlayerAction1.RemoveAt(PlayerAction1.Count - 1);
    }

    public int GetLast()
    {
        return PlayerAction1.Count - 1;
    }
}
