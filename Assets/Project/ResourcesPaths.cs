namespace Project
{
    public static class ResourcesPaths
    {
        public static class Themes
        {
            // paths
            private const string _THEMES = "Game themes/";
            private const string _DARK_THEMES = _THEMES + "Dark/";
            private const string _BRIGHT_THEMES = _THEMES + "Bright/";
            
            // asset names
            private const string _DEFAULT = "Game colors";

            public const string _DEFAULT_DARK = _DARK_THEMES + _DEFAULT;
            public const string _DEFAULT_BRIGHT = _BRIGHT_THEMES + _DEFAULT;
        }
    }
}