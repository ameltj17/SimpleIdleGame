using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MarketManager : MonoBehaviour
{
    public GameObject[] menus;
    public GameObject[] menusBtn;

    public void menuUpdate()
    {
        GameObject menuBtn = EventSystem.current.currentSelectedGameObject;
        int menuId = int.Parse(menuBtn.tag);

        GameObject menu = menus[menuId];
        openMenu(menu, menuBtn, menuId);
    }

    public void openMenu(GameObject menu, GameObject menuBtn, int menuId)
    {
        if (!menu.activeSelf)
        {
            menu.SetActive(true);
            changeBtnColor(menuBtn, "#E7CEB6");

            for (int i = 0; i < menus.Length; i++)
            {
                if (i != menuId)
                {
                    menus[i].SetActive(false);
                    changeBtnColor(menusBtn[i], "#C3B2A3");
                }
            }
        }
    }

    public void changeBtnColor(GameObject btn, string btnColor)
    {
        Color newColor;
        ColorUtility.TryParseHtmlString(btnColor, out newColor);
        btn.GetComponent<Image>().color = newColor;
    }
}
