using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPanelManager : PanelManager
{
    public Item[] chair;
    public Item[] table;
    public Item[] decoration;
    public Item[] recipe;
    public GameObject[] chairLock;
    public GameObject[] tableLock;

    private string type;
    private int idx;
    private Item itemSelected;
    private GameObject nextItemLock;

    public Image itemImg;
    public Text nameTxt;
    public Text earnCoinTxt;
    public Text levelMinTxt;
    public Text priceTxt;

    public Button buyBtn;
    public Text buyBtnTxt;

    public Text reqTxt;

    //smell 2: Long Method
    //getSelectedItemData + updateItemDetail + showItemDetail  bisa jadi long method

    //public void showItemDetail()
    //{
    //    int idx = int.Parse(EventSystem.current.currentSelectedGameObject.name);
    //    itemSelected = items[idx];
    //    nextIdx = idx + 1;

    //    openClosePanel();
    //    updateItemDetail();
    //    checkifItemBuyable();
    //}

    public void getSelectedItemDetail()
    {
        if(type == "T")
        {
            itemSelected = table[idx];
            nextItemLock = tableLock[idx + 1];
        }
        else if(type == "C")
        {
            itemSelected = chair[idx];
            nextItemLock = chairLock[idx + 1];
        }
        else if(type == "D")
        {
            itemSelected = decoration[idx];
        }
        else if(type == "R")
        {
            itemSelected = recipe[idx];
        }
    }

    public void updateItemDetailPanel()
    {
        itemImg.sprite = itemSelected.img;
        nameTxt.text = itemSelected.name;
        earnCoinTxt.text = itemSelected.earnCoin.ToString();
        levelMinTxt.text = itemSelected.levelMin.ToString();
        priceTxt.text = itemSelected.price.ToString();
        reqTxt.text = itemSelected.getItemStatus();

        buyBtnTxt.text = "Buy";
        buyBtn.GetComponent<Image>().color = Color.gray;

        if (itemSelected.buyable)
        {
            buyBtn.GetComponent<Image>().color = Color.green;
        }

        if (itemSelected.isPurchased)
        {
            buyBtnTxt.text = "In Use";
        }
    }

    public void showItemDetail_OnClick()
    {
        string code = EventSystem.current.currentSelectedGameObject.name;
        type = code.Substring(0, 1);
        idx = int.Parse(code.Substring(1));

        getSelectedItemDetail();
        updateItemDetailPanel();
        openClosePanel();
    }

    public void activateObjectUpdate() {
        ItemObjectManager.setObject(itemSelected.img);
        if (type == "T")
        {
            ItemObjectManager.tableChange = true;
            nextItemLock.SetActive(false);
        }
        else if (type == "C")
        {
            ItemObjectManager.chairChange = true;
            nextItemLock.SetActive(false);
        }
        else if (type == "D")
        {
            ItemObjectManager.decorChangeIdx = idx;
        }
        else if (type == "R")
        {
            Player.increaseNumOfUnlockedCustomer();
        }
    }

    public void buyItem_OnClick()
    {
        updateItemDetailPanel();
        if (itemSelected.buyable)
        {
            itemSelected.buyItem();
            updateItemDetailPanel();
            activateObjectUpdate();
        }
    }

    public Item getRecipe(int index)
    {
        return this.recipe[index];
    }
}
