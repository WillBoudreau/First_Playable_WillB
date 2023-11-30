using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace First_Playable_WillB
{
    internal class Program
    {
        //Int variables
        //player
        static int userHealth;
        static int playerPOSx;
        static int playerPOSy;
        static int playerDamage;
        static int maxPlayerPOSx;
        static int maxPlayerPOSy;
        //enemy
        static int enemyHealth;
        static int enemyPOSx;
        static int enemyPOSy;
        static int enemyDamage;
        //Collectables
        static int CollecCount;
        //Map
        static int MapPOSx;
        static int MapPOSY;
        static int MapPOSxMax;
        static int MapPOSyMax;
        //Time
        static int milliseconds;
        //String variables
        //Map
        static string path;
        static string StringRpgMap = @"Map.txt";
        static string[] mapGround;
        //Char vairiables
        //Map
        static char[,] mapLayout;
        //Bool variables
        //Player
        static bool GameOver;
        static bool Win;
        // Player Key Movement
        static ConsoleKeyInfo playerControl;
        static void Main(string[] args)
        {
            GameOver = false;
            Start();
            Console.WriteLine("Welcome brave adventurer!");
            Thread.Sleep(milliseconds);
            Console.WriteLine("You have entered a cave with the goal of collecting rare pices of ancient gold");
            Thread.Sleep(milliseconds);
            Console.WriteLine("The goal is priceless but be aware lost enemy's roam the dungeon and will protect the treasure at any cost");
            Thread.Sleep(milliseconds);
            Console.WriteLine("Lets get started adventurer!, Press any key to start");
            Console.ReadKey(true);

            while (GameOver == false)
            {
                ShowMap();
                DisplayHUD();
                Legend();
                UserInput();
                EnemyMovement();
            }
            if (GameOver == true)
            {
                EndGame();
            }
        }
        static void Start()
        {
            //Gives player starting values
            milliseconds = 1000;
            //Player Initilization
            userHealth = 10;
            //Enemy Initialization
            enemyHealth = 10;
            //Map Initialization
            mapGround = File.ReadAllLines(StringRpgMap);
            mapLayout = new char[mapGround.Length, mapGround[0].Length];
            MakeMap();
            MapPOSx = mapLayout.GetLength(0);
            MapPOSY = mapLayout.GetLength(1);
            MapPOSxMax = MapPOSx - 1;
            MapPOSyMax = MapPOSY - 1;


        }
        static void ShowMap()
        {
            Console.Clear();
            //Displayable Map
            for (int i = 0; i < MapPOSx; i++)
            {
                for (int j = 0; j < MapPOSY; j++)
                {
                    char c = mapLayout[i, j];
                    if(c == '%')
                    {
                        enemyPOSx = i;
                        enemyPOSy = j;
                    }
                    Console.Write(c);
                }
                Console.WriteLine();
            }
            PlayerPOS();
            EnemyMovement();
            Console.SetCursorPosition(0, 0);

        }
        static void MakeMap()
        {
            // For loop shows off the map
            mapGround = File.ReadAllLines(StringRpgMap);
            mapLayout = new char[mapGround.Length, mapGround[0].Length];
            for (int k = 0; k < mapGround.Length; k++)
            {
                for (int l = 0; l < mapGround[k].Length; l++)
                {
                    mapLayout[k,l] = mapGround[k][l];
                }
            }
        }
        static void PlayerPOS()
        {
            //Draws Player and sets them in the start position
            Console.SetCursorPosition(playerPOSx, playerPOSy);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("&");
        }
        static void DisplayHUD()// Hud for the user
        {
            Console.SetCursorPosition(0,MapPOSY + 1);
            Console.WriteLine("Player Health: " + userHealth + " | " + "Gold Collected " + " | " + "Enemy health " + enemyHealth);
            Console.ForegroundColor = ConsoleColor.Green;
            
        }
        static void Legend()//Legend for the users info
        {
            Console.Write("Player Icon = & \nEnemy Icon = % \nWall Icon =" + "=" +" and # \nSpike Icon = + \nGold Icon = $");
        }
        static void UserInput()
        {
            //Takes user input
            int moveX;
            int moveY;

            int newPlayerPOSx = playerPOSx;
            int newPlayerPOSy = playerPOSy;

            playerControl = Console.ReadKey(true);

            if(playerControl.Key == ConsoleKey.W)
            {
                moveY = Math.Max(playerPOSy - 1, 0);
                if(moveY == enemyPOSy && playerPOSx == enemyPOSx)
                {
                    enemyHealth -= 1;
                    if(enemyHealth <= 0)
                    {
                        enemyHealth = 0;
                        enemyPOSx = 0;
                        enemyPOSy = 0;
                    }
                }
            }
            if(playerControl.Key == ConsoleKey.A)
            {
                moveX = Math.Max(playerPOSx + 1,0);
                if(moveX == enemyPOSx && playerPOSx == enemyPOSx)
                {
                    enemyHealth -= 1;
                    if(enemyHealth <= 0)
                    {
                        enemyHealth = 0;
                        enemyPOSx = 0;
                        enemyPOSy = 0;
                    }
                }
            }
            if(playerControl.Key == ConsoleKey.S)
            {
                moveY = Math.Max(playerPOSy + 1, 0);
                if(moveY == enemyPOSy && playerPOSy == enemyPOSy)
                {
                    enemyHealth -= 1;
                    if(enemyHealth <= 0)
                    {
                        enemyHealth = 0;
                        enemyPOSy = 0;
                        enemyPOSx = 0;
                    }
                }
            }
            if(playerControl.Key == ConsoleKey.D)
            {
                moveX = Math.Max(playerPOSx + 1, 0);
                if(moveX == enemyPOSx && playerPOSx == enemyPOSx)
                {
                    enemyHealth -= 1;
                    if(enemyHealth <= 0)
                    {
                        enemyHealth = 0;
                        enemyPOSx = 0;
                        enemyPOSy = 0;
                    }
                }
            }
        }
        static void EnemyMovement()
        {
            int EnemyMoveX = enemyPOSx;
            int EnemyMoveY = enemyPOSy;
            int newEnemyPOSx = enemyPOSx;
            int newEnemyPOSy = enemyPOSy;

            Random randomRoll = new Random();
            int EnemyMoveResult = randomRoll.Next(1,5);

            if(EnemyMoveResult == 1)
            {
                newEnemyPOSy = EnemyMoveY + 1;
                if(EnemyMoveY >= MapPOSyMax)
                {
                    EnemyMoveY = MapPOSyMax;
                }
            }
            if(EnemyMoveResult == 2)
            {
                newEnemyPOSy = newEnemyPOSy - 1;
            }
        }
        static void EndGame()
        {
            Console.Clear();
            Console.WriteLine("NOOO Adventurer! Please come back! Adventurer!");
            Console.WriteLine("Game Over");
        }
        static void PlayerWin()
        {
            Console.WriteLine("How great Adventurer! You have returned safe and sound! Hooray!");
            Console.WriteLine("You Win!");
        }
    }
}