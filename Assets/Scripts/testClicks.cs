using UnityEngine;

public class TestMouseClick : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse clicked at: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
