using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        UnPauseGame();
    }

    public void PauseGame()
    {
        var pausables = FindObjectsOfType<MonoBehaviour>().OfType<IPausable>();

        foreach (IPausable pause in pausables)
        {
            pause.PauseMenu();
        }

        Time.timeScale = 0.0f;
        AppEvents.Invoke_OnMouseCursorEnable(true);
    }

    public void UnPauseGame()
    {
        var pausables = FindObjectsOfType<MonoBehaviour>().OfType<IPausable>();

        foreach (IPausable pause in pausables)
        {
            pause.UnPauseMenu();
        }

        Time.timeScale = 1.0f;
        AppEvents.Invoke_OnMouseCursorEnable(false);
    }

    private void OnDestroy()
    {
        UnPauseGame();
    }
}

interface IPausable
{
    void PauseMenu();
    void UnPauseMenu();
}