using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelNPCBehaviour.Locations
{
    class Home : ALocation
    {
        public Guid Owner;
        private bool isBedOccupied = false;

        public Home(string name, int locationSlot)
        {
            Name = name;
            Characters = new List<APerson>();
            type = LocationType.Home;
            Position = locationSlot;

            SatisfiedPurpose = new Dictionary<PurposeType, double>();
            SatisfiedPurpose.Add(PurposeType.Sleep, -0.4);
            SatisfiedPurpose.Add(PurposeType.Eat, -0.5);
            SatisfiedPurpose.Add(PurposeType.Drink, -0.7);
        }

        public override void CharacterEnter(APerson ch)
        {
            Console.WriteLine("{0} entered home", ch.Name);
            ch.CurrentRoleId = 1;
            Characters.Add(ch);

            if (ch.CurrentPurpose != PurposeType.Nothing)
                ActionRequest(ch);
        }

        public override void CharacterLeft(APerson ch)
        {
            Console.WriteLine("{0} left home", ch.Name);
            Characters.RemoveAt(Characters.FindIndex(c => c.id == ch.id));
            ch.CurrentRoleId = 0;
        }

        public override void ActionRequest(APerson ch)
        {
            if (Characters.IndexOf(ch) != -1)
            {
                if (ch.id == Owner)
                {
                    if (ch.IsBusy == false)
                    {
                        switch(ch.CurrentPurpose)
                        {
                            case PurposeType.Sleep:
                                ch.CurrentAction = new System.Threading.Thread(() => Sleep(ch));
                                ch.CurrentAction.Start();
                                break;
                            case PurposeType.Drink:
                                ch.CurrentAction = new System.Threading.Thread(() => Drink(ch));
                                ch.CurrentAction.Start();
                                break;
                            case PurposeType.Eat:
                                ch.CurrentAction = new System.Threading.Thread(() => Eat(ch));
                                ch.CurrentAction.Start();
                                break;
                        }
                    }
                }
            }
        }

        private void Sleep(APerson ch)
        {
            double initialRate = ch.TirednessRate;
            ch.TirednessRate = SatisfiedPurpose[PurposeType.Sleep];
            ch.IsBusy = true;
            isBedOccupied = true;

            Console.WriteLine("{0} is sleeping in {1}", ch.Name, Name);

            while (ch.Needs.Tiredness > 4.0)
            {

            }

            ch.TirednessRate = initialRate;
            ch.IsBusy = false;
            ch.CurrentPurpose = PurposeType.Nothing;
            isBedOccupied = false;
        }


        private void Eat(APerson ch)
        {
            double initialRate = ch.HungerRate;
            double initialHunger = ch.Needs.Hunger;
            ch.HungerRate= SatisfiedPurpose[PurposeType.Eat];
            ch.IsBusy = true;

            Console.WriteLine("{0} is eating in {1}", ch.Name, Name);

            while (initialHunger - ch.Needs.Hunger < 3.0)
            {

            }

            ch.HungerRate = initialRate;
            ch.IsBusy = false;
            ch.CurrentPurpose = PurposeType.Nothing;
        }

        private void Drink(APerson ch)
        {
            double initialRate = ch.ThirstRate;
            double initialThirst = ch.Needs.Thirst;
            ch.ThirstRate = SatisfiedPurpose[PurposeType.Drink];
            ch.IsBusy = true;

            Console.WriteLine("{0} is drinking in {1}", ch.Name, Name);

            while (initialThirst- ch.Needs.Thirst < 3.0)
            {

            }

            ch.ThirstRate = initialRate;
            ch.IsBusy = false;
            ch.CurrentPurpose = PurposeType.Nothing;
        }
    }
}
