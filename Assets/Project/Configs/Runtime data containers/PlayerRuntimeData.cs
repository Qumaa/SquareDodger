using UnityEngine;

namespace Project.Game
{
    public class PlayerRuntimeData : ILoadableFrom<PlayerConfig>
    {
        public GameObject PlayerPrefab { get; private set; }
        public float MovementSpeed { get; private set; }
        public Material PlayerMaterial { get; private set; }
        public float TrailLength { get; private set; }
        public Material TrailMaterial { get; private set; }
        
        public void Load(PlayerConfig data)
        {
            PlayerPrefab = data.PlayerPrefab;
            MovementSpeed = data.MovementSpeed;
            PlayerMaterial = data.PlayerMaterial;
            TrailLength = data.TrailLength;
            TrailMaterial = data.TrailMaterial;
        }
    }
}