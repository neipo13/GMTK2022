using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum InventoryFlag
{
    None = 0,
    Dentures = 1 << 0,
    PlayingChips = 1 << 1,
    Napkin = 1 << 2,
    Drink = 1 << 3,
    Card = 1 << 4,
    Virtue = 1 << 5,
    Dice = 1 << 6,
}