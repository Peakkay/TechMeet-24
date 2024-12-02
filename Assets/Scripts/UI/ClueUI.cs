using UnityEngine;
using UnityEngine.UI;

public class ClueUI : MonoBehaviour
{
    public GameObject cluePanel; // The UI panel to display clues.
    public GameObject clueTemplate; // A prefab for displaying each clue.

    public void UpdateClueUI()
    {
        // Clear existing clues in the UI.
        foreach (Transform child in cluePanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Populate with discovered clues.
        foreach (Clue clue in ClueManager.Instance.discoveredClues)
        {
            GameObject clueItem = Instantiate(clueTemplate, cluePanel.transform);
            clueItem.transform.Find("ClueName").GetComponent<Text>().text = clue.clueName;
            clueItem.transform.Find("ClueDescription").GetComponent<Text>().text = clue.description;

            if (clue.clueImage != null)
            {
                clueItem.transform.Find("ClueImage").GetComponent<Image>().sprite = clue.clueImage;
            }
        }
    }
}
