using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerInspector : MonoBehaviour
{
    public GameObject key;  // key
    public string inspectMessage = "Inspecting drawer...";
    public float inspectRange = 2.0f; // |Key inspection range
    public KeyCode inspectKey = KeyCode.E; // Keyboard key to press for inspection

    private Transform player; //PLayer location

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= inspectRange && Input.GetKeyDown(inspectKey))
        {
            InspectDrawer();
        }
    }

    void InspectDrawer()
    {
        if (key != null && key.activeInHierarchy)
        {
            Debug.Log(inspectMessage + " The key is inside the drawer.");
        }
        else
        {
            Debug.Log(inspectMessage + " The key is not inside the drawer.");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, inspectRange);
    }
}
