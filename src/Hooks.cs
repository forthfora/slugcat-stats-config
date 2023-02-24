using Mono.Cecil.Cil;
using MonoMod.Cil;
using RWCustom;
using System;
using UnityEngine;

namespace SlugcatStatsConfig
{
    internal static class Hooks
    {
        public static void ApplyHooks()
        {
            On.RainWorld.OnModsInit += RainWorld_OnModsInit;

            On.SlugcatStats.ctor += SlugcatStats_ctor;
            On.SlugcatStats.SlugcatFoodMeter += SlugcatStats_SlugcatFoodMeter;

            On.SlugcatStats.SlugcatCanMaul += SlugcatStats_SlugcatCanMaul;
            On.SlugcatStats.AutoGrabBatflys += SlugcatStats_AutoGrabBatflys;

            On.SlugcatStats.SpearSpawnModifier += SlugcatStats_SpearSpawnModifier;
            On.SlugcatStats.SpearSpawnElectricRandomChance += SlugcatStats_SpearSpawnElectricRandomChance;
            On.SlugcatStats.SpearSpawnExplosiveRandomChance += SlugcatStats_SpearSpawnExplosiveRandomChance;

            On.Player.Jump += Player_Jump;
            On.Player.Update += Player_Update;
        }

        private static void Player_Update(On.Player.orig_Update orig, Player self, bool eu)
        {
            orig(self, eu);

            if (Options.forceGlow.Value) self.glowing = true;
        }

        private static void Player_Jump(On.Player.orig_Jump orig, Player self)
        {
            orig(self);

            if (Options.extraJumpBoost.Value > -10.1f) self.jumpBoost += Options.extraJumpBoost.Value;
        }

        private static float SlugcatStats_SpearSpawnExplosiveRandomChance(On.SlugcatStats.orig_SpearSpawnExplosiveRandomChance orig, SlugcatStats.Name index)
        {
            if (Options.explosiveSpearSpawnChance.Value >= 0.0f) return Options.explosiveSpearSpawnChance.Value;

            return orig(index);
        }

        private static float SlugcatStats_SpearSpawnElectricRandomChance(On.SlugcatStats.orig_SpearSpawnElectricRandomChance orig, SlugcatStats.Name index)
        {
            if (Options.electricSpearSpawnChance.Value >= 0.0f) return Options.electricSpearSpawnChance.Value;

            return orig(index);
        }

        private static float SlugcatStats_SpearSpawnModifier(On.SlugcatStats.orig_SpearSpawnModifier orig, SlugcatStats.Name index, float originalSpearChance)
        {
            if (Options.spearSpawnChanceModifier.Value >= 0.0f) return Mathf.Pow(originalSpearChance, Options.spearSpawnChanceModifier.Value);

            return orig(index, originalSpearChance);
        }

        private static bool SlugcatStats_AutoGrabBatflys(On.SlugcatStats.orig_AutoGrabBatflys orig, SlugcatStats.Name slugcatNum)
        {
            if (Options.autoGrabBatflies.Value) return true;

            return orig(slugcatNum);
        }

        private static bool SlugcatStats_SlugcatCanMaul(On.SlugcatStats.orig_SlugcatCanMaul orig, SlugcatStats.Name slugcatNum)
        {
            if (Options.canMaul.Value) return true;

            return orig(slugcatNum);
        }

        private static void SlugcatStats_ctor(On.SlugcatStats.orig_ctor orig, SlugcatStats self, SlugcatStats.Name slugcat, bool malnourished)
        {
            orig(self, slugcat, malnourished);

            // General
            if (Options.throwingSkill.Value > 0.0f) self.throwingSkill = Options.throwingSkill.Value;
            if (Options.lungsFac.Value > 0.0f) self.lungsFac = Options.lungsFac.Value;
            if (Options.bodyWeightFac.Value > 0.0f) self.bodyWeightFac = Options.bodyWeightFac.Value;
            
            // Speed
            if (Options.runspeedFac.Value > 0.0f) self.runspeedFac = Options.runspeedFac.Value;
            if (Options.poleClimbSpeedFac.Value > 0.0f) self.poleClimbSpeedFac = Options.poleClimbSpeedFac.Value;
            if (Options.corridorClimbSpeedFac.Value > 0.0f) self.corridorClimbSpeedFac = Options.corridorClimbSpeedFac.Value;

            // Stealth
            if (Options.generalVisibilityBonus.Value > -1.1f) self.generalVisibilityBonus = Options.generalVisibilityBonus.Value;
            if (Options.visualStealthInSneakMode.Value > 0.0f) self.visualStealthInSneakMode = Options.visualStealthInSneakMode.Value;
            if (Options.loudnessFac.Value > 0.0f) self.loudnessFac = Options.loudnessFac.Value;

            if (malnourished) MalnourishedStats(self, slugcat);
        }

        private static void MalnourishedStats(SlugcatStats self, SlugcatStats.Name slugcat)
        {
            // General
            if (Options.throwingSkillStarving.Value > 0.0f) self.throwingSkill = Options.throwingSkillStarving.Value;
            if (Options.bodyWeightFacStarving.Value > 0.0f) self.bodyWeightFac = Options.bodyWeightFacStarving.Value;

            // Speed
            if (Options.runspeedFacStarving.Value > 0.0f) self.runspeedFac = Options.runspeedFacStarving.Value;
            if (Options.poleClimbSpeedFacStarving.Value > 0.0f) self.poleClimbSpeedFac = Options.poleClimbSpeedFacStarving.Value;
            if (Options.corridorClimbSpeedFacStarving.Value > 0.0f) self.corridorClimbSpeedFac = Options.corridorClimbSpeedFacStarving.Value;
        }

        private static IntVector2 SlugcatStats_SlugcatFoodMeter(On.SlugcatStats.orig_SlugcatFoodMeter orig, SlugcatStats.Name slugcat)
        {
            IntVector2 food = orig(slugcat);

            if (Options.hibernationFood.Value > 0)
            {
                food.y = Options.hibernationFood.Value;
            }

            int extraFood = food.x - food.y;

            if (Options.extraFood.Value >= 0)
            {
                extraFood = Options.extraFood.Value;
            }

            food.x = food.y + extraFood;

            return food;
        }

        private static bool isInit = false;

        private static void RainWorld_OnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
        {
            orig(self);

            if (isInit) return;
            isInit = true;

            MachineConnector.SetRegisteredOI(Plugin.MOD_ID, Options.instance);
        }
    }
}
    