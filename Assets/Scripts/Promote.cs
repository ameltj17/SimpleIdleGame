using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Promote : MonoBehaviour
{
    public Image promoteBg;
    public Image promoteBar;

    public GameObject promoteBarObj;
    public GameObject reminderAnimationObj;

    public Button promoteBtn;
    public bool buttonClicked;
    private float lastClickTime;
    public float clickTimeThreshold = 2f;

    public void increaseBar(float promoteSpeed)
    {
        promoteBar.fillAmount += promoteSpeed;
    }

    public void promote_OnClick()
    {
        int numOfUnlockedCustomers = Player.getNumOfUnlockedCustomer();
        if (numOfUnlockedCustomers > 0)
        {
            buttonClicked = true;
            promoteBarObj.SetActive(true);

            float promoteSpeed = Player.getPromoteSpeed();
            increaseBar(promoteSpeed);
            lastClickTime = Time.time;
        }
    }

    private System.Collections.IEnumerator ResetButtonClickedStatus()
    {
        yield return new WaitForSeconds(clickTimeThreshold);
        buttonClicked = false;
    }

    private void CheckButtonClicked()
    {
        if (!buttonClicked)
        {
            buttonClicked = true;
            StartCoroutine(ResetButtonClickedStatus());
        }
        lastClickTime = Time.time;
    }

    public bool barIsFull()
    {
        return promoteBar.fillAmount >= 1f;
    }

    public void resetBar()
    {
        promoteBar.fillAmount = 0;
    }

    public void showBar_Reminder()
    {
        if (Time.time - lastClickTime >= clickTimeThreshold)
        {
            reminderAnimationObj.SetActive(true);
            promoteBarObj.SetActive(false);
        }
        else
        {
            reminderAnimationObj.SetActive(false);
            promoteBarObj.SetActive(true);
        }
    }

    public void Start()
    {
        promoteBarObj.SetActive(false);
        promoteBtn.onClick.AddListener(CheckButtonClicked);
        lastClickTime = Time.time;
    }

    public void Update()
    {
        if (barIsFull())
        {
            resetBar();
            SpawnNewCustomer.addCustomertoQueue();
        }
        showBar_Reminder();
    }
}
