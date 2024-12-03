using UnityEngine;

public class MousePointer : MonoBehaviour
{
    private void Update()
    {
        // Move the pointer object to the mouse position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
    }
}
