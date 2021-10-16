using System;
using System.Threading;

namespace MementoPattern
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // Completed level 1
            Player player = new ();
            player.Level = 1;
            player.Score = 100;
            player.Health = "100%";
            Console.WriteLine("----------- Player info after completing level 1 ---------------------");
            player.DisplayPlayerInfo();
            // when player completes any level then create checkpoint for that level.
            CareTaker careTaker = new CareTaker();
            careTaker.LevelMarker = player.CreateMarker(player);

            // Delay
            Thread.Sleep(2000);

            player.Level = 2;
            player.Score = 130;
            player.Health = "80%";
            Console.WriteLine("--------------- Player info in level 2 --------------------------------");
            player.DisplayPlayerInfo();

            // if players loses all the lifeline then restore the game from level 1
            player.RestoreLevel(careTaker.LevelMarker);
            Console.WriteLine("------------- Player info after restoring level 1 data ----------------");
            player.DisplayPlayerInfo();
            Console.ReadLine();
        }
    }

    // Memento class
    public class Memento
    {
        public readonly int Level;
        public readonly int Score;
        public readonly string Health;

        public Memento(int level, int score, string health)
        {
            Level = level;
            Score = score;
            Health = health;
        }
    }

    // CareTaker class
    public class CareTaker
    {
        public Memento LevelMarker;
    }

    // Originator class
    public class Player
    {
        public int Level;
        public int Score;
        public string Health;
        public int Lifeline = 3;

        public Memento CreateMarker(Player player)
        {
            return new Memento(player.Level, player.Score, player.Health);
        }

        public void RestoreLevel(Memento playerMemento)
        {
            Level = playerMemento.Level;
            Score = playerMemento.Score;
            Health = playerMemento.Health;
            Lifeline -= 1;
        }

        public void DisplayPlayerInfo()
        {
            Console.WriteLine("Level: " + Level);
            Console.WriteLine("Score: " + Score);
            Console.WriteLine("Health: " + Health);
            Console.WriteLine("Lifeline left: " + Lifeline);
        }
    }
}
