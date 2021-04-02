using Character.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    [RequireComponent(typeof(PlayerHealthComponent))]
    public class PlayerController : MonoBehaviour, IPausable
    {
        public CrossHairScript CrossHair => CrossHairComponent;
        [SerializeField] private CrossHairScript CrossHairComponent;
        
        public bool IsFiring;
        public bool IsReloading;
        public bool IsJumping;
        public bool IsRunning;

        private GameUIController UIController;
        private PlayerInput playerInput;

        private void Awake()
        {
            UIController = FindObjectOfType<GameUIController>();
            playerInput = GetComponent<PlayerInput>();
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
