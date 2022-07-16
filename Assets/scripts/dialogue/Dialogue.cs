using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Dialogue
{
    public DialogueLine[] lines;

    public DialogueLine[] FilteredLines
    {
        get
        {
            //based on current flags, filter the list
            var flagsToCheck = lines.Select(l => l.requiredFlags).Distinct().OrderByDescending(x => x).ToList();
            var itemsToCheck = lines.Select(l => l.requiredItem).Distinct().OrderByDescending(x => x).ToList();
            foreach (var item in itemsToCheck)
            {
                var held = DataHolder.CheckItemObtained(item);
                if (held)
                {
                    foreach (var flag in flagsToCheck)
                    {
                        var complete = DataHolder.CheckEventComplete(flag);
                        if (complete)
                        {
                            return lines.Where(x => x.requiredItem == item && x.requiredFlags == flag).ToArray();
                        }
                    }

                }
            }
            return lines;
        }
    }
}
