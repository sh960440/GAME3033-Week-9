using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;
using System.Health;

namespace UI.Player_UI
{
    public class HealthInfoUI : MonoBehaviour
    {
        [SerializeField] TMP_Text healthText;

        private HealthComponent playerHealthComponent;


        private void OnEnable()
        {
            PlayerEvents.OnHealthInitialized += OnHealthInitialized;
        }

        private void OnDisable()
        {
            PlayerEvents.OnHealthInitialized -= OnHealthInitialized;
        }

        private void OnHealthInitialized(HealthComponent healthComponent)
        {
            playerHealthComponent = healthComponent;
        }

        // Update is called once per frame
        void Update()
        {
            healthText.text = playerHealthComponent.Health.ToString();
        }
    }
}

