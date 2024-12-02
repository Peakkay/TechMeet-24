using UnityEngine;

public class WireSlot : MonoBehaviour
{
    public int slotNumber; // The correct order for this slot
    private bool isOccupied = false;

    public bool IsOccupied => isOccupied;

    public void SnapWire(GameObject wire)
    {
        if (!isOccupied)
        {
            wire.transform.position = transform.position; // Snap wire to this slot
            isOccupied = true;
            WirePuzzleManager.Instance.AddToOrder(slotNumber);
        }
    }

    public void RemoveWire()
    {
        isOccupied = false;
        WirePuzzleManager.Instance.RemoveFromOrder(slotNumber);
    }
}

