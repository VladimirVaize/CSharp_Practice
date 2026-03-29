using AbstractFactory.AbstractFactory;

namespace AbstractFactory.Human
{
    public class HumanEquipmentFactory : IEquipmentFactory
    {
        public IWeapon CreateWeapon()
        {
            return new HumanWeapon();
        }

        public IArmor CreateArmor()
        {
            return new HumanArmor();
        }

        public IConsumable CreateConsumable()
        {
            return new HumanConsumable();
        }
    }
}
