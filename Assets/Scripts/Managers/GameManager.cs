using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    public bool autoStart = false;

    [Header("Public References")]
    public GameObject playerCastle;

    private SpawnManager spawnManager;

    private List<ThinkingSpawnable> playerBuildings, playerUnits, enemyUnits;
    private List<ThinkingSpawnable> allPlayer, allEnemy;
    private List<ThinkingSpawnable> allThinkingSpawnables;
    private List<ThinkingSpawnable> player;

    private bool gameOver = false;
    private bool updateAllSpawnables = false;


    private void Awake()
    {
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
        
    }

    public void GameOver()
    {

    }

    public void Update()
    {
        if (gameOver)
            return;

        ThinkingSpawnable targetToPass;
        ThinkingSpawnable s;

        for (int spawnableN = 0; spawnableN < allThinkingSpawnables.Count; spawnableN++)
        {
            s = allThinkingSpawnables[spawnableN];

            if (updateAllSpawnables)
                s.state = ThinkingSpawnable.States.Idle;

            switch (s.state)
            {
                case ThinkingSpawnable.States.Idle:
                    bool targetFound = FindClosestInList(s.transform.position, GetAttackList(s.faction, s.targetType), s.targetType, out targetToPass);
                    if (!targetFound)
                        Debug.LogError("No targets found");

                    s.SetTarget(targetToPass);
                    s.Seek();
                    break;

                case ThinkingSpawnable.States.Seeking:
                    if (s.IsTargetInRange())
                    {
                        s.Stop();
                    }
                    break;
            }
        }

        updateAllSpawnables = false;
    }

    private bool FindClosestInList(Vector3 p, List<ThinkingSpawnable> list, Spawnable.SpawnableTarget targetType, out ThinkingSpawnable targetToPass)
    {
        targetToPass = null;

        bool targetFound = false;

        // use in else when multiple targets iterate distances over all elements in list
        // and save min as min to assign to targetToPass.
        float minDistance = Mathf.Infinity;

        if (targetType == Spawnable.SpawnableTarget.Player)
        {
            targetToPass = list[0];
            targetFound = true;
        }

        return targetFound;
    }

    private List<ThinkingSpawnable> GetAttackList(Spawnable.Faction f, Spawnable.SpawnableTarget spawnableTarget)
    {
        switch (spawnableTarget)
        {
            case Spawnable.SpawnableTarget.Player:
                return player;
            default:
                Debug.LogError("Wrong faction when trying to get attack list");
                return null;
        }
    }

    private void SpawnSpawnable(SpawnableData spawnData, Vector3 position, Spawnable.Faction spawnableFaction)
    {
        GameObject enemyPrefabToSpawn = spawnData.associatedPrefab;
        GameObject newSpawnableGO = Instantiate<GameObject>(enemyPrefabToSpawn, position, Quaternion.identity);
        SetupSpawnable(newSpawnableGO, spawnData, spawnableFaction);

        // This way we start updating after an spawnable has been instantiated
        updateAllSpawnables = true;
    }

    private void SetupSpawnable(GameObject go, SpawnableData spawnableDataRef, Spawnable.Faction spawnableFaction)
    {
        switch (spawnableDataRef.sType)
        {
            case Spawnable.SpawnableType.Enemy:
                Enemy enemyScript = go.GetComponent<Enemy>();
                enemyScript.Activate(spawnableFaction, spawnableDataRef);
                AddSpawnableToList(enemyScript);
                break;
        }
    }

    private void AddSpawnableToList(ThinkingSpawnable s)
    {
        allThinkingSpawnables.Add(s);

        if (s.faction == Spawnable.Faction.Player)
        {
            allPlayer.Add(s);
            if (s.sType == Spawnable.SpawnableType.Building)
            {
                playerBuildings.Add(s);
            }
            else
            {
                playerUnits.Add(s);
            }
        }
        else if (s.faction == Spawnable.Faction.Enemy)
        {
            allEnemy.Add(s);
            if (s.sType == Spawnable.SpawnableType.Enemy)
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
