using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorFollower : MonoBehaviour
{
    private Vector3 mousePos;
    public Camera cam;
    public GameObject cursor;
    public Vector3 cursorPosition;
    public bool isCursor = true;

    public void Start()
    {
        //Start with the cursor invisible
        isCursor = true;
    }

    void Update()
    {
        //bools to make the cursor visible or invisible.

        if (isCursor == true)
        {
            Cursor.visible = false;
        }

        if (isCursor == false)
        {
            Cursor.visible = true;
        }
    }

    void LateUpdate()
    {
        //It takes the position of the real cursor
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //Make the game object of the player look at the position of the cursor
        transform.LookAt(new Vector3(mousePos.x, transform.position.y, mousePos.z));

        //Debug to see where the cursor is in the scene view
        Debug.DrawLine(transform.position, mousePos);

        //The object of the game cursor is in the same position as the real cursor
        cursor.transform.position = mousePos + cursorPosition;
    }
}
