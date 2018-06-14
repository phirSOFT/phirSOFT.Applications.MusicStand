namespace phirSOFT.Applications.MusicStand.Core.Test.Mocks
{
    public interface INode<out T>
    {
        T Value { get; }
    }
}