namespace Project.Architecture
{
    public struct ApplicationQuitterFactory : IFactory<IApplicationQuitter>
    {
        public IApplicationQuitter CreateNew()
        {
#if UNITY_EDITOR
            return new EditorApplicationQuitter();
#else
            return new BuildApplicationBuilder();       
#endif
        }
    }
}