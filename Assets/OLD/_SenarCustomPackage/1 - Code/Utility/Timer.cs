namespace OLD_SenarCustomSystem.Utility
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Sirenix.OdinInspector;
    using UnityEngine.Events;

    [System.Serializable]
    public class Timer
    {
        [MinValue(0), SerializeField, HideInEditorMode] private float currentTime = 0;
        [MinValue(0)] public float timer;

        [MinValue(0)] public float timeScale = 1;

        [ShowInInspector] public bool isEnded { get; private set; }

        [ShowInInspector] public bool isPaused { get; private set; }


        [TabGroup("Timer", "Play & End"), Space]
        public UnityEvent onPlay;
        [TabGroup("Timer", "Play & End"), Space]
        public UnityEvent onEnd;
        [TabGroup("Timer", "Pause & Reset"), Space]
        public UnityEvent onPause;
        [TabGroup("Timer", "Pause & Reset"), Space]
        public UnityEvent onReset;
        [TabGroup("Timer", "Tick"), Space]
        public UnityEvent_Float onTick;


        public Timer(float timer, float timeScale = 1)
        {
            this.timer = timer;
            isPaused = true;
            currentTime = 0;
            isEnded = false;
            this.timeScale = timeScale;
        }


        /// <summary>
        /// Put the timer in pause
        /// </summary>
        public void Pause()
        {
            isPaused = true;
            onPause?.Invoke();
        }

        /// <summary>
        /// Resume the timer play
        /// </summary>
        public void Play()
        {
            isPaused = false;
            onPlay?.Invoke();
        }

        /// <summary>
        /// Reset the timer progress
        /// </summary>
        public void Reset()
        {
            currentTime = 0;
            isEnded = false;
            onReset?.Invoke();
        }

        /// <summary>
        /// Update current time by dt
        /// </summary>
        /// <param name="dt"></param>
        public void Tick(float dt)
        {
            if (!isPaused && !isEnded)
            {
                currentTime += dt * timeScale;
                onTick?.Invoke(dt * timeScale);
                CheckTimerEnd();
            }
        }

        /// <summary>
        /// check if timer is over, and if yes throw event and set isEnded
        /// </summary>
        protected void CheckTimerEnd()
        {
            if (currentTime >= timer)
            {
                isEnded = true;
                onEnd?.Invoke();
            }
            else
            {
                isEnded = false;
            }
        }

    }

}
