using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPoolManager : PoolManager
{
    #region Public Fields

    public static BatteryPoolManager Instance;

    #endregion
    
    #region MonoBehaviour Callbacks

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        base.Start();
    }

    #endregion

    #region Public Methods

    public override GameObject ActivateObject(Vector3 position)
    {
        GameObject batteryObject = base.ActivateObject(position);
        batteryObject.GetComponent<BatteryController>().ResetTime();
        return batteryObject;
    }

    #endregion
}
