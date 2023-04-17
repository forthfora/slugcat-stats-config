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
            On.Player.PyroDeathThreshold += Player_PyroDeathThreshold;
        }

        private static bool isInit = false;

        private static void RainWorld_OnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
        {
            try
            {
                if (isInit) return;
                isInit = true;

                MachineConnector.SetRegisteredOI(Plugin.MOD_ID, Options.instance);

                IL.Player.GrabUpdate += Player_GrabUpdate;
            }
            catch (Exception e)
            {
                Plugin.Logger.LogWarning(e);
            }
            finally
            {
                orig(self);
            }
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

            if (slugcat == MoreSlugcats.MoreSlugcatsEnums.SlugcatStatsName.Slugpup)
            {
                if (Options.slugpupHibernationFood.Value > 0)
                {
                    food.y = Options.slugpupHibernationFood.Value;
                }

                int slugpupExtraFood = food.x - food.y;

                if (Options.slugpupExtraFood.Value >= 0)
                {
                    slugpupExtraFood = Options.slugpupExtraFood.Value;
                }

                food.x = food.y + slugpupExtraFood;

                return food;
            }

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



        // Glow
        private static void Player_Update(On.Player.orig_Update orig, Player self, bool eu)
        {
            orig(self, eu);

            if (Options.forceGlow.Value) self.glowing = true;
        }


        // Jump Boost
        private static void Player_Jump(On.Player.orig_Jump orig, Player self)
        {
            orig(self);

            if (Options.extraJumpBoost.Value > -10.1f) self.jumpBoost += Options.extraJumpBoost.Value;
        }


        // Needle Extraction Speed
        private static void Player_GrabUpdate(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            // First 10% Extraction Speed
            c.GotoNext(MoveType.After,
                x => x.MatchLdloc(16),
                x => x.MatchLdloc(16),
                x => x.MatchLdfld<PlayerGraphics.TailSpeckles>("spearProg"),
                x => x.MatchLdcR4(0.11f));

            c.Remove();

            c.Emit(OpCodes.Ldarg_0);
            c.EmitDelegate<Func<Player, float>>((player) => Options.instantNeedles.Value ? 1.0f : (Options.needleExtractSpeedFirst.Value / 100.0f) * 0.1f);


            // Rest of Extraction Speed
            c.GotoNext(MoveType.After,
                x => x.MatchLdloc(16),
                x => x.MatchLdloc(16),
                x => x.MatchLdfld<PlayerGraphics.TailSpeckles>("spearProg"),
                x => x.MatchLdcR4(1));

            c.Remove();

            c.Emit(OpCodes.Ldarg_0);
            c.EmitDelegate<Func<Player, float>>((player) => Options.instantNeedles.Value ? 1.0f : (Options.needleExtractSpeedLast.Value / 100.0f) * 0.05f);
        }


        // Artificer Drowning
        private static float Player_PyroDeathThreshold(On.Player.orig_PyroDeathThreshold orig, RainWorldGame game)
        {
            if (Options.pyroDeathThreshold.Value < 0.0) return orig(game);

            return Options.pyroDeathThreshold.Value;
        }
    }
}
    