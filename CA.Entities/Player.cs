using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.Entities
{
    // Creates Player Subclass

    public class Player : Character
    {
        public int Xp { get; set; }
        public int Level { get; set; }
        public List<Item> Bag { get; set; }
        public Player(string name, int hp, int str, int speed, int con, int res, int value, Item weapon, Item armor, int xp, int level, List<Item> bag)
            : base(name, hp, str, speed, con, res, value, weapon, armor)
        {
            Xp = xp;
            Level = level;
            Bag = bag;
        }

        void StatIncrease(int i)
        {
            bool finished = false;
            while (finished != true)
            {
                Console.WriteLine($"You have {i} stat points left. What would you like to increase?");
                Console.WriteLine("1) Strength 2) Speed 3) Constitution 4) Resistance");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    Str += 1;
                    finished = true;
                }
                else if (input == "2")
                {
                    Speed += 1;
                    finished = true;
                }
                else if (input == "3")
                {
                    Con += 1;
                    finished = true;
                }
                else if (input == "4")
                {
                    Res += 1;
                    finished = true;
                }
                else
                {
                    Console.WriteLine("Invalid Option");
                };
            }
        }

        void LevelUp()
        {
            for (int i = 3; i >= 1; i--)
            {
                Level++;
                StatIncrease(i);
            }
        }

        public void GainXp(int mobXp)
        {
            Xp += mobXp;
            if (Xp <= Math.Pow(10, Level) && (Level < 10))
            {
                LevelUp();
            }
            else if (Xp < 0)
            {

            }

        }

        public override void Attack(Character target)
        {
            Console.WriteLine($"You attacked {target.Name}.");
            target.ChangeHealth(-(Str + Weapon.Effect));
            Console.WriteLine($"You dealt {Str + Weapon.Effect} damage to {target.Name}.");
        }
    }
}
