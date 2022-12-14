using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewItem : MonoBehaviour
{
    [SerializeField]
    private Image childImg;

    public void ChangeImage(Sprite img)
    {
        childImg.sprite = img;
    }
}
