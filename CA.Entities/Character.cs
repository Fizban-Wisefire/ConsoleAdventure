﻿

namespace CA.Entities
{
    //Creates Character class

    public class Character
    {
        public string Name { get; set; }
        public int Hp { get; set; }
        public int Str { get; set; }
        public int Speed { get; set; }
        public int Con { get; set; }
        public int Res { get; set; }
        public int Value { get; set; }
        public Item Weapon { get; set; }
        public Item Armor { get; set; }
        public List<Location> Locs { get; set; }

        //Character Constructor

        public Character(string name, int hp, int str, int speed, int con, int res, int value, Item weapon, Item armor)
        {
            Name = name;
            Hp = hp;
            Str = str;
            Speed = speed;
            Con = con;
            Res = res;
            Value = value;
            Weapon = weapon;
            Armor = armor;
        }

        //Character Object Cloner to make a shallow clone 
        public Character Clone()
        {
            return (Character)this.MemberwiseClone();
        }


        //Where we will put all of the methods of the character class.

        public delegate int DamageCalculator();

        public int CalcDamage()
        {
            return (Str + Weapon.Effect);
        }

     
        public virtual void Attack(Character target)
        {
            DamageCalculator Damage;
            Damage = CalcDamage;
            Console.WriteLine($"{Name} attacked target.Name.");
            target.ChangeHealth(-Damage());
            Console.WriteLine($"{Name} dealt {Str + Weapon.Effect} damage to {target.Name}.");
        }

        public void ChangeHealth(int num)
        {
            Hp += num;
        }

    }
}
