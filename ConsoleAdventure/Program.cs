//----------------------------------------------------------------------------|
// Use for putting notes in for coming back later.                     *******|
//----------------------------------------------------------------------------|

bool game = true;
string input;

//Main Loop of the game.

Random random = new Random();


// Create Weapons, Armor, and Potions and adds usable items to lists

Item Unarmed = new Item("Weapon", "Unarmed", 1, 0);
Item Dagger = new Item("Weapon", "Dagger", 2, 5);
Item ShortSword = new Item("Weapon", "Short Sword", 4, 10);
Item LongSwort = new Item("Weapon", "Long Sword", 6, 15);
Item Claymore = new Item("Weapon", "Claymore", 8, 20);

List<Item> Weapons = new List<Item>();
Weapons.Add(Dagger);
Weapons.Add(ShortSword);
Weapons.Add(LongSwort);
Weapons.Add(Claymore);

Item Unarmored = new Item("Armor", "Unarmored", 0, 0);
Item Leather = new Item("Armor", "Leather", 2, 5);
Item Chainmail = new Item("Armor", "Chainmail", 4, 10);
Item PlateArmor = new Item("Armor", "Plate Armor", 6, 20); 

List<Item> Armors = new List<Item>();
Armors.Add(Leather);
Armors.Add(Chainmail);
Armors.Add(PlateArmor);

Item SmallPotion = new Item("Potion", "Small Potion", 5, 3);
Item MediumPotion = new Item("Potion", "Medium Potion", 10, 6);
Item LargePotion = new Item("Potion", "Large Potion", 15, 9);

List<Item> Potions = new List<Item>();
Potions.Add(SmallPotion);
Potions.Add(MediumPotion);
Potions.Add(LargePotion);

//Makes the Player and all Monsters of the Character class. And Create Players Inventory
List<Item> PlayerInventory = new List<Item>();
Player PlayerCharacter = new Player("Player", 5, 2, 2, 2, 2, 2, Unarmed, Unarmored, 0, 0, PlayerInventory);

// Ints to store the amount of potions the player has

int PlayerSmPotion = 0;
int PlayerPotion = 0;
int PlayerLgPotion = 0;

//----------------------------------------------------------------------------|
// Should I make a subclass for mobs?? Maybe add them in while creating mobs?  *******|
//----------------------------------------------------------------------------|

Character Empty = new Character(" ", 0, 0, 0, 0, 0, 0, Unarmed, Unarmored);
Character Foe = Empty;

Character Kobold = new Character("Kobold", 2, 1, 1, 1, 1, 1, Unarmed, Unarmored);
Character Goblin = new Character("Goblin", 1, 2, 5, 3, 3, 2, Unarmed, Unarmored);
Character Orc = new Character("Orc", 2, 2, 5, 3, 3, 4, Unarmed, Unarmored);
Character Ogre = new Character("Ogre", 3, 2, 5, 3, 3, 8, Unarmed, Unarmored);
Character Bandit = new Character("Bandit", 4, 2, 5, 3, 3, 16, Unarmed, Unarmored);
Character BanditCaptain = new Character("Bandit Captain", 5, 2, 5, 3, 3, 32, Unarmed, Unarmored);
Character Giant = new Character("Giant", 10, 10, 2, 10, 2, 64, Unarmed, Unarmored);
Character Goliath = new Character("Goliath", 15, 12, 2, 10, 10, 128, Unarmed, Unarmored);
Character Roc = new Character("Roc", 12, 14, 14, 8, 10, 256, Unarmed, Unarmored);
Character Dragon = new Character("Dragon", 6, 2, 5, 3, 3, 512, Unarmed, Unarmored);

//Adds all of the Monsters into a Monsters List

List<Character> Monsters = new List<Character>();
Monsters.Add(Goblin);
Monsters.Add(Orc);
Monsters.Add(Ogre);
Monsters.Add(Bandit);
Monsters.Add(BanditCaptain);
Monsters.Add(Dragon);

// Creats all methods for the game loop

void GameOver()
{
    Console.WriteLine("!!!!!GAME OVER!!!!!");
    game = false;
}

void Fight(Character target)
{

    Foe = target.Clone();
    Console.WriteLine(PlayerCharacter.Name + " is fighting " + Foe.Name);

    while (PlayerCharacter.Hp > 0 && Foe.Hp > 0)
    { 
        Console.WriteLine("It is your turn, what would you like to do?");
        Console.WriteLine("1) Attack 2) Bag 3) Run");
        input = Console.ReadLine();
        if (input == "1")
        {
            RefreshScreen();
            PlayerCharacter.Attack(Foe);
            if (Foe.Hp <= 0)
            {
                Console.WriteLine($"You have deafeted the {Foe.Name}!");
                Console.WriteLine($"They dropped {Foe.Value} gold.");
            }
            Console.WriteLine("It is the foes turn. they attack!");
            if (Foe.Hp > 0)
            {
            Foe.Attack(PlayerCharacter);
            }
        } else if (input == "2")
        {
            RefreshScreen();
            Inventory();
        } else if (input == "3")
        {
            Foe.ChangeHealth(-Foe.Hp);
        }
    }
    if (input == "3")
    {
        Console.WriteLine("You run away.");
    }
    else if (PlayerCharacter.Hp > 0)
    {
        Console.WriteLine(Foe.Name + " is dead.");
        PlayerCharacter.GainXp(Foe.Value);
        PlayerCharacter.Value += Foe.Value;
        Console.WriteLine("You gained " + Foe.Value + " gold and now have " + PlayerCharacter.Value + ".");
        Foe = null;
    } else 
    {
        Console.WriteLine(PlayerCharacter.Name + " is dead.");
        GameOver();
    }
    Console.ReadLine();
    Foe = Empty;
}

void RefreshScreen()
{
    Console.Clear();
    Console.WriteLine("          ----------   CONSOLE ADVENTURE   ----------          ");
}

void Shop()
{ 
    RefreshScreen();
    Console.WriteLine("This is the store");
    Console.WriteLine("You have " + PlayerCharacter.Value + " gold.");
    Console.WriteLine("What would you like to shop for? 1) Weapons 2) Armor 3) Potions");
    input = Console.ReadLine();
    RefreshScreen();
    // Lists and Allows Player to buy weapons after checking if he has enough gold for the selected weapon

    if (input == "1")
    {
        for (int i = 0; i < Weapons.Count; i += 1)
        {
            Item weapon = Weapons[i];
            Console.WriteLine(weapon.Name + " (" + weapon.Value + " gold)");
        }
        Console.WriteLine("Would you like to buy any?");
        Console.WriteLine("1) Dagger 2)Short Sword 3) Long Sword 4)Claymore");
        input = Console.ReadLine();
        if ((input == "1") && (PlayerCharacter.Value >= Weapons[0].Value))
        {
            PlayerCharacter.Weapon = Weapons[0];
            PlayerCharacter.Value -= Weapons[0].Value;
            Console.WriteLine("You have equiped a Dagger and have " + PlayerCharacter.Value + " gold left.");
        } else if ((input == "2") && (PlayerCharacter.Value >= Weapons[1].Value))
        {
            PlayerCharacter.Weapon = Weapons[1];
            PlayerCharacter.Value -= Weapons[1].Value;
            Console.WriteLine("You have equiped a Short Sword and have " + PlayerCharacter.Value + " gold left.");
        } else if ((input == "3") && (PlayerCharacter.Value >= Weapons[2].Value))
        {
            PlayerCharacter.Weapon = Weapons[2];
            PlayerCharacter.Value -= Weapons[2].Value;
            Console.WriteLine("You have equiped a Long Sword and have " + PlayerCharacter.Value + " gold left.");
        } else if ((input == "4") && (PlayerCharacter.Value >= Weapons[3].Value))
        {
            PlayerCharacter.Weapon = Weapons[3];
            PlayerCharacter.Value -= Weapons[3].Value;
            Console.WriteLine("You have equiped a Claymore and have " + PlayerCharacter.Value + " gold left.");
        }
        else
        {
            Console.WriteLine("Invalid option or not enough gold.");
            Shop();
        }
    }

    // Lists and Allows Player to buy armor after checking if he has enough gold for the selected armor

    else if (input == "2")
    {
        for (int i = 0; i < Armors.Count; i += 1)
        {
            Item armor = Armors[i];
            Console.WriteLine(armor.Name + " (" + armor.Value + " gold)");
        }
        Console.WriteLine("Would you like to buy any?");
        Console.WriteLine("1) Leather 2)Chainmail 3)Plate");
        input = Console.ReadLine();
        if ((input == "1") && (PlayerCharacter.Value >= Armors[0].Value))
        {
            PlayerCharacter.Armor = Armors[0];
            PlayerCharacter.Value -= Weapons[0].Value;
            Console.WriteLine("You have equiped Leather armor and have " + PlayerCharacter.Value + " gold left.");
        }
        else if ((input == "2") && (PlayerCharacter.Value >= Armors[1].Value))
        {
            PlayerCharacter.Armor = Armors[1];
            PlayerCharacter.Value -= Weapons[0].Value;
            Console.WriteLine("You have equiped a Chain Mail armor and have " + PlayerCharacter.Value + " gold left.");
        }
        else if ((input == "3") && (PlayerCharacter.Value >= Armors[2].Value))
        {
            PlayerCharacter.Armor = Armors[2];
            PlayerCharacter.Value -= Weapons[0].Value;
            Console.WriteLine("You have equiped a Plate armor and have " + PlayerCharacter.Value + " gold left.");
        }
        else
        {
            Console.WriteLine("Invalid option or not enough gold.");
            Shop();
        }
    }

    // Lists and Allows Player to buy potions after checking if he has enough gold for the selected potion

    else if (input == "3")
    {
        for (int i = 0; i < Potions.Count; i += 1)
        {
            Item potion = Potions[i];
            Console.WriteLine(potion.Name + " (" + potion.Value + " gold)");
        }
        Console.WriteLine("Would you like to buy any?");
        Console.WriteLine("1) Small Potion 2)Medium Potion 3)Large Potion");
        input = Console.ReadLine();

        // Checks to see if the play had enough for what they selected

        if ((input == "1") && (PlayerCharacter.Value >= Potions[0].Value))
        {
            PlayerSmPotion =+ 1;
            Console.WriteLine("You now have " + PlayerSmPotion + " small potions.");
        } else if ((input == "2") && (PlayerCharacter.Value >= Potions[1].Value))
        {
            PlayerPotion =+ 1;
            Console.WriteLine("You now have " + PlayerPotion + " medium potions.");
        } else if ((input == "3") && (PlayerCharacter.Value >= Potions[3].Value))
        {
            PlayerLgPotion =+ 1;
            Console.WriteLine("You now have " + PlayerLgPotion + " large potions.");
        } else
        {
            Console.WriteLine("Invalid Option or not enough gold.");
            Shop();
        }
    }

}

void Inventory()
{
    RefreshScreen();
    Console.WriteLine("Weapon: " + PlayerCharacter.Weapon.Name);
    Console.WriteLine("Armor: " + PlayerCharacter.Armor.Name);
    Console.WriteLine("Small Potions: " + PlayerSmPotion);
    Console.WriteLine("Medium Potions: " + PlayerPotion);
    Console.WriteLine("Large Potions: " + PlayerLgPotion);
    Console.WriteLine("Would you like to inspect your gear or use a potion?");
    Console.WriteLine("1) Weapon 2) Armor 3) Small Potion 4) Medium Potion 5) Large Potion");
    input = Console.ReadLine();
    if (input == "1")
    {
        Console.WriteLine($"Weapon: {PlayerCharacter.Weapon.Name}");
        Console.WriteLine($"Damage: {PlayerCharacter.Weapon.Effect}");
    }
    else if (input == "2")
    {
        Console.WriteLine($"Armor: {PlayerCharacter.Armor.Name}");
        Console.WriteLine($"Blocks: {PlayerCharacter.Armor.Effect} damage");
    }
    else if ((input == "3") && (PlayerSmPotion > 0))
    {
        PlayerCharacter.ChangeHealth(SmallPotion.Value);
        Console.WriteLine($"You have used a small health potion to heal {SmallPotion.Value} Hp");
        Console.WriteLine($"You now have {PlayerCharacter.Hp}.");
    }
    else if ((input == "4") && (PlayerPotion > 0))
    {
        PlayerCharacter.ChangeHealth(MediumPotion.Value);
        Console.WriteLine($"You have used a medium health potion to heal {MediumPotion.Value} Hp");
        Console.WriteLine($"You now have {PlayerCharacter.Hp}.");
    }
    else if ((input == "5") && (PlayerLgPotion > 0))
    {
        PlayerCharacter.ChangeHealth(LargePotion.Value);
        Console.WriteLine($"You have used a small health potion to heal {LargePotion.Value} Hp");
        Console.WriteLine($"You now have {PlayerCharacter.Hp}.");
    }
    else if (((input == "3") && (PlayerSmPotion <= 0)) || ((input == "4") && (PlayerPotion <= 0)) || ((input == "5") && (PlayerLgPotion <= 0)))
    {
        Console.WriteLine("You do not have any of those.");
        Console.ReadLine();
        Inventory();
    } 
    else {
        Console.WriteLine("Invalid Option");
    }
    Console.ReadLine();
}

void Info()
{
    Console.WriteLine("Player Stats");
    Console.WriteLine($"Player Health: {PlayerCharacter.Hp}");
    Console.WriteLine($"Player Strength: {PlayerCharacter.Str}");
    Console.WriteLine($"Player Damage: {PlayerCharacter.Str + PlayerCharacter.Weapon.Effect}");
    Console.WriteLine($"Player Speed: {PlayerCharacter.Speed}");
    Console.WriteLine($"Player Consitution: {PlayerCharacter.Con}");
    Console.WriteLine($"Player Restance: {PlayerCharacter.Res}");
    Console.WriteLine($"Player Gold: {PlayerCharacter.Value}");
}

while (game)
{

    //Main Menu Print

    RefreshScreen();
    Console.WriteLine("Press Enter to Start");
    Console.ReadLine();

    //Controls flow of Players choice through the game.

    //----------------------------------------------------------------------------|
    // Is it better to use a bool or simply while (true) and break?        *******|
    //----------------------------------------------------------------------------|

    bool choice = true;
    while (choice)
    {

        RefreshScreen();

        Console.WriteLine("What would you like to do? 1)Fight! 2)Shop 3)Inventory 4)Info");

        input = Console.ReadLine();
        RefreshScreen();
        if (input == "1")
        {
            Character target = Monsters[random.Next(0, Monsters.Count)];
            Fight(target);
            choice = false;

        }
        else if (input == "2")
        {
            Shop();
            choice = false;
        }
        else if (input == "3")
        {
            Inventory();
            choice = false;
        }
        else if (input == "4")
        {
            Info();
            choice = false;
        }
        else
        {
            Console.WriteLine("INVALID OPTION");
        }

    }
}


//----------------------------------------------------------------------------|
// Do I need to  move the class to a sepearte file?                    *******|
//----------------------------------------------------------------------------|

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
        return (Character) this.MemberwiseClone();
    }


    //Where we will put all of the methods of the character class.
    
    public void Attack(Character target)
    {
        Console.WriteLine(Name + " attacked " + target.Name);
        target.Hp -= Str;
        Console.WriteLine($"{Name} dealt {Str + Weapon.Effect} damage to {target.Name}");
    }

    public void ChangeHealth(int num)
    {
        Hp += num;
    }

}

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
        } else if (Xp < 0)
        {

        }

    } }

// Creates Item Class

public class Item
{
    public string Type { get; set; }
    public string Name { get; set; }
    public int Effect { get; set; }
    public int Value { get; set; }

    // Weapon Constructor

    public Item(string type, string name, int effect, int value)
    {
        Type = type;
        Name = name;
        Effect = effect;
        Value = value;
    }
}

