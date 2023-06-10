using Project.Game;

namespace Project.Architecture
{
    public interface IGameFinisher
    {
        void Finish();
        IGameplay GameToFinish { get; set; }
    }
}