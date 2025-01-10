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

    public ItemType itemType;
    public int stack;

}
