using AbstractFactory.Elf;
using AbstractFactory.Human;
using AbstractFactory.Orc;
using AbstractFactory.Undead;
using System;

namespace AbstractFactory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hero human = new Hero(new HumanEquipmentFactory());
            Hero elf = new Hero(new ElfEquipmentFactory());
            Hero orc = new Hero(new OrcEquipmentFactory());
            Hero undead = new Hero(new UndeadEquipmentFactory());

            Console.WriteLine("=== Герой фракции Люди ===");
            human.ShowEquipment();
            human.UseConsumable();

            Console.WriteLine("\n=== Герой фракции Эльфы ===");
            elf.ShowEquipment();
            elf.UseConsumable();

            Console.WriteLine("\n=== Герой фракции Орки ===");
            orc.ShowEquipment();
            orc.UseConsumable();

            Console.WriteLine("\n=== Бонус-задание: Герой фракции Нежить ===");
            undead.ShowEquipment();
            undead.UseConsumable();
        }
    }
}
