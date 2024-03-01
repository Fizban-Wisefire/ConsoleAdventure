

namespace ConsoleAdventure
{

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

}
