using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private GameHUDWidget gameCanvas;
    [SerializeField] private GameHUDWidget pauseCanvas;

    private GameHUDWidget activeWidget;

    private void Start()
    {
        DisableAllMenus();
        EnableGameMenu();
    }
    
    public void EnablePauseMenu()
    {
        if (activeWidget) activeWidget.DisableWidget();

        activeWidget = pauseCanvas;
        activeWidget.EnableWidget();
    }

    public void EnableGameMenu()
    {
        if (activeWidget) activeWidget.DisableWidget();

        activeWidget = gameCanvas;
        activeWidget.EnableWidget();
    }

    public void DisableAllMenus()
    {
        gameCanvas.DisableWidget();
        pauseCanvas.DisableWidget();
    }
}

public abstract class GameHUDWidget : MonoBehaviour
{
    public virtual void EnableWidget()
    {
        gameObject.SetActive(true);
    }

    public virtual void DisableWidget()
    {
        gameObject.SetActive(false);
    }
}
