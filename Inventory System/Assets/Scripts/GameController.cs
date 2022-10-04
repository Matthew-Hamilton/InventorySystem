using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject testItem;
    [SerializeField] Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        //Creates Inventory System
        if(Input.GetKeyDown(KeyCode.Space))
        {
            inventory.InitializeInventory(5, 5);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Instantiate(testItem);
        }
    }
}
