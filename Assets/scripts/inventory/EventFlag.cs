using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags] 
public enum EventFlag 
{
    None = 0,
    TalkedToLion = 1 << 0,
    GotNote = 1 << 1,
    GladysWon = 1 << 2,
    GotDrink = 1 << 3,
    DealerGone = 1 << 4,
    DrinkGiven = 1 << 5,
    Virtuous = 1 << 6,
    CardGiven = 1 << 7,
    BlocksKnocked = 1 << 8,
}