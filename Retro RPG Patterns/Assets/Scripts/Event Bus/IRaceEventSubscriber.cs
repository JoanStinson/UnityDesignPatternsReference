namespace JGM.Game.EventBus
{
    public interface IRaceEventSubscriber 
    {
        void OnEnable();
        void OnDisable();
    }
}