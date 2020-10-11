namespace GlBindings
{
    public abstract class BaseBindable<T>
    {
        public bool IsCurrentlyBound() { return CurrentlyBound == this; }

        public abstract void Bind();

        public static BaseBindable<T> CurrentlyBound { get; set; }
    }
}
