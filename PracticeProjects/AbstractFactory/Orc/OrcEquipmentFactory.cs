using AbstractFactory.AbstractFactory;

namespace AbstractFactory.Orc
{
    public class OrcEquipmentFactory : IEquipmentFactory
    {
        public IWeapon CreateWeapon()
        {
            return new OrcWeapon();
        }

        public IArmor CreateArmor()
        {
            return new OrcArmor();
        }

        public IConsumable CreateConsumable()
        {
            return new OrcConsumable();
        }
    }
}
