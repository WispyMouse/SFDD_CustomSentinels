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

        // todo: Should have range of number of tracks in play
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
            // Observe no third target
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

        // todo: Range of number of environments
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

        // todo random range of targets
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

        [Test()]
        public void PhaseModulator_NoTarget()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            StartGame();

            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDPhaseModulator");

            // Go to villain turn
            // There are no villain targets with 4 or less hp
            // no crashing
        }

        // todo range of targets
        [Test()]
        public void PhaseModulator_Target()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            // Create a villain target with 4 hp
            StartGame();

            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDPTripleDeckTurnTable");

            // Go to villain turn
            // There is a villain targets with 4 or less hp
            // observe they skip their turn
        }

        // todo range of number of tracks in hand
        [Test()]
        public void TripleDeckTurnTable_Power()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            StartGame();

            // Stack the deck so that two tracks are in hand
            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDPTripleDeckTurnTable");

            // Use the power
            // Choose to play two tracks
            // Observe they enter and trigger
            // Observe that all tracks are trashed ???
        }

        // todo range of targets
        // todo range of track cards in trash
        [Test()]
        public void SuperMaxHiFi_AugmentSonic()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            // fill trash with random number of track cards
            // create a villain target
            StartGame();

            // Stack the deck so that two tracks are in hand
            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDSuperMaxHiFi");

            // Deal sonic damage some how
            // Deal an additional target x sonic damage
        }

        // todo range of targets
        // todo range of track cards in trash
        [Test()]
        public void SuperMaxHiFi_DoesNotAugmentNotSonic()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            // fill trash with random number of track cards
            // create a villain target
            StartGame();

            // Stack the deck so that two tracks are in hand
            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDSuperMaxHiFi");

            // Deal not sonic damage some how
            // Observe no additional damage
        }

        [Test()]
        public void SonicSoothe_Heal()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            // fill trash with random number of track cards
            // create a villain target
            StartGame();

            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDSonicSoothe");

            // Wait until next start of turn
            // Observe heal for target
        }

        [Test()]
        public void SonicSoothe_DiscardDamageBlock()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            // fill trash with random number of track cards
            // create a villain target
            StartGame();

            // Have Sonic Soothe in hand
            // Take damage
            // Observe no damage and discard
        }

        [Test()]
        public void DancefloorDige_DrawOneShot()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            StartGame();

            // Stack the deck so that a one-shot is on top

            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDDancefloorDirge");

            // Draw that card
            // Observe it is a one-shot
            // Play it
        }

        [Test()]
        public void DancefloorDige_DrawNotOneShot()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            StartGame();

            // Stack the deck so that a not-one-shot is on top

            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDDancefloorDirge");

            // Draw that card
            // Observe it is a not a one-shot
            // Can't play it
        }

        [Test()]
        public void DancefloorDige_StartOfTurnMelee()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            StartGame();

            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDDancefloorDirge");

            // Wait until start of turn
            // Deal damage
        }

        // todo random number of instances
        [Test()]
        public void LimbicOverdrive_HeroDamageOnce()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            StartGame();

            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDLimbicOverdrive");

            // Heroes take random damage some how
            // Observe they may play a card
            // Observe it happens only once per hero
        }

        [Test()]
        public void LimbicOverdrive_LeavesPlayDraw()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            StartGame();

            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDLimbicOverdrive");

            // Remove the card some how
            // Observe each player draws
        }

        // todo range of turns
        // todo range of targets
        [Test()]
        public void BulidUpToTheDrop_WaitForTurnsAndDealDamage()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            StartGame();

            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDBuildUpToTheDrop");

            // Wait a number of turns
            // Observe each turn a counter is placed
            // Leave play some how
            // Observe damage to targets based on counters
        }

        [Test()]
        public void ReverbDistortion_EntersHandTrigger()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            StartGame();

            // Draw the card
            // Observe damage trigger
        }

        [Test()]
        public void ReberbDistortion_StartOfTurnTrigger()
        {
            SetupGameController("BaronBlade", "SFDD_CustomSentinels.SFDDImpulse", "Megalopolis");

            StartGame();

            GoToPlayCardPhaseAndPlayCard(impulse, "SFDDReverbDistortion");

            // Wait until next turn
            // Observe damage
        }
    }
}
