using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySpace : MonoBehaviour
{
    private bool isFilled = false;
    public bool IsFilled
    {
        private set { isFilled = value; }
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

    public void SetAdjacentSpace(Direction dir, InventorySpace newAdjacent)
    {
       // adjacentSpaces[(int)dir] = newAdjacent;
    }

    public void SetPosition(float x, float y, float z)
    {

    }

    public enum Direction
    { 
        Up,
        Right,
        Down,
        Left
    }
}
