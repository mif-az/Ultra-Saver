namespace Ultra_Saver.Models;

public abstract class Model<T> : IEquatable<Model<T>> where T : IEquatable<T>
{
    public bool Equals(Model<T>? other)
    {
        return other?.GetSignature().Equals(this.GetSignature()) ?? false;
    }

    public abstract T GetSignature();

}