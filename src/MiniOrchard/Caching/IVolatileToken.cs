namespace MiniOrchard.Caching {
    public interface IVolatileToken {
        bool IsCurrent { get; }
    }
}