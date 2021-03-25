using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Menu
{
    public class MainMenuWidget : MenuWidget
    {
        [SerializeField] private string startMenuName = "Load Game Menu";
        [SerializeField] private string optionsMenuName = "Options Menu";
        
        public void OpenStartMenu()
        {
            menuController.EnableMenu(startMenuName);
        }

        public void OpenOptionsMenu()
        {
            menuController.EnableMenu(optionsMenuName);
        }

        public void QuitApplication()
        {
            Application.Quit();
        }
    }
}

