﻿using System;
using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;
using System.Collections;

namespace SFDD_CustomSentinels.SFDDImpulse
{
    public class SFDDSonicSootheCardController : CardController
    {
        public SFDDSonicSootheCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override IEnumerator Play()
        {
            return this.GameController.SendMessageAction("You knew this card does nothing. Why would you play it?", Priority.Medium, GetCardSource());
        }
    }
}
