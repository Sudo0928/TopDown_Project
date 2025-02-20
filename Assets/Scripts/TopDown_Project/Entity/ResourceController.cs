using System;
using UnityEngine;

namespace TopDown_Project
{
    public class ResourceController : MonoBehaviour
    {
        [SerializeField] private float healthChangeDelay = 0.5f;

        private BaseController baseController;
        private StatHandler statHandler;
        private AnimationHandler animationHandler;

        // HealChangeDelay를 작동 시키지 않기 위해서 초기값으로 설정
        private float timeSinceLastChange = float.MaxValue;

        public float CurrentHealth { get; private set; }
        public float MaxHealth => statHandler.Health;

        public AudioClip damageClip;

        private Action<float, float> OnChangeHealth;

        private void Awake()
        {
            statHandler = GetComponent<StatHandler>();
            animationHandler = GetComponent<AnimationHandler>();
            baseController = GetComponent<BaseController>();
        }

        private void Start()
        {
            CurrentHealth = statHandler.Health;
        }

        private void Update()
        {
            if (timeSinceLastChange < healthChangeDelay)
            {
                timeSinceLastChange += Time.deltaTime;
                if (timeSinceLastChange >= healthChangeDelay)
                {
                    animationHandler.InvincibilityEnd();
                }
            }
        }

        public bool ChangeHealth(float change)
        {
            if (change == 0 || timeSinceLastChange < healthChangeDelay)
            {
                return false;
            }

            timeSinceLastChange = 0;
            CurrentHealth += change;
            CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
            CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

            OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);

            if (change < 0)
            {
                animationHandler.Damage();

                if (damageClip != null)
                {
                    SoundManager.PlayClip(damageClip);
                }
            }

            if (CurrentHealth <= 0f)
            {
                Death();
            }

            return true;
        }

        private void Death()
        {
            baseController.Death();
        }

        public void AddHealthChangeEvent(Action<float, float> action)
        {
            OnChangeHealth += action;
        }

        public void RemoveHealthChangeEvent(Action<float, float> action)
        {
            OnChangeHealth -= action;
        }
    }
}