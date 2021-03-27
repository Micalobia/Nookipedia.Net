namespace Nookipedia.Net
{
    public interface IListEndpoint
    {
        string Endpoint();
    }

    public interface ISingleEndpoint
    {
        string Endpoint(string sub);
    }

    public interface IEndpoint : ISingleEndpoint, IListEndpoint
    {
    }

    internal static class SingleEndpoint<T> where T : ISingleEndpoint, new()
    {
        private static readonly T _value = new();
        public static string Endpoint(string name) => _value.Endpoint(name);
    }

    internal static class ListEndpoint<T> where T : IListEndpoint, new()
    {
        private static readonly T _value = new();
        public static string Endpoint() => _value.Endpoint();
    }
}
