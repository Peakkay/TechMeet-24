using UnityEngine;

public class PersistentUI : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject); // Prevent this GameObject from being destroyed
    }
}
