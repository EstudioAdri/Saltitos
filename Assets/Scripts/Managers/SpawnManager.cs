using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    public Camera mainCamera;

    // Layer masks que van seteadas en la interfaz
    [SerializeField] private float spawnEveryNSecond;
    [SerializeField] private List<SpawnableData> AvailableEnemiesToSpawn;
    [SerializeField] private bool toggleEnemySpawn = false;
    [SerializeField] private EnvironmentManager environmentManager;

    public LayerMask enemyRoadFieldMask, playerFieldMask;
    public UnityAction<SpawnableData, Vector3> OnSpawn;

    private Vector3 inputCreationOffset = new Vector3(0f, 0f, 1f); //offsets the creation of units so that they are not under the player's finger

    public void Awake()
    {
        environmentManager = FindObjectOfType<EnvironmentManager>();
    }
    public IEnumerator SpawnPeriodic(Spawnable.SpawnableType type, Spawnable.Faction faction, Spawnable.EnemyType enemyToSpawn)
    {
        while (true)
        {
            if (toggleEnemySpawn)
            {
                SpawnFromTypeAndFaction(type, faction, enemyToSpawn);
            }
            yield return new WaitForSeconds(spawnEveryNSecond);
        }
    }

    public void SpawnFromTypeAndFaction(Spawnable.SpawnableType type, Spawnable.Faction faction, Spawnable.EnemyType whichEnemy)
    {
        switch (type)
        {
            case Spawnable.SpawnableType.Entity:
                if (Spawnable.Faction.Enemy == faction)
                {
                    LocatedPlaceable spawn = environmentManager.GetFirstAvailableSpawn();

                    if (spawn != null)
                    {
                        SpawnableData spawnableData = AvailableEnemiesToSpawn.Where(x => x.EnemyType == whichEnemy &&
                                                                                                    x.EnemyType != Spawnable.EnemyType.NotEnemy).FirstOrDefault();
                        if (spawnableData != null)
                            OnSpawn(spawnableData, spawn.transform.position);
                        else
                            Debug.Log("SpawnManager has not spawnableData selected");
                    }
                    else
                    {
                        Debug.Log("There are no spawns in this environment");
                    }
                }
                break;
            default:
                Debug.Log("Spawnable type and/or faction not found.");
                break;
        }
    }

    private void SpawnEntityManual(Spawnable.SpawnableType type)
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // Comprobamos si el click está dentro del fieldMask donde queremos spawnear enemgios
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyRoadFieldMask))
        {
            if (OnSpawn != null)
            {
                switch (type)
                {
                    case Spawnable.SpawnableType.Entity:
                        OnSpawn(Resources.Load<SpawnableData>("GameData/Spawnables/TestAlien"), hit.point + inputCreationOffset);
                        break;
                    case Spawnable.SpawnableType.Castle:
                        OnSpawn(Resources.Load<SpawnableData>("GameData/Spawnables/TestCastle"), hit.point + inputCreationOffset);
                        break;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnEveryNSecond = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log("Button Down OK");
            SpawnEntityManual(Spawnable.SpawnableType.Entity);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            // Debug.Log("Button Down OK");
            SpawnEntityManual(Spawnable.SpawnableType.Castle);
        }
    }
}
