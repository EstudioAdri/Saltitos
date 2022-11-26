using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public bool GameOver { get; set; }

    [SerializeField] int fpsTarget = 120;
    [SerializeField] private GameObject playerPrefab;

    [Header("Settings")]
    public bool autoStart = false;
    private bool once;

    [Header("Game session parameters")]
    [SerializeField] private Spawnable.EnemyType enemyToSpawn;

    private SpawnManager spawnManager;
    private EnvironmentManager environmentManager;

    private List<ThinkingSpawnable> playerBuildings, playerUnits, enemyUnits;
    private List<ThinkingSpawnable> allPlayer, allEnemy;
    private List<ThinkingSpawnable> allThinkingSpawnables;
    

    // These need to be worked on.s
    private List<ThinkingSpawnable> player;
    [SerializeField] private ThinkingSpawnable castle;



    private void Awake()
    {
        Application.targetFrameRate = fpsTarget;
        environmentManager = GetComponent<EnvironmentManager>();
        spawnManager = GetComponent<SpawnManager>();
        playerBuildings = new List<ThinkingSpawnable>();
        playerUnits = new List<ThinkingSpawnable>();
        enemyUnits = new List<ThinkingSpawnable>();
        allPlayer = new List<ThinkingSpawnable>();
        allEnemy = new List<ThinkingSpawnable>();
        allThinkingSpawnables = new List<ThinkingSpawnable>();
        player = new List<ThinkingSpawnable>();



        // listener al spawn del spawnmanager
        spawnManager.OnSpawn += SpawnSpawnable;
    }

    // Start is called before the first frame update
    void Start()
    {

        GameObject playerInstantiation = Instantiate<GameObject>(playerPrefab);
        playerInstantiation.transform.position = new Vector3(10, 0, 10);
        environmentManager.CreateSpawn(Placeable.PlaceableType.Spawn);


        once = true;

    }

    public void GameOverEvent()
    {

    }

    public void Update()
    {
        if (GameOver)
            return;
        
        if (once)
        {
            StartCoroutine(spawnManager.SpawnPeriodic(Spawnable.SpawnableType.Entity, Spawnable.Faction.Enemy, enemyToSpawn));
            once = false;
        }
    }

    public bool FindClosestInList(Vector3 p, List<ThinkingSpawnable> list, Spawnable.SpawnableType targetType, out ThinkingSpawnable targetToPass)
    {
        targetToPass = null;

        bool targetFound = false;

        // use in else when multiple targets iterate distances over all elements in list
        // and save min as min to assign to targetToPass.
        // float minDistance = Mathf.Infinity;

        if (targetType == Spawnable.SpawnableType.Castle)
        {
            targetToPass = castle;
            targetFound = true;
        }

        return targetFound;
    }

    public List<ThinkingSpawnable> GetAttackList(Spawnable.Faction f, Spawnable.SpawnableType spawnableTarget)
    {
        switch (spawnableTarget)
        {
            case Spawnable.SpawnableType.Player:
                return player;
            case Spawnable.SpawnableType.Castle:
                return new List<ThinkingSpawnable>() { castle };
            default:
                Debug.LogError("Wrong faction when trying to get attack list");
                return null;
        }
    }

    private void SpawnSpawnable(SpawnableData spawnData, Vector3 position)
    {
        GameObject enemyPrefabToSpawn = spawnData.associatedPrefab;
        GameObject newSpawnableGO = Instantiate<GameObject>(enemyPrefabToSpawn);
        newSpawnableGO.transform.position = position;
        SetupSpawnable(newSpawnableGO, spawnData);
    }

    private void SetupSpawnable(GameObject go, SpawnableData spawnableData)
    {
        Entity entityScript = go.GetComponent<Entity>();
        entityScript.Activate(spawnableData);
        AddSpawnableToList(entityScript);
    }

    private void AddSpawnableToList(ThinkingSpawnable s)
    {
        allThinkingSpawnables.Add(s);

        if (s.faction == Spawnable.Faction.Player)
        {
            allPlayer.Add(s);
            if (s.spawnableType == Spawnable.SpawnableType.Building)
            {
                playerBuildings.Add(s);
            }
            else if (s.spawnableType == Spawnable.SpawnableType.Player)
            {
                player.Add(s);
            }
            else
            {
                playerUnits.Add(s);
            }
        }
        else if (s.faction == Spawnable.Faction.Enemy)
        {
            allEnemy.Add(s);
            if (s.spawnableType == Spawnable.SpawnableType.Entity)
            {
                enemyUnits.Add(s);
            }
            else
            {
                Debug.LogError("Wrong type of SpawnableType when trying to spawn an Enemy");
            }
        }
        else
        {
            Debug.LogError("Error when trying to add a ThiningSpawnable");
        }
    }
}
