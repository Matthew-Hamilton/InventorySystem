using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpace : MonoBehaviour
{

    [SerializeField] bool canPlace = false;
    GameObject highlighting = null;
    float closestCollider;



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("InventorySpace"))
        {
            Debug.Log("triggered drag");
            if (highlighting != null)
            {
                if ((highlighting.transform.position - transform.position).magnitude > (other.transform.position - transform.position).magnitude)
                {
                    highlighting.GetComponent<InventorySpace>().SetColour(Color.white);
                    highlighting = other.gameObject;
                    highlighting.GetComponent<InventorySpace>().SetColour(Color.blue);
                    return;
                }
                return;
            }
            highlighting = other.gameObject;
            highlighting.GetComponent<InventorySpace>().SetColour(Color.blue);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("InventorySpace"))
        {
            //other.GetComponent<InventorySpace>().SetColour(Color.white);

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }


}


