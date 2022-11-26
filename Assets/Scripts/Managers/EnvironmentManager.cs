using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public Camera mainCamera;

    [SerializeField] private List<PlaceableData> AvailablePlaceablesToSpawn;

    private List<LocatedPlaceable> AllSpawnPlaceables;
    public LayerMask obstacleFieldMask;

    private Vector3 inputCreationOffset = new Vector3(0f, 1.89f, 1f); //offsets the creation of units so that they are not under the player's finger

    private void Awake()
    {
        AllSpawnPlaceables = new List<LocatedPlaceable>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            CreateSpawn(Placeable.PlaceableType.Spawn);
        }
    }

    public LocatedPlaceable GetFirstAvailableSpawn()
    {
        return AllSpawnPlaceables.First();
    }

    private void SetupPlaceable(GameObject go, PlaceableData placeableData)
    {
        // aqui le podemos poner scripts en el futuro en lugar del located a pelo
        Spawn placeableScript = go.GetComponent<Spawn>();
        placeableScript.Activate(placeableData);
        AddPlaceableToList(placeableScript);
    }

    private void PlacePlaceable(PlaceableData placeableData, Vector3 position)
    {
        GameObject placeablePrefabToPlace = placeableData.associatedPrefab;
        GameObject newPlaceableGO = Instantiate<GameObject>(placeablePrefabToPlace);
        newPlaceableGO.transform.position = position;
        SetupPlaceable(newPlaceableGO, placeableData);
    }

    public void CreateSpawn(Placeable.PlaceableType placeableType)
    {
        switch (placeableType)
        {
            case Placeable.PlaceableType.Spawn:
                PlaceableData placeableData = AvailablePlaceablesToSpawn.Where(x => x.placeableType == placeableType).FirstOrDefault();

                if (placeableData != null)
                {
                    Vector3 a = new Vector3(-11.8f, 1.89f, 0.5f);
                    PlacePlaceable(placeableData, a);
                }
                break;
        }
    }

    private void CreatePlaceable(Placeable.PlaceableType placeableType)
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // Comprobamos si el click está dentro del fieldMask donde queremos spawnear enemgios
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, obstacleFieldMask))
        {
            switch (placeableType)
            {
                case Placeable.PlaceableType.Spawn:
                    PlaceableData placeableData = AvailablePlaceablesToSpawn.Where(x => x.placeableType == placeableType).FirstOrDefault();

                    if (placeableData != null)
                        PlacePlaceable(placeableData, hit.point + inputCreationOffset);
                    break;
            }
        }
    }

    private void AddPlaceableToList(LocatedPlaceable l)
    {
        if (l.placeableType == Placeable.PlaceableType.Spawn)
        {
            AllSpawnPlaceables.Add(l);
        }
        else
        {
            Debug.Log("Error trying to add a LocatedPlaceable");
        }
    }
}