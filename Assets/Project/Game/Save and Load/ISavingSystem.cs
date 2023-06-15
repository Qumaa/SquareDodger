namespace Project.Game
{
    public interface ISavingSystem<T>
    {
        void SaveData(T data);
        T LoadData();
    }
    
}
