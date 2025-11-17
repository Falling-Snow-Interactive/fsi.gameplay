using System;
using UnityEngine;

namespace Fsi.Gameplay.Timers
{
    [Serializable]
    public class Timer
    {
        private event Action onComplete;
        private event Action<float> onTick;

        [SerializeField]
        private TimerStatus status;
        public TimerStatus Status => status;

        public bool Active => status is TimerStatus.Play or TimerStatus.Pause;
        
        [Min(0)]
        [SerializeField]
        private float time;
        
        [SerializeField]
        private float remaining = 0;

        public float Normalized => 1f - (remaining / time);

        public Timer(float time)
        {
            this.time = time;
            remaining = time;
            status = TimerStatus.None;
        }
        
        public Timer Play()
        {
            TimerManager.Instance.Add(this);
            
            status = TimerStatus.Play;
            remaining = time;
            return this;
        }

        public Timer Stop(bool complete = false)
        {
            status = complete ? TimerStatus.Complete : TimerStatus.Stop;
            remaining = 0;
            if (complete)
            {
                onComplete?.Invoke();
            }
            return this;
        }

        public Timer Pause()
        {
            status = TimerStatus.Pause;
            return this;
        }
        
        public void Tick(float deltaTime)
        {
            if (status != TimerStatus.Play)
            {
                return;
            }
            
            remaining -= deltaTime;
            if (remaining <= 0)
            {
                status = TimerStatus.Complete;
                onComplete?.Invoke();
            }
            
            onTick?.Invoke(deltaTime);
        }

        public Timer OnComplete(Action onComplete)
        {
            this.onComplete += onComplete;
            return this;
        }

        public Timer OnTick(Action<float> onTick)
        {
            this.onTick += onTick;
            return this;
        }

        public override string ToString() => $"Timer - {remaining:0.00}s/{time}s ({Normalized * 100:00}%)";
    }
}