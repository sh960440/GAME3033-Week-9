using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Menu
{
    public abstract class MenuWidget : MonoBehaviour
    {
        [SerializeField] private string menuName;

        protected MenuController menuController;

        private void Awake()
        {
            menuController = FindObjectOfType<MenuController>();
            if (menuController)
            {
                menuController.AddMenu(menuName, this);
            }
            else
            {
                Debug.LogError("Menu controller not found!");
            }
        }

        public void ReturnToRootMenu()
        {
            if (menuController)
            {
                menuController.ReturnToRootMenu();
            }
        }

        public void EnabelWidget()
        {
            gameObject.SetActive(true);
        }

        public void DisabelWidget()
        {
            gameObject.SetActive(false);
        }
    }
}

