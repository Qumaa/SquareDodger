namespace Project
{
    public abstract class SavingSystem<T> : ISavingSystem<T>
    {
        protected readonly string _savePath;

        protected SavingSystem(string savePath)
        {
            _savePath = savePath;
        }
        
        public abstract void SaveData(T data);

        public abstract T LoadData();

        protected abstract T CreateNewGenericInstance();
    }
}