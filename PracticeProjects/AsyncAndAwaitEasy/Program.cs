using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsyncAndAwaitEasy
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Кухня открывается! Начинаем готовить обед...\n");

            Chef chef = new Chef();
            await chef.CookDinnerAsync();

            Console.WriteLine("\nКухня закрывается. Обед готов!");
        }
    }

    public class Chef
    {
        private Random _rand = new Random();
        public async Task CookPastaAsync()
        {
            Console.WriteLine("Начинаю варить пасту");
            await Task.Delay(3000);
            Console.WriteLine("Паста готова!");
        }

        public async Task<string> PrepareSauceAsync()
        {
            Console.WriteLine("Готовлю томатный соус");
            await Task.Delay(2000);
            Console.WriteLine("Соус готов");

            return "Томатный соус";
        }

        public async Task<string[]> PrepareSaladAsync()
        {
            Console.WriteLine("Нарезаю овощи для салата");
            var tasks = await Task.WhenAll(
                ChopTomatoesAsync(),
                ChopCucumbersAsync(),
                ChopPeppersAsync()
                );

            Console.WriteLine($"Салат готов: {string.Join(", ", tasks)}");

            return tasks;
        }

        public async Task BakeMeatAsync()
        {
            try
            {
                Console.WriteLine("Запекаю мясо");
                await Task.Delay(4000);
                if (_rand.NextDouble() < 0.3)
                {
                    throw new Exception("Духовка сломалась!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Мясо готово в духовке!");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                var originalColor = Console.ForegroundColor;
                try
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nОшибка: {ex.Message}");

                }
                finally
                {
                    Console.ForegroundColor = originalColor;
                }

                try
                {
                    Console.WriteLine("Готовим на сковороде");
                    await Task.Delay(1000);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Мясо готово на сковороде!");
                }
                finally
                {
                    Console.ForegroundColor = originalColor;
                }
            }
        }

        public async Task CookDinnerAsync()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            await Task.WhenAll(CookPastaAsync(), PrepareSauceAsync(), PrepareSaladAsync());

            await BakeMeatAsync();

            timer.Stop();

            Console.WriteLine($"\nОбщее время приготовления: {(int)timer.Elapsed.TotalSeconds} сек");
        }

        private async Task<string> ChopTomatoesAsync()
        {
            await Task.Delay(1000);
            return "Помидоры";
        }

        private async Task<string> ChopCucumbersAsync()
        {
            await Task.Delay(1500);
            return "Огурцы";
        }

        private async Task<string> ChopPeppersAsync()
        {
            await Task.Delay(1000);
            return "Перец";
        }
    }
}
