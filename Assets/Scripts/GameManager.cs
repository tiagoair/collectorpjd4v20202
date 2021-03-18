using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Public Fields

    public static GameManager Instance;
    
    public bool IsGameOver => _isGameOver;

    public bool IsGameRunning
    {
        get => _isGameRunning;
        set => _isGameRunning = value;
    }

    #endregion
    
    #region Private Fields

    private bool _isGameOver;

    private bool _isGameRunning;

    #endregion
    
    #region MonoBehaviour Callbacks
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    #region Public Methods

    public void PlayerIsDead()
    {
        if (!_isGameOver && _isGameRunning)
        {
            _isGameOver = true;
            _isGameRunning = false;
            Time.timeScale = 0f;
            Observer.SetGameOver(true);
        }
    }

    public void ResetGame()
    {
        if (_isGameOver)
        {
            Time.timeScale = 1f;
            _isGameOver = false;
            SceneManager.LoadScene(0);
        }
    }

    #endregion
}
