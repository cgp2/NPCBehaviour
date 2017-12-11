using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ModelNPCBehaviour
{
    public class Character : APerson
    {
        public Character(Guid guid, string name, int initialLocation = 0)
        {
            id = guid;
            Name = name;

            Relationshipsh = new List<PersonRelationships>();
            Needs = new HumanNeeds();
            CurrentPosition = initialLocation;

            if (PurposeChooseTimer == null)
            {
                PurposeChooseTimer = new System.Timers.Timer();
                PurposeChooseTimer.AutoReset = true;
                PurposeChooseTimer.Interval = 5000;
                PurposeChooseTimer.Elapsed += PurposeSelection;
                PurposeChooseTimer.Enabled = true;
                PurposeChooseTimer.Start();
            }
        }

        public override void TimeTick()
        {
            Needs.Hunger += HungerRate;
            Needs.Loneliness += LonelinessRate;
            Needs.Sorrow += SorrowRate;
            Needs.Thirst += ThirstRate;
            Needs.Tiredness += TirednessRate;
        }

        private void PurposeSelection(object source, EventArgs e)
        {
            if (IsBusy == false)
            {
                List<double> Ut = new List<double>();
                Ut.Add(HungerUtility());
                Ut.Add(ThirstUtility());
                Ut.Add(SorrowUtility());
                Ut.Add(ThirednessUtility());

                var maxind = Ut.IndexOf(Ut.Max());
                switch (maxind)
                {
                    case 1:
                        CurrentPurpose = PurposeType.Eat;
                        break;
                    case 2:
                        CurrentPurpose = PurposeType.Drink;
                        break;
                    case 3:
                        CurrentPurpose = PurposeType.HaveFun;
                        break;
                    case 4:
                        CurrentPurpose = PurposeType.Sleep;
                        break;
                }
                //if (Needs.Hunger > 7)
                //{
                //    CurrentPurpose = PurposeType.Eat;
                //}
                //else if (Needs.Thirst > 6)
                //{
                //    CurrentPurpose = PurposeType.Drink;
                //}
                //else if (Needs.Tiredness > 8)
                //{
                //    CurrentPurpose = PurposeType.Sleep;
                //}
                //else if (Needs.Sorrow > 7)
                //{
                //    CurrentPurpose = PurposeType.HaveFun;
                //}
                //else if (Needs.Loneliness > 7)
                //{
                //    CurrentPurpose = PurposeType.Chat;
                //}
                //else
                //{
                //if (Needs.Sorrow > Needs.Loneliness)
                //    CurrentPurpose = PurposeType.HaveFun;
                //else
                //    CurrentPurpose = PurposeType.Chat;
                //}

                var opt = ChoseOptimalLocation();
                if (opt.Position != CurrentPosition)
                    MoveTo(opt.Position);
                else
                    opt.ActionRequest(this);
                
            }
        }

        private Locations.ALocation ChoseOptimalLocation()
        {
            Locations.ALocation optimalLocation = null;

            if (CurrentPurpose != PurposeType.Sleep)
            {
                double maxWeight = -1;
                foreach (var loc in KnownLocations)
                {
                    int weight = 0;
                    if (loc.SatisfiedPurpose.TryGetValue(CurrentPurpose, out weight))
                    {
                        if (CurrentPosition != loc.Position)
                        {
                            weight = Math.Abs(weight) * (1 / Math.Abs(CurrentPosition - loc.Position));
                            if (weight > maxWeight)
                            {
                                maxWeight = weight;
                                optimalLocation = loc;
                            }
                        }
                        else
                        {
                            optimalLocation = loc;
                            break;
                        }
                    }
                }
            }
            else
            {
                foreach(var loc in KnownLocations)
                {
                    try
                    {
                        if ((loc as Locations.Home).Owner == id)
                        {
                            optimalLocation = loc;
                            break;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

            return optimalLocation;
        }

        public override double HungerUtility()
        {
            return Needs.Hunger * Needs.Hunger / 12.0 + 0.2;
        }

        public override double ThirstUtility()
        {
            return (1.0 / (-Needs.Thirst + 10.0));
        }

        public override double SorrowUtility()
        {
            return (Needs.Sorrow / 2 + 1) * 0.7;
        }

        public override double ThirednessUtility()
        {
            return (Needs.Tiredness - 10.0) * Needs.Tiredness + 2.0;
        }
    }
}
