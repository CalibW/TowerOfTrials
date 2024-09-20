using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUpGenerator : MonoBehaviour
{
    public static DamagePopUpGenerator current; // Singleton instance of DamagePopUpGenerator
    public GameObject prefab; // Prefab for the damage pop-up

    public void Awake()
    {
        // Set the singleton instance to this
        current = this;
    }

    void Update()
    {
        // Check for key press (P) to create a test damage pop-up
        if(Input.GetKeyDown(KeyCode.P))
        {
            CreatePopUp(Vector3.one, Random.Range(0, 1000).ToString(), Color.yellow); // Create a pop-up at position (1,1,1) with random text and color
        }
    }

    // Function to create a damage pop-up at a specified position with given text and color
    public void CreatePopUp(Vector3 position, string text, Color color)
    {
        var popup = Instantiate(prefab, position, Quaternion.identity); // Instantiate the pop-up prefab at the given position
        var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>(); // Get the TextMeshPro component from the child of the pop-up
        temp.text = text; // Set the text of the pop-up
        temp.faceColor = color; // Set the color of the text

        // Destroy the pop-up after 1 second
        Destroy(popup, 1f);
    }
}
