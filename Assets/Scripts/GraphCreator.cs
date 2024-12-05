using UnityEngine;
using System.Collections.Generic;

public class GraphCreator : MonoBehaviour
{
    public static GraphCreator Instance { get; private set; }

    [Header("Graph Data for Suspects")]
    public GraphData sophiaGraph;
    public GraphData adrianGraph;
    public GraphData liamGraph;
    public GraphData mayaGraph;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PopulateGraphs();
    }

    /// <summary>
    /// Populates the graph data for all suspects.
    /// </summary>
    private void PopulateGraphs()
    {
        // Sophia's Graph
        sophiaGraph = new GraphData
        {
            nodes = new List<Node>
            {
                new Node { name = "Locked Drawer", status = false },
                new Node { name = "Carrington Rush", status = false },
                new Node { name = "Restricted Area", status = false },
                new Node { name = "Carrington Secretive", status = false },
                new Node { name = "Overloaded Circuit", status = false },
                new Node { name = "Torn Email", status = false },
                new Node { name = "AI Tampering Report", status = false },
                new Node { name = "Broken AI Component", status = false },
                new Node { name = "Hidden Network Access Logs", status = false },
                new Node { name = "Hologram", status = false },
                new Node { name = "Connection", status = false },
                new Node { name = "Encrypted File", status = false }
            },
            edges = new List<Edge>
            {
                new Edge { from = "Locked Drawer", to = "Carrington Rush", type = "Unidirectional" },
                new Edge { from = "Carrington Rush", to = "Restricted Area", type = "Unidirectional" },
                new Edge { from = "Restricted Area", to = "Carrington Secretive", type = "Unidirectional" },
                new Edge { from = "Overloaded Circuit", to = "Torn Email", type = "Unidirectional" },
                new Edge { from = "Torn Email", to = "AI Tampering Report", type = "Unidirectional" },
                new Edge { from = "AI Tampering Report", to = "Broken AI Component", type = "Unidirectional" },
                new Edge { from = "Broken AI Component", to = "Hidden Network Access Logs", type = "Unidirectional" },
                new Edge { from = "Hologram", to = "Connection", type = "Unidirectional" }
            }
        };

        // Adrian's Graph
        adrianGraph = new GraphData
        {
            nodes = new List<Node>
            {
                new Node { name = "Locked Drawer", status = false },
                new Node { name = "Carrington Rush", status = false },
                new Node { name = "Academic Rivalry", status = false },
                new Node { name = "Torn Mail", status = false },
                new Node { name = "Financial Motives", status = false },
                new Node { name = "Funding Rejection Letter", status = false },
                new Node { name = "Hologram", status = false },
                new Node { name = "Encrypted File", status = false },
                new Node { name = "Incomplete File", status = false }
            },
            edges = new List<Edge>
            {
                new Edge { from = "Locked Drawer", to = "Carrington Rush", type = "Unidirectional" },
                new Edge { from = "Carrington Rush", to = "Academic Rivalry", type = "Unidirectional" },
                new Edge { from = "Torn Mail", to = "Financial Motives", type = "Unidirectional" },
                new Edge { from = "Financial Motives", to = "Funding Rejection Letter", type = "Unidirectional" }
            }
        };

        // Liam's Graph
        liamGraph = new GraphData
        {
            nodes = new List<Node>
            {
                new Node { name = "Stars", status = false },
                new Node { name = "Blueprint", status = false }
            },
            edges = new List<Edge>
            {
                new Edge { from = "Stars", to = "Blueprint", type = "Unidirectional" }
            }
        };

        // Maya's Graph
        mayaGraph = new GraphData
        {
            nodes = new List<Node>
            {
                new Node { name = "External Funding", status = false },
                new Node { name = "Encrypted File", status = false },
                new Node { name = "Hologram", status = false }
            },
            edges = new List<Edge>
            {
                new Edge { from = "External Funding", to = "Encrypted File", type = "Unidirectional" }
            }
        };

        Debug.Log("All graphs populated successfully.");
    }

    /// <summary>
    /// Gets the graph data for a specific suspect.
    /// </summary>
    /// <param name="suspect">The suspect name.</param>
    /// <returns>GraphData for the suspect.</returns>
    public GraphData GetGraph(string suspect)
    {
        switch (suspect.ToLower())
        {
            case "sophia":
                return sophiaGraph;
            case "adrian":
                return adrianGraph;
            case "liam":
                return liamGraph;
            case "maya":
                return mayaGraph;
            default:
                Debug.LogError($"No graph found for suspect: {suspect}");
                return null;
        }
    }
}

