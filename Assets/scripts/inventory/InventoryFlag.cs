using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum InventoryFlag
{
    None = 0,
    Bone = 1 << 0,
    Key = 1 << 1,
    Teeth = 1 << 2,
}