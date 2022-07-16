using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public void RiggedSlotMachine()
    {
        DataHolder.SetEventComplete(EventFlag.GladysWon);
    }

    public void DrinkGiven()
    {
        DataHolder.SetEventComplete(EventFlag.DrinkGiven);
        DataHolder.RemoveObtainedItem(InventoryFlag.Drink);
    }

    public void BlocksKnocked()
    {
        DataHolder.SetEventComplete(EventFlag.BlocksKnocked);
        //remove all inventory?
    }
}
