using DG.Tweening;
using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public class AnimatedGameFinisher : IAnimatedGameFinisher
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

            var balanceTween = DOTween.To(
                () => PlayerShader.ColorBalance,
                x => PlayerShader.ColorBalance = x,
                1,
                1);


            float pos = 0;
            _animation.Insert(pos, camPosTween)
                .Insert(pos, camWidthTween);

            pos = _animation.Duration();

            _animation
                .Insert(pos, shaderTween)
                .Insert(pos, balanceTween);
        }

        public void Reset()
        {
            _animation?.Kill();
        }
    }
}