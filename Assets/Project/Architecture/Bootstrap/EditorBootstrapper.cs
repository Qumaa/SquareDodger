using Project.Architecture;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project
{
    public class EditorBootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            if (FindObjectOfType<Bootstrapper>() == null)
                SceneManager.LoadScene(SceneNames.BOOTSTRAP);
            
            Destroy(this);
        }
    }
}
