using NUnit.Framework;
using System;
using Workshopping;
using Workshopping.MigrantCoder;
using Handelabra.Sentinels.Engine.Model;
using Handelabra.Sentinels.Engine.Controller;
using System.Linq;
using System.Collections;
using Handelabra.Sentinels.UnitTest;
using Workshopping.TheBaddies;
using System.Collections.Generic;
namespace MyModTest
{
    [TestFixture()]
    public class ImpulseTest : BaseTest
    {
        protected HeroTurnTakerController impulse { get { return FindHero("SFDDImpulse"); } }

        [Test()]
        public void CanInitialize()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            StartGame();

            GoToPlayCardPhase(impulse);
        }
    }
}
