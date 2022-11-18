using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    public bool autoStart = false;

    [Header("Public References")]
    public GameObject player;

    // private List<ThinkingPlaceable> enemies;
    private bool gameOver = false;
    private bool updateAllSpawnables = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
