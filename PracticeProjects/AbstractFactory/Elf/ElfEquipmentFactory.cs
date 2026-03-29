using AbstractFactory.AbstractFactory;

namespace AbstractFactory.Elf
{
    public class ElfEquipmentFactory : IEquipmentFactory
    {
        public IWeapon CreateWeapon()
        {
            return new ElfWeapon();
        }

        public IArmor CreateArmor()
        {
            return new ElfArmor();
        }

        public IConsumable CreateConsumable()
        {
            return new ElfConsumable();
        }
    }
}
