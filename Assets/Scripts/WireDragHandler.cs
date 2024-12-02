using UnityEngine;
using UnityEngine.EventSystems;

public class WireDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject correctSlot; // Reference to the correct slot for this wire
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private bool isCorrectlyPlaced = false; // Flag to track if the wire is placed correctly

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.anchoredPosition; // Save the original position
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isCorrectlyPlaced) return; // Prevent dragging if already placed correctly

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false; // Disable raycasts for smooth dragging
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isCorrectlyPlaced) return; // Prevent dragging if already placed correctly

        rectTransform.anchoredPosition += eventData.delta; // Move wire with mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isCorrectlyPlaced) return; // Prevent dragging if already placed correctly

        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;

        if (RectTransformUtility.RectangleContainsScreenPoint(correctSlot.GetComponent<RectTransform>(), Input.mousePosition))
        {
            Debug.Log("Wire placed correctly in the slot!");

            // Snap wire to the correct slot's position
            rectTransform.anchoredPosition = correctSlot.GetComponent<RectTransform>().anchoredPosition;

            // Mark the wire as correctly placed
            isCorrectlyPlaced = true;

            // Notify the PuzzleManager that this wire is correctly placed
            WirePuzzleManager.Instance.WirePlacedCorrectly(this);
        }
        else
        {
            Debug.Log("Wire returned to its original position.");
            // Return wire to its original position if dropped incorrectly
            rectTransform.anchoredPosition = originalPosition;
        }
    }
}
