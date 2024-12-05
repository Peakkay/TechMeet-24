using UnityEngine;
using TMPro; // Import TMP namespace
using System.Collections.Generic;

[System.Serializable]
public class Node
{
    public string name;
    public bool status;
}

[System.Serializable]
public class Edge
{
    public string from;
    public string to;
    public string type; // "Unidirectional" or "Bidirectional"
}

[System.Serializable]
public class GraphData
{
    public List<Node> nodes;
    public List<Edge> edges;
}

public class GraphGenerator : MonoBehaviour
{
    public RectTransform canvas;
    public GameObject nodePrefab;
    public Material BooltrueMaterial;
    public Material BoolFalseMaterial;
    public GraphData graphData;

    private Dictionary<string, GameObject> nodeObjects = new Dictionary<string, GameObject>();

    void Start()
    {
        GenerateGraph();
    }

void GenerateGraph()
{
    int rows = 6; // Adjust number of rows for better readability
    for (int i = 0; i < graphData.nodes.Count; i++)
    {
        CreateNode(graphData.nodes[i].name,true, i, graphData.nodes.Count, rows);
    }

    foreach (var edge in graphData.edges)
    {
        CreateEdge(edge);
    }
}

   void CreateNode(string nodeName, bool stat , int index, int totalNodes, int columns)
{
    // Instantiate the node UI prefab
    var nodeObject = Instantiate(nodePrefab, canvas);
    nodeObject.name = nodeName;
    nodeObject.SetActive(stat);

    // Define grid range
    float minX = -800f, maxX = 800f; // Horizontal range
    float minY = -500f, maxY = 500f; // Vertical range

    // Calculate grid properties
    int rows = Mathf.CeilToInt((float)totalNodes / columns); // Number of rows
    float horizontalSpacing = (maxX - minX) / (columns - 1); // Spacing between columns
    float verticalSpacing = (maxY - minY) / (rows - 1);      // Spacing between rows

    // Determine column and row for the current index
    int col = index / rows;    // Column is determined first
    int row = index % rows;    // Then place within the column

    // Calculate position
    float xPos = minX + col * horizontalSpacing;
    float yPos = maxY - row * verticalSpacing;

    // Set node position
    RectTransform rectTransform = nodeObject.GetComponent<RectTransform>();
    rectTransform.anchoredPosition = new Vector2(xPos, yPos);

    // Assign TMP text label to display the node name
    TextMeshProUGUI tmpText = nodeObject.GetComponentInChildren<TextMeshProUGUI>();
    if (tmpText != null)
    {
        tmpText.text = nodeName;
    }

    // Store the created node
    nodeObjects[nodeName] = nodeObject;
}


  void CreateEdge(Edge edge)
{
    if (!nodeObjects.ContainsKey(edge.from) || !nodeObjects.ContainsKey(edge.to))
    {
        return;
    }

    GameObject fromNode = nodeObjects[edge.from];
    GameObject toNode = nodeObjects[edge.to];

    // Get anchored positions directly
    Vector2 fromPosition = fromNode.GetComponent<RectTransform>().anchoredPosition;
    Vector2 toPosition = toNode.GetComponent<RectTransform>().anchoredPosition;

    // Create a new GameObject for the LineRenderer
    GameObject edgeObject = new GameObject($"Edge_{edge.from}_{edge.to}");
    edgeObject.transform.SetParent(canvas, false);

    // Add LineRenderer component
    LineRenderer lineRenderer = edgeObject.AddComponent<LineRenderer>();
    lineRenderer.positionCount = 2;

    // Use positions as they are in the canvas coordinate space
    lineRenderer.SetPosition(0, new Vector3(fromPosition.x + 970, fromPosition.y + 550 , 0));
    lineRenderer.SetPosition(1, new Vector3(toPosition.x + 970 , toPosition.y + 550 , 0));

    // Configure LineRenderer properties
    lineRenderer.startWidth = 5f;
    lineRenderer.endWidth = 5f;
    lineRenderer.sortingOrder = 100; // Higher order value to render above nodes

    // Assign material based on edge type
    if (fromNode.activeSelf && toNode.activeSelf)
    {
        lineRenderer.material = BooltrueMaterial;
    }
    else
    {
        lineRenderer.material = BoolFalseMaterial;
    }
}


}
