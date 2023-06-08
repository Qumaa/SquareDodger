using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public class TempThemeResolver : IThemeResolver
    {
        private const string _THEME = ResourcesPaths.Themes._DEFAULT_DARK;
        
        public GameColorsConfig Resolve()
        {
            var theme = Resources.Load<GameColorsConfig>(_THEME);

            return theme;
        }
    }
}