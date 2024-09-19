using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveUI : MonoBehaviour
{

    public PlayerAttributes PlayerAttributes;

     void Awake()
    {
        if (FindObjectsOfType<Quest>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if(PlayerAttributes == null)
        {
            Destroy(gameObject);
        }
    }
}
