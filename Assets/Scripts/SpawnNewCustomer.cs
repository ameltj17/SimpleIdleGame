using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnNewCustomer : MonoBehaviour
{
    public Text queueTxt;
    public GameObject[] regularCustomers;
    public GameObject[] bossCustomers;
    public Transform parent;
    public Vector2[] positions;

    private static int[] seat = { 0, 0, 0, 0 };
    private static int customerQueue = 0;
    private static GameObject objectSelected;
    private static Vector2 objectPosition;
    private static Transform objectParent;

    public GameObject randomCustomer(GameObject[] customers, int numOfUnlockedCustomers)
    {
        int max = numOfUnlockedCustomers;
        int idx = Random.Range(0, max);
        return customers[idx];
    }

    public static void addCustomertoQueue()
    {
        customerQueue++;
    }

    public static int getCustomerQueue()
    {
        return customerQueue;
    }

    private void setAndInstatiateObject(int positionIdx)
    {
        if(Player.getNumOfServedCustomers() > 9)
        {
            objectSelected = randomCustomer(bossCustomers, Player.getNumOfUnlockedCustomer());
            Player.resetNumOfServedCustomer();
        }
        else
        {
            objectSelected = randomCustomer(regularCustomers, Player.getNumOfUnlockedCustomer());
            Player.increaseNumOfServedCustomer();
        }

        objectPosition = positions[positionIdx];
        objectParent = parent;

        GameObject newCustomer = Instantiate(objectSelected, objectPosition, Quaternion.identity, objectParent);
        newCustomer.tag = positionIdx.ToString();
        seat[positionIdx] = 1;
    }

    public static int getAvailableSeatNumber()
    {
        for (int i = 0; i < seat.Length; i++)
        {
            if (seat[i] == 0)
            {
                return i;
            }
        }
        return -1;
    }

    private void manageQueue()
    {
        if (customerQueue >= 1)
        {
            int positionIdx = getAvailableSeatNumber();
            if (positionIdx != -1)
            {
                setAndInstatiateObject(positionIdx);
                customerQueue--;
            }
        }
        queueTxt.text = customerQueue.ToString();
        if(customerQueue == 10)
        {
            queueTxt.text += " (max)";
        }
    }

    public static void setSeatAvail(int index)
    {
        seat[index] = 0;
    }

    private void Start()
    {
    }

    private void Update()
    {
        manageQueue();
    }
}
