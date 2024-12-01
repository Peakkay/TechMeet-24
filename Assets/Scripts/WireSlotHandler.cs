using UnityEngine;

public class WireSlotHandler : MonoBehaviour
{
    public int slotNumber; // The expected order of this slot
    private bool isOccupied = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isOccupied && collision.CompareTag("Wire"))
        {
            WireDragHandler wire = collision.GetComponent<WireDragHandler>();
            if (wire != null)
            {
                collision.transform.position = transform.position; // Snap to this slot
                isOccupied = true;
                WirePuzzleManager.Instance.AddToOrder(slotNumber);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (isOccupied && collision.CompareTag("Wire"))
        {
            isOccupied = false;
            WirePuzzleManager.Instance.RemoveFromOrder(slotNumber);
        }
    }
}
