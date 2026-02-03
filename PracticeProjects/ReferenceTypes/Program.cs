using System;

namespace ReferenceTypes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MagicStone[] bag = new MagicStone[5];
            MagicStone[] weaponSockets = new MagicStone[2];

            bag[0] = new FireStone();
            bag[1] = new WaterStone();
            bag[2] = new EarthStone();
            bag[3] = new AirStone();

            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== Магическая кузница (прототип) ===\n");

                Console.WriteLine("1 - Показать состояние");
                Console.WriteLine("2 - Переместить камень");
                Console.WriteLine("3 - Объединить камни");
                Console.WriteLine("4 - Выход");
                Console.Write("\nВыберите действие: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1: ShowStatus(bag, weaponSockets); break;
                        case 2: MoveStone(bag, weaponSockets); break;
                        case 3: CombineStones(bag, weaponSockets); break;
                        case 4: running = false; break;
                        default: ShowWarning("Неизвестное действие!"); break;
                    }
                }
                else
                    ShowWarning("Ошибка: Нужно ввести число!");
            }

        }

        static void DrawInventory(MagicStone[] bag, MagicStone[] sockets)
        {
            Console.Clear();
            Console.WriteLine("=== Магическая кузница (прототип) ===\n");
            Console.WriteLine("=== Сумка ===");

            for (int i = 0; i < bag.Length; i++)
            {
                if (i != 0)
                    Console.Write($" | ");
                if (bag[i] != null)
                    bag[i].PrintInfo();
                else
                    Console.Write("[Пусто]");
            }

            Console.WriteLine("\n\n=== Сокеты оружия ===");

            for (int i = 0; i < sockets.Length; i++)
            {
                if (i != 0)
                    Console.Write($" | ");
                if (sockets[i] != null)
                    sockets[i].PrintInfo();
                else
                    Console.Write("[Пусто]");
            }
        }

        static void ShowStatus(MagicStone[] bag, MagicStone[] sockets)
        {

            DrawInventory(bag, sockets);
            Console.WriteLine("\n\nНажмите чтобы вернуться в меню.");
            Console.ReadKey();
        }

        static bool TryMoveStone(MagicStone[] from, int fromIndex, MagicStone[] to, int toIndex)
        {
            if (from[fromIndex] == null || to[toIndex] != null)
                return false;

            to[toIndex] = from[fromIndex];
            from[fromIndex] = null;
            return true;
        }

        static void MoveStone(MagicStone[] bag, MagicStone[] sockets)
        {
            DrawInventory(bag, sockets);

            Console.WriteLine("\n\nЧто делаем?");
            Console.WriteLine("1 - Из сумки в оружие");
            Console.WriteLine("2 - Из оружия в сумку");
            Console.Write("Выберите направление: ");

            if (!int.TryParse(Console.ReadLine(), out int direction) || (direction != 1 && direction != 2))
            {
                ShowWarning("Неверный выбор направления!");
                return;
            }

            MagicStone[] from, to;
            string fromName, toName;

            if(direction == 1)
            {
                from = bag; to = sockets;
                fromName = "сумки"; toName = "оружия";
            }
            else
            {
                from = sockets; to = bag;
                fromName = "оружия"; toName = "сумки";
            }

            Console.Write($"Выберите слот {fromName} (1-{from.Length}): ");

            if (!TryReadIndex(from.Length, out int fromIndex)) return;

            Console.Write($"Выберите слот {toName} (1-{to.Length}): ");

            if (!TryReadIndex(to.Length, out int toIndex)) return;



            if (TryMoveStone(from, fromIndex, to, toIndex))
                ShowMessage($"Камень перемещен из {fromName} в {toName}!");
            else
                ShowWarning("Невозможно переместить камень! Проверьте, что исходный слот не пуст, а целевой - свободен.");
        }

        static void CombineStones(MagicStone[] bag, MagicStone[] sockets)
        {
            DrawInventory(bag, sockets);

            Console.Write("\n\nВыберите первый кристалл (из сумки): ");
            if (!TryReadIndex(bag.Length, out int firstIndex)) return;

            Console.Write("Выберите второй кристалл (из сумки): ");
            if (!TryReadIndex(bag.Length, out int secondIndex)) return;

            if (bag[firstIndex] == null || bag[secondIndex] == null)
            {
                ShowWarning("Ошибка: Один из слотов пуст");
                return;
            }

            int totalPower = bag[firstIndex].Power + bag[secondIndex].Power;
            MagicStone combinedStone = new MagicStone("Камень Стихий", totalPower);

            string oldStone1 = bag[firstIndex].Name;
            string oldStone2 = bag[secondIndex].Name;
            
            bag[firstIndex] = combinedStone;
            bag[secondIndex] = null;

            DrawInventory(bag, sockets);
            ShowMessage($"\n\n{oldStone1} и {oldStone2} объединены в {combinedStone.Name} с силой {totalPower}!");
        }

        static bool IsValidIndex(int index, int arrayLength)
        {
            return index >= 0 && index < arrayLength;
        }

        static bool TryReadIndex(int arrayLength, out int index)
        {
            index = 0;
            
            if(!int.TryParse(Console.ReadLine(), out int input))
            {
                ShowWarning("Ошибка: Нужно ввести число!");
                return false;
            }

            index = input - 1;

            if (!IsValidIndex(index, arrayLength))
            {
                ShowWarning("Ошибка: Неверный индекс!");
                return false;
            }
            return true;
        }

        static void ShowMessage(string message)
        {
            Console.WriteLine("\n\n" + message);
            Console.ReadKey();
        }

        static void ShowWarning(string warning)
        {
            Console.WriteLine("\n\n" + warning);
            Console.ReadKey();
        }
    }

    public class MagicStone
    {
        public string Name { get; }
        public int Power { get; }
        public int Id { get; }

        private static int currentId = 0;

        public MagicStone(string name, int power)
        {
            Name = name;
            Power = power;
            Id = currentId++;
        }

        public void PrintInfo()
        {
            Console.Write($"[Id:{Id} {Name}(Pwr:{Power})]");
        }
    }

    public class FireStone : MagicStone
    {
        public FireStone() : base("Камень Огня", 10) { }
    }

    public class WaterStone : MagicStone
    {
        public WaterStone() : base("Камень Воды", 7) { }
    }

    public class EarthStone : MagicStone
    {
        public EarthStone() : base("Камень Земли", 12) { }
    }

    public class AirStone : MagicStone
    {
        public AirStone() : base("Камень Воздуха", 6) { }
    }

    public class LightningStone : MagicStone
    {
        public LightningStone() : base("Камень Молнии", 15) { }
    }
}
