using UnityEngine;
using UnityEngine.EventSystems;

public class WireDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject correctSlot; // Reference to the correct slot for this wire
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas; // Reference to the parent canvas
    private Vector2 originalPosition;
    private bool isCorrectlyPlaced = false; // Flag to track if the wire is placed correctly
    private Vector2 dragOffset; // Offset between pointer and wire center

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.anchoredPosition; // Save the original position
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isCorrectlyPlaced) return; // Prevent dragging if already placed correctly

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false; // Disable raycasts for smooth dragging

        // Calculate the offset between pointer position and wire center in parent canvas space
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform, // Parent canvas' RectTransform
            eventData.position,
            eventData.pressEventCamera,
            out dragOffset
        );

        // Adjust dragOffset relative to the wire's anchored position
        dragOffset -= rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isCorrectlyPlaced) return; // Prevent dragging if already placed correctly

        // Convert screen point to local point in parent canvas
        Vector2 localPointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPointerPosition
        );

        // Apply the offset to position the wire correctly under the pointer
        rectTransform.anchoredPosition = localPointerPosition - dragOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isCorrectlyPlaced) return; // Prevent dragging if already placed correctly

        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;

        if (RectTransformUtility.RectangleContainsScreenPoint(correctSlot.GetComponent<RectTransform>(), Input.mousePosition, eventData.pressEventCamera))
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
