using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] GameObject itemSpace;
    [SerializeField] List<GameObject> spaces;
    Vector3 mousePosition;
    Vector3 lastMousePosition;

   [SerializeField] bool isDragging;
    bool isPlaced = false;

    private void Start()
    {
        spaces = new List<GameObject>();
        spaces.Add(Instantiate(itemSpace, new Vector3(0.64f, 0.64f) + transform.position, transform.rotation, transform));
        spaces.Add(Instantiate(itemSpace, new Vector3(-.64f, 0.64f) + transform.position, transform.rotation, transform));
        spaces.Add(Instantiate(itemSpace, new Vector3(-.64f, -0.64f) + transform.position, transform.rotation, transform));
    }

    private void Update()
    {
        if(isDragging)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mouseDelta = mousePosition - lastMousePosition;

            transform.position += new Vector3(mouseDelta.x, mouseDelta.y, 0);

            lastMousePosition = mousePosition;

            foreach(GameObject space in spaces)
            {
                space.GetComponent<ItemSpace>().UpdateClosestSpace();
            }

            foreach (GameObject space in spaces)
            {
                if(space.GetComponent<ItemSpace>().highlighting != null)
                    space.GetComponent<ItemSpace>().highlighting.GetComponent<SpriteRenderer>().color = Color.red;
            }

            if(Input.GetKeyDown(KeyCode.Q))
            {
                transform.RotateAround(transform.position, Vector3.forward, 90);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                transform.RotateAround(transform.position, Vector3.forward, -90);
            }
        }
    }

    public void OnMouseDown()
    {
       lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
        if(isPlaced)
            PickupItem();
        Debug.Log("Item Clicked");
       
    }

    public void OnMouseUp()
    {
        isDragging=false;
        if (CanPlaceItem())
            PlaceItem();
        
    }

    bool CanPlaceItem()
    {
        Debug.Log("CanPlaceCalled");
        int numCanPlace = 0;
        foreach (GameObject space in spaces)
        {
            if (space.GetComponent<ItemSpace>().highlighting != null && !space.GetComponent<ItemSpace>().highlighting.GetComponent<InventorySpace>().IsFilled)
                numCanPlace++;
        }
        if(numCanPlace == spaces.Count)
            return true;
        return false;
    }

    void PlaceItem()
    {
        foreach (GameObject space in spaces)
        {
            space.GetComponent<ItemSpace>().highlighting.GetComponent<InventorySpace>().IsFilled = true;
        }

        transform.position -= spaces[0].transform.position - spaces[0].GetComponent<ItemSpace>().highlighting.transform.position;
        isPlaced = true;
    }

    void PickupItem()
    {
        foreach (GameObject space in spaces)
        {
            space.GetComponent<ItemSpace>().highlighting.GetComponent<InventorySpace>().IsFilled = false;
        }
        isPlaced=false;
    }

}
