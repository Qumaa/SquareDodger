using UnityEngine;

namespace Project.Architecture
{
    public class GameFinisher : IGameFinisher
    {
        public IGameplay GameToFinish { get; set; }

        public void Finish()
        {
            GameToFinish.Pause();
            Debug.Log("Make a normal game finishing you lazy ass");
        }
    }
}