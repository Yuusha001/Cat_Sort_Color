using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicScrollViews : MonoBehaviour
{
    [SerializeField]
    private Transform scrollViewContent;

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private List<Sprite> spriteList;

    private void Start()
    {
        foreach (Sprite child in spriteList)
        {
            GameObject newSpaceship = Instantiate(prefab, scrollViewContent);
            if (newSpaceship.TryGetComponent<ScrollViewItem>(out ScrollViewItem item))
            {
                item.ChangeImage(child);
            }
        }
    }
}
