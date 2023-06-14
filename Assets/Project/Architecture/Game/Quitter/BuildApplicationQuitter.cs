using UnityEngine;

namespace Project.Architecture
{
    public class BuildApplicationQuitter : IApplicationQuitter
    {
        public void Quit()
        {
            Application.Quit();
        }
    }
}