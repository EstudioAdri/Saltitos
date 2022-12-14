using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public Camera mainCamera;

    [SerializeField] private List<PlaceableData> AvailablePlaceablesToSpawn;

    private List<Spawn> AllSpawnPlaceables = new();
    public LayerMask obstacleFieldMask;

    private Vector3 inputCreationOffset = new Vector3(0f, 1.89f, 1f); //offsets the creation of units so that they are not under the player's finger

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            CreatePlaceable(Placeable.PlaceableType.Obstacle, "PineTree");
        }
    }

    public Spawn GetFirstAvailableSpawn()
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
        GameObject newPlaceableGO = Instantiate(placeablePrefabToPlace);
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

    private void CreatePlaceable(Placeable.PlaceableType placeableType, string objectID)
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // Comprobamos si el click est?? dentro del fieldMask donde queremos spawnear enemgios
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            PlaceableData placeableData = AvailablePlaceablesToSpawn.Where(x => x.objectID == objectID).FirstOrDefault();

            if (placeableData != null)
                PlacePlaceable(placeableData, hit.point);
            else
                Debug.Log("Error trinyg to create a placeable.");
        }
    }

    private void AddPlaceableToList(LocatedPlaceable locatedPlaceable)
    {
        if (locatedPlaceable.placeableType == Placeable.PlaceableType.Spawn)
        {
            AllSpawnPlaceables.Add(locatedPlaceable as Spawn);
        }
        else
        {
            Debug.Log("Error trying to add a LocatedPlaceable");
        }
    }
}