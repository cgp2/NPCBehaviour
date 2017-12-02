using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ModelNPCBehaviour.Locations
{
    public abstract class ALocation
    {
        public string Name;
        public LocationType type;
        public Dictionary<PurposeType, double> SatisfiedPurpose;
        public int Position;
        public List<APerson> Characters;
        //protected Thread thWeiter = new Thread(CharacterWeiter);

        public abstract void CharacterEnter(APerson ch);
        public abstract void CharacterLeft(APerson ch);

        public abstract void ActionRequest(APerson ch);
    }
}
