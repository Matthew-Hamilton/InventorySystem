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
        }
    }

    public void OnMouseDown()
    {
       lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
        Debug.Log("Item Clicked");
       
    }

    public void OnMouseUp()
    {
        isDragging=false;
    }
}
