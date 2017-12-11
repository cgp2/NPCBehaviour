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

            SatisfiedPurpose = new Dictionary<PurposeType, int>();
            SatisfiedPurpose.Add(PurposeType.Eat, -40);
            SatisfiedPurpose.Add(PurposeType.Drink, -50);
            SatisfiedPurpose.Add(PurposeType.HaveFun, -30);
            SatisfiedPurpose.Add(PurposeType.Chat, -25);
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
            int initialSorrow = ch.Needs.Sorrow;
            int initialRate = ch.SorrowRate;
            ch.SorrowRate = SatisfiedPurpose[PurposeType.HaveFun];
            ch.IsBusy = true;

            Console.WriteLine("{0} is dancing in {1}", ch.Name, Name);

            while (initialSorrow - ch.Needs.Sorrow < 50)
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
           int initialLonelyness = ch.Needs.Loneliness;
           int initialRate = ch.LonelinessRate;
            ch.LonelinessRate = SatisfiedPurpose[PurposeType.Chat];
            ch.IsBusy = true;
            isBarmenBusy = true;

            Console.WriteLine("{0} is chating in {1}", ch.Name, Name);

            while (initialLonelyness - ch.Needs.Loneliness < 60)
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
            int initialHunger = ch.Needs.Hunger;
            int initialRate = ch.HungerRate;
            ch.HungerRate = SatisfiedPurpose[PurposeType.Eat];
            ch.IsBusy = true;

            Console.WriteLine("{0} is eating in {1}", ch.Name, Name);

            while (initialHunger -ch.Needs.Hunger < 60)
            {

            }


            ch.HungerRate = initialRate;
            ch.IsBusy = false;
        }

        private void Drink(APerson ch)
        {
            int initialThirst = ch.Needs.Thirst;
            int initialRate = ch.ThirstRate;
            ch.ThirstRate = SatisfiedPurpose[PurposeType.Drink];
            ch.IsBusy = true;

            Console.WriteLine("{0} is drinking in {1}", ch.Name, Name);

            while (initialThirst - ch.Needs.Thirst < 70)
            {

            }

            ch.ThirstRate = initialRate;
            ch.IsBusy = false;
            ch.CurrentPurpose = PurposeType.Nothing;
        }
    }
}
