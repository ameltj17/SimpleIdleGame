using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNewCustomer : MonoBehaviour
{
    public static GameObject[] inputSelected;
    public static Vector2[] inputPositions;
    public static Transform inputParent;

    public GameObject[] selected;
    public Transform parent;
    public Vector2[] positions;

    private static int[] seat = { 0, 0, 0, 0 };
    private static int customerQueue = 0;

    public int randomCustomer()
    {
        int max = Player.getNumOfUnlockedCustomer();
        return Random.Range(0, max);
    }

    public static void addCustomertoQueue()
    {
        customerQueue++;
    }

    private void setAndInstatiateObject(int positionIdx)
    {
        Debug.Log("tunggu");
        GameObject newCustomer = Instantiate(inputSelected[randomCustomer()], inputPositions[positionIdx], Quaternion.identity, inputParent);
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
    }

    public static void setSeatAvail(int index)
    {
        seat[index] = 0;
    }

    private void Start()
    {
        inputSelected = selected;
        inputPositions = positions;
        inputParent = parent;
    }

    private void Update()
    {
        manageQueue();
    }
}
