using System;

namespace ColonyOfAnt
{
    public class SpecialInsect : Ant
    {
        public override void DescribeItselfBriefly()
        {
            Console.Write($"Тип: {string.Join(" ", myModifier)}\n" +
                          $"--- Параметры: здоровье: {hp} защита: {defense} урон: {damage}\n" +
                          $"Модификаторы:\n");
            foreach (var modifier in myModifier)
            {
                Console.Write("--- ");
                switch (modifier)
                {
                    case "ленивый":
                        Console.WriteLine("не может брать ресурсы;");
                        break;
                    case "обычный":
                        Console.WriteLine("может быть атакован войнами;");
                        break;
                    case "агрессивный":
                        Console.WriteLine("атакует врагов(3 цели за раз и наносит 2 укуса)");
                        break;
                    case "аномальный":
                        Console.WriteLine("атакует своих вместо врагов;");
                        break;
                    case "мирный":
                        Console.WriteLine(
                            "группа/колония игнорирует все негативные эффекты (включая агрессивных насекомых/животных) на территории;");
                        break;
                    case "неуязвимый":
                        Console.WriteLine("не может быть атакован войнами;");
                        break;
                    case "эпический":
                        Console.WriteLine("защита и здоровье всех в походе увеличена в двое;");
                        break;
                }
            }
        }
    }
}