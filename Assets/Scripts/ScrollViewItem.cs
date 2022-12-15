using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollViewItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image childImg;

    public void ChangeImage(Sprite img)
    {
        childImg.sprite = img;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click " + childImg.name);
    }
}
