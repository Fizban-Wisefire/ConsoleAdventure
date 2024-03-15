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
        public List<Item> ReadWeapons(List<Item> weapons)
        {
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
        public List<Item> ReadArmors(List<Item> armors)
        {
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
        public List<Item> ReadPotions(List<Item> potions)
        {
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

        // Creates Armors List
        public List<Character> ReadMonsters(List<Character> monsters, List<Item> Weapons, List<Item> Armors)
        {
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
                        monsters.Add(new Character(reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), Weapons[0], Armors[0]));
                    }
                }
                monsterCommand.Dispose();
                connection.Dispose();
                return monsters;
            }
        }

        public DB()
        {

        }
    }
}