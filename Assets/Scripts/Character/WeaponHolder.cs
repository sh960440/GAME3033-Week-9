using System;
using Character.UI;
using Parent;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace Character
{
    public class WeaponHolder : MonoBehaviour
    {
        [Header("Weapon To Spawn"), SerializeField]
        private WeaponScriptable WeaponToSpawn;

        [SerializeField] private Transform WeaponSocketLocation;

        private Transform GripIKLocation;
        private bool WasFiring = false;
        private bool FiringPressed = false;
        
        //Components
        public PlayerController Controller => PlayerController;
        private PlayerController PlayerController;
        
        public CrossHairScript Crosshair => PlayerCrosshair;
        private CrossHairScript PlayerCrosshair;
        private Animator PlayerAnimator;
        
        //Ref
        private Camera ViewCamera;
        private WeaponComponent EquippedWeapon;
        
        private static readonly int AimHorizontalHash = Animator.StringToHash("AimHorizontal");
        private static readonly int AimVerticalHash = Animator.StringToHash("AimVertical");
        private static readonly int IsFiringHash = Animator.StringToHash("IsFiring");
        private static readonly int IsReloadingHash = Animator.StringToHash("IsReloading");
        private static readonly int WeaponTypeHash = Animator.StringToHash("WeaponType");


        private new void Awake()
        {
            PlayerAnimator = GetComponent<Animator>();
            PlayerController = GetComponent<PlayerController>();
            if (PlayerController)
            {
                PlayerCrosshair = PlayerController.CrossHair;
            }
            
            ViewCamera = Camera.main;
        }

        // Start is called before the first frame update
        void Start()
        {
            if (WeaponToSpawn) EquipWeapon(WeaponToSpawn);
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (GripIKLocation == null) return;
            PlayerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            PlayerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, GripIKLocation.position);
        }
        
        private void OnFire(InputValue pressed)
        {
            //FiringPressed = pressed.ReadValue<float>() == 1f ? true : false;
            FiringPressed = pressed.isPressed;

            if (EquippedWeapon == null) return;
            
            if (FiringPressed)
                StartFiring();
            else
                StopFiring();
            
        }

        private void StartFiring()
        {
            if (GripIKLocation == null) return;

            //TODO: Weapon Seems to be reloading after no bullets left
            if (EquippedWeapon.WeaponInformation.BulletsAvailable <= 0 &&
                EquippedWeapon.WeaponInformation.BulletsInClip <= 0) return;
       
            PlayerController.IsFiring = true;
            PlayerAnimator.SetBool(IsFiringHash, true);
            EquippedWeapon.StartFiringWeapon();
        }

        private void StopFiring()
        {
            if (GripIKLocation == null) return;

            PlayerController.IsFiring = false;
            PlayerAnimator.SetBool(IsFiringHash, false);
            EquippedWeapon.StopFiringWeapon();
        }

        
        private void OnReload(InputValue button)
        {
            StartReloading();
        }

        public void StartReloading()
        {
            if (GripIKLocation == null) return;

            if (EquippedWeapon.WeaponInformation.BulletsAvailable <= 0 && PlayerController.IsFiring)
            {
                StopFiring();
                return;
            }

            PlayerController.IsReloading = true;
            PlayerAnimator.SetBool(IsReloadingHash, true);
            EquippedWeapon.StartReloading();
            
            InvokeRepeating(nameof(StopReloading), 0, .1f);
        }
        
        private void StopReloading()
        {
            if (GripIKLocation == null) return;

            if (PlayerAnimator.GetBool(IsReloadingHash)) return;
            
            PlayerController.IsReloading = false;
            EquippedWeapon.StopReloading();
            CancelInvoke(nameof(StopReloading));
            
            if (!WasFiring || !FiringPressed) return;
            
            StartFiring();
            WasFiring = false;
        }
        
        private void OnLook(InputValue obj)
        {
            Vector3 independentMousePosition = ViewCamera.ScreenToViewportPoint(PlayerCrosshair.CurrentAimPosition);
            
            PlayerAnimator.SetFloat(AimHorizontalHash, independentMousePosition.x);
            PlayerAnimator.SetFloat(AimVerticalHash, independentMousePosition.y);
        }
        
        public void EquipWeapon(WeaponScriptable weaponScriptable)
        {
            if (weaponScriptable == null) return;

            GameObject spawnedWeapon = Instantiate(weaponScriptable.itemPrefab, WeaponSocketLocation.position, WeaponSocketLocation.rotation, WeaponSocketLocation);
            if (!spawnedWeapon) return;
            
            EquippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();
            if (!EquippedWeapon) return;
            
            EquippedWeapon.Initialize(this, weaponScriptable);
            
            PlayerEvents.Invoke_OnWeaponEquipped(EquippedWeapon);
            
            GripIKLocation = EquippedWeapon.GripLocation;
            PlayerAnimator.SetInteger(WeaponTypeHash, (int)EquippedWeapon.WeaponInformation.WeaponType);
        }

        public void UnEquipItem()
        {
            if (EquippedWeapon == null) return;

            Destroy(EquippedWeapon.gameObject);
            EquippedWeapon = null;
        }
    }
}
