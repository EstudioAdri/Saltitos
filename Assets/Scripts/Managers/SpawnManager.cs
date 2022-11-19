using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask enemyRoadFieldMask, playerFieldMask;
    public UnityAction<SpawnableData, Vector3, Spawnable.Faction> OnSpawn;

    private Vector3 inputCreationOffset = new Vector3(0f, 0f, 1f); //offsets the creation of units so that they are not under the player's finger

    private void SpawnEnemyManual()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyRoadFieldMask))
        {
            if (OnSpawn != null)
            {
                OnSpawn(Resources.Load<SpawnableData>("TestAlien"), hit.point + inputCreationOffset, Spawnable.Faction.Enemy);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SpawnEnemyManual();
    }
}
