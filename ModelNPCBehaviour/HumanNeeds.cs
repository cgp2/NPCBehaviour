using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelNPCBehaviour
{
    public struct HumanNeeds
    {

        private double _hunger;
        private double _thirst;
        private double _sorrow;
        private double _tiredness;
        private double _loneliness;

        public double Hunger
        {   
            get { return _hunger; }
            set
            {
                if (value >= 0 && value <= 10)
                    _hunger = value;
            }
        }
        public double Thirst
        {
            get { return _thirst; }
            set
            {
                if (value >= 0 && value <= 10)
                    _thirst = value;
            }
        }
        public double Sorrow
        {
            get { return _sorrow; }
            set
            {
                if (value >= 0 && value <= 10)
                    _sorrow = value;
            }
        }
        public double Tiredness
        {
            get { return _tiredness; }
            set
            {
                if (value >= 0 && value <= 10)
                    _tiredness = value;
            }
        }
        public double Loneliness
        {
            get { return _loneliness; }
            set
            {
                if (value >= 0 && value <= 10)
                    _loneliness = value;
            }
        }

        public HumanNeeds(double hunger = 0, double thirst = 0, double sorrow = 0, double tiredness = 0, double lonelyness = 0)
        {
            _hunger = hunger;
            _thirst = thirst;
            _sorrow = sorrow;
            _tiredness = tiredness;
            _loneliness = lonelyness;
        }
    }
}
