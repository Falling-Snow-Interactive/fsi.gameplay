using System;
using Fsi.General.Timers;
using UnityEngine;
using UnityEngine.VFX;

namespace Fsi.Gameplay.VisualEffects
{
    public class VfxDespawner : MonoBehaviour
    {
        [SerializeField]
        private VisualEffect vfx;

        [SerializeField]
        private float delay = 1f;

        [SerializeField]
        private VfxDespawnOn despawnOn = VfxDespawnOn.None;

        private Timer delayTimer;
        
        private void Start()
        {
            delayTimer = new Timer(delay);
            delayTimer.Start();
        }

        private void FixedUpdate()
        {
            if (delayTimer.Status != TimerStatus.Finished)
            {
                return;
            }
            
            switch (despawnOn)
            {
                case VfxDespawnOn.None:
                    break;
                case VfxDespawnOn.Immediate:
                    Destroy(gameObject);
                    break;
                case VfxDespawnOn.ZeroParticles:
                    if (vfx.aliveParticleCount == 0)
                    {
                        Destroy(gameObject);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
