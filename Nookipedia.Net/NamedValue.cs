namespace Nookipedia.Net
{
    public record NamedValue(string Name, object Value)
    {
        public static implicit operator NamedValue((string Name, object Value) value) => new(value.Name, value.Value);
    }
}
