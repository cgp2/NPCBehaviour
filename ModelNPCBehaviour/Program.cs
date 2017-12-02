using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace ModelNPCBehaviour
{
    class Program
    {




        static void Main(string[] args)
        {
            ModulationCore mc = new ModulationCore();

            mc.AddCharacter("Anna");


            mc.AddLocation("25/17", LocationType.Tavern, 1);
            mc.AddLocation("Anna's home", LocationType.Home, 0);
            (mc.Locations.Last() as Locations.Home).Owner = mc.Characters[0].id;
            mc.AddLocation("Robert's home", LocationType.Home, 2);


            mc.AddCharacter("Robert");
            (mc.Locations.Last() as Locations.Home).Owner = mc.Characters[1].id;
            mc.Characters[1].Needs.Tiredness = 9;
            mc.Characters[1].Needs.Sorrow = 6;

            while (true) { }
        }
    }
}
