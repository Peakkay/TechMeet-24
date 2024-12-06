using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        // Load the Clue UI scene additively
        SceneManager.LoadScene("FinaleClue", LoadSceneMode.Additive);
    }
}
