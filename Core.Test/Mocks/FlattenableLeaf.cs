namespace phirSOFT.Applications.MusicStand.Core.Test.Mocks
{
    struct FlattenableLeaf<T> : INode<T>
    {
        public FlattenableLeaf(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }
}