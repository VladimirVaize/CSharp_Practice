using PatternServiceLocator.Audio;
using System;

namespace PatternServiceLocator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceLocator.RegisterAudioService(new SoundManager());

            Player player = new Player();
            Enemy enemy = new Enemy();

            player.Attack();
            enemy.TakeDamage();

            ServiceLocator.GetAudioService().SetVolume(0.5f);

            player.Attack();
        }
    }
}
