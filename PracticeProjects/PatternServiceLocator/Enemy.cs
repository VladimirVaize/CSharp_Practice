using PatternServiceLocator.Audio;
using System;

namespace PatternServiceLocator
{
    public class Enemy
    {
        public void TakeDamage()
        {
            Console.WriteLine("Враг получил урон!");
            ServiceLocator.GetAudioService().PlaySound("enemy_hurt");
        }
    }
}
