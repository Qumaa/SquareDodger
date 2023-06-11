﻿namespace Project
{
    public class PausableAndResettable : IPausableAndResettable
    {
        protected bool _isPaused { get; private set; }
        
        public void Pause()
        {
            _isPaused = true;
            OnPaused();
        }

        protected virtual void OnPaused()
        {
        }

        public void Resume()
        {
            _isPaused = false;
            OnResumed();
        }

        protected virtual void OnResumed()
        {
        }

        public void Reset()
        {
            OnReset();
        }

        protected virtual void OnReset()
        {
        }
    }
}