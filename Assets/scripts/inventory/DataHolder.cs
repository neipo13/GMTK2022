using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DataHolder
{
    public static Vector3 playerPosition;

    #region Events
    public static EventFlag completedEvents;
    public static EventFlag SetEventComplete(EventFlag ev)
    {
        completedEvents = completedEvents | ev;
        return completedEvents;
    }

    public static bool CheckEventComplete(EventFlag ev)
    {
        return (completedEvents & ev) == ev;
    }

    public static EventFlag RemoveCompletedEvent(EventFlag ev)
    {
        completedEvents = completedEvents & (~ev);
        return completedEvents;
    }
    #endregion

    #region Inventory
    public static InventoryFlag currentItems;
    public static InventoryFlag SetItemObtained(InventoryFlag ev)
    {
        currentItems = currentItems | ev;
        return currentItems;
    }

    public static bool CheckItemObtained(InventoryFlag ev)
    {
        return (currentItems & ev) == ev;
    }

    public static InventoryFlag RemoveObtainedItem(InventoryFlag ev)
    {
        currentItems = currentItems & (~ev);
        return currentItems;
    }
    #endregion


    public static IEnumerable<Enum> GetFlags(this Enum e)
    {
        return Enum.GetValues(e.GetType()).Cast<Enum>().Where(v => !Equals((int)(object)v, 0) && e.HasFlag(v)); ;
    }
}
