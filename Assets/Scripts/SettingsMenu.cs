using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingsMenu : MonoBehaviour
{
    public static SettingsMenu Instance;

    [Header("space between menu items")]
    [SerializeField]
    Vector2 spacing;

    [Space]
    [Header("Main button rotation")]
    [SerializeField]
    float rotationDuration;

    [SerializeField]
    Ease rotationEase;

    [Space]
    [Header("Animation")]
    [SerializeField]
    float expandDuration;

    [SerializeField]
    float collapseDuration;

    [SerializeField]
    Ease expandEase;

    [SerializeField]
    Ease collapseEase;

    [Space]
    [Header("Fading")]
    [SerializeField]
    float expandFadeDuration;

    [SerializeField]
    float collapseFadeDuration;

    Button mainButton;
    SettingsMenuItem[] menuItems;

    //is menu opened or not
    bool isExpanded = false;

    Vector2 mainButtonPosition;
    int itemsCount;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //add all the items to the menuItems array
        itemsCount = transform.childCount - 1;
        menuItems = new SettingsMenuItem[itemsCount];
        for (int i = 0; i < itemsCount; i++)
        {
            // +1 to ignore the main button
            menuItems[i] = transform.GetChild(i + 1).GetComponent<SettingsMenuItem>();
        }

        mainButton = transform.GetChild(0).GetComponent<Button>();
        mainButton.onClick.AddListener(ToggleMenu);
        //SetAsLastSibling () to make sure that the main button will be always at the top layer
        mainButton.transform.SetAsLastSibling();

        mainButtonPosition = mainButton.GetComponent<RectTransform>().anchoredPosition;

        //set all menu items position to mainButtonPosition
        ResetPositions();
    }

    void ResetPositions()
    {
        for (int i = 0; i < itemsCount; i++)
        {
            menuItems[i].rectTrans.anchoredPosition = mainButtonPosition;
        }
    }

    public void ToggleMenu()
    {
        isExpanded = !isExpanded;

        if (isExpanded == true)
        {
            //menu opened
            for (int i = 0; i < itemsCount; i++)
            {
                menuItems[i].gameObject.SetActive(true);
                menuItems[i].rectTrans
                    .DOAnchorPos(mainButtonPosition + spacing * (i + 1), expandDuration)
                    .SetEase(expandEase);
                //Fade to alpha=1 starting from alpha=0 immediately
                menuItems[i].img.DOFade(1f, expandFadeDuration).From(0f);
            }
        }
        else
        {
            //menu closed
            Close();
        }
        //rotate main button arround Z axis by 180 degree starting from 0
        mainButton.transform
            .DORotate(Vector3.forward * 180f, rotationDuration)
            .From(Vector3.zero)
            .SetEase(rotationEase);
    }

    public void Close()
    {
        for (int i = 0; i < itemsCount; i++)
        {
            menuItems[i].rectTrans
                .DOAnchorPos(mainButtonPosition, collapseDuration)
                .SetEase(collapseEase);
            //Fade to alpha=0
            menuItems[i].img.DOFade(0f, collapseFadeDuration);
            menuItems[i].gameObject.SetActive(false);
        }
        isExpanded = false;
    }

    public void OnItemClick(int index)
    {
        // here you can add you logic
        // switch (index)
        // {
        //     case 0:
        //         //first button
        //         break;
        //     case 1:
        //         //second button
        //         Close();
        //         Debug.Log("chung ");
        //         break;
        //     case 2:
        //         //third button
        //         Debug.Log("Vibration");
        //         break;
        // }
    }

    void OnDestroy()
    {
        //remove click listener to avoid memory leaks
        if (mainButton != null)
            mainButton.onClick.RemoveListener(ToggleMenu);
    }
}
