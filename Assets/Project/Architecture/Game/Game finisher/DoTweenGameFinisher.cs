using System;
using DG.Tweening;
using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public class DoTweenGameFinisher : IAnimatedGameFinisher
    {
        private IPlayer _player;
        
        public IGameplay GameToFinish { get; set; }
        public ICameraController CameraController { get; set; }
        public IPlayerBlendingShader PlayerShader { get; set; }
        public IPlayer Player
        {
            get => _player;
            set => SetPlayer(value);
        }

        public void Finish()
        {
            GameToFinish.Pause();
            Animate();
        }

        private void Animate()
        {
            AnimateCamera(AnimateShader);
        }

        private void AnimateCamera(Action callback)
        {
            DOTween.To(() => CameraController.Position, x => CameraController.Position = x,
                (Vector2) Player.Transform.position, 1);

            DOTween.To(() => CameraController.WidthInUnits, x => CameraController.WidthInUnits = x,
                3f, 1.5f).OnComplete(() => callback());
        }

        private void AnimateShader()
        {
            DOTween.To(() => PlayerShader.BlendingRadius, x => PlayerShader.BlendingRadius = x,
                2, 0.8f);
        }

        private void SetPlayer(IPlayer player)
        {
            _player = player;
            _player.OnDied += Finish;
        }
    }
}