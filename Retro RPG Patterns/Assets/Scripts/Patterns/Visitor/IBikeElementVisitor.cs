namespace JGM.Patterns.Visitor
{
    public interface IBikeElementVisitor
    {
        void Visit(BikeShieldVisitable bikeShield);
        void Visit(BikeEngineVisitable bikeEngine);
        void Visit(BikeWeaponVisitable bikeWeapon);
    }
}