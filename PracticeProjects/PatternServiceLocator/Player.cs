using PatternServiceLocator.Audio;
using System;

namespace PatternServiceLocator
{
    public class Player
    {
        public void Attack()
        {
            Console.WriteLine("Игрок атакует!");
            ServiceLocator.GetAudioService().PlaySound("sword_swing");
        }
    }
}
