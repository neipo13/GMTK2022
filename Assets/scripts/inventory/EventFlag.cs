using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags] public enum EventFlag
{
    None = 0,
    TriedToSleep = 1 << 0,
    TalkedToHintBoy = 1 << 1,
    GladysWon = 1 << 2,
    ToldHintBoy = 1 << 3,
}