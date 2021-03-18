using System;

public static class Observer
{
    #region UI events

    public static event Action<float> OnSetSlider;

    public static void SetSlider(float value)
    {
        OnSetSlider?.Invoke(value);
    }
    
    public static event Action<bool> OnSetGameOver;

    public static void SetGameOver(bool value)
    {
        OnSetGameOver?.Invoke(value);
    }

    #endregion
}
