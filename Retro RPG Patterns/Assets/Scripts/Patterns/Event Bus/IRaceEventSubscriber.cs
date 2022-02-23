namespace JGM.Patterns.EventBus
{
    public interface IRaceEventSubscriber 
    {
        void OnEnable();
        void OnDisable();
    }
}