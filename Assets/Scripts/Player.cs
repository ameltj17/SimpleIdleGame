using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text coin;
    public Text level;

    Animator playerAnimation;

    public static bool isIdle = false;
    public static bool startCooking = false;
    public static bool cooking = false;
    public static bool endCooking = false;

    private static int playerLevel = 1;
    private static int playerCoin = 800;
    private static float promoteSpeed = 0.1f;
    private static int numOfUnlockedCustomers = 0;

    public void checkActivities()
    {
        isIdle = this.playerAnimation.GetCurrentAnimatorStateInfo(0).IsName("Player_Idle");
        if (isIdle && startCooking)
        {
            playerAnimation.SetTrigger("cooking");
        }
        else if (!isIdle)
        {
            cooking = true;
            startCooking = false;
        }
        else if(isIdle && cooking)
        {
            endCooking = true;
            cooking = false;
        }
    }

    public static int getCoin()
    {
        return playerCoin;
    }

    public static void addCoin(int ammount)
    {
        playerCoin += ammount;
        Debug.Log(ammount);
    }

    public static int getLevel()
    {
        return playerLevel;
    }

    public static void addLevel()
    {
        //condition
        playerLevel++;
    }

    public void updateCoinandStar()
    {
        coin.text = playerCoin.ToString();
        level.text = playerLevel.ToString();
    }

    public static float getPromoteSpeed()
    {
        return promoteSpeed;
    }

    public static void setPromoteSpeed(float speed)
    {
        promoteSpeed = speed;
    }

    public static int getNumOfUnlockedCustomer()
    {
        return numOfUnlockedCustomers;
    }

    public static void increaseNumOfUnlockedCustomer()
    {
        numOfUnlockedCustomers++;
    }

    void Start()
    {
        playerAnimation = GetComponent<Animator>();
        startCooking = false;
    }

    void Update()
    {
        checkActivities();
        updateCoinandStar();
    }
}
