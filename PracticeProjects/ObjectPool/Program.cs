using System;
using System.Collections.Generic;

namespace ObjectPool
{
    internal class Program
    {
        private static readonly Random _random = new Random();
        static void Main(string[] args)
        {
            ParticlePool particlePool = new ParticlePool(5);

            List<Particle> activeParticles = new List<Particle>();
            for (int i = 0; i < 10; i++)
            {
                Particle p = particlePool.Get();
                p.X = i;
                p.Y = 10;
                p.Symbol = '*';
                p.Color = ConsoleColor.Red;
                p.Lifetime = _random.Next(3, 11);
                p.CurrentTime = 0;
                activeParticles.Add(p);

                Console.WriteLine($"Создана/получена частица #{i + 1}, активных частиц: {activeParticles.Count}");
            }

            for (int frame = 0; frame < 10; frame++)
            {
                Console.WriteLine($"\n--- Кадр {frame + 1} ---");

                for (int i = activeParticles.Count - 1; i >= 0; i--)
                {
                    activeParticles[i].CurrentTime++;

                    if (activeParticles[i].CurrentTime >= activeParticles[i].Lifetime)
                    {
                        Console.Write($"Частица ");
                        activeParticles[i].WriteSymbol();
                        Console.WriteLine($" на позиции ({activeParticles[i].X}, {activeParticles[i].Y}) умерла, возвращаем в пул");
                        particlePool.Return(activeParticles[i]);
                        activeParticles.RemoveAt(i);
                    }
                    else
                    {
                        Console.Write($"Частица ");
                        activeParticles[i].WriteSymbol();
                        Console.WriteLine($" на позиции ({activeParticles[i].X}, {activeParticles[i].Y}) живет, осталось: {activeParticles[i].Lifetime - activeParticles[i].CurrentTime}");
                    }
                }

                if (frame == 5)
                {
                    Console.WriteLine("\n--- Новый выстрел! ---");
                    for (int i = 0; i < 4; i++)
                    {
                        Particle p = particlePool.Get();
                        p.X = frame + i;
                        p.Y = 15;
                        p.Symbol = '·';
                        p.Color = ConsoleColor.Yellow;
                        p.Lifetime = 2;
                        p.CurrentTime = 0;
                        activeParticles.Add(p);
                        Console.WriteLine($"Получена новая частица, активных частиц: {activeParticles.Count}");
                    }
                }
            }
            Console.WriteLine("\n--- Демонстрация завершена ---");
        }
    }

    public class Particle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Symbol { get; set; }
        public ConsoleColor Color { get; set; }
        public float Lifetime { get; set; }
        public float CurrentTime { get; set; }

        public void Reset()
        {
            X = 0; Y = 0;
            Symbol = ' ';
            Color = ConsoleColor.White;
            Lifetime = 0;
            CurrentTime = 0;
        }

        public void WriteSymbol()
        {
            Console.ForegroundColor = Color;
            Console.Write($"'{Symbol}'");
            Console.ResetColor();
        }
    }

    public class ParticlePool
    {
        private Stack<Particle> _particles = new Stack<Particle>();

        public ParticlePool(int count)
        {
            for (int i = 0; i < count; i++)
                Add(new Particle());
        }

        public Particle Get()
        {
            if (_particles.Count == 0)
                Add(new Particle());

            return _particles.Pop();
        }

        public void Return(Particle particle)
        {
            if (particle == null) return;
            particle.Reset();
            _particles.Push(particle);
        }

        private void Add(Particle particle) => _particles.Push(particle);
    }
}
