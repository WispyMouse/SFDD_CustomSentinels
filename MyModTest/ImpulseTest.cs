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

        [Test()]
        public void RemixAbility_NoTrackInPlay()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            // Stack Impulse's deck so that there's a track in hand
            StartGame();

            GoToUsePowerPhase(impulse);

            AssertNumberOfUsablePowers(impulse, 1);
            UsePower(impulse);

            // Choose a track card to play in hand
            // That card enters play
            // Choose a track in play
            // Return that track to hand
            // That card is not in play
            // That card is in your hand
        }

        [Test()]
        public void RemixAbility_TrackInPlay()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            // Stack Impulse's deck so that there's a track in hand
            StartGame();

            // Play a track
            GoToPlayCardPhaseAndPlayCard(impulse, "");

            GoToUsePowerPhase(impulse);

            AssertNumberOfUsablePowers(impulse, 1);
            UsePower(impulse);

            // Choose a track card to play in hand
            // That card enters play
            // Choose a track in play
            // Return that track to hand
            // That card is not in play
            // That card is in your hand
            // The track you didn't choose is still in play
        }

        [Test()]
        public void SerratedVinylSlash_NoKill()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            StartGame();

            // Create a villain target with >3 health

            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDSerratedVinylSlash");

            // Select that villain target
            // Observe they were damaged and that the next phase has come
        }

        [Test()]
        public void SerratedVinylSlash_KillOne()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            StartGame();

            // Create a villain target with <=3 health
            // Create a villain target with > 2 health

            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDSerratedVinylSlash");

            // Select the low health target
            // Observe its destruction
            // Select the second target
            // Observe it takes damage
        }

        [Test()]
        public void SerratedVinylSlash_KillBoth()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            StartGame();

            // Create a villain target with <=3 health
            // Create a villain target with <= 2 health

            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDSerratedVinylSlash");

            // Select the low health target
            // Observe its destruction
            // Select the second target
            // Observe its destruction
        }

        [Test()]
        public void SerratedVinylSlash_EnvironmentMixup()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            StartGame();

            // Create an environment target with <=3 health
            // Create a villain target with <= 2 health

            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDSerratedVinylSlash");

            // Select the low health target
            // Observe its destruction
            // Select the second target
            // Observe its destruction
        }

        [Test()]
        public void LightSmokeAndMirrors_NoEnvironment()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            StartGame();

            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDLightSmokeAndMirrors");

            // Select the villain
            // Observe that this does no damage
        }

        [Test()]
        public void LightSmokeAndMirrors_OneEnvironment()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            StartGame();

            // Create an environment target

            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDLightSmokeAndMirrors");

            // Select the villain
            // Observe that this does one damage
        }

        [Test()]
        public void LightSmokeAndMirrors_RandomEnvironment()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            // Create a random number of environment targets

            StartGame();

            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDLightSmokeAndMirrors");

            // Select the villain
            // Observe that this does the right damage damage
        }

        [Test()]
        public void RecordScratch_NoTracks()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            StartGame();

            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDRecordScratch");

            // Observe that no targets can be selected
        }

        [Test()]
        public void RecordScratch_RandomTracks_OneTarget()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            // Stack the deck to put a random number of tracks in hand

            StartGame();

            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDRecordScratch");

            // Observe only one target can be selected
            // Observe damage is done
        }

        [Test()]
        public void RecordScratch_RandomTracks_EnoughTargets()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            // Stack the deck to put a random number of tracks in hand
            // Create dozens of villain targets

            StartGame();

            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDRecordScratch");

            // Observe the correct number of targets selected
            // Observe damage is done
        }

        [Test()]
        public void Breakdown_TargetDealsLessDamage()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            StartGame();

            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDBreakdown");

            // Go to that target's turn
            // Have them try to deal damage
            // Observe the damage is reduced
        }

        [Test()]
        public void DowntempoShift_Works()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            StartGame();

            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDDowntempoShift");

            // Target a player
            // They draw 2, discard 1
        }

        [Test()]
        public void AccousticDeadzone_ExemptHeroTarget()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            StartGame();

            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDAccousticDeadzone");

            // Attach to a target
            // Have a villain try to damage them in an AoE
            // Observe exemption
        }

        [Test()]
        public void AccousticDeadzone_NotExemptHeroTarget()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            StartGame();

            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDAccousticDeadzone");

            // Attach to a target
            // Have a villain try to damage them with single target
            // Observe that they are not exempt
        }

        /*Phase Modulator
Equipment - Limited
At the start of the villain turn, choose a villain target with 4 HP or less to skip their turn.

Triple Deck Turntable
Equipment
Power: Play 2 track cards, then remove all track cards from play.

Super Max Hi-Fi
Equipment - Limited
When Impulse deals Sonic damage, deal 1 additional target X sonic damage, where x is the number of track cards in your trash.
Sonic Soothe
Ongoing - Track
At the start of your turn, target hero heals 2 HP. If Impulse would take damage, discard this card instead.

Dancefloor Dirge
Ongoing - Track
When this track enters play, draw a card. If that card is a one-shot, you may play it.
At the start of your turn, you may deal 1 target 2 melee damage.

Limbic Overdrive
Ongoing - Track
The first time a hero takes damage since the start of your turn, they may play a card. When this card leaves play,e ach hero may draw a card.

Buld Up to the Drop
Ongoing - Track - Limited
At the start of your turn, add 1 build up counter to this track. Whis card leaves play,l deal up to 3 targets X sonic damage, where X is the number of build up counters on this track.

200 Beatdowns Per Minute
Ongoing - Track
When this track enters play, Impulse deals each non-hero target 2 sonic damage. At the start of your turn, draw a card.

Reverb Distortion
Ongoing - Track
When this card enters your hand, Impulse deals 1 non-Hero target 2 sonic damage. At the start of your turn, deal 1 sonic damage to 2 non-hero targets.
         * */
    }
}
