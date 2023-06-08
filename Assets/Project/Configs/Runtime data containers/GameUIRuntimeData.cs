﻿using UnityEngine;

namespace Project.Game
{
    public class GameUIRuntimeData : ILoadableFrom<GameUIConfig>
    {
        public GameObject UICanvasPrefab { get; private set; }
        public GameObject MainMenuPrefab { get; private set; }


        public void Load(GameUIConfig data)
        {
            UICanvasPrefab = data.UICanvasPrefab;
            MainMenuPrefab = data.MainMenuPrefab;
        }
    }
}