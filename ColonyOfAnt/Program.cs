using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static ColonyOfAnt.Utility;

namespace ColonyOfAnt
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            World world = new World();

            while (DaysBeforeDisaster >= 0)
            {
                var flag = false;
                while (!flag)
                {
                    Console.Clear();
                    world.TellAboutWorld();
                    Console.WriteLine("Отправьте q, чтобы запустить день\n" +
                                      "Отправьте w, чтобы узнать информацию о какой-то колонии\n" +
                                      "Отправьте e, чтобы узнать информацию о муравье\n" +
                                      "Отправьте r, чтобы завершить программу\n");
                    var input = Console.ReadLine();
                    Console.WriteLine();

                    switch (input)
                    {
                        case "q":
                            flag = true;
                            world.NextDay();
                            world.AdditionalTask();
                            Console.Clear();
                            break;
                        case "w":
                            world.TellAboutColony();
                            break;
                        case "e":
                            world.TellAboutAnt();
                            break;
                        case "r":
                            return;
                        default:
                            Console.WriteLine("вы ввели неверное значение!");
                            break;
                    }
                }
            }

            world.ShowWinner();
            Console.ReadLine();
        }
    }
}