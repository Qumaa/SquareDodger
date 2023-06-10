using System;
using Project.Game;

namespace Project.UI
{
    public abstract class GameInputCanvasUI : GameCanvasUI, IGameInputCanvasUI
    {
        private IGameInputService _inputService;
        private Action _inputHandler;

        public IGameInputService InputService
        {
            get => _inputService;
            set => SetInputService(value);
        }

        public override void Show()
        {
            base.Show();
            UpdateInputServiceEvent();
        }

        public override void Hide()
        {
            base.Hide();
            UpdateInputServiceEvent();
        }

        private void SetInputService(IGameInputService service)
        {
            _inputService = service;
            _inputHandler = GetInputHandler();
            UpdateInputServiceEvent();
        }

        private void UpdateInputServiceEvent()
        {
            if (InputService == null)
                return;

            if (_visible)
                InputService.OnScreenTouchInput += _inputHandler;
            else
                InputService.OnScreenTouchInput -= _inputHandler;
        }

        protected abstract Action GetInputHandler();
    }
}