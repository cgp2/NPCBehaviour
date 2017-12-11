using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelNPCBehaviour
{
    public struct HumanNeeds
    {

        private int _hunger;
        private int _thirst;
        private int _sorrow;
        private int _tiredness;
        private int _loneliness;

        public int Hunger
        {   
            get { return _hunger; }
            set
            {
                if (value >= 0 && value <= 100)
                    _hunger = value;
            }
        }
        public int Thirst
        {
            get { return _thirst; }
            set
            {
                if (value >= 0 && value <= 100)
                    _thirst = value;
            }
        }
        public int Sorrow
        {
            get { return _sorrow; }
            set
            {
                if (value >= 0 && value <= 100)
                    _sorrow = value;
            }
        }
        public int Tiredness
        {
            get { return _tiredness; }
            set
            {
                if (value >= 0 && value <= 100)
                    _tiredness = value;
            }
        }
        public int Loneliness
        {
            get { return _loneliness; }
            set
            {
                if (value >= 0 && value <= 100)
                    _loneliness = value;
            }
        }

        public HumanNeeds(int hunger = 0, int thirst = 0, int sorrow = 0, int tiredness = 0, int lonelyness = 0)
        {
            _hunger = hunger;
            _thirst = thirst;
            _sorrow = sorrow;
            _tiredness = tiredness;
            _loneliness = lonelyness;
        }
    }
}
