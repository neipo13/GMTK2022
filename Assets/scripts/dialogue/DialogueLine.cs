using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    [TextArea(3, 5)]
    public string text;

    [SerializeField]
    public EventFlag requiredFlags;

    public EventFlag flagToSet;
}
