using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveUI : MonoBehaviour
{
    public PlayerAttributes PlayerAttributes; // Reference to the player's attributes

    void Awake()
    {
        // Check if there are multiple Quest objects in the scene
        if (FindObjectsOfType<Quest>().Length > 1)
        {
            Destroy(gameObject); // Destroy this GameObject if there is more than one Quest object
        }
        else
        {
            DontDestroyOnLoad(gameObject); // Keep this GameObject when loading new scenes
        }
    }

    void Start()
    {
        // Ensure this GameObject persists across scene loads
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        // Check if PlayerAttributes is null (destroy this GameObject if it is)
        if (PlayerAttributes == null)
        {
            Destroy(gameObject); // Remove this GameObject if there are no player attributes
        }
    }
}
