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
    public Sprite image;

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

         foreach (var graph in suspectGraphs)
        {
            InitializeGraph(graph);
        }

        ShowPage(0); // Show the first suspect graph

        // Add button listeners for navigation
        leftArrowButton.onClick.AddListener(() => ShowPage(currentPageIndex - 1));
        rightArrowButton.onClick.AddListener(() => ShowPage(currentPageIndex + 1));
    }

    /// <summary>
    /// Displays the specified graph page.
    /// </summary>
    private void ShowPage(int pageIndex)
    {
        Debug.Log($"{pageIndex} called");
        if (pageIndex < 0 || pageIndex >= graphPages.Count) return;

        // Hide all pages
        foreach (var page in graphPages)
        {
            page.gameObject.SetActive(false);
        }

        // Show selected page
        currentPageIndex = pageIndex;
        graphPages[pageIndex].gameObject.SetActive(true);

        switch(pageIndex){
            case 0:
                pageLabel.text = "Suspect: Sophia";
                break;
            case 1:
                pageLabel.text = "Suspect: Adrian";
                break;
            case 2:
                pageLabel.text = "Suspect: Liam";
                break;
            case 3:
                pageLabel.text = "Suspect: Maya";
                break;
        }
    }

    private string GetUniqueKey(int graphIndex, string nodeName)
    {
        return $"{graphIndex}_{nodeName}";
    }
    private void InitializeGraph(GraphData graph)
    {
        int graphIndex = suspectGraphs.IndexOf(graph);

        // Define grid parameters
        float startX = -400f; // Starting X position
        float startY = 200f;  // Starting Y position
        float spacingX = 200f; // Horizontal spacing
        float spacingY = 150f; // Vertical spacing
        int columns = 3;       // Number of columns in the grid

        for (int i = 0; i < graph.nodes.Count; i++)
        {
            var node = graph.nodes[i];
            string uniqueKey = GetUniqueKey(graphIndex, node.name);

            if (!nodeObjects.ContainsKey(uniqueKey))
            {
                // Instantiate the node prefab for each node
                GameObject newNode = Instantiate(nodePrefab, graphPages[graphIndex]);
                newNode.name = node.name;

                Image imageComponent = newNode.GetComponentInChildren<Image>();
                if (imageComponent != null && image != null)
                {
                    imageComponent.sprite = image;
                    imageComponent.preserveAspect = true; // Ensure the image maintains its aspect ratio
                    RectTransform imageRect = imageComponent.GetComponent<RectTransform>();
                    imageRect.sizeDelta = new Vector2(150f, 100f); // Width and height in pixels
                }
                // Set the node's text to its name
                TextMeshProUGUI tmpText = newNode.GetComponentInChildren<TextMeshProUGUI>();
                tmpText.text = node.name;

                // Calculate grid-based position
                int row = i / columns; // Determine the row based on the index
                int col = i % columns; // Determine the column based on the index

                float xPos = startX + (col * spacingX);
                float yPos = startY - (row * spacingY);

                // Set the node's position
                RectTransform rectTransform = newNode.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(xPos, yPos);

                // Store the created node with a unique key
                nodeObjects[uniqueKey] = newNode;

                // Set its initial active state based on its discovery status
                newNode.SetActive(node.status);

                Debug.Log($"Node created: {node.name} with key {uniqueKey} at position ({xPos}, {yPos}) for graph {graphIndex}");
            }
        }
    }


    /// <summary>
    /// Dynamically updates all nodes to reflect discovered clues.
    /// </summary>
public void UpdateAllNodes()
{
    foreach (var graph in suspectGraphs)
    {
        int graphIndex = suspectGraphs.IndexOf(graph); // Identify the graph index

        foreach (var node in graph.nodes)
        {
            // Generate the unique key for the node
            string uniqueKey = GetUniqueKey(graphIndex, node.name);

            // Check if the node corresponds to a discovered clue
            Clue matchingClue = ClueManager.Instance.discoveredClues.Find(clue => clue.clueName == node.name);
            if (matchingClue != null)
            {
                node.status = true; // Update the node's status

                if (nodeObjects.ContainsKey(uniqueKey))
                {
                    // Activate the node using the unique key
                    nodeObjects[uniqueKey].SetActive(true);
                }
                else
                {
                    Debug.LogWarning($"Node with key {uniqueKey} not found in nodeObjects.");
                }
            }
        }
    }

    Debug.Log("All nodes updated based on discovered clues.");
}

}
