using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueLine
{
    [TextArea(3, 5)]
    public string text;

    [SerializeField]
    public EventFlag requiredFlags;

    public EventFlag flagToSet;
    public OptionLine Option1;
    public OptionLine Option2;
}

[System.Serializable]
public class OptionLine
{
    public UnityEvent OnChoice;
    public string Text;
}
