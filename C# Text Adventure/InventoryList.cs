using System;
using System.Collections.Generic;
using System.Text;

public class InventoryList
{
    private List<Item> _list = new();
    public double InventoryWeight { get; private set; }
    public double MaxInventoryWeight { get; set; }

    public InventoryList(double maxInventoryWeight = Double.MaxValue)
    {
        MaxInventoryWeight = maxInventoryWeight;
    }
    public void Add(Item item)
    {
        if (InventoryWeight + item.Weight > MaxInventoryWeight) throw new ItemTooHeavyException();
        InventoryWeight += item.Weight;
        _list.Add(item);
    }

    public void Remove(Item item)
    {
        _list.Remove(item);
        InventoryWeight -= item.Weight;
    }

    public void RemoveAt(int index)
    {
        InventoryWeight -= _list[index].Weight;
        _list.RemoveAt(index);
    }
    public int Count => _list.Count;
    public Item this[int index] => _list[index];

    public void Sort()
    {
        _list.Sort((a, b) => string.Compare(a.Name, b.Name));
    }
}

public class ItemTooHeavyException : Exception{

}