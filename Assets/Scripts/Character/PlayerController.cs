using Character.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Health;
using Weapons;
using System;
using System.Linq;

namespace Character
{
    [RequireComponent(typeof(PlayerHealthComponent))]
    public partial class PlayerController : MonoBehaviour, IPausable, ISavable
    {
        public bool IsFiring;
        public bool IsReloading;
        public bool IsJumping;
        public bool IsRunning;
        public bool InInventory;

        public CrossHairScript CrossHair => CrossHairComponent;
        [SerializeField] private CrossHairScript CrossHairComponent;

        public HealthComponent Health => HealthComponent;
        private HealthComponent HealthComponent;

        public InventoryComponent Inventory => InventoryComponent;
        private InventoryComponent InventoryComponent;

        public WeaponHolder WeaponHolder => WeaponHolderComponent;
        private WeaponHolder WeaponHolderComponent;

        private GameUIController UIController;
        private PlayerInput playerInput;

        private void Awake()
        {
            UIController = FindObjectOfType<GameUIController>();
            playerInput = GetComponent<PlayerInput>();
            if (HealthComponent == null)
            {
                HealthComponent = GetComponent<HealthComponent>();
            }
            if (InventoryComponent == null)
            {
                InventoryComponent = GetComponent<InventoryComponent>();
            }
            if (WeaponHolderComponent == null)
            {
                WeaponHolderComponent = GetComponent<WeaponHolder>();
            }
        }

        public void OnPauseGame(InputValue value)
        {
            Debug.Log("Pause");
            PauseManager.Instance.PauseGame();
        }

        public void OnUnPauseGame(InputValue value)
        {
            Debug.Log("Unpause");
            PauseManager.Instance.UnPauseGame();
        }

        public void OnInventory(InputValue button)
        {
            if (InInventory)
            {
                InInventory = false;
                OpenInventory(false);
            }
            else
            {
                InInventory = true;
                OpenInventory(true);
            }
        }

        public void OpenInventory(bool open)
        {
            if (open)
            {
                PauseManager.Instance.PauseGame();
                UIController.EnableInventoryMenu();
            }
            else
            {
                PauseManager.Instance.UnPauseGame();
                UIController.EnableGameMenu();
            }
        }

        public void PauseMenu()
        {
            UIController.EnablePauseMenu();
            playerInput.SwitchCurrentActionMap("PauseActionMap");
        }

        public void UnPauseMenu()
        {
            UIController.EnableGameMenu();
            playerInput.SwitchCurrentActionMap("PlayerActionMap");
        }

        public void OnSave(InputValue button)
        {
            SaveSystem.Instance.SaveGame();
        }

        public void OnLoad(InputValue button)
        {
            SaveSystem.Instance.LoadGame();
        }

        public SaveDataBase SaveData()
        {
            // Transform, Health, Name, Item list, Weapon stats
            PlayerSaveData saveData = new PlayerSaveData
            {
                 Name = gameObject.name,
                 Position = transform.position,
                 Rotation = transform.rotation,
                 CurrentHealth = HealthComponent.Health,
                 EquippedWeaponData = new WeaponSaveData(WeaponHolder.EquippedWeapon.WeaponInformation)

            };

            var itemSaveList = Inventory.GetItemList().Select(item => new ItemSaveData(item)).ToList();
            saveData.ItemList = itemSaveList;

            return saveData;
        }

        public void LoadData(SaveDataBase saveData)
        {
            PlayerSaveData playerData = (PlayerSaveData) saveData;
            if (playerData == null) return;

            Transform playerTransform = transform;
            playerTransform.position = playerData.Position;
            playerTransform.rotation = playerData.Rotation;

            Health.SetCurrentHealth(playerData.CurrentHealth);

            foreach(ItemSaveData itemSaveData in playerData.ItemList)
            {
                ItemScriptable item = InventoryReferencer.Instance.GetItemReference(itemSaveData.Name);
                Inventory.AddItem(item, itemSaveData.Amount);
            }

            WeaponScriptable weapon = (WeaponScriptable)Inventory.FindItem(playerData.EquippedWeaponData.Name);

            if (!weapon) return;

            weapon.weaponStats = playerData.EquippedWeaponData.WeaponStats;
            WeaponHolder.EquipWeapon(weapon);
        }        
    }
}
