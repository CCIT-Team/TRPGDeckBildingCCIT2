using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public ItemData_ Data { get; private set; }

    public Item(ItemData_ data) => Data = data;
}
