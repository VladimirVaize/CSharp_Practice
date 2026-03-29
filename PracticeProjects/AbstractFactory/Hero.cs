using AbstractFactory.AbstractFactory;
using System;

namespace AbstractFactory
{
    public class Hero
    {
        public IWeapon Weapon { get; private set; }
        public IArmor Armor { get; private set; }
        public IConsumable Consumable { get; private set; }

        public Hero(IEquipmentFactory factory)
        {
            Weapon = factory.CreateWeapon() ?? throw new ArgumentNullException(nameof(Weapon));
            Armor = factory.CreateArmor() ?? throw new ArgumentNullException(nameof(Armor));
            Consumable = factory.CreateConsumable() ?? throw new ArgumentNullException(nameof(Consumable));
        }

        public void ShowEquipment()
        {
            Console.WriteLine("Экипировка:");
            Weapon.ShowInfo();
            Armor.ShowInfo();
            Consumable.ShowInfo();
        }

        public void UseConsumable()
        {
            Console.WriteLine("Использование предмета:");
            Consumable.Use();
        }
    }
}
