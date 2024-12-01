using UnityEngine;
using UnityEngine.EventSystems;

public class WireDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 initialPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = transform.position; // Save the starting position
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag started for: " + gameObject.name);
        canvasGroup.alpha = 0.6f; // Make the wire semi-transparent while dragging
        canvasGroup.blocksRaycasts = false; // Allow it to pass through other objects
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag going on for: " + gameObject.name);
        // Move the wire with the mouse
        rectTransform.anchoredPosition += eventData.delta / rectTransform.lossyScale;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag ended for: " + gameObject.name);
        canvasGroup.alpha = 1f; // Restore visibility
        canvasGroup.blocksRaycasts = true; // Reactivate raycast blocking

        // If the wire is not dropped on a valid slot, reset its position
        if (!eventData.pointerEnter || !eventData.pointerEnter.CompareTag("WireSlot"))
        {
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        rectTransform.position = initialPosition; // Return to starting position
    }
}
