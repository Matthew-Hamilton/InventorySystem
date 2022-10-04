using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] int height;
    [SerializeField] int width;
    GameObject[,] spaces;
    [SerializeField] GameObject space;

    public Inventory()
    {
        

    }

    public void InitializeInventory(int _height, int _width)
    {
        height = _height;
        width = _width;

        spaces = new GameObject[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                spaces[i, j] = Instantiate(space, new Vector3(((i + 0.5f) * 1.28f) - width * .64f, ((j + 0.5f) * 1.28f) - height * .64f, 0), space.transform.rotation,transform);
                spaces[i, j].GetComponent<InventorySpace>().InventoryPosition = new Vector2(i, j);

            }
        }
    }
}

