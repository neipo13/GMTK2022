using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueLine
{
    public string name;

    [TextArea(3, 5)]
    public string text;

    [SerializeField]
    public EventFlag requiredFlags;

    public EventFlag flagToSet;

    public InventoryFlag requiredItem;
    public InventoryFlag givesItem;
    public InventoryFlag removesItem;

    public OptionLine Option1;
    public OptionLine Option2;
}

[System.Serializable]
public class OptionLine
{
    public UnityEvent OnChoice;
    public string Text;
}
