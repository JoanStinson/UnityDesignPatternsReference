namespace JGM.Patterns.State
{
    public interface IBikeState
    {
        void Handle(BikeController bikeController);
    }
}