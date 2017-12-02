using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelNPCBehaviour
{
    public struct PersonRelationships
    {
        public string OwnerName;
        public string TargetName;
        public int Strenght;

        public PersonRelationships(string ownerName, string targetName, int strenght)
        {
            OwnerName = ownerName;
            TargetName = targetName;
            Strenght = strenght;
        }
    }
}
