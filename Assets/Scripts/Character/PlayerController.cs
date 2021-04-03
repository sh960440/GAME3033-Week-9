using Character.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Health;
using Weapons;

namespace Character
{
    [RequireComponent(typeof(PlayerHealthComponent))]
    public class PlayerController : MonoBehaviour, IPausable
    {
        public bool IsFiring;
        public bool IsReloading;
        public bool IsJumping;
        public bool IsRunning;

        public CrossHairScript CrossHair => CrossHairComponent;
        [SerializeField] private CrossHairScript CrossHairComponent;

        public HealthComponent Health => HealthComponent;
        private HealthComponent HealthComponent;

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
    }
}
