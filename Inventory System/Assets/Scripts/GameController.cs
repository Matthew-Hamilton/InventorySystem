using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            inventory.InitializeInventory(5, 5);
        }
    }
}
