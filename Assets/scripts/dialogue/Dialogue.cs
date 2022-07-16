using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Dialogue 
{
    public string name;

    public DialogueLine[] lines;

    public DialogueLine[] FilteredLines
    {
        get
        {
            //based on current flags, filter the list
            var flagsToCheck = lines.Select(l => l.requiredFlags).Distinct().OrderByDescending(x => x).ToList();
            foreach(var flag in flagsToCheck)
            {
                var complete = DataHolder.CheckEventComplete(flag);
                if (complete)
                {
                    return lines.Where(x => x.requiredFlags == flag).ToArray();
                }
            }
            return lines;
        }
    }
}
