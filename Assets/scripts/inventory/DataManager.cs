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



    public void StartAdventure()
    {
        DataHolder.currentItems = InventoryFlag.None;
        DataHolder.completedEvents = EventFlag.None;
        DataHolder.playerPosition = new Vector3(0f, -13f, 0f);
    }
}
