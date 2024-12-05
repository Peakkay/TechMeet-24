using UnityEngine;

public class GraphManager : MonoBehaviour
{
    [System.Serializable]
    public class GraphEdge
    {
        public LineRenderer lineRenderer; // The line renderer for this edge
        public int sourceNodeIndex; // Index of the source node
        public int targetNodeIndex; // Index of the target node
        public bool isBidirectional; // True if the edge is bidirectional
    }

    [System.Serializable]
    public class GraphNode
    {
        public GameObject nodeObject; // The UI element for the node
        public bool isAcquired; // Whether the clue has been acquired
    }

    public GraphNode[] nodes; // All the nodes in the graph
    public GraphEdge[] edges; // All the edges in the graph

    void Start()
    {
        // Initialize all edges as red
        foreach (var edge in edges)
        {
            SetEdgeColor(edge.lineRenderer, Color.red);
        }
    }

    public void AcquireClue(int nodeIndex)
    {
        if (nodeIndex >= 0 && nodeIndex < nodes.Length)
        {
            nodes[nodeIndex].isAcquired = true;

            // Update edges connected to this node
            foreach (var edge in edges)
            {
                if (edge.sourceNodeIndex == nodeIndex || (edge.isBidirectional && edge.targetNodeIndex == nodeIndex))
                {
                    SetEdgeColor(edge.lineRenderer, Color.green);
                }
            }
        }
    }

    private void SetEdgeColor(LineRenderer edge, Color color)
    {
        edge.startColor = color;
        edge.endColor = color;
        edge.material = new Material(Shader.Find("Sprites/Default"));
        edge.material.color = color;
    }
}
