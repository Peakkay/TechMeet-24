using UnityEngine;

public class DebugMouseClick : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log($"Clicked on: {gameObject.name}");
    }
}
