using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySpace : MonoBehaviour
{
    private Vector2 inventoryPosition;
    public Vector2 InventoryPosition
    {
        get { return inventoryPosition; }
        set { inventoryPosition = value; }
    }
    //Used by Item/ItemSpace to check whether an item can be placed in this slot
    [SerializeField]private bool isFilled = false;
    public bool IsFilled
    {
        set { isFilled = value; }
        get { return isFilled; }
    }
    SpriteRenderer spriteRenderer;
    //InventorySpace[] adjacentSpaces;

    public InventorySpace()
    {
    }

    public void SetColour(Color color)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = color;
    }
}
