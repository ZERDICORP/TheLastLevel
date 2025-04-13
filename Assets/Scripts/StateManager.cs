using UnityEngine;
using UnityEngine.InputSystem;

public class StateManager
{
    private static StateManager _instance;
    private static bool _initialized = false;

    private StateManager()
    {
        InitDefaults();
    }

    public static StateManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new StateManager();
            }
            return _instance;
        }
    }

    private void InitDefaults()
    {
        if (_initialized) return;
        _initialized = true;

        SetCurrentLevel("Level1");
        SetDoubleJump(false);
        SetRealityChanger(false);
    }

    public void SetCurrentLevel(string level)
    {
        PlayerPrefs.SetString("CurrLevel", level);
        PlayerPrefs.Save();
    }

    public string GetCurrentLevel()
    {
        return PlayerPrefs.GetString("CurrLevel");
    }

    public void SetDoubleJump(bool value)
    {
        PlayerPrefs.SetInt("DoubleJump", value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool GetDoubleJump()
    {
        return PlayerPrefs.GetInt("DoubleJump") == 1;
    }

    public void SetRealityChanger(bool value)
    {
        PlayerPrefs.SetInt("RealityChanger", value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool GetRealityChanger()
    {
        return PlayerPrefs.GetInt("RealityChanger") == 1;
    }

    public bool GetBool(string key)
    {
        return PlayerPrefs.GetInt(key) == 1;
    }

    public void ClearAll()
    {
        PlayerPrefs.DeleteAll();
        _initialized = false;
        InitDefaults();
    }
}
