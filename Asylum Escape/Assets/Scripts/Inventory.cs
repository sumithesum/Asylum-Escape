using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    private List<Item> itemList;

    [SerializeField]
    private int size = 5;

    public Inventory()
    {
        itemList = new List<Item>();
        for(int i = 0; i < size; i++)
            addItem(new Item { itemType = Item.ItemType.Null, stack = 1 });
        
    }


    public void addItem(Item item)
    {
        itemList.Add(item);
    } 
    
    public List<Item> GetItems()
    {
        return itemList;
    }
}
