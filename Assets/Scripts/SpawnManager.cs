using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    #region Serializable Private Fields

    [SerializeField] private GameObject objectToSpawn;

    [SerializeField] private float timeToSpawn;
    
    
    #endregion
    
    #region MonoBehaviour Callbacks

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObjectInCameraView", timeToSpawn, timeToSpawn);
    }

    #endregion

    #region Private Methods

    private void SpawnObjectInCameraView()
    {
        float ySize = Camera.main.orthographicSize;

        float xSize = ySize * Camera.main.aspect;

        float cameraX = Camera.main.transform.position.x;

        float cameraY = Camera.main.transform.position.y;

        Vector3 spawnPosition = new Vector3(
            Random.Range(cameraX-xSize, cameraX+xSize),
            Random.Range(cameraY-ySize, cameraY+ySize),
            0f);

        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }

    #endregion
}
