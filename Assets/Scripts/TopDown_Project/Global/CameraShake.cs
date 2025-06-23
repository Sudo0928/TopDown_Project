using Cinemachine;
using UnityEngine;

namespace TopDown_Project
{
    public class CameraShake : MonoBehaviour
    {
        private CinemachineVirtualCamera virtualCamera;
        private CinemachineBasicMultiChannelPerlin perline;
        private float shakeTimeRemaining;

        bool isInit = false;

        private void Awake()
        {
            if(isInit == false) Init();
        }

        void Init()
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
            perline = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            isInit = true;
        }

        public void ShakeCamera(float duration, float amplitude, float frequency)
        {
            if(isInit == false) Init();

            if (shakeTimeRemaining > duration)
                return;

            shakeTimeRemaining = duration;

            perline.m_AmplitudeGain = amplitude;
            perline.m_FrequencyGain = frequency;
        }

        private void Update()
        {
            if(shakeTimeRemaining > 0)
            {
                shakeTimeRemaining -= Time.deltaTime;
                if(shakeTimeRemaining <= 0f)
                {
                    StopShake();
                }
            }
        }

        public void StopShake()
        {
            shakeTimeRemaining = 0f;
            perline.m_FrequencyGain = 0f;
            perline.m_AmplitudeGain = 0f;
        }
    }
}
