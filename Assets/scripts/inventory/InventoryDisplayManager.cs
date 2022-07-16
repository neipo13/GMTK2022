using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplayManager : MonoBehaviour
{
    public Image[] itemDisplays;

    public Sprite[] spritesForFlag;

    private void Update()
    {
        //check inventory items
        var possibleItems = DataHolder.currentItems.GetFlags().Select(x => (InventoryFlag)x).ToArray();
        //display dem jawns
        for (int i = 0; i < 3; i++)
        {
            if(possibleItems.Length <= i)
            {
                itemDisplays[i].sprite = null;
                itemDisplays[i].enabled = false;
            }
            else
            {
                var flag = (int)possibleItems[i];
                var index = -1;
                var val = 0;
                for(int j = 0; val != 1; j++)
                {
                    val = flag >> j;
                    index = j;
                }
                itemDisplays[i].enabled = true;
                itemDisplays[i].sprite = spritesForFlag[index];
            }
        }
    }
}
