using UnityEngine;
public class PanelController : MonoBehaviour
{
    public GameObject panel; // Assign the panel GameObject in the Inspector
    private bool isPanelVisible = false; // Tracks the current visibility of the panel
    public GameObject clueDesc;

    void Update()
    {
        // Check for 'C' key press
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Check");
            TogglePanel();
        }
    }
    private void TogglePanel()
    {
        isPanelVisible = !isPanelVisible; // Toggle the visibility state
        panel.SetActive(isPanelVisible); // Show or hide the panel
        if(!isPanelVisible)
        {
            clueDesc.SetActive(false);
        }
    }
}
