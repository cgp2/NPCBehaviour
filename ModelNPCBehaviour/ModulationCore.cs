using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace ModelNPCBehaviour
{
    public enum LocationType {Square, Home, Tavern };
    public enum PurposeType { Eat, Sleep, HaveFun, Drink, Chat, Nothing};

    public class CharacterChangeLocationEventArgs : EventArgs
    {
        public int PreviousLocation;
        public int DestinationTarget;
        public Guid characterId;
    }
    public delegate void CharacterChangeLocationEventHandler(CharacterChangeLocationEventArgs e);

    class ModulationCore
    {
        private System.Timers.Timer needsDegedrationTimer;

        public List<APerson> Characters;
        public List<Locations.ALocation> Locations;

        public ModulationCore()
        {
            if (needsDegedrationTimer == null)
            {
                needsDegedrationTimer = new System.Timers.Timer();
                needsDegedrationTimer.AutoReset = true;
                needsDegedrationTimer.Interval =2000;
                needsDegedrationTimer.Elapsed += TimeTick;
                needsDegedrationTimer.Enabled = true;
                needsDegedrationTimer.Start();
            }

            Characters = new List<APerson>();
            Locations = new List<Locations.ALocation>();

            AddLocation("Town Square", LocationType.Square, 4);
        }

        void CharacterChangedLocation(CharacterChangeLocationEventArgs e)
        {
            APerson ch = Characters.Find(c => c.id == e.characterId);
            var destination = Locations.Find(l => l.Position == e.DestinationTarget);
            var startLocation = Locations.Find(l => l.Position == e.PreviousLocation);

            try
            {
                startLocation.CharacterLeft(ch);
            }
            catch
            {
                Stopwatch stp = new Stopwatch();
                stp.Start();

                while(stp.ElapsedMilliseconds < 1000)
                {
                }
                stp.Stop();
                CharacterChangedLocation(e);
            }

            destination.CharacterEnter(ch);

            ch.CurrentPosition = e.DestinationTarget;

            Console.WriteLine("{0} has left {1} and entered {2}", ch.Name, startLocation.Name, destination.Name);
            }

        public void AddCharacter(string name, int startLocation = 4)
        {
            var loc = Locations.Find(l => l.Position == startLocation);
            if (loc != null)
            {
                var ch = new Character(Guid.NewGuid(), name, startLocation);
                ch.ChangeLocationEventHandler += CharacterChangedLocation;
                ch.KnownLocations = Locations;
                Characters.Add(ch);
                loc.CharacterEnter(ch);
            }
            else
            {
                Console.WriteLine("There is no such location!");
            }
        }

        public void AddLocation(string name, LocationType type, int position, APerson owner = null)
        {
            if (Locations.FindIndex(l => l.Position == position) == -1)
            {
                switch (type)
                {
                    case LocationType.Home:
                        Locations.Add(new Locations.Home(name, position));
                        break;
                    case LocationType.Tavern:
                        Locations.Add(new Locations.Tavern(name, position));
                        break;
                    case LocationType.Square:
                        Locations.Add(new Locations.Square(name, position));
                        break;
                }

                foreach(var ch in Characters)            
                    ch.KnownLocations.Add(Locations.Last());
                
            }
            else
                Console.WriteLine("This position is already occypied!");
        }

        private void TimeTick(object source, EventArgs e)
        {
            try
            {
                foreach (var character in Characters)
                {
                    character.TimeTick();
                    Console.WriteLine(GetCharacterStats(character.Name));
                }
            }
            catch { }
        }

        public string GetCharacterStats(string name)
        {
            var character = Characters.FindLast(c => c.Name == name);
            string ret = character.Name + "'s stats: \n Hunger : " + character.Needs.Hunger + 
                                                    "\n Loneliness : " + character.Needs.Loneliness + 
                                                    "\n Sorrow : " + character.Needs.Sorrow + 
                                                    "\n Thirst : " + character.Needs.Thirst + 
                                                    "\n Tiredness : " + character.Needs.Tiredness;

            return ret;
        }
    }



}
