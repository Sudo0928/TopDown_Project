using UnityEngine;

namespace TopDown_Project
{
    public class DustParticleControl : MonoBehaviour
    {
        [SerializeField] private bool createDustOnWalk = true;
        [SerializeField] private ParticleSystem dustParticleSyste;

        public void CreateDustParticles()
        {
            if (createDustOnWalk)
            {
                dustParticleSyste.Stop();
                dustParticleSyste.Play();
            }
        }
    }
}
