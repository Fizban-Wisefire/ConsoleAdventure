using CA.Entities;
using Microsoft.Data.Sqlite;

namespace CA.Sqlite
{
    public class DB
    {
        // Used to create a database from the local json files.
        public void CreateDB(List<Character> Monsters, List<Item> Weapons, List<Item> Armors, List<Item> Potions)
        {
            Console.ReadLine();
            using (var connection = new SqliteConnection("Data Source=adventure.db"))
            {

                connection.Open();

                // Creates all tables for characters and items
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    CREATE TABLE character (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        name TEXT NOT NULL,
                        hp INTEGER,
                        str INTEGER,
                        speed INTEGER,
                        con INTEGER,
                        res INTEGER,
                        value INTEGER,
                        weapon STRING,
                        armor STRING NOT NULL
                    );

                    CREATE TABLE weapon (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        type TEXT NOT NULL,
                        name TEXT NOT NULL,
                        effect INTEGER NOT NULL,
                        value INTEGER NOT NULL
                    );

                    CREATE TABLE armor (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        type TEXT NOT NULL,
                        name TEXT NOT NULL,
                        effect INTEGER NOT NULL,
                        value INTEGER NOT NULL
                    );

                    CREATE TABLE potion (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        type TEXT NOT NULL,
                        name TEXT NOT NULL,
                        effect INTEGER NOT NULL,
                        value INTEGER NOT NULL
                    );
                ";
                command.ExecuteNonQuery();



                // Enters all characters in the character table
                for (int i = 0; i < Monsters.Count; i++)
                {
                    command = connection.CreateCommand();
                    //                    Console.WriteLine(monsters[i].Value);
                    //                    Console.ReadLine();
                    command.CommandText =
                        @"
                            INSERT INTO character (name, hp, str, speed, con, res, value, weapon, armor)
                            VALUES ($name, $hp, $str, $speed, $con, $res, $value, $Weapon, $Armor)
                        ";
                    command.Parameters.AddWithValue("$name", Monsters[i].Name);
                    command.Parameters.AddWithValue("$hp", Monsters[i].Hp);
                    command.Parameters.AddWithValue("$str", Monsters[i].Str);
                    command.Parameters.AddWithValue("$speed", Monsters[i].Speed);
                    command.Parameters.AddWithValue("$con", Monsters[i].Con);
                    command.Parameters.AddWithValue("$res", Monsters[i].Res);
                    command.Parameters.AddWithValue("$value", Monsters[i].Value);
                    command.Parameters.AddWithValue("$Weapon", Monsters[i].Weapon.Name);
                    command.Parameters.AddWithValue("$Armor", Monsters[i].Armor.Name);
                    command.ExecuteNonQuery();
                }

                // Enters all weapons in the character table
                for (int i = 0; i < Weapons.Count; i++)
                {
                    command = connection.CreateCommand();
                    command.CommandText =
                        @"
                            INSERT INTO weapon (type, name, effect, value)
                            VALUES ($type, $name, $effect, $value)
                        ";
                    command.Parameters.AddWithValue("$type", Weapons[i].Type);
                    command.Parameters.AddWithValue("$name", Weapons[i].Name);
                    command.Parameters.AddWithValue("$effect", Weapons[i].Effect);
                    command.Parameters.AddWithValue("$value", Weapons[i].Value);
                    command.ExecuteNonQuery();
                }

                // Enters all armors in the character table
                for (int i = 0; i < Armors.Count; i++)
                {
                    command = connection.CreateCommand();
                    command.CommandText =
                        @"
                            INSERT INTO armor (type, name, effect, value)
                            VALUES ($type, $name, $effect, $value)
                        ";
                    command.Parameters.AddWithValue("$type", Armors[i].Type);
                    command.Parameters.AddWithValue("$name", Armors[i].Name);
                    command.Parameters.AddWithValue("$effect", Armors[i].Effect);
                    command.Parameters.AddWithValue("$value", Armors[i].Value);
                    command.ExecuteNonQuery();
                }

                // Enters all potions in the character table
                for (int i = 0; i < Potions.Count; i++)
                {
                    command = connection.CreateCommand();
                    command.CommandText =
                        @"
                            INSERT INTO potion (type, name, effect, value)
                            VALUES ($type, $name, $effect, $value)
                        ";
                    command.Parameters.AddWithValue("$type", Potions[i].Type);
                    command.Parameters.AddWithValue("$name", Potions[i].Name);
                    command.Parameters.AddWithValue("$effect", Potions[i].Effect);
                    command.Parameters.AddWithValue("$value", Potions[i].Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Creates Weapons List
        public List<Item> ReadWeapons()
        {
            List<Item> weapons = new List<Item>();
            using (var connection = new SqliteConnection("Data Source=adventure.db"))
            {
                connection.Open();
                var weaponCommand = connection.CreateCommand();
                weaponCommand.CommandText =
                    @"
                    SELECT * 
                    FROM weapon
                    ";
                using (var reader = weaponCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        weapons.Add(new Item(reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4)));
                    }
                    weaponCommand.Dispose();
                }
                return weapons;
            }
        }

        // Creates Armors List
        public List<Item> ReadArmors()
        {
            List<Item> armors = new List<Item>();
            using (var connection = new SqliteConnection("Data Source=adventure.db"))
            {
                connection.Open();
                var armorCommand = connection.CreateCommand();
                armorCommand.CommandText =
                    @"
                    SELECT * 
                    FROM armor
                    ";
                using (var reader = armorCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        armors.Add(new Item(reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4)));
                    }
                    armorCommand.Dispose();
                }
                return armors;
            }
        }

        // Creates Armors List
        public List<Item> ReadPotions()
        {
            List<Item> potions = new List<Item>();
            using (var connection = new SqliteConnection("Data Source=adventure.db"))
            {
                connection.Open();
                var potionCommand = connection.CreateCommand();
                potionCommand.CommandText =
                    @"
                    SELECT * 
                    FROM potion
                    ";
                using (var reader = potionCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        potions.Add(new Item(reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4)));
                    }
                    potionCommand.Dispose();
                }
                return potions;
            }
        }
        // Creates Locations List
        public List<Location> ReadLocations()
        {
            List<Location> locations = new List<Location>();
            using (var connection = new SqliteConnection("Data Source=adventure.db"))
            {
                connection.Open();
                var potionCommand = connection.CreateCommand();
                potionCommand.CommandText =
                    @"
                    SELECT * 
                    FROM location
                    ";
                using (var reader = potionCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        locations.Add(new Location(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8)));
                    }
                    potionCommand.Dispose();
                }
                return locations;
            }
        }
        // Creates Armors List
        public List<Character> ReadMonsters(List<Item> Weapons, List<Item> Armors, List<Location> Locations)
        {
            List<Character> monsters = new List<Character>();
            using (var connection = new SqliteConnection("Data Source=adventure.db"))
            {
                connection.Open();
                var monsterCommand = connection.CreateCommand();
                monsterCommand.CommandText =
                    @"
                        SELECT * 
                        FROM character
                    ";
                using (var reader = monsterCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Character monster = new Character(reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), Weapons[reader.GetInt32(8)], Armors[reader.GetInt32(9)]);
                        monster.Locs = new List<Location>() { Locations[reader.GetInt32(10)] };
                        monsters.Add(monster);
                    }
                }
                monsterCommand.Dispose();
                connection.Dispose();
                return monsters;
            }
        }

        public Player ReadPlayer(List<Item> Weapons, List<Item> Armors)
        {
            List<Item> PlayerInventory = new List<Item>();
            using (var connection = new SqliteConnection("Data Source=adventure.db"))
            {
                connection.Open();
                var monsterCommand = connection.CreateCommand();
                monsterCommand.CommandText =
                    @"
                        SELECT * 
                        FROM player
                    ";
                using (var reader = monsterCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Player PlayerCharacter = new Player(reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), Weapons[reader.GetInt32(8)], Armors[reader.GetInt32(9)], reader.GetInt32(10), reader.GetInt32(11), PlayerInventory);
                        return PlayerCharacter;
                    }
                }
                monsterCommand.Dispose();
                connection.Dispose();

                Player NewPlayer = new Player("Player", 5, 2, 2, 2, 2, 2, Weapons[0], Armors[0], 0, 0, PlayerInventory);
                return NewPlayer;
            }
        }

        private void saveWeapons(List<Item> weapons)
        {
            using (var connection = new SqliteConnection("Data Source=adventure.db"))
            {

                connection.Open();

                // Creates all tables for characters and items
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    DROP TABLE weapon;

                    CREATE TABLE weapon (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        type TEXT NOT NULL,
                        name TEXT NOT NULL,
                        effect INTEGER NOT NULL,
                        value INTEGER NOT NULL
                    );
                ";
                command.ExecuteNonQuery();

                // Enters all new weapons in the weapons table
                for (int i = 0; i < weapons.Count; i++)
                {
                    command = connection.CreateCommand();
                    command.CommandText =
                        @"
                                                        INSERT INTO weapon (type, name, effect, value)
                                                        VALUES ($type, $name, $effect, $value)
                                                    ";
                    command.Parameters.AddWithValue("$type", weapons[i].Type);
                    command.Parameters.AddWithValue("$name", weapons[i].Name);
                    command.Parameters.AddWithValue("$effect", weapons[i].Effect);
                    command.Parameters.AddWithValue("$value", weapons[i].Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void saveArmors(List<Item> armors)
        {
            using (var connection = new SqliteConnection("Data Source=adventure.db"))
            {

                connection.Open();

                // Creates all tables for characters and items
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    DROP TABLE armor;

                    CREATE TABLE armor (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        type TEXT NOT NULL,
                        name TEXT NOT NULL,
                        effect INTEGER NOT NULL,
                        value INTEGER NOT NULL
                    );
                ";
                command.ExecuteNonQuery();

                // Enters all new armors in the armors table
                for (int i = 0; i < armors.Count; i++)
                {
                    command = connection.CreateCommand();
                    command.CommandText =
                        @"
                            INSERT INTO armor (type, name, effect, value)
                            VALUES ($type, $name, $effect, $value)
                        ";
                    command.Parameters.AddWithValue("$type", armors[i].Type);
                    command.Parameters.AddWithValue("$name", armors[i].Name);
                    command.Parameters.AddWithValue("$effect", armors[i].Effect);
                    command.Parameters.AddWithValue("$value", armors[i].Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void savePotions(List<Item> potions)
        {
            using (var connection = new SqliteConnection("Data Source=adventure.db"))
            {

                connection.Open();

                // Creates all tables for characters and items
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    DROP TABLE potion;

                    CREATE TABLE potion (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        type TEXT NOT NULL,
                        name TEXT NOT NULL,
                        effect INTEGER NOT NULL,
                        value INTEGER NOT NULL
                    );
                ";
                command.ExecuteNonQuery();

                // Enters all new armors in the armors table
                for (int i = 0; i < potions.Count; i++)
                {
                    command = connection.CreateCommand();
                    command.CommandText =
                        @"
                            INSERT INTO potion (type, name, effect, value)
                            VALUES ($type, $name, $effect, $value)
                        ";
                    command.Parameters.AddWithValue("$type", potions[i].Type);
                    command.Parameters.AddWithValue("$name", potions[i].Name);
                    command.Parameters.AddWithValue("$effect", potions[i].Effect);
                    command.Parameters.AddWithValue("$value", potions[i].Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void saveLocations(List<Location> locations)
        {
            using (var connection = new SqliteConnection("Data Source=adventure.db"))
            {

                connection.Open();

                // Creates all tables for characters and items
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    
                    DROP TABLE location;

                    CREATE TABLE location (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        name TEXT NOT NULL,
                        arriveDescription TEXT,
                        leavingPrevDescription TEXT,
                        leavingNextDescription TEXT,
                        deeperDescription TEXT,
                        length INT,
                        prevLocation INT,
                        nextLocation INT
                    );
                ";
                command.ExecuteNonQuery();

                // Enters all new armors in the armors table
                for (int i = 0; i < locations.Count; i++)
                {
                    command = connection.CreateCommand();
                    command.CommandText =
                        @"
                            INSERT INTO location (name, arriveDescription, leavingPrevDescription, leavingNextDescription, deeperDescription, length, prevLocation, nextLocation)
                            VALUES ($name, $arriveDescription, $leavingPrevDescription, $leavingNextDescription, $deeperDescription, $length, $prevLocation, $nextLocation)
                        ";
                    command.Parameters.AddWithValue("$name", locations[i].Name);
                    command.Parameters.AddWithValue("$arriveDescription", locations[i].ArriveDescription);
                    command.Parameters.AddWithValue("$leavingPrevDescription", locations[i].LeavingPrevDescription);
                    command.Parameters.AddWithValue("$leavingNextDescription", locations[i].LeavingNextDescription);
                    command.Parameters.AddWithValue("$deeperDescription", " ");
                    command.Parameters.AddWithValue("$length", locations[i].Length);
                    command.Parameters.AddWithValue("$prevLocation", locations[i].PrevLocation);
                    command.Parameters.AddWithValue("$nextLocation", locations[i].NextLocation);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void saveCharacters(List<Character> Characters, List<Item> Weapons, List<Item> Armors)
        {
            using (var connection = new SqliteConnection("Data Source=adventure.db"))
            {

                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    DROP TABLE character;

                    CREATE TABLE character (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        name TEXT NOT NULL,
                        hp INTEGER NOT NULL,
                        str INTEGER NOT NULL,
                        speed INTEGER NOT NULL,
                        con INTEGER NOT NULL,
                        res INTEGER NOT NULL,
                        value INTEGER NOT NULL,
                        weapon INTEGER NOT NULL,
                        armor INTEGER NOT NULL
                    );
                ";
                command.ExecuteNonQuery();

                for (var i = 0; i < Characters.Count; i++)
                {
                    command = connection.CreateCommand();
                    command.CommandText =
                        @"
                        INSERT INTO potion (name, hp, str, speed, con, res, value, weapon, armor)
                        VALUES ($name, $hp, $str, $speed, $con, $res, $value, $weapon, $armor)
                    ";
                    command.Parameters.AddWithValue("$name", Characters[i].Name);
                    command.Parameters.AddWithValue("$hp", Characters[i].Hp);
                    command.Parameters.AddWithValue("$str", Characters[i].Str);
                    command.Parameters.AddWithValue("$speed", Characters[i].Speed);
                    command.Parameters.AddWithValue("$con", Characters[i].Con);
                    command.Parameters.AddWithValue("$res", Characters[i].Res);
                    command.Parameters.AddWithValue("$value", Characters[i].Value);
                    int weaponIndex = Weapons.IndexOf(Characters[i].Weapon);
                    int armorIndex = Armors.IndexOf(Characters[i].Armor);
                    command.Parameters.AddWithValue("$weapon", weaponIndex >= 0 ? weaponIndex : 0);
                    command.Parameters.AddWithValue("$armor", armorIndex >= 0 ? armorIndex : 0);
                }
                command.ExecuteNonQuery();
            }
        }


        private void savePlayer(Player PlayerCharacter, List<Item> Weapons, List<Item> Armors)
        {
            using (var connection = new SqliteConnection("Data Source=adventure.db"))
            {

                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    DROP TABLE player;

                    CREATE TABLE player (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        name TEXT NOT NULL,
                        hp INTEGER,
                        str INTEGER,
                        speed INTEGER,
                        con INTEGER,
                        res INTEGER,
                        value INTEGER,
                        weapon INTEGER,
                        armor INTEGER,
                        xp INTEGER,
                        level INTEGER
                    );
                ";
                command.ExecuteNonQuery();

                command = connection.CreateCommand();
                command.CommandText =
                    @"
                        INSERT INTO player (name, hp, str, speed, con, res, value, weapon, armor, xp, level)
                        VALUES ($name, $hp, $str, $speed, $con, $res, $value, $weapon, $armor, $xp, $level)
                    ";
                command.Parameters.AddWithValue("$name", PlayerCharacter.Name);
                command.Parameters.AddWithValue("$hp", PlayerCharacter.Hp);
                command.Parameters.AddWithValue("$str", PlayerCharacter.Str);
                command.Parameters.AddWithValue("$speed", PlayerCharacter.Speed);
                command.Parameters.AddWithValue("$con", PlayerCharacter.Con);
                command.Parameters.AddWithValue("$res", PlayerCharacter.Res);
                command.Parameters.AddWithValue("$value", PlayerCharacter.Value);
                int weaponIndex = Weapons.IndexOf(PlayerCharacter.Weapon);
                int armorIndex = Armors.IndexOf(PlayerCharacter.Armor);
                command.Parameters.AddWithValue("$weapon", weaponIndex >= 0 ? weaponIndex : 0);
                command.Parameters.AddWithValue("$armor", armorIndex >= 0 ? armorIndex : 0);
                command.Parameters.AddWithValue("$xp", PlayerCharacter.Xp);
                command.Parameters.AddWithValue("$level", PlayerCharacter.Level);
                command.ExecuteNonQuery();
            }
        }

        public void saveGameState()
            {

            }

        public void AddToDB(Player PlayerCharacter, List<Item> Weapons, List<Item> Armors, List<Location> Locations)
        {
            //Made to fix characters not having weapons, armor, or locations as well as adding things in through console later

            bool change = true;
            while (change)
            {
                Console.Clear();
                Console.WriteLine("What would you like to add to the game?");
                Console.WriteLine("1) Weapon 2) Armor 3) Potion 4) Location 5) Character 6) Save Player");
                Console.WriteLine("Press any other button to cancel.");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    List<Item> weapons = new List<Item>();
                    using (var connection = new SqliteConnection("Data Source=adventure.db"))
                    {
                        connection.Open();
                        var Command = connection.CreateCommand();
                        Command.CommandText =
                           @"
                            SELECT * 
                            FROM weapon
                            ";
                        using (var reader = Command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                weapons.Add(new Item(reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4)));
                            }
                            Command.Dispose();
                        }
                        List<Item> deletes = new List<Item>();
                        for (int i = 0; i < weapons.Count; i ++)
                        {
                            Item weapon = weapons[i];
                            Console.WriteLine($"Name: {weapon.Name} Effect: {weapon.Effect} Value: {weapon.Value}");
                            Console.WriteLine("Would you like to change this item? 1) Yes 2) No");
                            input = Console.ReadLine();
                            if (input == "1")
                            {
                                Console.WriteLine("What would you like to change?");
                                Console.WriteLine("1) Name 2) Effect 3) Value 4) Delete");
                                Console.WriteLine("Press any other button to cancel.");
                                input = Console.ReadLine();
                                if (input == "1")
                                {
                                    Console.WriteLine($"The current name is {weapon.Name}.");
                                    Console.WriteLine("What would you like the new name to be?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    if (input.Length > 0)
                                    {
                                        weapon.Name = input;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input: Moving Forward");
                                    }
                                }
                                else if (input == "2")
                                {
                                    Console.WriteLine($"The current effect is {weapon.Effect}.");
                                    Console.WriteLine("What would you like the new effect to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    int effect = 0;
                                    try
                                    {
                                        effect = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        weapon.Effect = effect;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input: Moving Forward");
                                    }
                                }
                                else if (input == "3")
                                {
                                    Console.WriteLine($"The current value is {weapon.Value}.");
                                    Console.WriteLine("What would you like the new value to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    int value = 0;
                                    try
                                    {
                                        value = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        weapon.Value = value;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input: Moving Forward");
                                    }
                                }
                                else if (input == "4")
                                {
                                    Console.WriteLine("*** WARNING YOU ARE ABOUT TO DELETE A REQUIRED ITEM ***");
                                    Console.WriteLine("Are you sure?");
                                    Console.WriteLine("YES) Yes 2) No");
                                    input = Console.ReadLine();
                                    if (input == "YES")
                                    {
                                        deletes.Add(weapon);
                                    }
                                }
                            }
                        }
                        foreach (Item delete in deletes)
                        {
                            weapons.Remove(delete);
                        }
                        bool changes = true;
                        while (changes)
                        {
                            Console.Clear();
                            foreach (Item weapon in weapons)
                            {
                                Console.WriteLine($"Name: {weapon.Name} Effect: {weapon.Effect} Value: {weapon.Value}");
                            }
                            Console.WriteLine("Finished weapon list, would you like to 1) Add Weapon 2) Save Changes 3) Cancel Changes");
                            input = Console.ReadLine();
                            if (input == "1")
                            {
                                bool nameFinished = true;
                                string name = " ";
                                bool effectFinished = true;
                                int effect = 0;
                                bool valueFinished = true;
                                int value = 0;
                                while (nameFinished)
                                {
                                    Console.WriteLine("What will the name of this weapon be?");
                                    name = Console.ReadLine();
                                    if (input.Length > 0)
                                    {
                                        Console.WriteLine($"Are you sure you want the name to be {name}");
                                        Console.WriteLine("1) Yes 2) No");
                                        input = Console.ReadLine();
                                        if (input == "1")
                                        {
                                            nameFinished = false;
                                        }
                                    }
                                }
                                while (effectFinished)
                                {
                                    Console.WriteLine("What would you like the effect to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    try
                                    {
                                        effect = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        Console.WriteLine($"Are you sure you want the value to be {effect}?");
                                        Console.WriteLine("1) Yes 2) No");
                                        input = Console.ReadLine();
                                        if (input == "1")
                                        {
                                            effectFinished = false;
                                        }
                                    }
                                }
                                while (valueFinished)
                                {
                                    Console.WriteLine("What would you like the value to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    try
                                    {
                                        value = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        Console.WriteLine($"Are you sure you want the value to be {value}?");
                                        Console.WriteLine("1) Yes 2) No");
                                        input = Console.ReadLine();
                                        if (input == "1")
                                        {
                                            valueFinished = false;
                                        }
                                    }
                                }
                                Item weapon = new Item("Weapon", name, effect, value);
                                weapons.Add(weapon);
                            }
                            else if (input == "2")
                            {
                                foreach (Item weapon in weapons)
                                {
                                    Console.WriteLine($"Name: {weapon.Name} Effect: {weapon.Effect} Value{weapon.Value}");
                                }
                                Console.WriteLine("Are you sure you want to save these changes? It will overwrite original database?");
                                Console.WriteLine("Type YES to save or no to cancel");
                                saveWeapons(weapons);
                            }
                            else if (input == "3")
                            {
                                weapons = ReadWeapons();
                                changes = false;
                            }
                        }
                    }
                }
                else if (input == "2")
                {
                    List<Item> armors = new List<Item>();
                    using (var connection = new SqliteConnection("Data Source=adventure.db"))
                    {
                        connection.Open();
                        var command = connection.CreateCommand();
                        command.CommandText =
                           @"
                            SELECT * 
                            FROM armor
                            ";
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                armors.Add(new Item(reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4)));
                            }
                            command.Dispose();
                        }
                        List<Item> deletes = new List<Item>();
                        for (int i = 0; i < armors.Count; i++)
                        {
                            Item armor = armors[i];
                            Console.WriteLine($"Name: {armor.Name} Effect: {armor.Effect} Value: {armor.Value}");
                            Console.WriteLine("Would you like to change this item? 1) Yes 2) No");
                            input = Console.ReadLine();
                            if (input == "1")
                            {
                                Console.WriteLine("What would you like to change?");
                                Console.WriteLine("1) Name 2) Effect 3) Value 4) Delete");
                                Console.WriteLine("Press any other button to cancel.");
                                input = Console.ReadLine();
                                if (input == "1")
                                {
                                    Console.WriteLine($"The current name is {armor.Name}.");
                                    Console.WriteLine("What would you like the new name to be?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    if (input.Length > 0)
                                    {
                                        armor.Name = input;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input: Moving Forward");
                                    }
                                }
                                else if (input == "2")
                                {
                                    Console.WriteLine($"The current effect is {armor.Effect}.");
                                    Console.WriteLine("What would you like the new effect to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    int effect = 0;
                                    try
                                    {
                                        effect = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        armor.Effect = effect;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input: Moving Forward");
                                    }
                                }
                                else if (input == "3")
                                {
                                    Console.WriteLine($"The current value is {armor.Value}.");
                                    Console.WriteLine("What would you like the new value to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    int value = 0;
                                    try
                                    {
                                        value = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        armor.Value = value;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input: Moving Forward");
                                    }
                                }
                                else if (input == "4")
                                {
                                    Console.WriteLine("*** WARNING YOU ARE ABOUT TO DELETE A REQUIRED ITEM ***");
                                    Console.WriteLine("Are you sure?");
                                    Console.WriteLine("YES) Yes 2) No");
                                    input = Console.ReadLine();
                                    if (input == "YES")
                                    {
                                        deletes.Add(armor);
                                    }
                                }
                            }
                        }
                        foreach (Item delete in deletes)
                        {
                            armors.Remove(delete);
                        }
                        bool changes = true;
                        while (changes)
                        {
                            Console.Clear();
                            foreach (Item armor in armors)
                            {
                                Console.WriteLine($"Name: {armor.Name} Effect: {armor.Effect} Value: {armor.Value}");
                            }
                            Console.WriteLine("Finished armor list, would you like to 1) Add Armor 2) Save Changes 3) Cancel Changes");
                            input = Console.ReadLine();
                            if (input == "1")
                            {
                                bool nameFinished = true;
                                string name = " ";
                                bool effectFinished = true;
                                int effect = 0;
                                bool valueFinished = true;
                                int value = 0;
                                while (nameFinished)
                                {
                                    Console.WriteLine("What will the name of this armor be?");
                                    name = Console.ReadLine();
                                    if (input.Length > 0)
                                    {
                                        Console.WriteLine($"Are you sure you want the name to be {name}");
                                        Console.WriteLine("1) Yes 2) No");
                                        input = Console.ReadLine();
                                        if (input == "1")
                                        {
                                            nameFinished = false;
                                        }
                                    }
                                }
                                while (effectFinished)
                                {
                                    Console.WriteLine("What would you like the effect to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    try
                                    {
                                        effect = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        Console.WriteLine($"Are you sure you want the value to be {effect}?");
                                        Console.WriteLine("1) Yes 2) No");
                                        input = Console.ReadLine();
                                        if (input == "1")
                                        {
                                            effectFinished = false;
                                        }
                                    }
                                }
                                while (valueFinished)
                                {
                                    Console.WriteLine("What would you like the value to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    try
                                    {
                                        value = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        Console.WriteLine($"Are you sure you want the value to be {value}?");
                                        Console.WriteLine("1) Yes 2) No");
                                        input = Console.ReadLine();
                                        if (input == "1")
                                        {
                                            valueFinished = false;
                                        }
                                    }
                                }
                                Item armor = new Item("Armor", name, effect, value);
                                armors.Add(armor);
                            }
                            else if (input == "2")
                            {
                                foreach (Item armor in armors)
                                {
                                    Console.WriteLine($"Name: {armor.Name} Effect: {armor.Effect} Value{armor.Value}");
                                }
                                Console.WriteLine("Are you sure you want to save these changes? It will overwrite original database?");
                                Console.WriteLine("Type YES to save or no to cancel");
                                saveArmors(armors);
                            }
                            else if (input == "3")
                            {
                                armors = ReadArmors();
                                changes = false;
                            }
                        }
                    }
                }
                else if (input == "3")
                {
                    List<Item> potions = new List<Item>();
                    using (var connection = new SqliteConnection("Data Source=adventure.db"))
                    {
                        connection.Open();
                        var command = connection.CreateCommand();
                        command.CommandText =
                           @"
                            SELECT * 
                            FROM potion
                            ";
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                potions.Add(new Item(reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4)));
                            }
                            command.Dispose();
                        }
                        List<Item> deletes = new List<Item>();
                        for (int i = 0; i < potions.Count; i++)
                        {
                            Item potion = potions[i];
                            Console.WriteLine($"Name: {potion.Name} Effect: {potion.Effect} Value: {potion.Value}");
                            Console.WriteLine("Would you like to change this item? 1) Yes 2) No");
                            input = Console.ReadLine();
                            if (input == "1")
                            {
                                Console.WriteLine("What would you like to change?");
                                Console.WriteLine("1) Name 2) Effect 3) Value 4) Delete");
                                Console.WriteLine("Press any other button to cancel.");
                                input = Console.ReadLine();
                                if (input == "1")
                                {
                                    Console.WriteLine($"The current name is {potion.Name}.");
                                    Console.WriteLine("What would you like the new name to be?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    if (input.Length > 0)
                                    {
                                        potion.Name = input;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input: Moving Forward");
                                    }
                                }
                                else if (input == "2")
                                {
                                    Console.WriteLine($"The current effect is {potion.Effect}.");
                                    Console.WriteLine("What would you like the new effect to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    int effect = 0;
                                    try
                                    {
                                        effect = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        potion.Effect = effect;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input: Moving Forward");
                                    }
                                }
                                else if (input == "3")
                                {
                                    Console.WriteLine($"The current value is {potion.Value}.");
                                    Console.WriteLine("What would you like the new value to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    int value = 0;
                                    try
                                    {
                                        value = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        potion.Value = value;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input: Moving Forward");
                                    }
                                }
                                else if (input == "4")
                                {
                                    Console.WriteLine("*** WARNING YOU ARE ABOUT TO DELETE A REQUIRED ITEM ***");
                                    Console.WriteLine("Are you sure?");
                                    Console.WriteLine("YES) Yes 2) No");
                                    input = Console.ReadLine();
                                    if (input == "YES")
                                    {
                                        deletes.Add(potion);
                                    }
                                }
                            }
                        }
                        foreach (Item delete in deletes)
                        {
                            potions.Remove(delete);
                        }
                        bool changes = true;
                        while (changes)
                        {
                            Console.Clear();
                            foreach (Item potion in potions)
                            {
                                Console.WriteLine($"Name: {potion.Name} Effect: {potion.Effect} Value: {potion.Value}");
                            }
                            Console.WriteLine("Finished potion list, would you like to 1) Add Potion 2) Save Changes 3) Cancel Changes");
                            input = Console.ReadLine();
                            if (input == "1")
                            {
                                bool nameFinished = true;
                                string name = " ";
                                bool effectFinished = true;
                                int effect = 0;
                                bool valueFinished = true;
                                int value = 0;
                                while (nameFinished)
                                {
                                    Console.WriteLine("What will the name of this potion be?");
                                    name = Console.ReadLine();
                                    if (input.Length > 0)
                                    {
                                        Console.WriteLine($"Are you sure you want the name to be {name}");
                                        Console.WriteLine("1) Yes 2) No");
                                        input = Console.ReadLine();
                                        if (input == "1")
                                        {
                                            nameFinished = false;
                                        }
                                    }
                                }
                                while (effectFinished)
                                {
                                    Console.WriteLine("What would you like the effect to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    try
                                    {
                                        effect = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        Console.WriteLine($"Are you sure you want the value to be {effect}?");
                                        Console.WriteLine("1) Yes 2) No");
                                        input = Console.ReadLine();
                                        if (input == "1")
                                        {
                                            effectFinished = false;
                                        }
                                    }
                                }
                                while (valueFinished)
                                {
                                    Console.WriteLine("What would you like the value to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    try
                                    {
                                        value = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        Console.WriteLine($"Are you sure you want the value to be {value}?");
                                        Console.WriteLine("1) Yes 2) No");
                                        input = Console.ReadLine();
                                        if (input == "1")
                                        {
                                            valueFinished = false;
                                        }
                                    }
                                }
                                Item potion = new Item("Potion", name, effect, value);
                                potions.Add(potion);
                            }
                            else if (input == "2")
                            {
                                foreach (Item potion in potions)
                                {
                                    Console.WriteLine($"Name: {potion.Name} Effect: {potion.Effect} Value{potion.Value}");
                                }
                                Console.WriteLine("Are you sure you want to save these changes? It will overwrite original database?");
                                Console.WriteLine("Type YES to save or no to cancel");
                                savePotions(potions);
                            }
                            else if (input == "3")
                            {
                                potions = ReadPotions();
                                changes = false;
                            }
                        }
                    }
                }
                else if (input == "4")
                {

                    List<Location> locations = new List<Location>();
                    using (var connection = new SqliteConnection("Data Source=adventure.db"))
                    {
                        connection.Open();
                        var Command = connection.CreateCommand();
                        Command.CommandText =
                           @"
                            SELECT * 
                            FROM location
                            ";
                        using (var reader = Command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Location location = new Location(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8));
                                locations.Add(location);
                            }
                            Command.Dispose();
                        }
                        List<Location> deletes = new List<Location>();
                        for (int i = 0; i < locations.Count; i++)
                        {
                            Location location = locations[i];
                            Console.WriteLine($"Name: {location.Name} \nArrive Description: {location.ArriveDescription} \nLeaving Towards Next: {location.LeavingNextDescription} \nLeaving Towards Previous: {location.LeavingPrevDescription} \nLength: {location.Length}\nPrevious Location: {location.PrevLocation} \nNext Location: {location.NextLocation}");
                            Console.WriteLine("Would you like to change this location? 1) Yes 2) No");
                            input = Console.ReadLine();
                            if (input == "1")
                            {
                                Console.WriteLine("What would you like to change?");
                                Console.WriteLine("1) Name 2) Arrive Description 3) Leaving Previous 4) Leaving Next 5) Length 6) Previous Location 7) Next Location 8) Delete");
                                Console.WriteLine("Press any other button to cancel.");
                                input = Console.ReadLine();
                                if (input == "1")
                                {
                                    Console.WriteLine($"The current name is {location.Name}.");
                                    Console.WriteLine("What would you like the new name to be?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    if (input.Length > 0)
                                    {
                                        location.Name = input;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input: Moving Forward");
                                    }
                                }
                                else if (input == "2")
                                {
                                    Console.WriteLine($"The current Arrive Description is {location.ArriveDescription}.");
                                    Console.WriteLine("What would you like the new name to be?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    if (input.Length > 0)
                                    {
                                        location.ArriveDescription = input;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input: Moving Forward");
                                    }
                                }
                                else if (input == "3")
                                {
                                    Console.WriteLine($"The current Leaving Towards Next is {location.LeavingNextDescription}.");
                                    Console.WriteLine("What would you like the new name to be?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    if (input.Length > 0)
                                    {
                                        location.LeavingNextDescription = input;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input: Moving Forward");
                                    }
                                }
                                else if (input == "4")
                                {
                                    Console.WriteLine($"The current Leaving Towards Previous is {location.LeavingPrevDescription}.");
                                    Console.WriteLine("What would you like the new name to be?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    if (input.Length > 0)
                                    {
                                        location.LeavingPrevDescription = input;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input: Moving Forward");
                                    }
                                }
                                else if (input == "5")
                                {
                                    Console.WriteLine($"The current effect is {location.Length}.");
                                    Console.WriteLine("What would you like the length to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    int effect = 0;
                                    try
                                    {
                                        effect = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        location.Length = effect;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input: Moving Forward");
                                    }
                                }
                                else if (input == "6")
                                {
                                    Console.WriteLine($"The current Previous Location is {location.PrevLocation}.");
                                    Console.WriteLine("What would you like the Previous Location to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    int effect = 0;
                                    try
                                    {
                                        effect = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        location.PrevLocation = effect;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input: Moving Forward");
                                    }
                                }
                                else if (input == "7")
                                {
                                    Console.WriteLine($"The current Next Location is {location.NextLocation}.");
                                    Console.WriteLine("What would you like the Next Location to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    int effect = 0;
                                    try
                                    {
                                        effect = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        location.NextLocation = effect;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input: Moving Forward");
                                    }
                                }
                                else if (input == "8")
                                {
                                    Console.WriteLine("*** WARNING YOU ARE ABOUT TO DELETE A REQUIRED ITEM ***");
                                    Console.WriteLine("Are you sure?");
                                    Console.WriteLine("YES) Yes 2) No");
                                    input = Console.ReadLine();
                                    if (input == "YES")
                                    {
                                        deletes.Add(location);
                                    }
                                }
                            }
                        }
                        foreach (Location delete in deletes)
                        {
                            locations.Remove(delete);
                        }
                        bool changes = true;
                        while (changes)
                        {
                            Console.Clear();
                            foreach (Location location in locations)
                            {
                                Console.WriteLine($"Name: {location.Name}  Length: {location.Length} Previous Location: {location.PrevLocation} Next Location: {location.NextLocation}");
                            }
                            Console.WriteLine("Finished location list, would you like to 1) Add Location 2) Save Changes 3) Cancel Changes");
                            input = Console.ReadLine();
                            if (input == "1")
                            {
                                bool nameFinished = true;
                                string name = " ";
                                bool arriveFinished = true;
                                string arrive = " ";
                                bool leavePFinished = true;
                                string leaveP = " ";
                                bool leaveNFinished = true;
                                string leaveN = " ";
                                bool deeperDescriptFinished = true;
                                string deeperDescript = " ";
                                bool lengthFinished = true;
                                int length = 0;
                                bool prevLocfinished = true;
                                int prevLoc = 0;
                                bool nextLocfinished = true;
                                int nextLoc = 0;
                                while (nameFinished)
                                {
                                    Console.WriteLine("What will the name of this location be?");
                                    name = Console.ReadLine();
                                    if (input.Length > 0)
                                    {
                                        Console.WriteLine($"Are you sure you want the name to be {name}");
                                        Console.WriteLine("1) Yes 2) No");
                                        input = Console.ReadLine();
                                        if (input == "1")
                                        {
                                            nameFinished = false;
                                        }
                                    }
                                }
                                while (arriveFinished)
                                {
                                    Console.WriteLine("What will the arrive description of this location be?");
                                    arrive = Console.ReadLine();
                                    if (input.Length > 0)
                                    {
                                        Console.WriteLine($"Are you sure you want the arrive description to be {arrive}");
                                        Console.WriteLine("1) Yes 2) No");
                                        input = Console.ReadLine();
                                        if (input == "1")
                                        {
                                            arriveFinished = false;
                                        }
                                    }
                                }
                                while (leavePFinished)
                                {
                                    Console.WriteLine("What will the leave previous description of this location be?");
                                    leaveP = Console.ReadLine();
                                    if (input.Length > 0)
                                    {
                                        Console.WriteLine($"Are you sure you want the leave previous description to be {leaveP}");
                                        Console.WriteLine("1) Yes 2) No");
                                        input = Console.ReadLine();
                                        if (input == "1")
                                        {
                                            leavePFinished = false;
                                        }
                                    }
                                }
                                while (leaveNFinished)
                                {
                                    Console.WriteLine("What will the leave next description of this location be?");
                                    leaveN = Console.ReadLine();
                                    if (input.Length > 0)
                                    {
                                        Console.WriteLine($"Are you sure you want the leave next description to be {leaveN}");
                                        Console.WriteLine("1) Yes 2) No");
                                        input = Console.ReadLine();
                                        if (input == "1")
                                        {
                                            leaveNFinished = false;
                                        }
                                    }
                                }
                                while (lengthFinished)
                                {
                                    Console.WriteLine("What would you like the length to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    try
                                    {
                                        length = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        Console.WriteLine($"Are you sure you want the length to be {length}?");
                                        Console.WriteLine("1) Yes 2) No");
                                        input = Console.ReadLine();
                                        if (input == "1")
                                        {
                                            lengthFinished = false;
                                        }
                                    }
                                }
                                while (prevLocfinished)
                                {
                                    Console.WriteLine("What would you like the previous location to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    try
                                    {
                                        prevLoc = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        Console.WriteLine($"Are you sure you want the previous location to be {prevLoc}?");
                                        Console.WriteLine("1) Yes 2) No");
                                        input = Console.ReadLine();
                                        if (input == "1")
                                        {
                                            prevLocfinished = false;
                                        }
                                    }
                                }
                                while (nextLocfinished)
                                {
                                    Console.WriteLine("What would you like the next location to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    try
                                    {
                                        nextLoc = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        Console.WriteLine($"Are you sure you want the next location to be {nextLoc}?");
                                        Console.WriteLine("1) Yes 2) No");
                                        input = Console.ReadLine();
                                        if (input == "1")
                                        {
                                            nextLocfinished = false;
                                        }
                                    }
                                }
                                Location location = new Location(name, arrive, leaveP, leaveN, length, prevLoc, nextLoc);
                                locations.Add(location);
                            }
                            else if (input == "2")
                            {
                                foreach (Location location in locations)
                                {
                                    if (location.NextLocation < location.Length)
                                    {
                                        Console.WriteLine($"Name: {location.Name}  Length: {location.Length} Previous Location: {locations[location.PrevLocation].Name} Next Location: {locations[location.NextLocation].Name}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Name: {location.Name}  Length: {location.Length} Previous Location: {locations[location.PrevLocation].Name} Next Location: NO NEXT LOCATION");
                                    }
                                }
                                Console.WriteLine("Are you sure you want to save these changes? It will overwrite original database?");
                                Console.WriteLine("Type YES to save or no to cancel");
                                input = Console.ReadLine();
                                if (input == "YES")
                                {
                                    saveLocations(locations);
                                }
                            }
                            else if (input == "3")
                            {
                                locations = ReadLocations();
                                changes = false;
                            }
                        }
                    }
                }
                else if (input == "5")
                {
                    List<Character> characters = new List<Character>();
                    using (var connection = new SqliteConnection("Data Source=adventure.db"))
                    {
                        connection.Open();
                        var command = connection.CreateCommand();
                        command.CommandText =
                           @"
                            SELECT * 
                            FROM character
                            ";
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Character character = new Character(reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), Weapons[reader.GetInt32(8)], Armors[reader.GetInt32(9)]);
                                characters.Add(character);
                            }
                            command.Dispose();
                        }
                        List<Character> deletes = new List<Character>();
                        for (int i = 0; i < characters.Count; i++)
                        {
                            Character character = characters[i];
                            Console.WriteLine($"Name: {character.Name} HP: {character.Hp} Str: {character.Str} SPEED: {character.Speed} CON: {character.Con} RES: {character.Res} Value: {character.Value} Weapon: {character.Weapon} Armor: {character.Armor}");
                            Console.WriteLine("Would you like to change this character? 1) Yes 2) No");
                            input = Console.ReadLine();
                            if (input == "1")
                            {
                                Console.WriteLine("What would you like to change?");
                                Console.WriteLine("1) Name 2) HP 3) STR 4) SPEED 5) CON 6) RES 7) Value 8) Weapon 9) Armor 10) Delete");
                                Console.WriteLine("Press any other button to cancel.");
                                input = Console.ReadLine();
                                if (input == "1")
                                {
                                    Console.WriteLine($"The current name is {character.Name}.");
                                    Console.WriteLine("What would you like the new name to be?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    if (input.Length > 0)
                                    {
                                        character.Name = input;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input: Moving Forward");
                                    }
                                }
                                else if (input == "2")
                                {
                                    Console.WriteLine($"The current HP is {character.Hp}.");
                                    Console.WriteLine("What would you like the new HP to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    int value = 0;
                                    try
                                    {
                                        value = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        character.Hp = value;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input: Moving Forward");
                                    }
                                }
                                else if (input == "3")
                                {
                                    Console.WriteLine($"The current STR is {character.Str}.");
                                    Console.WriteLine("What would you like the new STR to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    int value = 0;
                                    try
                                    {
                                        value = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        character.Str = value;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input: Moving Forward");
                                    }
                                }
                                else if (input == "4")
                                {
                                    Console.WriteLine($"The current SPEED is {character.Speed}.");
                                    Console.WriteLine("What would you like the new SPEED to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    int value = 0;
                                    try
                                    {
                                        value = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        character.Speed = value;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input: Moving Forward");
                                    }
                                }
                                else if (input == "5")
                                {
                                    Console.WriteLine($"The current CON is {character.Con}.");
                                    Console.WriteLine("What would you like the new CON to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    int value = 0;
                                    try
                                    {
                                        value = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        character.Con = value;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input: Moving Forward");
                                    }
                                }
                                else if (input == "6")
                                {
                                    Console.WriteLine($"The current RES is {character.Res}.");
                                    Console.WriteLine("What would you like the new RES to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    int value = 0;
                                    try
                                    {
                                        value = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        character.Res = value;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input: Moving Forward");
                                    }
                                }
                                else if (input == "7")
                                {
                                    Console.WriteLine($"The current Value is {character.Value}.");
                                    Console.WriteLine("What would you like the new Value to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    int value = 0;
                                    try
                                    {
                                        value = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        character.Value = value;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input: Moving Forward");
                                    }
                                }
                                else if (input == "8")
                                {
                                    Console.WriteLine($"The current Weapon is {character.Weapon}.");
                                    Console.WriteLine("What would you like the new Weapon to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    int value = 0;
                                    try
                                    {
                                        value = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        character.Weapon = Weapons[value];
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input: Moving Forward");
                                    }
                                }
                                else if (input == "9")
                                {
                                    Console.WriteLine($"The current Armor is {character.Armor}.");
                                    Console.WriteLine("What would you like the new Armor to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    int value = 0;
                                    try
                                    {
                                        value = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        character.Armor = Armors[value];
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input: Moving Forward");
                                    }
                                }
                                else if (input == "10")
                                {
                                    Console.WriteLine("*** WARNING YOU ARE ABOUT TO DELETE A REQUIRED ITEM ***");
                                    Console.WriteLine("Are you sure?");
                                    Console.WriteLine("YES) Yes 2) No");
                                    input = Console.ReadLine();
                                    if (input == "YES")
                                    {
                                        deletes.Add(character);
                                    }
                                }
                            }
                        }
                        foreach (Character delete in deletes)
                        {
                            characters.Remove(delete);
                        }
                        bool changes = true;
                        while (changes)
                        {
                            Console.Clear();
                            foreach (Character character in characters)
                            {
                                Console.WriteLine($"Name: {character.Name} HP: {character.Hp} Str: {character.Str} SPEED: {character.Speed} CON: {character.Con} RES: {character.Res} Value: {character.Value} Weapon: {character.Weapon} Armor: {character.Armor}");
                            }
                            Console.WriteLine("Finished character list, would you like to 1) Add Character 2) Save Changes 3) Cancel Changes");
                            input = Console.ReadLine();
                            if (input == "1")
                            {
                                bool nameFinished = true;
                                string name = " ";
                                bool hpFinished = true;
                                int hp = 0;
                                bool strFinished = true;
                                int str = 0;
                                bool speedFinished = true;
                                int speed = 0;
                                bool conFinished = true;
                                int con = 0;
                                bool resFinished = true;
                                int res = 0;
                                bool valueFinished = true;
                                int value = 0;
                                bool weaponFinished = true;
                                int weapon = 0;
                                bool armorFinished = true;
                                int armor = 0;
                                while (nameFinished)
                                {
                                    Console.WriteLine("What will the name of this potion be?");
                                    name = Console.ReadLine();
                                    if (input.Length > 0)
                                    {
                                        Console.WriteLine($"Are you sure you want the name to be {name}");
                                        Console.WriteLine("1) Yes 2) No");
                                        input = Console.ReadLine();
                                        if (input == "1")
                                        {
                                            nameFinished = false;
                                        }
                                    }
                                }
                                while (hpFinished)
                                {
                                    Console.WriteLine("What would you like the effect to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    try
                                    {
                                        hp = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        Console.WriteLine($"Are you sure you want the value to be {hp}?");
                                        Console.WriteLine("1) Yes 2) No");
                                        input = Console.ReadLine();
                                        if (input == "1")
                                        {
                                            hpFinished = false;
                                        }
                                    }
                                }
                                while (strFinished)
                                {
                                    Console.WriteLine("What would you like the value to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    try
                                    {
                                        str = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        Console.WriteLine($"Are you sure you want the value to be {str}?");
                                        Console.WriteLine("1) Yes 2) No");
                                        input = Console.ReadLine();
                                        if (input == "1")
                                        {
                                            strFinished = false;
                                        }
                                    }
                                }
                                while (speedFinished)
                                {
                                    Console.WriteLine("What would you like the value to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    try
                                    {
                                        speed = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        Console.WriteLine($"Are you sure you want the value to be {speed}?");
                                        Console.WriteLine("1) Yes 2) No");
                                        input = Console.ReadLine();
                                        if (input == "1")
                                        {
                                            speedFinished = false;
                                        }
                                    }
                                }
                                while (conFinished)
                                {
                                    Console.WriteLine("What would you like the value to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    try
                                    {
                                        con = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        Console.WriteLine($"Are you sure you want the value to be {con}?");
                                        Console.WriteLine("1) Yes 2) No");
                                        input = Console.ReadLine();
                                        if (input == "1")
                                        {
                                            conFinished = false;
                                        }
                                    }
                                }
                                while (resFinished)
                                {
                                    Console.WriteLine("What would you like the value to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    try
                                    {
                                        res = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        Console.WriteLine($"Are you sure you want the value to be {res}?");
                                        Console.WriteLine("1) Yes 2) No");
                                        input = Console.ReadLine();
                                        if (input == "1")
                                        {
                                            resFinished = false;
                                        }
                                    }
                                }
                                while (valueFinished)
                                {
                                    Console.WriteLine("What would you like the value to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    try
                                    {
                                        value = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        Console.WriteLine($"Are you sure you want the value to be {value}?");
                                        Console.WriteLine("1) Yes 2) No");
                                        input = Console.ReadLine();
                                        if (input == "1")
                                        {
                                            valueFinished = false;
                                        }
                                    }
                                }
                                while (weaponFinished)
                                {
                                    Console.WriteLine("What would you like the value to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    try
                                    {
                                        weapon = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        Console.WriteLine($"Are you sure you want the value to be {weapon}?");
                                        Console.WriteLine("1) Yes 2) No");
                                        input = Console.ReadLine();
                                        if (input == "1")
                                        {
                                            weaponFinished = false;
                                        }
                                    }
                                }
                                while (armorFinished)
                                {
                                    Console.WriteLine("What would you like the value to be?");
                                    Console.WriteLine("It MUST be a number?");
                                    input = Console.ReadLine();
                                    bool error = false;
                                    try
                                    {
                                        armor = Convert.ToInt32(input);
                                    }
                                    catch
                                    {
                                        error = true;
                                    }
                                    if (!error)
                                    {
                                        Console.WriteLine($"Are you sure you want the value to be {armor}?");
                                        Console.WriteLine("1) Yes 2) No");
                                        input = Console.ReadLine();
                                        if (input == "1")
                                        {
                                            armorFinished = false;
                                        }
                                    }
                                }
                                Character character = new Character(name, hp, str, speed, con, res ,value , Weapons[weapon], Armors[armor]);
                                characters.Add(character);
                            }
                            else if (input == "2")
                            {
                                foreach (Character character in characters)
                                {
                                    Console.WriteLine($"Name: {character.Name} HP: {character.Hp} Str: {character.Str} SPEED: {character.Speed} CON: {character.Con} RES: {character.Res} Value: {character.Value} Weapon: {character.Weapon} Armor: {character.Armor}");
                                }
                                Console.WriteLine("Are you sure you want to save these changes? It will overwrite original database?");
                                Console.WriteLine("Type YES to save or no to cancel");
                                saveCharacters(characters, Weapons, Armors);
                            }
                            else if (input == "3")
                            {
                                characters = ReadMonsters(Weapons, Armors, Locations);
                                changes = false;
                            }
                        }
                    }
                }
                else if (input == "6")
                {
                    savePlayer(PlayerCharacter, Weapons, Armors);
                }
                else
                {
                    Console.WriteLine("Invalide Options: Try Again");
                    Console.ReadLine();
                }
            }
        }
        public DB()
        {

        }
    }
}