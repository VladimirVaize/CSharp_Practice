using AbstractFactory.AbstractFactory;

namespace AbstractFactory.Undead
{
    public class UndeadEquipmentFactory : IEquipmentFactory
    {
        public IWeapon CreateWeapon()
        {
            return new UndeadWeapon();
        }

        public IArmor CreateArmor()
        {
            return new UndeadArmor();
        }

        public IConsumable CreateConsumable()
        {
            return new UndeadConsumable();
        }
    }
}
