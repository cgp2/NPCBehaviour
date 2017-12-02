using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ModelNPCBehaviour
{
    public abstract class APerson
    {
        public PurposeType CurrentPurpose;
        public Thread CurrentAction;
        public System.Timers.Timer PurposeChooseTimer;

        public Guid id;
        public HumanNeeds Needs;
        public string Name;
        public int CurrentRoleId = 0;
        public int CurrentPosition = 0;
        public bool IsBusy = false;


        public double HungerRate = 0.2;
        public double ThirstRate = 0.2;
        public double SorrowRate = 0.2;
        public double TirednessRate = 0.2;
        public double LonelinessRate = 0.2;

        public List<Locations.ALocation> KnownLocations; 

        public List<PersonRelationships> Relationshipsh;

        public event CharacterChangeLocationEventHandler ChangeLocationEventHandler;

        protected virtual void OnChangeLocation(CharacterChangeLocationEventArgs e)
        {
            ChangeLocationEventHandler?.Invoke(e);
        }

        public void MoveTo(int destinationPosition)
        {
            CharacterChangeLocationEventArgs e = new CharacterChangeLocationEventArgs();
            e.DestinationTarget = destinationPosition;
            e.PreviousLocation = CurrentPosition;
            e.characterId = id;
            OnChangeLocation(e);
        }

        public abstract void TimeTick();

      
    }
}
