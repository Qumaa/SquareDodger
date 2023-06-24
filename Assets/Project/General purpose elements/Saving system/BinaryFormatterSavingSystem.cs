using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Project
{
    public abstract class BinaryFormatterSavingSystem<T> : SavingSystem<T>
    {
        private readonly BinaryFormatter _formatter;

        protected BinaryFormatterSavingSystem(string savePath) : 
            base(savePath)
        {
            _formatter = new BinaryFormatter();
        }

        public override void SaveData(T data)
        {
            using var fileStream = File.Create(_savePath);
            _formatter.Serialize(fileStream, data);
        }

        public override T LoadData()
        {
            if (!File.Exists(_savePath))
                return CreateEmptyDataInstance();

            using FileStream fileStream = File.Open(_savePath, FileMode.Open);
            return (T) _formatter.Deserialize(fileStream);
        }
    }
}