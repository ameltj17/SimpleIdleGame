using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomerAbstract : MonoBehaviour
{
    [SerializeField]
    protected GameObject customerObj;
    [SerializeField]
    protected Animator idleAnimation;

    [SerializeField]
    protected GameObject moodObj;
    [SerializeField]
    protected Sprite[] moodEmoji;

    [SerializeField]
    protected Sprite empty;
    [SerializeField]
    protected GameObject orderBtn;
    [SerializeField]
    protected GameObject plate;
    [SerializeField]
    protected Sprite emptyPlate;
    [SerializeField]
    protected Sprite foodPlate;

    [SerializeField]
    protected int pay;
    [SerializeField]
    protected int moodMax;
    [SerializeField]
    protected int moodLevel;

    protected float waitingDuration = 5f;
    protected float eatingDuration = 5f;
    protected bool isServed = false;
    protected bool doneEating = false;

    protected int seatNumber()
    {
        string no = customerObj.tag;
        return int.Parse(no);
    }

    protected void cleanTable()
    {
        SpawnNewCustomer.setSeatAvail(seatNumber());
        Destroy(gameObject);
    }

    public void OnMouseDown()
    {
        if (!isServed && Player.isIdle)
        {
            isServed = true;
            moodObj.SetActive(false);
            orderBtn.SetActive(false);
            Player.startCooking = true;
        }
        else if (doneEating)
        {
            Player.addCoin(pay);
            Player.setCoinTransaction(pay);
            cleanTable();
        }
    }

    protected void changeMood()
    {
        float emoPercentage = (float)(moodMax - moodLevel) / moodMax;
        int emoIdx = 0;
        if (emoPercentage <= 0.2)
        {
            emoIdx = 0;
        }
        else if (emoPercentage <= 0.5)
        {
            emoIdx = 1;
        }
        else if (emoPercentage <= 0.8)
        {
            emoIdx = 2;
        }
        else if (emoPercentage <= 1.0)
        {
            emoIdx = 3;
        }
        moodObj.GetComponent<SpriteRenderer>().sprite = moodEmoji[emoIdx];
    }

    protected void waiting()
    {
        waitingDuration -= Time.deltaTime;
        if (waitingDuration <= 0f)
        {
            moodLevel -= 1;
            waitingDuration = 5f;
            Debug.Log(moodLevel);
        }
        changeMood();

        if (moodLevel <= 0)
        {
            cleanTable();
        }
    }

    protected abstract void eating();

    protected void leaveShop()
    {
        plate.GetComponent<SpriteRenderer>().sprite = emptyPlate;
        moodObj.SetActive(false);
        Animator animator = GetComponent<Animator>();
        Destroy(animator);
        customerObj.GetComponent<SpriteRenderer>().sprite = empty;
    }

    protected abstract void updateActivities();

    protected void Update()
    {
        if (gameObject.tag != "5")
        {
            updateActivities();
        }
    }

    protected void Start()
    {
        GetComponent<Animator>().runtimeAnimatorController = idleAnimation.runtimeAnimatorController;
    }
}
