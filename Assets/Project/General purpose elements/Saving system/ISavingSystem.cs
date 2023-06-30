namespace Project
{
    public interface ISavingSystem<T>
    {
        bool HasSavedData { get; }
        void SaveData(T data);
        T LoadData();
    }
    
}
