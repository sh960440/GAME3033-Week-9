using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Menu
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private string StartingMenu = "Main Menu";
        [SerializeField] private string RootMenu = "Main Menu";

        private MenuWidget activeWidget;
        private Dictionary<string, MenuWidget> menus = new Dictionary<string, MenuWidget>();

        // Start is called before the first frame update
        void Start()
        {
            AppEvents.Invoke_OnMouseCursorEnable(true);

            DisableAllMenus();
            EnableMenu(StartingMenu);
        }

        public void AddMenu(string menuName, MenuWidget menuWidget)
        {
            if (string.IsNullOrEmpty(menuName)) return;
            if (menus.ContainsKey(menuName))
            {
                Debug.LogError("Menu already exists in dictionary!");
                return;
            }

            if (menuWidget == null) return;
            menus.Add(menuName, menuWidget);
        }

        public void EnableMenu(string menuName)
        {
            if (string.IsNullOrEmpty(menuName)) return;
            if (menus.ContainsKey(menuName))
            {
                DisableActiveMenu();

                activeWidget = menus[menuName];
                activeWidget.EnabelWidget();
            }
            else
            {
                Debug.LogError("Menu is not available in Dictionary!");
            }
        }

        public void DisableMenu(string menuName)
        {
            if (string.IsNullOrEmpty(menuName)) return;
            if (menus.ContainsKey(menuName))
            {
                menus[menuName].DisabelWidget();
            }
            else
            {
                Debug.LogError("Menu is not available in Dictionary!");
            }
        }

        public void ReturnToRootMenu()
        {
            EnableMenu(RootMenu);
        }

        private void DisableActiveMenu()
        {
            if (activeWidget) activeWidget.DisabelWidget();
        }

        private void DisableAllMenus()
        {
            foreach (MenuWidget menu in menus.Values)
            {
                menu.DisabelWidget();
            }
        }
    }
}



