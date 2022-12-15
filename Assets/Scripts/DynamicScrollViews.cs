using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicScrollViews : MonoBehaviour
{
    [SerializeField]
    private Transform UnlockedItem;

    [SerializeField]
    private Transform LockedItem;

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private List<Sprite> CatUnlockedItemList,
        CatLockedItemList;

    [SerializeField]
    private List<Sprite> ShipUnlockedItemList,
        ShipLockedItemList;

    [SerializeField]
    TMPro.TMP_Text unlockedTxt,
        lockedTxt;

    [SerializeField]
    bool buyCats,
        buyShips,
        buyHQs,
        buyBGs;

    void LoadAllImages(Transform scrollViewContent, List<Sprite> spriteList, bool Unlocked)
    {
        foreach (Sprite child in spriteList)
        {
            //GameObject newSpaceship = Instantiate(prefab, scrollViewContent);
            GameObject ScrollItem = ObjPooling.SharedInstance.GetPooledObject(
                Unlocked ? "UnlockedItem" : "LockedItem"
            );
            ScrollItem.SetActive(true);
            if (ScrollItem.TryGetComponent<ScrollViewItem>(out ScrollViewItem item))
            {
                item.ChangeImage(child);
            }
        }
    }

    public void LoadCatShop()
    {
        buyBGs = buyHQs = buyShips = false;
        if (buyCats)
            return;
        unlockedTxt.text = "My Cats";
        lockedTxt.text = "New Cats";
        ObjPooling.SharedInstance.DisableListItems("UnlockedItem");
        ObjPooling.SharedInstance.DisableListItems("LockedItem");
        CatUnlockedItemList.Clear();
        CatLockedItemList.Clear();
        if (GameManager.instance.playerData != null)
        {
            foreach (var item in GameManager.instance.playerData.UnlockedCharactors)
            {
                var img = ConfigManager.instance.LoadCatsImagebyCodeName(item.Key);
                if (item.Value)
                    CatUnlockedItemList.Add(img);
                else
                    CatLockedItemList.Add(img);
            }
            LoadAllImages(UnlockedItem, CatUnlockedItemList, true);
            LoadAllImages(LockedItem, CatLockedItemList, false);
        }
        buyCats = true;
    }

    public void LoadShipsShop()
    {
        buyBGs = buyHQs = buyCats = false;
        if (buyShips)
            return;
        unlockedTxt.text = "My Spacelifts";
        lockedTxt.text = "New Spacelifts";
        ObjPooling.SharedInstance.DisableListItems("UnlockedItem");
        ObjPooling.SharedInstance.DisableListItems("LockedItem");
        ShipUnlockedItemList.Clear();
        ShipLockedItemList.Clear();
        if (GameManager.instance.playerData != null)
        {
            foreach (var item in GameManager.instance.playerData.UnlockedSpaceLifts)
            {
                var img = ConfigManager.instance.LoadShipsImagebyCodeName(item.Key);
                if (item.Value)
                    ShipUnlockedItemList.Add(img);
                else
                    ShipLockedItemList.Add(img);
            }
            LoadAllImages(UnlockedItem, ShipUnlockedItemList, true);
            LoadAllImages(LockedItem, ShipLockedItemList, false);
        }
        buyShips = true;
    }

    private void Start()
    {
        ObjPooling.SharedInstance.CreatePool("UnlockedItem", prefab, UnlockedItem, 15);
        ObjPooling.SharedInstance.CreatePool("LockedItem", prefab, LockedItem, 15);
        LoadCatShop();
    }
}
