using System;

namespace Project.Game
{
    [Serializable]
    public class PlayerSettingsData
    {
        public GameThemes CurrentTheme;
        public bool IsCurrentThemeDark;

        public PlayerSettingsData()
        {
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            CurrentTheme = GameThemes.Default;
            IsCurrentThemeDark = true;
        }
    }
    
}