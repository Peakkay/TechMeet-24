using UnityEngine;

public class ClickTest : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("Clicked on " + gameObject.name);
    }
}
