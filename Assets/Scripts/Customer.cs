using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField]
    private GameObject customerObj;
    [SerializeField]
    private Animator idleAnimation;

    [SerializeField]
    private GameObject moodObj;
    [SerializeField]
    private Sprite[] moodEmoji;

    [SerializeField]
    private Sprite empty;
    [SerializeField]
    private GameObject orderBtn;
    [SerializeField]
    private GameObject plate;
    [SerializeField]
    private Sprite emptyPlate;
    [SerializeField]
    private Sprite foodPlate;

    [SerializeField]
    private int pay;
    [SerializeField]
    private int moodMax;
    [SerializeField]
    private int moodLevel;

    private float waitingDuration = 0f;
    private float eatingDuration = 0f;
    private bool isServed = false;
    private bool doneEating = false;

    private int seatNumber()
    {
        string no = customerObj.tag;
        return int.Parse(no);
    }

    private void leaveShop()
    {
        SpawnNewCustomer.setSeatAvail(seatNumber());
        Destroy(gameObject);
    }

    private void OnMouseDown()
    { 
        if (!isServed && Player.isIdle)
        {
            orderBtn.SetActive(false);
            Player.startCooking = true;
            isServed = true;
        }
        else if(doneEating)
        {
            Player.addCoin(pay);
            leaveShop();
        }
    }

    private void changeMood()
    {
        float emoPercentage = (float)(moodMax - moodLevel)/moodMax;
        int emoIdx = 0;
        if(emoPercentage <= 0.2)
        {
            emoIdx = 0;
        }
        else if(emoPercentage <= 0.5)
        {
            emoIdx = 1;
        }
        else if(emoPercentage <= 0.8)
        {
            emoIdx = 2;
        }
        else if (emoPercentage <= 1.0)
        {
            emoIdx = 3;
        }
        moodObj.GetComponent<SpriteRenderer>().sprite = moodEmoji[emoIdx];
    }

    private void waiting()
    {
        waitingDuration += Time.deltaTime;
        if (waitingDuration >= 5f)
        {
            moodLevel -= 1;
            waitingDuration = 0f;
            Debug.Log(moodLevel);
        }
        changeMood();

        if (moodLevel <= 0)
        {
            leaveShop();
        }
    }

    private void eating()
    {
        eatingDuration += Time.deltaTime;
        if (eatingDuration >= 5f)
        {
            doneEating = true;
        }
    }

    private void updateActivities()
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
            plate.GetComponent<SpriteRenderer>().sprite = emptyPlate;
            moodObj.GetComponent<SpriteRenderer>().sprite = empty;
            Animator animator = GetComponent<Animator>();
            Destroy(animator);
            customerObj.GetComponent<SpriteRenderer>().sprite = empty;
        }
    }

    private void Update()
    {
        if(gameObject.tag != "5")
        {
            updateActivities();
        }
    }

    private void Start()
    {
        GetComponent<Animator>().runtimeAnimatorController = idleAnimation.runtimeAnimatorController; 
    }
}
