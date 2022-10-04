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
        //CreateRectangle(3);
    }

    public void GenerateRandomShape()
    {

        spaces = new List<GameObject>();
        int randomShape = Mathf.FloorToInt(Random.Range(0, 4));
        int randomSize = Mathf.FloorToInt(Random.Range(1, 4));
        switch ((itemShape)randomShape)
        {
            case itemShape.Line:
                CreateLine(randomSize);
                break;
            case itemShape.Box:
                CreateBox(randomSize);
                break;
            case itemShape.Diagonal:
                CreateDiagonal(randomSize);
                break;
            case itemShape.Rectangle:
                CreateRectangle(randomSize);
                break;
            default:
                Debug.Log("Invalid Shape");
                break;
        }

        foreach (GameObject space in spaces)
            space.GetComponent<SpriteRenderer>().color = Color.black;
    }

    public Item(itemShape shape, int size, int height = 2)
    {
        switch(shape)
        {
            case itemShape.Line:

                break;
            case itemShape.Box:

                break;
            case itemShape.Diagonal:

                break;
            case itemShape.Rectangle:

                break;
            default:
                Debug.Log("Invalid Shape");
                break;
        }

        foreach (GameObject space in spaces)
            space.GetComponent<SpriteRenderer>().color = Color.black;
    }

    void CreateLine(int size)
    {

        for(int i = 0; i < size; i++)
        {
            spaces.Add(Instantiate(itemSpace, new Vector3((i+0.5f)*1.28f - ((size*0.5f) * 1.28f), 0, 0) + transform.position, transform.rotation, transform));
        }
    }
    void CreateDiagonal(int size)
    {
        for (int i = 0; i < size; i++)
        {
            spaces.Add(Instantiate(itemSpace, new Vector3((i + 0.5f) * 1.28f - ((size * 0.5f) * 1.28f), (i + 0.5f) * 1.28f - ((size * 0.5f) * 1.28f), 0) + transform.position, transform.rotation, transform));
        }
    }

    void CreateBox(int size)
    {
        for (int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            spaces.Add(Instantiate(itemSpace, new Vector3((i+0.5f) * 1.28f - ((size * 0.5f) * 1.28f), (j+0.5f) * 1.28f - ((size * 0.5f) * 1.28f), 0) + transform.position, transform.rotation, transform));
        }
    }
    void CreateRectangle(int size, int height = 2)
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < size; j++)
                spaces.Add(Instantiate(itemSpace, new Vector3((i + 0.5f) * 1.28f - ((height * 0.5f) * 1.28f), (j + 0.5f) * 1.28f - ((size * 0.5f) * 1.28f), 0) + transform.position, transform.rotation, transform));
        }
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

    bool PlaceItem()
    {
        List<ItemSpace> tempList = new List<ItemSpace>();
        foreach (GameObject space in spaces)
        {
            if (space.GetComponent<ItemSpace>().highlighting.GetComponent<InventorySpace>().IsFilled)
            {
                UndoPlaceItem(tempList);
                return false;
            }
            space.GetComponent<ItemSpace>().highlighting.GetComponent<InventorySpace>().IsFilled = true;
            tempList.Add(space.GetComponent<ItemSpace>());
        }

        transform.position -= spaces[0].transform.position - spaces[0].GetComponent<ItemSpace>().highlighting.transform.position;
        isPlaced = true;
        return true;
    }

    void UndoPlaceItem(List<ItemSpace> toUndo)
    {
        foreach(ItemSpace space in toUndo)
            space.highlighting.GetComponent<InventorySpace>().IsFilled = false;

    }

    void PickupItem()
    {
        foreach (GameObject space in spaces)
        {
            space.GetComponent<ItemSpace>().highlighting.GetComponent<InventorySpace>().IsFilled = false;
        }
        isPlaced=false;
    }

    public enum itemShape
    {
        Line,
        Box,
        Diagonal,
        Rectangle
    }

}
