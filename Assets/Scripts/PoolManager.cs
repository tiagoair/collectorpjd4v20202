using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    #region Serializable Private Fields

    [SerializeField] private GameObject pooledObject;

    [SerializeField] private int initialSize;
    
    #endregion

    #region Private Fields

    private List<GameObject> _objectPool;

    #endregion
    
    #region MonoBehaviour Callbacks

    // Start is called before the first frame update
    protected void Start()
    {
        InitializePool();
    }

    #endregion

    #region Protected Methods

    protected void InitializePool()
    {
        _objectPool = new List<GameObject>();
        
        for (int i = 0; i < initialSize; i++)
        {
            GameObject createdObject = Instantiate(pooledObject, transform);
            createdObject.SetActive(false);
            _objectPool.Add(createdObject);
        }
    }

    #endregion

    #region Public Methods

    public virtual GameObject ActivateObject(Vector3 position)
    {
        if (_objectPool.Count > 0)
        {
            GameObject objectToActivate = _objectPool[0];
            objectToActivate.SetActive(true);
            objectToActivate.transform.parent = null;
            objectToActivate.transform.position = position;
            _objectPool.RemoveAt(0);

            return objectToActivate;
        }
        
        GameObject createdObject = Instantiate(pooledObject, position, Quaternion.identity);

        return createdObject;
        
    }

    public virtual void DeactivateObject(GameObject objectToDeactivate)
    {
        objectToDeactivate.SetActive(false);
        objectToDeactivate.transform.parent = transform;
        _objectPool.Add(objectToDeactivate);
    }

    #endregion
}
