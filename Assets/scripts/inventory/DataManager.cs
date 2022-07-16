using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public void ToldHintBoy()
    {
        DataHolder.SetEventComplete(EventFlag.ToldHintBoy);
    }

    public void RiggedSlotMachine()
    {
        DataHolder.SetEventComplete(EventFlag.GladysWon);
    }
}
