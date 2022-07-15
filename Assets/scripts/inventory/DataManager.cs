using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public EventFlag completedEvents;
    public EventFlag SetEventComplete(EventFlag ev)
    {
        completedEvents = completedEvents | ev;
        return completedEvents;
    }

    public bool CheckEventComplete(EventFlag ev)
    {
        return (completedEvents & ev) == ev;
    }

    public EventFlag RemoveCompletedEvent(EventFlag ev)
    {
        completedEvents = completedEvents & ev;
        return completedEvents;
    }
}
