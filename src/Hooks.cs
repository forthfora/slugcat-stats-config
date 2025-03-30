using Mono.Cecil.Cil;
using MonoMod.Cil;
using RWCustom;
using UnityEngine;

namespace SlugcatStatsConfig;

public static class Hooks
{
    private static bool IsInit { get; set; }

    private static void RainWorld_OnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
    {
        try
        {
            ModOptions.RegisterOI();

            if (IsInit)
            {
                return;
            }

            IsInit = true;


            // Init Info
            var mod = ModManager.ActiveMods.FirstOrDefault(mod => mod.id == Plugin.MOD_ID);

            if (mod is null)
            {
                Plugin.Logger.LogError($"Failed to initialize: ID '{Plugin.MOD_ID}' wasn't found in the active mods list!");
                return;
            }

            Plugin.ModName = mod.name;
            Plugin.Version = mod.version;
            Plugin.Authors = mod.authors;

            ApplyHooks();
        }
        catch (Exception e)
        {
            Plugin.Logger.LogError($"OnModsInit:\n{e}");
        }
        finally
        {
            orig(self);
        }
    }

    public static void ApplyHooks()
    {
        On.RainWorld.OnModsInit += RainWorld_OnModsInit;

        On.SlugcatStats.ctor += SlugcatStats_ctor;
        On.SlugcatStats.SlugcatFoodMeter += SlugcatStats_SlugcatFoodMeter;

        On.SlugcatStats.SlugcatCanMaul += SlugcatStats_SlugcatCanMaul;
        On.SlugcatStats.AutoGrabBatflys += SlugcatStats_AutoGrabBatflys;

        On.SlugcatStats.SpearSpawnModifier_Timeline_float += SlugcatStats_SpearSpawnModifier;
        On.SlugcatStats.SpearSpawnElectricRandomChance_Timeline += SlugcatStats_SpearSpawnElectricRandomChance;
        On.SlugcatStats.SpearSpawnExplosiveRandomChance_Timeline += SlugcatStats_SpearSpawnExplosiveRandomChance;

        On.Player.Jump += Player_Jump;
        On.Player.Update += Player_Update;
        On.Player.PyroDeathThreshold += Player_PyroDeathThreshold;

        try
        {
            IL.Player.GrabUpdate += Player_GrabUpdate;
        }
        catch (Exception e)
        {
            Plugin.Logger.LogError($"Failed to IL Hook Player.Update:\n{e}");
        }
    }

    private static void SlugcatStats_ctor(On.SlugcatStats.orig_ctor orig, SlugcatStats self, SlugcatStats.Name slugcat, bool malnourished)
    {
        orig(self, slugcat, malnourished);

        // General
        if (ModOptions.throwingSkill.Value > 0.0f) self.throwingSkill = ModOptions.throwingSkill.Value;
        if (ModOptions.lungsFac.Value > 0.0f) self.lungsFac = ModOptions.lungsFac.Value;
        if (ModOptions.bodyWeightFac.Value > 0.0f) self.bodyWeightFac = ModOptions.bodyWeightFac.Value;

        // Speed
        if (ModOptions.runspeedFac.Value > 0.0f) self.runspeedFac = ModOptions.runspeedFac.Value;
        if (ModOptions.poleClimbSpeedFac.Value > 0.0f) self.poleClimbSpeedFac = ModOptions.poleClimbSpeedFac.Value;
        if (ModOptions.corridorClimbSpeedFac.Value > 0.0f) self.corridorClimbSpeedFac = ModOptions.corridorClimbSpeedFac.Value;

        // Stealth
        if (ModOptions.generalVisibilityBonus.Value > -1.1f) self.generalVisibilityBonus = ModOptions.generalVisibilityBonus.Value;
        if (ModOptions.visualStealthInSneakMode.Value > 0.0f) self.visualStealthInSneakMode = ModOptions.visualStealthInSneakMode.Value;
        if (ModOptions.loudnessFac.Value > 0.0f) self.loudnessFac = ModOptions.loudnessFac.Value;

        if (malnourished) MalnourishedStats(self, slugcat);
    }

    private static void MalnourishedStats(SlugcatStats self, SlugcatStats.Name _)
    {
        // General
        if (ModOptions.throwingSkillStarving.Value > 0.0f) self.throwingSkill = ModOptions.throwingSkillStarving.Value;
        if (ModOptions.bodyWeightFacStarving.Value > 0.0f) self.bodyWeightFac = ModOptions.bodyWeightFacStarving.Value;

        // Speed
        if (ModOptions.runspeedFacStarving.Value > 0.0f) self.runspeedFac = ModOptions.runspeedFacStarving.Value;
        if (ModOptions.poleClimbSpeedFacStarving.Value > 0.0f) self.poleClimbSpeedFac = ModOptions.poleClimbSpeedFacStarving.Value;
        if (ModOptions.corridorClimbSpeedFacStarving.Value > 0.0f) self.corridorClimbSpeedFac = ModOptions.corridorClimbSpeedFacStarving.Value;
    }

    private static IntVector2 SlugcatStats_SlugcatFoodMeter(On.SlugcatStats.orig_SlugcatFoodMeter orig, SlugcatStats.Name slugcat)
    {
        var food = orig(slugcat);

        if (slugcat == MoreSlugcats.MoreSlugcatsEnums.SlugcatStatsName.Slugpup)
        {
            if (ModOptions.slugpupHibernationFood.Value > 0)
            {
                food.y = ModOptions.slugpupHibernationFood.Value;
            }

            var slugpupExtraFood = food.x - food.y;

            if (ModOptions.slugpupExtraFood.Value >= 0)
            {
                slugpupExtraFood = ModOptions.slugpupExtraFood.Value;
            }

            food.x = food.y + slugpupExtraFood;

            return food;
        }

        if (ModOptions.hibernationFood.Value > 0)
        {
            food.y = ModOptions.hibernationFood.Value;
        }

        var extraFood = food.x - food.y;

        if (ModOptions.extraFood.Value >= 0)
        {
            extraFood = ModOptions.extraFood.Value;
        }

        food.x = food.y + extraFood;

        return food;
    }



    private static float SlugcatStats_SpearSpawnExplosiveRandomChance(On.SlugcatStats.orig_SpearSpawnExplosiveRandomChance_Timeline orig, SlugcatStats.Timeline timeline)
    {
        if (ModOptions.explosiveSpearSpawnChance.Value >= 0.0f) return ModOptions.explosiveSpearSpawnChance.Value;

        return orig(timeline);
    }

    private static float SlugcatStats_SpearSpawnElectricRandomChance(On.SlugcatStats.orig_SpearSpawnElectricRandomChance_Timeline orig, SlugcatStats.Timeline timeline)
    {
        if (ModOptions.electricSpearSpawnChance.Value >= 0.0f) return ModOptions.electricSpearSpawnChance.Value;

        return orig(timeline);
    }

    private static float SlugcatStats_SpearSpawnModifier(On.SlugcatStats.orig_SpearSpawnModifier_Timeline_float orig, SlugcatStats.Timeline timeline, float originalSpearChance)
    {
        if (ModOptions.spearSpawnChanceModifier.Value >= 0.0f) return Mathf.Pow(originalSpearChance, ModOptions.spearSpawnChanceModifier.Value);

        return orig(timeline, originalSpearChance);
    }

    private static bool SlugcatStats_AutoGrabBatflys(On.SlugcatStats.orig_AutoGrabBatflys orig, SlugcatStats.Name slugcatNum)
    {
        if (ModOptions.autoGrabBatflies.Value) return true;

        return orig(slugcatNum);
    }

    private static bool SlugcatStats_SlugcatCanMaul(On.SlugcatStats.orig_SlugcatCanMaul orig, SlugcatStats.Name slugcatNum)
    {
        if (ModOptions.canMaul.Value) return true;

        return orig(slugcatNum);
    }


    // Glow
    private static void Player_Update(On.Player.orig_Update orig, Player self, bool eu)
    {
        orig(self, eu);

        if (ModOptions.forceGlow.Value) self.glowing = true;
    }

    // Jump Boost
    private static void Player_Jump(On.Player.orig_Jump orig, Player self)
    {
        orig(self);

        if (ModOptions.extraJumpBoost.Value > -10.1f) self.jumpBoost += ModOptions.extraJumpBoost.Value;
    }

    // Needle Extraction Speed
    private static void Player_GrabUpdate(ILContext il)
    {
        var c = new ILCursor(il);

        // First 10% Extraction Speed
        if (!c.TryGotoNext(MoveType.After,
                x => x.MatchLdloc(16),
                x => x.MatchLdloc(16),
                x => x.MatchLdfld<PlayerGraphics.TailSpeckles>("spearProg"),
                x => x.MatchLdcR4(0.11f)))
        {
            throw new Exception("Goto Failed");
        }

        c.Emit(OpCodes.Pop);
        c.EmitDelegate<Func<float>>(() => ModOptions.instantNeedles.Value ? 1.0f : (ModOptions.needleExtractSpeedFirst.Value / 100.0f) * 0.1f);

        // Rest of Extraction Speed
        if (!c.TryGotoNext(MoveType.After,
            x => x.MatchLdloc(16),
            x => x.MatchLdloc(16),
            x => x.MatchLdfld<PlayerGraphics.TailSpeckles>("spearProg"),
            x => x.MatchLdcR4(1)))
        {
            throw new Exception("Goto Failed");
        }

        c.Emit(OpCodes.Pop);
        c.EmitDelegate<Func<float>>(() => ModOptions.instantNeedles.Value ? 1.0f : (ModOptions.needleExtractSpeedLast.Value / 100.0f) * 0.05f);
    }

    // Artificer Drowning
    private static float Player_PyroDeathThreshold(On.Player.orig_PyroDeathThreshold orig, RainWorldGame game)
    {
        if (ModOptions.pyroDeathThreshold.Value < 0.0) return orig(game);

        return ModOptions.pyroDeathThreshold.Value;
    }
}
