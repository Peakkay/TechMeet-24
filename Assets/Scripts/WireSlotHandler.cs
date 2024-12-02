using UnityEngine;

public class WireSlotHandler : MonoBehaviour
{
    public int slotNumber; // The expected order of this slot
    private bool isOccupied = false; // Tracks if this slot is occupied

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hello");
        if (!isOccupied && collision.CompareTag("Wire"))
        {
            Debug.Log($"Wire entered Slot {slotNumber}");
            WireDragHandler wire = collision.GetComponent<WireDragHandler>();
            if (wire != null)
            {
                // Snap wire to this slot
                collision.transform.position = transform.position;
                isOccupied = true;

                // Notify the Puzzle Manager
                WirePuzzleManager.Instance.AddToOrder(slotNumber);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("bye");
        if (isOccupied && collision.CompareTag("Wire"))
        {
            Debug.Log($"Wire exited Slot {slotNumber}");
            isOccupied = false;

            // Notify the Puzzle Manager to remove this slot from the order
            WirePuzzleManager.Instance.RemoveFromOrder(slotNumber);
        }
    }
}
