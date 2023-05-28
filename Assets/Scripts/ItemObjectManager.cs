using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemObjectManager : MonoBehaviour
{
    public GameObject[] chairs;
    public GameObject[] tables;
    public GameObject[] decors;

    public static Sprite newObject;

    public static bool chairChange;
    public static bool tableChange;
    public static bool newChange;
    public static int decorChangeIdx = -1;

    public static void setObject(Sprite img)
    {
        newObject = img;
    }

    public void changeSetOfObjects(GameObject[] objs)
    {
        for (int i = 0; i < objs.Length; i++)
        {
            objs[i].GetComponent<SpriteRenderer>().sprite = newObject;
        }
    }

    public void changeObject(GameObject[] obj, int idx)
    {
        obj[idx].GetComponent<SpriteRenderer>().sprite = newObject;
    }

    public void updateObject()
    {
        if (chairChange)
        {
            changeSetOfObjects(chairs);
            chairChange = false;
        }
        else if (tableChange)
        {
            changeSetOfObjects(tables);
            tableChange = false;
        }
        else if (decorChangeIdx > -1)
        {
            changeObject(decors, decorChangeIdx);
            decorChangeIdx = -1;
        }
    }

    public void Update()
    {
        updateObject();
    }
}
