using System;
using UnityEngine.SceneManagement;

namespace Project.Architecture
{
    public class InitializeMenuState : GameState
    {
        public InitializeMenuState(IGameStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            LoadMenu();
            LoadGame();
        }

        private void LoadGame()
        {
            SceneManager.LoadScene(SceneNames.GAME, LoadSceneMode.Additive);
        }
        
        private void LoadMenu()
        {
            SceneManager.LoadScene(SceneNames.MENU, LoadSceneMode.Additive);
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }
    }
}