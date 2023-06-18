#if UNITY_EDITOR
using UnityEditor;

namespace Project.Architecture
{
    public class EditorApplicationQuitter : IApplicationQuitter
    {
        public void Quit()
        {
            EditorApplication.isPlaying = false;
        }
    }
}
#endif