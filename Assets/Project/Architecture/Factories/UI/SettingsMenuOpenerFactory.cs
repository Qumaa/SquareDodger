using Project.UI;

namespace Project.Architecture
{
    public struct SettingsMenuOpenerFactory : IFactory<ISettingsMenuOpener>
    {
        public ISettingsMenuOpener CreateNew()
        {
            return new FocusableSettingsMenuOpener();
        }
    }
}