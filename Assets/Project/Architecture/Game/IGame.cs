namespace Project.Architecture
{
    public interface IGame
    {
        void FixedUpdate();
        void Update();
        void Pause();
        void Resume();
        void Finish();
    }
}