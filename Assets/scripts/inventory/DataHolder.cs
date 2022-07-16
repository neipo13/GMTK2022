using System.Collections;
using System.Collections.Generic;
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
        completedEvents = completedEvents & ev;
        return completedEvents;
    }
    #endregion

    #region Inventory
    #endregion
}
