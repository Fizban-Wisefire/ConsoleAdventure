using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.Entities
{
    public class Location
    {
        public string Name { get; set; }
        public string ArriveDescription { get; set; }
        public string LeavingPrevDescription { get; set; }
        public string LeavingNextDescription { get; set; }
        public string DeeperDescription { get; set; } 
        public int PrevLocation { get; set; }
        public int NextLocation { get; set; }
        public int Length { get; set; }

        public Location(string name, string arriveDescription, string leavingNextDescription, string leavingPrevDescription, int length, int prevLocation, int nextLocation)
        {
            Name = name;
            ArriveDescription = arriveDescription;
            LeavingPrevDescription = leavingPrevDescription;
            LeavingNextDescription = leavingNextDescription;
            Length = length;
            PrevLocation = prevLocation;
            NextLocation = nextLocation;
        }

        public void Continue(Player PC, List<Location> locations)
        {
            if (PC.Depth < Length)
            {
                if (PC.Depth +1 >= Length)
                {
                    Console.WriteLine($"You continue deeper into the {Name}. {LeavingNextDescription}");
                }   else
                {
                    Console.WriteLine($"You continue deeper into the {Name}. {DeeperDescription}");
                }
                PC.Depth += 1;
            } else
            {
                Location Next = locations[PC.CurrentLocation.NextLocation];
                Console.WriteLine($"You come to the end of {Name} and see the enviroment change to {NextLocation}. Are you ready to continue? 1)Yes 2)No");
                string respone = Console.ReadLine();
                if (respone == "1")
                {
                    Next.Arrive(PC, Next);
                }
            }
        }

        public void Return(Player PC, List<Location> locations)
        {
            Location Prev = locations[PC.CurrentLocation.PrevLocation];
            Console.WriteLine(LeavingPrevDescription);
            Prev.Arrive(PC, Prev);
        }

        public void Arrive(Player PlayerCharacter, Location Destination)
        {
            PlayerCharacter.CurrentLocation = Destination;
            Console.WriteLine($"You have arrived at the {Name}");
            Console.WriteLine(ArriveDescription);
        }
    }
}