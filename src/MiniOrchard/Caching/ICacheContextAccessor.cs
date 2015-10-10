namespace MiniOrchard.Caching {
    public interface ICacheContextAccessor {
        IAcquireContext Current { get; set; }
    }
}