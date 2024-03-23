
using System.Text.Json;
using System.Reflection;
using CA.Entities;
using CA.Sqlite;
using static CA.Entities.Character;


bool game = true;
string input;

// Initializes Lists for Characters and Items then fills the Lists from Json files
Character Foe;

// Creates random to use throughout code

Random random = new Random();

//Creates a db Object
DB dB = new DB();

//Updates Object Lists with Sqlite Methods
List<Item> Weapons = dB.ReadWeapons();
List<Item> Armors = dB.ReadArmors();
List<Item> Potions = dB.ReadPotions();
List<Location> Locations = dB.ReadLocations();
List<Character> Monsters = dB.ReadMonsters(Weapons, Armors, Locations);


//Makes the Player and all Monsters of the Character class. And Create Players Inventory
Item Unarmed = new Item("Weapon", "Unarmed", 1, 0);
Item Unarmored = new Item("Armor", "Unarmored", 1, 0);
List<Item> PlayerInventory = new List<Item>();
Player PlayerCharacter = dB.ReadPlayer(Weapons, Armors);

// Sets Player Locations *******************************
PlayerCharacter.CurrentLocation = Locations[0];

// Ints to store the amount of potions the player has

int PlayerSmPotion = 0;
int PlayerPotion = 0;
int PlayerLgPotion = 0;

dB.AddToDB(PlayerCharacter, Weapons, Armors, Locations);


// Creats all methods for the game loop

void GameOver()
{
    Console.WriteLine("!!!!!GAME OVER!!!!!");
    game = false;
}

void Inn()
{
    Console.WriteLine("You are in the Inn, what would you like to do?");
    Console.WriteLine("1) Eat for 5gp 2) Sleep for 25gp 3) Ask for quest");
    input = Console.ReadLine();
    if ((input == "1") && (PlayerCharacter.Value >= 5))
    {
        Console.WriteLine("You eat and regain 10 health");
        PlayerCharacter.Value -= 5;
        PlayerCharacter.ChangeHealth(10);
    }
    else if ((input == "2") && (PlayerCharacter.Value >= 25))
    {
        PlayerCharacter.ChangeHealth(50);
    }
    else if (input == "3")
    {
        Console.WriteLine("Sorry I don't have any quests at the moment");
    }
    else
    {
        Console.WriteLine("Invalid Option or insufficient Funds");
    }
}

void Well()
{
    Console.WriteLine("You walk up to the towns well, what would you like to do?");
    Console.WriteLine("1) Throw in a coin 2) Leave");
    input = Console.ReadLine();
    if ((input == "1") && (PlayerCharacter.Value >= 1));
    {
        Console.WriteLine("You throw a coin into the well and wish for safety.");

        // ADD SAVE FUNCTION HERE ****************
    }
}

void Travel()
{
    // Handles Travel if there is both a previous and a next location attached to the Current Location
    if (PlayerCharacter.CurrentLocation.NextLocation > Locations.Count) 
    {
        Location currentLoc = PlayerCharacter.CurrentLocation;
        do
        {
            RefreshScreen();
            Console.WriteLine("Where would you like to go?");
            Console.WriteLine($"1) {Locations[PlayerCharacter.CurrentLocation.PrevLocation].Name} 2) {Locations[PlayerCharacter.CurrentLocation.NextLocation].Name} 3) Cancel");
            input = Console.ReadLine();
            if (input == "1")
            {
                PlayerCharacter.CurrentLocation = Locations[PlayerCharacter.CurrentLocation.PrevLocation];
            }
            else if (input == "2")
            {
                PlayerCharacter.CurrentLocation = Locations[PlayerCharacter.CurrentLocation.NextLocation];
            }
            else if (input == "3")
            {
                break;
            }
        }
        while (PlayerCharacter.CurrentLocation == currentLoc);
        }
    // Handles Travel if there is only a previous location attached to the Current Location
    else
    {
        Location currentLoc = PlayerCharacter.CurrentLocation;
        do
        {
            RefreshScreen();
            Console.WriteLine("Where would you like to go?");
            Console.WriteLine($"1) {Locations[PlayerCharacter.CurrentLocation.PrevLocation].Name} 2) Cancel");
            input = Console.ReadLine();
            if (input == "1")
            {
                PlayerCharacter.CurrentLocation = Locations[PlayerCharacter.CurrentLocation.PrevLocation];
            } else if (input == "2")
            {
                break;
            }
        }
        while (PlayerCharacter.CurrentLocation == currentLoc);

    }
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
            if (Foe.Hp > 0)
            {
                Foe.Attack(PlayerCharacter);
            }
        }
        else if (input == "2")
        {
            RefreshScreen();
            Inventory();
        }
        else if (input == "3")
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
    }
    else
    {
        Console.WriteLine(PlayerCharacter.Name + " is dead.");
        GameOver();
    }
    Console.ReadLine();
    Foe = null;
}

void RefreshScreen()
{
    Console.Clear();
    Console.WriteLine("          ----------   CONSOLE ADVENTURE   ----------          ");
    Console.WriteLine($"          {PlayerCharacter.CurrentLocation.Name} {PlayerCharacter.Depth}                                                    ");
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
        }
        else if ((input == "2") && (PlayerCharacter.Value >= Weapons[1].Value))
        {
            PlayerCharacter.Weapon = Weapons[1];
            PlayerCharacter.Value -= Weapons[1].Value;
            Console.WriteLine("You have equiped a Short Sword and have " + PlayerCharacter.Value + " gold left.");
        }
        else if ((input == "3") && (PlayerCharacter.Value >= Weapons[2].Value))
        {
            PlayerCharacter.Weapon = Weapons[2];
            PlayerCharacter.Value -= Weapons[2].Value;
            Console.WriteLine("You have equiped a Long Sword and have " + PlayerCharacter.Value + " gold left.");
        }
        else if ((input == "4") && (PlayerCharacter.Value >= Weapons[3].Value))
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
            PlayerSmPotion = +1;
            Console.WriteLine("You now have " + PlayerSmPotion + " small potions.");
        }
        else if ((input == "2") && (PlayerCharacter.Value >= Potions[1].Value))
        {
            PlayerPotion = +1;
            Console.WriteLine("You now have " + PlayerPotion + " medium potions.");
        }
        else if ((input == "3") && (PlayerCharacter.Value >= Potions[2].Value))
        {
            PlayerLgPotion = +1;
            Console.WriteLine("You now have " + PlayerLgPotion + " large potions.");
        }
        else
        {
            Console.WriteLine("Invalid Option or not enough gold.");
            input = Console.ReadLine();
            Shop();
        }
        input = Console.ReadLine();
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
        PlayerCharacter.ChangeHealth(Potions[0].Value);
        PlayerSmPotion -= 1;
        Console.WriteLine($"You have used a small health potion to heal {Potions[0].Value} Hp");
        Console.WriteLine($"You now have {PlayerCharacter.Hp}.");
    }
    else if ((input == "4") && (PlayerPotion > 0))
    {
        PlayerCharacter.ChangeHealth(Potions[1].Value);
        PlayerPotion -= 1;
        Console.WriteLine($"You have used a medium health potion to heal {Potions[1].Value} Hp");
        Console.WriteLine($"You now have {PlayerCharacter.Hp}.");
    }
    else if ((input == "5") && (PlayerLgPotion > 0))
    {
        PlayerCharacter.ChangeHealth(Potions[2].Value);
        PlayerLgPotion -= 1;
        Console.WriteLine($"You have used a large health potion to heal {Potions[2].Value} Hp");
        Console.WriteLine($"You now have {PlayerCharacter.Hp}.");
    }
    else if (((input == "3") && (PlayerSmPotion <= 0)) || ((input == "4") && (PlayerPotion <= 0)) || ((input == "5") && (PlayerLgPotion <= 0)))
    {
        Console.WriteLine("You do not have any of those.");
        Console.ReadLine();
        Inventory();
    }
    else
    {
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

//Main Loop of the game.

while (game)
{

    //Main Menu Print

    RefreshScreen();
    Console.WriteLine("Press Enter to Start");
    Console.ReadLine();

    //Controls flow of Players choice through the game.

    bool choice = true;
    while (choice)
    {
        RefreshScreen();

        if (PlayerCharacter.CurrentLocation == Locations[0])
        {
            Console.WriteLine("You are in town, what would you like to do?");
            Console.WriteLine("1) Vist 2) Leave 3) Bag 4) Info");
            input = Console.ReadLine();
            if (input == "1")
            {
                Console.WriteLine("Where would you like to go?");
                Console.WriteLine("1) Store 2) Inn 3) Well");
                input = Console.ReadLine();
                if (input == "1")
                {
                    Inn();
                }
                else if (input == "2")
                {
                    Shop();
                }
                else if (input == "3")
                {
                    Well();
                }
            }
            else if (input == "2")
            {
                Travel();
            }
            else if (input == "3")
            {
                Inventory();
            }
            else if (input == "4")
            {
                Info();
            }
            else
            {
                Console.WriteLine("Invalid Option!!");
                Console.ReadLine();
            }
        }
        else
        {
            Console.WriteLine($"You are in the {PlayerCharacter.CurrentLocation.Name}");
            Console.WriteLine("What would you like to do? 1)Fight! 2)Leave 3)Inventory 4)Info");

            input = Console.ReadLine();
            RefreshScreen();
            if (input == "1")
            {
                List<Character> foes = new List<Character>();
                // Creates a LINQ Query to filter Monsters by Player Location
                IEnumerable<Character> targetQuery =
                    from foe in Monsters
                    where foe.Locs.Contains(PlayerCharacter.CurrentLocation)
                    select foe;

                foreach (Character foe in targetQuery)
                {
                    foes.Add(foe);
                }
                foreach (Character foe in foes)
                {
                    Console.WriteLine(foe.Name);
                }
                Console.ReadLine();
                Character target = foes[random.Next(2, Monsters.Count)];
                Fight(target);
                choice = false;

            }
            else if (input == "2")
            {
                Travel();
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
}

/*
// Creates the method used to read the objects from files
void ReadJson()
{
    string assemblyLocation = Assembly.GetExecutingAssembly().Location;
    string assemblyPath = Path.GetDirectoryName(assemblyLocation);

    string WeaponFile = File.ReadAllText(Path.Combine(assemblyPath, ".\\Files\\Weapons.json"));
    Weapons = JsonSerializer.Deserialize<List<Item>>(WeaponFile);

    string ArmorFile = File.ReadAllText(Path.Combine(assemblyPath, ".\\Files\\Armors.json"));
    Armors = JsonSerializer.Deserialize<List<Item>>(ArmorFile);

    string PotionFile = File.ReadAllText(Path.Combine(assemblyPath, ".\\Files\\Potions.json"));
    Potions = JsonSerializer.Deserialize<List<Item>>(PotionFile);

    string CharacterFile = File.ReadAllText(Path.Combine(assemblyPath, ".\\Files\\Monsters.json"));
    Monsters = JsonSerializer.Deserialize<List<Character>>(CharacterFile);
}
*/