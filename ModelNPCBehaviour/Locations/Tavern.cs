using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelNPCBehaviour.Locations
{
    class Tavern : ALocation
    {
        public int ConsumersIn = 0;
        private bool isBarmenBusy = false;

        public Tavern(string name, int locationSlot)
        {
            Name = name;
            type = LocationType.Tavern;
            Characters = new List<APerson>();
            Position = locationSlot;

            SatisfiedPurpose = new Dictionary<PurposeType, double>();
            SatisfiedPurpose.Add(PurposeType.Eat, -0.7);
            SatisfiedPurpose.Add(PurposeType.Drink, -1);
            SatisfiedPurpose.Add(PurposeType.HaveFun, -0.4);
            SatisfiedPurpose.Add(PurposeType.Chat, -0.2);
        }

        public override void CharacterEnter(APerson ch)
        {
            Console.WriteLine("{0} entered tavern", ch.Name);
            ch.CurrentRoleId = 1;
            Characters.Add(ch);

            if (ch.CurrentPurpose != PurposeType.Nothing)
                ActionRequest(ch);
        }

        public override void CharacterLeft(APerson ch)
        {
            Console.WriteLine("{0} left tavern", ch.Name);
            int a = Characters.FindIndex(c => c.id == ch.id);
            Characters.RemoveAt(Characters.FindIndex(c => c.id == ch.id));
            ch.CurrentRoleId = 0;
        }


        public override void ActionRequest(APerson ch)
        {
            if (Characters.IndexOf(ch) != -1)
            {
                if (ch.IsBusy == false)
                {
                    switch (ch.CurrentPurpose)
                    {
                        case PurposeType.Drink:
                            ch.CurrentAction = new System.Threading.Thread(() => Drink(ch));
                            ch.CurrentAction.Start();
                            break;
                        case PurposeType.Eat:
                            ch.CurrentAction = new System.Threading.Thread(() => Eat(ch));
                            ch.CurrentAction.Start();
                            break;
                        case PurposeType.Chat:
                            if(isBarmenBusy == false)
                            {
                                ch.CurrentAction = new System.Threading.Thread(() => Chat(ch));
                                ch.CurrentAction.Start();
                            }
                            break;
                        case PurposeType.HaveFun:
                            ch.CurrentAction = new System.Threading.Thread(() => Dance(ch));
                            ch.CurrentAction.Start();
                            break;
                    }
                }

            }
        }


        private void Dance(APerson ch)
        {
            double initialSorrow = ch.Needs.Sorrow;
            double initialRate = ch.SorrowRate;
            ch.SorrowRate = SatisfiedPurpose[PurposeType.HaveFun];
            ch.IsBusy = true;

            Console.WriteLine("{0} is dancing in {1}", ch.Name, Name);

            while (initialSorrow - ch.Needs.Sorrow < 3.0)
            {
                if (ch.Needs.Sorrow < Math.Abs(ch.SorrowRate))
                    break;
            }

            ch.SorrowRate = initialRate;
            ch.IsBusy = false;
            ch.CurrentPurpose = PurposeType.Nothing;
        }

        private void Chat(APerson ch)
        {
            double initialLonelyness = ch.Needs.Loneliness;
            double initialRate = ch.LonelinessRate;
            ch.LonelinessRate = SatisfiedPurpose[PurposeType.Chat];
            ch.IsBusy = true;
            isBarmenBusy = true;

            Console.WriteLine("{0} is chating in {1}", ch.Name, Name);

            while (initialLonelyness - ch.Needs.Loneliness < 2.0)
            {
                if (ch.Needs.Loneliness < Math.Abs(ch.LonelinessRate))
                    break;
            }

            isBarmenBusy = false;
            ch.IsBusy = false;
            ch.LonelinessRate = initialRate;
            ch.CurrentPurpose = PurposeType.Nothing;
        }

        private void Eat(APerson ch)
        {
            double initialHunger = ch.Needs.Hunger;
            double initialRate = ch.HungerRate;
            ch.HungerRate = SatisfiedPurpose[PurposeType.Eat];
            ch.IsBusy = true;

            Console.WriteLine("{0} is eating in {1}", ch.Name, Name);

            while (initialHunger -ch.Needs.Hunger < 3.0)
            {

            }


            ch.HungerRate = initialRate;
            ch.IsBusy = false;
        }

        private void Drink(APerson ch)
        {
            double initialThirst = ch.Needs.Thirst;
            double initialRate = ch.ThirstRate;
            ch.ThirstRate = SatisfiedPurpose[PurposeType.Drink];
            ch.IsBusy = true;

            Console.WriteLine("{0} is drinking in {1}", ch.Name, Name);

            while (initialThirst - ch.Needs.Thirst < 3.0)
            {

            }

            ch.ThirstRate = initialRate;
            ch.IsBusy = false;
            ch.CurrentPurpose = PurposeType.Nothing;
        }
    }
}
