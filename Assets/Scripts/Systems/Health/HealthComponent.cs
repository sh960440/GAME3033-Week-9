using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Health;
using Character;

namespace System.Health
{
    public class HealthComponent : MonoBehaviour, IDamagable
    {
        public float Health => currentHealth;
        public float MaxHealth => totalHealth;
        private float currentHealth;
        [SerializeField] private float totalHealth; 

        [SerializeField] private ConsumableScriptable potionItem;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            currentHealth = totalHealth;
        }

        public virtual void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Destroy();
            }
        }

        public virtual void Destroy()
        {
            Destroy(gameObject);
        }

        public void HealPlayer(int effect)
        {
            if (currentHealth < MaxHealth)
            {
                currentHealth = Mathf.Clamp(currentHealth + effect, 0, MaxHealth);
            }
        }

        public void SetCurrentHealth(float health)
        {
            currentHealth = health;
        }
    }
}