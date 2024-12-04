using UnityEngine;

public class StarHandler : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log($"Star clicked: {gameObject.name}");
        StarMapManager.Instance.StarSelected(this.gameObject);
    }
}
