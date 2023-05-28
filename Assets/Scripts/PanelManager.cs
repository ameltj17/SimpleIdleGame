using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject[] panels;

    public virtual void openClosePanel()
    {
        int btnId = int.Parse(EventSystem.current.currentSelectedGameObject.tag);
        GameObject panel = panels[btnId];

        if (!panel.activeSelf)
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }
}
