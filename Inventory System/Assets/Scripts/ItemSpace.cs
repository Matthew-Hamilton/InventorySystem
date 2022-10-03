using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpace : MonoBehaviour
{

    [SerializeField] bool canPlace = false;
    GameObject highlighting = null;
    float closestCollider;
    [SerializeField]List<Collider2D> colliders;

    private void Start()
    {
        colliders = new List<Collider2D>();
    }

    public void UpdateClosestSpace()
    {
        if (colliders.Count < 0)
            return;

        GameObject temp = highlighting;
        closestCollider = float.MaxValue;
        foreach(Collider2D collider in colliders)
        {
            if((collider.transform.position - transform.position).magnitude < closestCollider && (collider.transform.position - transform.position).magnitude < 0.4f)
            {
                temp = collider.gameObject;
                closestCollider = (collider.transform.position - transform.position).magnitude;
            }
        }
        if (temp == highlighting)
            return;
        HighlightSlot(temp);
    }

    void HighlightSlot(GameObject toHighlight)
    {
        highlighting.GetComponent<SpriteRenderer>().color = Color.white;
        toHighlight.GetComponent<SpriteRenderer>().color = Color.red;
        highlighting = toHighlight;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("InventorySpace"))
        {
            colliders.Add(other);
            Debug.Log("triggered drag");
            //if (highlighting != null)
            //{
            //    if ((highlighting.transform.position - transform.position).magnitude > (other.transform.position - transform.position).magnitude)
            //    {
            //        highlighting.GetComponent<InventorySpace>().SetColour(Color.white);
            //        highlighting = other.gameObject;
            //        highlighting.GetComponent<InventorySpace>().SetColour(Color.blue);
            //        return;
            //    }
            //    return;
            //}
            if(highlighting == null)
                highlighting = other.gameObject;
            //highlighting.GetComponent<InventorySpace>().SetColour(Color.blue);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("InventorySpace"))
        {
            colliders.Remove(other);
            //other.GetComponent<InventorySpace>().SetColour(Color.white);

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }


}


