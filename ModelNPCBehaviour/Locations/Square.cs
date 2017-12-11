using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelNPCBehaviour.Locations
{
    class Square : ALocation
    {
        public Square(string name, int locationSlot)
        {
            Name = name;
            type = LocationType.Square;
            Characters = new List<APerson>();
            Position = locationSlot;
            SatisfiedPurpose = new Dictionary<PurposeType, int>();
        }


        public override void CharacterEnter(APerson ch)
        {
            Console.WriteLine("{0} visite {1}", ch.Name, Name);
            ch.CurrentRoleId = 1;
            Characters.Add(ch);
        }

        public override void CharacterLeft(APerson ch)
        {
            Console.WriteLine("{0} left {1}", ch.Name, Name);
            Characters.RemoveAt(Characters.FindIndex(c => c.id == ch.id));
            ch.CurrentRoleId = 0;
        }


        public override void ActionRequest(APerson ch)
        {
            if (Characters.IndexOf(ch) != -1)
            {
                if (ch.CurrentAction == null)
                {
                    switch (ch.CurrentPurpose)
                    {

                    }
                }

            }
        }
    }
}
