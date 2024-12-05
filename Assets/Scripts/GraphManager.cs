using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class GraphManager : Singleton<GraphManager>
{
    [Header("Graph Pages")]
    public List<RectTransform> graphPages; // List of pages for each suspect
    private int currentPageIndex = 0;      // Currently active page

    [Header("Graph Data for Suspects")]
    public List<GraphData> suspectGraphs; // Graph data for each suspect (Sophia, Adrian, Liam, Maya)

    [Header("UI Elements")]
    public TextMeshProUGUI pageLabel;      // Label to display suspect name
    public Button leftArrowButton;         // Button for previous page
    public Button rightArrowButton;        // Button for next page

    [Header("Graph Generation")]
    public GameObject nodePrefab;
    public Material BoolTrueMaterial;
    public Material BoolFalseMaterial;
    private Dictionary<string, GameObject> nodeObjects = new Dictionary<string, GameObject>();

    void Start()
    {
        // Fetch the graphs for each suspect dynamically
        suspectGraphs = new List<GraphData>
        {
            GraphCreator.Instance.GetGraph("Sophia"),
            GraphCreator.Instance.GetGraph("Adrian"),
            GraphCreator.Instance.GetGraph("Liam"),
            GraphCreator.Instance.GetGraph("Maya")
        };

        ShowPage(0); // Show the first suspect graph

        // Subscribe to clue discovery
        if (ClueManager.Instance != null)
        {
            ClueManager.Instance.OnClueDiscovered += DiscoverClue; // Subscribe to the event
        }

        // Add button listeners for navigation
        leftArrowButton.onClick.AddListener(() => ShowPage(currentPageIndex - 1));
        rightArrowButton.onClick.AddListener(() => ShowPage(currentPageIndex + 1));
    }

    void OnDestroy()
    {
        // Unsubscribe from clue discovery
        if (ClueManager.Instance != null)
        {
            ClueManager.Instance.OnClueDiscovered -= DiscoverClue;
        }
    }

    /// <summary>
    /// Displays the specified graph page.
    /// </summary>
    private void ShowPage(int pageIndex)
    {
        if (pageIndex < 0 || pageIndex >= graphPages.Count) return;

        // Hide all pages
        foreach (var page in graphPages)
        {
            page.gameObject.SetActive(false);
        }

        // Show selected page
        currentPageIndex = pageIndex;
        graphPages[pageIndex].gameObject.SetActive(true);

        // Update the page label to show the current suspect's name
        pageLabel.text = $"Suspect: {suspectGraphs[pageIndex].nodes[0].name}"; // Display name of the first node (suspect)
    }

    /// <summary>
    /// Called when a clue is discovered. Updates the relevant graph.
    /// </summary>
    public void DiscoverClue(Clue clue)
    {
        // Check all suspect graphs to find the matching clue
        foreach (var graph in suspectGraphs)
        {
            Node discoveredNode = graph.nodes.Find(node => node.name == clue.clueName);
            if (discoveredNode != null)
            {
                discoveredNode.status = true;

                // If this graph page is visible, update the graph
                if (graphPages[suspectGraphs.IndexOf(graph)].gameObject.activeSelf)
                {
                    UpdateGraph(graph, suspectGraphs.IndexOf(graph));
                }

                Debug.Log($"Clue discovered: {clue.clueName}");
                return;
            }
        }
    }

    /// <summary>
    /// Updates the graph by showing the discovered clues.
    /// </summary>
    private void UpdateGraph(GraphData graph, int pageIndex)
    {
        foreach (var node in graph.nodes)
        {
            if (nodeObjects.ContainsKey(node.name))
            {
                // Update node visibility based on its status (discovered or not)
                nodeObjects[node.name].SetActive(node.status);
            }
        }

        // Update edges if necessary (you can apply similar logic if edges should also be dynamically updated based on discovery)
        foreach (var edge in graph.edges)
        {
            if (nodeObjects.ContainsKey(edge.from) && nodeObjects.ContainsKey(edge.to))
            {
                GameObject fromNode = nodeObjects[edge.from];
                GameObject toNode = nodeObjects[edge.to];

                // Get the LineRenderer component of the edge
                string edgeName = $"Edge_{edge.from}_{edge.to}";
                Transform edgeTransform = graphPages[pageIndex].Find(edgeName);
                if (edgeTransform != null)
                {
                    LineRenderer lineRenderer = edgeTransform.GetComponent<LineRenderer>();

                    // Update material based on the nodes' statuses
                    if (fromNode.activeSelf && toNode.activeSelf)
                    {
                        lineRenderer.material = BoolTrueMaterial;
                    }
                    else
                    {
                        lineRenderer.material = BoolFalseMaterial;
                    }
                }
            }
        }

        Debug.Log("Graph updated dynamically.");
    }
}
