using System;
using System.Collections.Generic;
using System.Threading;

namespace TurnBaseCombatGame
{
    class Program
    {
        enum Difficulty
        {
            Easy,
            Medium,
            Hard
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("     Terminal Combat Game");
                Console.WriteLine();
                Console.WriteLine("     Select Difficulty");
                Console.WriteLine("  e - Easy       esc - Exit");
                Console.WriteLine("  m - Medium");
                Console.WriteLine("  h - Hard");
                Console.WriteLine();
                Console.Write("  > ");

                string option = Console.ReadLine();

                if (option.ToLower() == "esc")
                {
                    break;
                }

                if (option.ToLower() == "e" || option.ToLower() == "m" || option.ToLower() == "h")
                {
                    CombatDifficulty(ParseDifficulty(option));
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("  Invalid Option, Please Try Again.");
                    Console.WriteLine();
                }
            }
        }

        static void CombatDifficulty(Difficulty difficulty)
        {
            Dictionary<Difficulty, (int playerHealth, int enemyHealth, int playerAttack, int enemyAttack, int playerHeal, int enemyHeal)> difficultySettings =
                new Dictionary<Difficulty, (int, int, int, int, int, int)>
                {
                    {Difficulty.Easy, (100, 100, 20, 10, 30, 20) },
                    {Difficulty.Medium, (100, 100, 15, 15, 15, 15) },
                    {Difficulty.Hard, (100, 100, 30, 30, 10, 10) }
                };

            var setting = difficultySettings[difficulty];
            GamePlay(setting.playerHealth, setting.enemyHealth, setting.playerAttack, setting.enemyAttack, setting.playerHeal, setting.enemyHeal);

        }
        static void GamePlay(int playerHealth, int enemyHealth, int playerAttack, int enemyAttack, int playerHeal, int enemyHeal)
        {
            string action = "";
            int MaxHealth = playerHealth;
            Random enemyRand = new Random();

            Console.WriteLine();
            Console.WriteLine(" | Player Stats:             |  Enemy Stats:            |");
            Console.WriteLine($" |  Max Health: {MaxHealth} Hp       |   Max Health: {MaxHealth} Hp     |");
            Console.WriteLine($" |  Max Attack Dmg: {playerAttack} Dmg   |   Max Attack Dmg: {playerAttack} Dmg |");
            Console.WriteLine($" |  Max Heal: {playerHeal} Hp          |   Max Heal: {playerHeal} Hp        |");

            Thread.Sleep(2000);

            while (true)
            {
                // Player Action
                try
                {
                    Console.WriteLine();
                    Console.WriteLine($"  Player Health: {playerHealth} Hp, Enemy Health: {enemyHealth} Hp");
                    Console.WriteLine("  Choose Actions: a - Attack   h - Heal");
                    Console.Write("  > ");

                    action = Console.ReadLine();
                }
                catch (FormatException)
                {
                    Console.WriteLine("  Invalid Action, Please Try Again.");
                    continue;
                }

                // Player Turn Conditions
                if (action.ToLower() == "a" || action.ToLower() == "h")
                {
                    switch (action)
                    {
                        case "a":
                            enemyHealth -= playerAttack;
                            Console.WriteLine();
                            Console.WriteLine("-------------------------------------------------");
                            Console.WriteLine($"  Player Attacked {playerAttack} Dmg");
                            Console.WriteLine($"  Player Health: {playerHealth} Hp, Enemy Health: {enemyHealth} Hp");
                            Console.WriteLine("-------------------------------------------------");
                            Thread.Sleep(2000);
                            break;
                        case "h":
                            playerHealth += playerHeal;

                            if (playerHealth > MaxHealth)
                            {
                                playerHealth = MaxHealth;
                            }

                            Console.WriteLine();
                            Console.WriteLine("-------------------------------------------------");
                            Console.WriteLine($"  Player Healed {playerHeal} Hp");
                            Console.WriteLine($"  Player Health: {playerHealth} Hp, Enemy Health: {enemyHealth} Hp");
                            Console.WriteLine("-------------------------------------------------");
                            Thread.Sleep(2000);
                            break;
                    }
                    if (enemyHealth <= 0)
                    {
                        Console.WriteLine("  You Win!");
                        Console.WriteLine();
                        Thread.Sleep(2000);
                        break;
                    }
                }

                // Enemy Action
                Console.WriteLine();
                Console.Write("  Enemy Turn");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                Console.Write(".");


                int enemyAction = enemyRand.Next(0, 2);
                Thread.Sleep(1000);

                // Enemy Turn Conditions
                switch (enemyAction)
                {
                    case 0:
                        enemyHealth += enemyHeal;

                        if (enemyHealth > MaxHealth)
                        {
                            enemyHealth = MaxHealth;
                        }

                        Console.WriteLine();
                        Console.WriteLine("-------------------------------------------------");
                        Console.WriteLine($"  Enemy Healed {enemyHeal} Hp");
                        Console.WriteLine($"  Player Health: {playerHealth} Hp, Enemy Health: {enemyHealth} Hp");
                        Console.WriteLine("-------------------------------------------------");
                        Thread.Sleep(2000);
                        break;

                    case 1:
                        playerHealth -= enemyAttack;
                        Console.WriteLine();
                        Console.WriteLine("-------------------------------------------------");
                        Console.WriteLine($"  Enemy Attacked {enemyAttack} Dmg");
                        Console.WriteLine($"  Player Health: {playerHealth} Hp, Enemy Health: {enemyHealth} Hp");
                        Console.WriteLine("-------------------------------------------------");
                        Thread.Sleep(2000);
                        break;

                }
                if (playerHeal <= 0)
                {
                    Console.WriteLine("  You Lose!");
                    Console.WriteLine();
                    Thread.Sleep(2000);
                    break;
                }
            }
        }

        static Difficulty ParseDifficulty(string option)
        {
            switch (option)
            {
                case "e":
                    return Difficulty.Easy;
                case "m":
                    return Difficulty.Medium;
                case "h":
                    return Difficulty.Hard;
                default:
                    throw new ArgumentException("  Invalid option");
            }
        }
    }
}

