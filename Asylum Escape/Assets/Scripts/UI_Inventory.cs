using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplete;

    public void Awake()
    {
        itemSlotContainer = transform.Find("Inventory");
        itemSlotTemplete = itemSlotContainer.Find("ItemSlotTemplate");
        if(itemSlotContainer == null || itemSlotTemplete == null)
        {
            print("itemSlotC= " + itemSlotContainer);
            print("itemSlotT= " + itemSlotTemplete);

        }

    }

    public void Start()
    {
        
    }

    public void setInventory(Inventory inventory)
    {
        this.inventory = inventory;
        refresInventory();
    }



    public  void refresInventory()
    {
        int poz = 0;
        float cellSize = 200;
        foreach (Item item in inventory.GetItems())
        {
            RectTransform itemSlotTransform = Instantiate(itemSlotTemplete, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotTransform.gameObject.SetActive(true);
            itemSlotTransform.anchoredPosition = new Vector2(poz * cellSize - 400f, 0);
            poz++;
        }
    }
}
