using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum ItemType
    {
        Key,
        Null,
        Battery
    }
    public Item(ItemType type)
    {
        itemType = type;
        stack = 1;
    }

    public ItemType itemType;
    public int stack;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Key: return ItemAssestsManager.Instance.keySprite;
            case ItemType.Null: return ItemAssestsManager.Instance.Null;
            case ItemType.Battery: return ItemAssestsManager.Instance.batterySprite;
        }
    }


    public bool itemUse()
    {

        //Need to talk how to use items

        if (Item.ItemType.Null != itemType)
            return true;
        else
            return false;
    }
}
