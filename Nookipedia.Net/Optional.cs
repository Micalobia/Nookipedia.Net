namespace Nookipedia.Net
{
    public sealed class Optional<T>
    {

        public bool Present { get; init; }
        public T? Value { get; init; }

        private Optional()
        {
            Present = false;
            Value = default;
        }

        public Optional(T value)
        {
            Present = true;
            Value = value;
        }

        public T? GetOrDefault(T? @default = default) => Present ? Value : @default;

        public Optional<T> Or(Optional<T> other) => GetOrDefault(other.GetOrDefault());

        public static Optional<T> Of(T value) => new(value);
        public static Optional<T> Empty() => new();

        public override string ToString() => $"Optional{{{(Present ? Value?.ToString() ?? "Null" : "Empty")}}}";

        public static implicit operator Optional<T>(T? value) => value is null ? new() : new(value);
        public static implicit operator T?(Optional<T> value) => value.Present ? value.Value : throw new MissingOptionalException();
    }
}
