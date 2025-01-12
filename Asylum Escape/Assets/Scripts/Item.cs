using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum ItemType
    {
        Key,
        Null
    }
    public enum MaxStack
    {
        Key = 1,
        Null = 1
        
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
        }
    }
}
