using DG.Tweening;
using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public class DoTweenGameFinisher : IAnimatedGameFinisher
    {
        public IGameplay GameToFinish { get; set; }
        public ICameraController CameraController { get; set; }
        public IPlayerBlendingShader PlayerShader { get; set; }
        public IPlayer Player { get; set; }

        private Sequence _animation;

        public void Finish()
        {
            GameToFinish.Pause();
            Animate();
        }

        private void Animate()
        {
            InitAnimation();

            _animation.Play();
        }

        private void InitAnimation()
        {
            _animation = DOTween.Sequence();

            var camPosTween = DOTween.To(
                () => CameraController.Position, 
                x => CameraController.Position = x,
                (Vector2) Player.Transform.position, 
                1);

            var camWidthTween = DOTween.To(
                () => CameraController.WidthInUnits,
                x => CameraController.WidthInUnits = x,
                3f, 
                1.5f);
            
            var shaderTween = DOTween.To(
                () => PlayerShader.HardBlendingRadius, 
                x => PlayerShader.HardBlendingRadius = x,
                2,
                0.8f);

            _animation.Insert(0, camPosTween)
                .Insert(0, camWidthTween)
                .Append(shaderTween);
        }

        public void Reset()
        {
            _animation?.Restart();
            _animation?.Kill();
        }
    }
}