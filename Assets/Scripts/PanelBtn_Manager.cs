using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelBtn_Manager : MonoBehaviour
{
    public List<GameObject> OpenButtons;
    public List<GameObject> CloseButtons;
    public List<GameObject> panels;

    private GameObject currentBtn;

    public void openPanel_OnClick()
    {
        GameObject currentBtn = EventSystem.current.currentSelectedGameObject;
        int index = OpenButtons.IndexOf(currentBtn);
        panels[index].SetActive(true);
    }

    public void closePanel_OnClick()
    {
        GameObject currentBtn = EventSystem.current.currentSelectedGameObject;
        int index = CloseButtons.IndexOf(currentBtn);
        panels[index].SetActive(false);
    }

}
