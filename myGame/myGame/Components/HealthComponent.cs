using Microsoft.Xna.Framework;
using System;

namespace myGame.Components{
    public class HealthComponent
    {
        private int maxHealth;
        private int currentHealth;
        private float invulnerabilityTime;
        private float invulnerabilityTimer;
        private bool isInvulnerable;

        public int Health => currentHealth;
        public bool IsInvulnerable => isInvulnerable;

        public event Action OnHealthChanged;
        public event Action OnDeath;

        public HealthComponent(int maxHealth = 3, float invulnerabilityTime = 1.5f)
        {
            this.maxHealth = maxHealth;
            this.invulnerabilityTime = invulnerabilityTime;
            Reset();
        }

        public void Update(GameTime gameTime)
        {
            if (isInvulnerable)
            {
                invulnerabilityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (invulnerabilityTimer <= 0)
                {
                    isInvulnerable = false;
                }
            }
        }

        public void TakeDamage()
        {
            if (!isInvulnerable)
            {
                currentHealth--;
                isInvulnerable = true;
                invulnerabilityTimer = invulnerabilityTime;
                OnHealthChanged?.Invoke();

                if (currentHealth <= 0)
                {
                    OnDeath?.Invoke();
                }
            }
        }

        public void Reset()
        {
            currentHealth = maxHealth;
            isInvulnerable = false;
            invulnerabilityTimer = 0f;
            OnHealthChanged?.Invoke();
        }
    } 
}