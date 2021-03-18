using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergySlider : MonoBehaviour
{
    #region Private Fields

    private Slider UISlider;

    #endregion
    
    
    #region MonoBehaviour Callbacks

    private void OnEnable()
    {
        Observer.OnSetSlider += SetSlider;
    }

    private void OnDisable()
    {
        Observer.OnSetSlider -= SetSlider;
    }

    // Start is called before the first frame update
    void Start()
    {
        UISlider = GetComponent<Slider>();
    }

    #endregion

    #region Private Methods

    private void SetSlider(float value)
    {
        UISlider.value = value;
    }

    #endregion
}
