//----------------------------------------------------------------------------|
// Use for putting notes in for coming back later.                     *******|
//----------------------------------------------------------------------------|

bool game = true;
string input;

//Main Loop of the game.

Random random = new Random();

//Makes the Player and all Monsters of the Character class.

Character PlayerCharacter = new Character("Player", 5, 5, 5, 5, 5, 5);

//----------------------------------------------------------------------------|
// Should I make a subclass for mobs??                                 *******|
//----------------------------------------------------------------------------|

Character Goblin = new Character("Goblin", 1, 2, 5, 3, 3, 1);
Character Orc = new Character("Orc", 2, 2, 5, 3, 3, 2);
Character Ogre = new Character("Ogre", 3, 2, 5, 3, 3, 3);
Character Bandit = new Character("Bandit", 4, 2, 5, 3, 3, 4);
Character BanditCaptain = new Character("Bandit Captain", 5, 2, 5, 3, 3, 5);
Character Dragon = new Character("Dragon", 6, 2, 5, 3, 3, 6);

//Adds all of the Monsters into a Monsters List

var Monsters = new List<Character>();
Monsters.Add(Goblin);
Monsters.Add(Orc);
Monsters.Add(Ogre);
Monsters.Add(Bandit);
Monsters.Add(BanditCaptain);
Monsters.Add(Dragon);

//Where we will put all other methods of the game.

//*****************************************************************************
//----------------------------------------------------------------------------|
// Having to find out how to make a copy of an item and not use original  ****| I THINK i need to make a dictionary for each monster then add all of the dictionarys to a list so that I can pull 1 at a time to make an object while in a loop.
//----------------------------------------------------------------------------| If you make an object in a for loop it is destroyed when the loop is destroyed.
//*****************************************************************************
void Fight(Character Player, Character Foe)
{
    Console.WriteLine(Player.Name + " is fighting " + Foe.Name);
    while (PlayerCharacter.Hp > 0 && Foe.Hp > 0)
    {
        PlayerCharacter.Attack(Foe);
        if (Foe.Hp > 0)
        {
            Foe.Attack(Player);
        }
    } if (PlayerCharacter.Hp > 0)
    {
        Console.WriteLine(Foe.Name + " is dead.");
        Player.Value += Foe.Value;
        Console.WriteLine("You gained " + Foe.Value + " gold and now have " + PlayerCharacter.Value + ".");
    } else 
    {
        Console.WriteLine(PlayerCharacter.Name + " is dead.");
    }
}

void Shop()
{ 
    Console.WriteLine("This is the store");
    Console.WriteLine("You have " + PlayerCharacter.Value + " gold.");
    Console.WriteLine("What would you like to buy? 1) Health Potion(3 gold) 2) Weapon Upgrade(5 gold)");
    input = Console.ReadLine();
    if (input == "1")
    {
        PlayerCharacter.Value -= 3;
        PlayerCharacter.Hp += 5;
        Console.WriteLine("You heal by 5, you now have " + PlayerCharacter.Hp + " HP.");
        Console.WriteLine("You now have " + PlayerCharacter.Value + " gold.");
    }
    else if (input == "2")
    {
        PlayerCharacter.Str += 1;
        Console.WriteLine("Your attack went up by one and is now " + PlayerCharacter.Str + ".");
        Console.WriteLine("You now have " + PlayerCharacter.Value + " gold.");
    }

}

void Inventory()
{
    Console.WriteLine("This is your inventory");
}

void Info()
{
    Console.WriteLine("this is the information screen");
}

while (game)
{

    //Main Menu Print

    Console.WriteLine("Welcome to Console Adventure");
    Console.WriteLine("Press Enter to Start");
    Console.ReadLine();

    //Controls flow of Players choice through the game.

    //----------------------------------------------------------------------------|
    // Is it better to use a bool or simply while (true) and break?        *******|
    //----------------------------------------------------------------------------|

    bool choice = true;
    while (choice)
    {

        Console.WriteLine("What would you like to do? 1)Fight! 2)Shop 3)Inventory 4)Info");

        input = Console.ReadLine();

        if (input == "1")
        {
            Character target = Monsters[random.Next(0, Monsters.Count)];
            Fight(PlayerCharacter, target);
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
    public int Dex { get; set; }
    public int Con { get; set; }
    public int Res { get; set; }
    public int Value { get; set; }

    //Character class Constructor

    public Character(string name, int hp, int str, int dex, int con, int res, int value)
    {
        Name = name;
        Hp = hp;
        Str = str;
        Dex = dex;
        Con = con;
        Res = res;
        Value = value;
    }

    //Where we will put all of the methods of the character class.
    
    public void Attack(Character target)
    {
        Console.WriteLine(Name + " attacked " + target.Name);
        target.Hp -= Str;
        Console.WriteLine(target.Name + " has " + target.Hp + " health left.");
    }

}


