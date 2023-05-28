using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text coin;
    public Text level;
    public Text coinTrans;

    Animator playerAnimation;

    public static bool isIdle = false;
    public static bool startCooking = false;
    public static bool cooking = false;
    public static bool endCooking = false;

    private static int playerLevel = 1;
    private static int playerCoin = 800;
    private static string playerCoinTrans = "";
    private static string playerCoinTransColor = "";
    private static float promoteSpeed = 0.1f;

    private static int numOfUnlockedCustomers = 0;
    private static int numOfServedCustomers = 0;

    private float duration = 1f;

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

    public static void setCoinTransaction(int ammount)
    {
        if(ammount < 0)
        {
            playerCoinTransColor = "#CB2901";
            playerCoinTrans = ammount.ToString();
        }
        else
        {
            playerCoinTransColor = "#7D6813";
            playerCoinTrans = "+" + ammount.ToString();
        }
    }

    public void updateCoinTransaction()
    {
        if(playerCoinTrans != "")
        {
            duration -= Time.deltaTime;
            coinTrans.text = playerCoinTrans;
            coinTrans.color = ColorUtility.TryParseHtmlString(playerCoinTransColor, out Color convertedColor) ? convertedColor : Color.white;
            if (duration <= 0f)
            {
                coinTrans.text = "";
                playerCoinTrans = "";
                playerCoinTransColor = "";
                duration = 1f;
            }
        }
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

    public static int getNumOfServedCustomers()
    {
        return numOfServedCustomers;
    }

    public static void increaseNumOfServedCustomer()
    {
        numOfServedCustomers++;
    }

    public static void resetNumOfServedCustomer()
    {
        numOfServedCustomers = 0;
    }

    void Start()
    {
        playerAnimation = GetComponent<Animator>();
        startCooking = false;
        coinTrans.text = "";
    }

    void Update()
    {
        checkActivities();
        updateCoinandStar();
        updateCoinTransaction();
    }
}
