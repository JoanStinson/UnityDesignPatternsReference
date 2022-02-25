namespace JGM.Patterns.Visitor
{
    public interface IBikeElementVisitable
    {
        void Accept(IBikeElementVisitor visitor);
    }
}