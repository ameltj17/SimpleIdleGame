using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularCustomer : CustomerAbstract
{
    protected override void eating()
    {
        eatingDuration -= Time.deltaTime;
        if (eatingDuration <= 0f)
        {
            doneEating = true;
        }
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
            leaveShop();
        }
    }
}
