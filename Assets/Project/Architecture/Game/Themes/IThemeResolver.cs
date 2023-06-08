using Project.Game;

namespace Project.Architecture
{
    public interface IThemeResolver
    {
        GameColorsConfig Resolve();
    }
}