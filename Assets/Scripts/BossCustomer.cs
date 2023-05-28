using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCustomer : CustomerAbstract
{
    [SerializeField]
    protected GameObject bossCrown;
    private int nFood = 3;

    protected override void eating()
    {
        eatingDuration -= Time.deltaTime;
        if (eatingDuration <= 0f)
        {
            doneEating = true;
            nFood--;
        }
    }

    protected void reOrder() {
        isServed = false;
        doneEating = false;
        moodLevel = moodMax;
        eatingDuration = 5f;

        plate.GetComponent<SpriteRenderer>().sprite = empty;
        orderBtn.SetActive(true);
        moodObj.SetActive(true);
    }

    protected override void updateActivities()
    {
        if (!isServed)
        {
            waiting();
        }
        else if (isServed && Player.endCooking && !doneEating)
        {
            plate.GetComponent<SpriteRenderer>().sprite = foodPlate;
            eating();
        }
        else if (doneEating)
        {
            if(nFood <= 0)
            {
                leaveShop();
                bossCrown.SetActive(false);
            }
            else
            {
                reOrder();
            }
        }
    }
}
