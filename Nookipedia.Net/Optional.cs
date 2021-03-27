namespace Nookipedia.Net
{
    public sealed class Optional<T>
    {
        private readonly bool _present;
        private readonly T _value;

        public bool Present => _present;
        public T Value => _value;

        private Optional(bool present, T value = default)
        {
            _present = present;
            _value = value;
        }

        public static Optional<T> Of(T value) => new Optional<T>(true, value);
        public static Optional<T> Empty() => new Optional<T>(false);

        public static implicit operator Optional<T>(T value) => value is null ? Empty() : Of(value);
        public static implicit operator T(Optional<T> value) => value.Present ? value.Value : throw new MissingOptionalException();
    }
}
