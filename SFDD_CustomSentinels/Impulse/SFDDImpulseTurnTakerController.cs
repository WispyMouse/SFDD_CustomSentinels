using System;
using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;
using System.Collections;

namespace SFDD_CustomSentinels.SFDDImpulse
{
    public class SFDDImpulseTurnTakerController : HeroTurnTakerController
    {
        public SFDDImpulseTurnTakerController(TurnTaker turnTaker, GameController gameController)
            : base(turnTaker, gameController)
        {
        }
    }
}
