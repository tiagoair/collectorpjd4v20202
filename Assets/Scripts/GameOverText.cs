using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverText : MonoBehaviour
{
    #region Private Fields

    private TMP_Text _text;

    #endregion
    
    #region MonoBehaviour Callbacks

    private void OnEnable()
    {
        Observer.OnSetGameOver += SetGameOver;
    }

    private void OnDisable()
    {
        Observer.OnSetGameOver -= SetGameOver;
    }

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    #endregion

    #region Private Methods

    private void SetGameOver(bool state)
    {
        _text.enabled = state;
    }

    #endregion
}
