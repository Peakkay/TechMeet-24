using UnityEngine;
using UnityEngine.EventSystems;

public class WireInteractable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 initialPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f; // Make the wire semi-transparent
        canvasGroup.blocksRaycasts = false; // Allow it to pass through other objects
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / rectTransform.lossyScale;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // If dropped in the wrong place, return to the initial position
        if (!eventData.pointerEnter || !eventData.pointerEnter.CompareTag("WireSlot"))
        {
            rectTransform.position = initialPosition;
        }
    }

    public void ResetPosition()
    {
        rectTransform.position = initialPosition;
    }
}
