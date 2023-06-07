using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public class TempThemeResolver : IThemeResolver
    {
        private const string _THEME = ResourcesPaths.Themes._DEFAULT_DARK;
        
        public GameColorsRuntimeData Resolve()
        {
            var data = new GameColorsRuntimeData();
            var theme = Resources.Load<GameColorsConfig>(_THEME);
            
            data.Load(theme);

            return data;
        }
    }
}