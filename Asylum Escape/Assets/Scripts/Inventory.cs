using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    private List<Item> itemList;

    [SerializeField]
    static public int size = 5;

    public Inventory()
    {
        itemList = new List<Item>();
        for(int i = 0; i < size; i++)
            addItem(new Item(Item.ItemType.Null));
        
    }


    public void addItem(Item item)
    {
        itemList.Add(item);
    } 
    
    public void elemineteItem(int position)
    {
        itemList[position] = new Item(Item.ItemType.Null);
    }

    public List<Item> GetItems()
    {
        return itemList;
    }

    public void setItem(int poz , Item item)
    {
        itemList[poz] = item;
    }

    public string printInv()
    {
        string s = "";
        foreach(Item i in itemList)
        {
            s += i.itemType + "     ";
        }
        return s;
    }
}
