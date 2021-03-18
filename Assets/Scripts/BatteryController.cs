using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryController : MonoBehaviour
{

    #region Serializable Private Fields

    [SerializeField] private float timeToLive;
    
    #endregion

    #region Private Fields

    private float _createdTime;

    #endregion
    
    #region MonoBehaviour Callbacks

    // Start is called before the first frame update
    void Start()
    {
        ResetTime();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTime();
    }

    #endregion

    #region Private Methods

    private void CheckTime()
    {
        if (Time.time > _createdTime + timeToLive)
        {
            BatteryPoolManager.Instance.DeactivateObject(gameObject);
        }
    }

    #endregion

    #region Public Methods

    public void ResetTime()
    {
        _createdTime = Time.time;
    }

    #endregion
}
