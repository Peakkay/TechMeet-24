using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void DrawLine(Vector3 startPosition, Vector3 endPosition)
    {
        // Ensure the LineRenderer has two positions
        lineRenderer.positionCount = 2;

        // Set the positions for the LineRenderer
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);
    }
}
