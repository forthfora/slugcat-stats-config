using Menu.Remix.MixedUI;

namespace SlugcatStatsConfig;

// Based on the options script from SBCameraScroll by SchuhBaum
// https://github.com/SchuhBaum/SBCameraScroll/blob/Rain-World-v1.9/SourceCode/MainModOptions.cs
public class ModOptions : OptionsTemplate
{
    public static ModOptions Instance { get; } = new();

    public static void RegisterOI()
    {
        if (MachineConnector.GetRegisteredOI(Plugin.MOD_ID) != Instance)
        {
            MachineConnector.SetRegisteredOI(Plugin.MOD_ID, Instance);
        }
    }

    #region Options

    public static Configurable<bool> canMaul = Instance.config.Bind("canMaul", false, new ConfigurableInfo(
        "Whether all slugcats can maul held creatures, normally exclusive to Artificer and Inv.",
        null, "", "Can Maul?"));

    public static Configurable<bool> autoGrabBatflies = Instance.config.Bind("autoGrabBatflies", false, new ConfigurableInfo(
        "Whether all slugcats automatically grab batflies when nearby.",
        null, "", "Auto Grab Batflies?"));

    public static Configurable<float> spearSpawnChanceModifier = Instance.config.Bind("spearSpawnChanceModifier", -0.1f, new ConfigurableInfo(
        "Determines chance modifier for a normal spear to spawn. Less is more likely." +
        "\nSurvivor = Pow 1, Hunter = Pow 0.85, Saint = Pow 1.4",
        new ConfigAcceptableRange<float>(-0.1f, 2.0f), "", "Spear Spawn Chance Modifier"));

    public static Configurable<float> explosiveSpearSpawnChance = Instance.config.Bind("explosiveSpearSpawnChance", -0.1f, new ConfigurableInfo(
        "Determines chance for an explosive spear to spawn. Higher is more likely." +
        "\nSurvivor = 0.0, Artificer = 0.012",
        new ConfigAcceptableRange<float>(-0.1f, 1.0f), "", "Explosive Spear Spawn Chance"));

    public static Configurable<float> electricSpearSpawnChance = Instance.config.Bind("electricSpearSpawnChance", -0.1f, new ConfigurableInfo(
        "Determines chance for an electric spear to spawn. Higher is more likely." +
        "\nSurvivor = 0.0, Artificer = 0.065",
        new ConfigAcceptableRange<float>(-0.1f, 1.0f), "", "Electric Spear Spawn Chance"));



    public static Configurable<int> hibernationFood = Instance.config.Bind("hibernationFood", -1, new ConfigurableInfo(
        "Number of food pips necessary to hibernate." +
        "\nHold and drag up or down to change.",
        new ConfigAcceptableRange<int>(-1, int.MaxValue / 2), "", "Hibernation Food"));

    public static Configurable<int> extraFood = Instance.config.Bind("extraFood", -1, new ConfigurableInfo(
        "Number of food pips that can be stored as extra." +
        "\nHold and drag up or down to change.",
        new ConfigAcceptableRange<int>(-1, int.MaxValue / 2), "", "Extra Food"));


    public static Configurable<int> slugpupHibernationFood = Instance.config.Bind("slugpupHibernationFood", -1, new ConfigurableInfo(
        "Number of food pips necessary for each slugpup to hibernate." +
        "\nHold and drag up or down to change.",
        new ConfigAcceptableRange<int>(-1, int.MaxValue / 2), "", "Slugpup Hibernation Food"));

    public static Configurable<int> slugpupExtraFood = Instance.config.Bind("slugpupExtraFood", -1, new ConfigurableInfo(
        "Number of food pips that each slugpup can store as extra." +
        "\nHold and drag up or down to change.",
        new ConfigAcceptableRange<int>(-1, int.MaxValue / 2), "", "Slugpup Extra Food"));



    public static Configurable<float> lungsFac = Instance.config.Bind("lungsFac", -0.1f, new ConfigurableInfo(
        "Determines lung capacity. LOWER values mean slugcat can hold their breath for longer." +
        "\nSurvivor = 1.0, Rivulet = 0.15, Monk = 1.2",
        new ConfigAcceptableRange<float>(-0.1f, 5.0f), "", "Lungs Factor"));

    public static Configurable<float> generalVisibilityBonus = Instance.config.Bind("generalVisibilityBonus", -1.1f, new ConfigurableInfo(
        "A bonus on how easily slugcat is spotted by other creatures (minimum reverts to default). Lower is less visible." +
        "\nSurvivor = 0.0, Monk = -0.1, Gourmand = 0.3",
        new ConfigAcceptableRange<float>(-1.1f, 1.0f), "", "Visibility Bonus"));

    public static Configurable<float> visualStealthInSneakMode = Instance.config.Bind("visualStealthInSneakMode", -0.1f, new ConfigurableInfo(
        "Determines how easily slugcat is spotted by other creatures when crawling. Higher is less visible." +
        "\nSurvivor = 0.5, Gourmand = 0.2, Monk = 0.6",
        new ConfigAcceptableRange<float>(-0.1f, 1.0f), "", "Crawling Stealth"));

    public static Configurable<float> loudnessFac = Instance.config.Bind("loudnessFac", -0.1f, new ConfigurableInfo(
        "Determines how easily slugcat is heard by other creatures. Lower is more quiet." +
        "\nSurvivor = 1.0, Gourmand = 1.5, Monk = 0.75",
        new ConfigAcceptableRange<float>(-0.1f, 2.0f), "", "Loudness Factor"));



    public static Configurable<int> throwingSkill = Instance.config.Bind("throwingSkill", -1, new ConfigurableInfo(
        "Determines throwing velocity, spear damage, throw delay, etc. Higher values are better" +
        "\nHold and drag up or down to change. Monk = 0, Survivor = 1, Hunter = 2",
        new ConfigAcceptableRange<int>(-1, 2), "", "Throwing Skill"));

    public static Configurable<float> poleClimbSpeedFac = Instance.config.Bind("poleClimbSpeedFac", -0.1f, new ConfigurableInfo(
        "Determines how quickly slugcat can climb poles." +
        "\nSurvivor = 1.0, Rivulet = 1.8",
        new ConfigAcceptableRange<float>(-0.1f, 10.0f), "", "Pole Climb Speed Factor"));

    public static Configurable<float> corridorClimbSpeedFac = Instance.config.Bind("corridorClimbSpeedFac", -0.1f, new ConfigurableInfo(
        "Determines how quickly slugcat can climb through corridors." +
        "\nSurvivor = 1.0, Rivulet = 1.6",
        new ConfigAcceptableRange<float>(-0.1f, 10.0f), "", "Corridor Climb Speed Factor"));

    public static Configurable<float> runspeedFac = Instance.config.Bind("runspeedFac", -0.1f, new ConfigurableInfo(
        "Determines how fast slugcat can run." +
        "\nSurvivor = 1.0, Rivulet = 1.75",
        new ConfigAcceptableRange<float>(-0.1f, 10.0f), "", "Run Speed Factor"));

    public static Configurable<float> bodyWeightFac = Instance.config.Bind("bodyWeightFac", -0.1f, new ConfigurableInfo(
        "Determines slugcat's weight." +
        "\nSurvivor = 1.0, Spearmaster = 0.85, Gourmand = 1.35",
        new ConfigAcceptableRange<float>(-0.1f, 10.0f), "", "Body Weight Factor"));


    #endregion

    #region Options Starving

    public static Configurable<int> throwingSkillStarving = Instance.config.Bind("throwingSkillStarving", -1, new ConfigurableInfo(
        "Determines throwing velocity, spear damage, throw delay, etc. Higher values are better" +
        "\nHold and drag up or down to change. Default = 0, Gourmand = 2",
        new ConfigAcceptableRange<int>(-1, 2), "", "Starving Throwing Skill"));

    public static Configurable<float> poleClimbSpeedFacStarving = Instance.config.Bind("poleClimbSpeedFacStarving", -0.1f, new ConfigurableInfo(
        "Determines how quickly slugcat can climb poles." +
        "\nSurvivor = 0.8, Rivulet = 1.1",
        new ConfigAcceptableRange<float>(-0.1f, 10.0f), "", "Starving Pole Climb Speed Factor"));

    public static Configurable<float> corridorClimbSpeedFacStarving = Instance.config.Bind("corridorClimbSpeedFacStarving", -0.1f, new ConfigurableInfo(
        "Determines how quickly slugcat can climb through corridors." +
        "\nSurvivor = 0.86, Rivulet = 1.2",
        new ConfigAcceptableRange<float>(-0.1f, 10.0f), "", "Starving Corridor Climb Speed Factor"));

    public static Configurable<float> runspeedFacStarving = Instance.config.Bind("runspeedFacStarving", -0.1f, new ConfigurableInfo(
        "Determines how fast slugcat can run." +
        "\nSurvivor = 0.875, Rivulet = 1.27",
        new ConfigAcceptableRange<float>(-0.1f, 10.0f), "", "Starving Run Speed Factor"));

    public static Configurable<float> bodyWeightFacStarving = Instance.config.Bind("bodyWeightFacStarving", -0.1f, new ConfigurableInfo(
        "Determines slugcat's weight." +
        "\nSurvivor = 0.9, Gourmand = 1.15",
        new ConfigAcceptableRange<float>(-0.1f, 10.0f), "", "Starving Body Weight Factor"));

    #endregion

    #region Extras

    public static Configurable<bool> forceGlow = Instance.config.Bind("forceGlow", false, new ConfigurableInfo(
        "Whether all slugcats will be forced to have the neuron glow effect. Not persistent.",
        null, "", "Force Glow?"));

    public static Configurable<float> extraJumpBoost = Instance.config.Bind("extraJumpBoost", -10.1f, new ConfigurableInfo(
        "Determines extra jump power. Will only be added/subtracted ontop of the normal jump's power." +
        "\nBase Jump: Survivor = 8.0, Rivulet = 14.0",
        new ConfigAcceptableRange<float>(-10.1f, 10.0f), "", "Extra Jump Boost"));

    public static Configurable<int> needleExtractSpeedFirst = Instance.config.Bind("needleExtractSpeedFirst", 100, new ConfigurableInfo(
        "How long Spearmaster takes to extract spears relative to normal. Applies to the first 10% of extraction.",
        new ConfigAcceptableRange<int>(1, 1000), "", "Needle Extract Speed Multiplier" +
                                                     "\n(First 10%)"));

    public static Configurable<int> needleExtractSpeedLast = Instance.config.Bind("needleExtractSpeedLast", 100, new ConfigurableInfo(
        "How long Spearmaster takes to extract spears relative to normal. Applies to the remaining 90% of extraction.",
        new ConfigAcceptableRange<int>(1, 1000), "", "Needle Extract Speed Multiplier" +
                                                     "\n(Last 90%)"));

    public static Configurable<bool> instantNeedles = Instance.config.Bind("instantNeedles", false, new ConfigurableInfo(
        "Overrides the other speed configs and makes needle extraction instantaneous.",
        null, "", "Instant Needles?"));



    public static Configurable<float> pyroDeathThreshold = Instance.config.Bind("pyroDeathThreshold", -0.1f, new ConfigurableInfo(
        "Determines the percentage of air in lungs that Artificer will die at or below, default 65%." +
        "\nA value of 0.0 will grant Artificer normal lung capacity.",
        new ConfigAcceptableRange<float>(-0.1f, 1.0f), "", "Pyro Death Threshold"));

    #endregion

    private const int NUMBER_OF_TABS = 5;

    public override void Initialize()
    {
        base.Initialize();
        Tabs = new OpTab[NUMBER_OF_TABS];
        var tabIndex = -1;



        AddTab(ref tabIndex, "General");

        AddDragger(hibernationFood, (string)hibernationFood.info.Tags[0]);
        AddDragger(extraFood, (string)extraFood.info.Tags[0]);
        DrawDraggers(ref Tabs[tabIndex]);

        AddCheckBox(canMaul, (string)canMaul.info.Tags[0]);
        AddCheckBox(autoGrabBatflies, (string)autoGrabBatflies.info.Tags[0]);
        DrawCheckBoxes(ref Tabs[tabIndex]);

        AddNewLine(1);

        AddFloatSlider(lungsFac, (string)lungsFac.info.Tags[0]);
        DrawFloatSliders(ref Tabs[tabIndex]);

        AddNewLine(2);

        AddFloatSlider(generalVisibilityBonus, (string)generalVisibilityBonus.info.Tags[0]);
        AddFloatSlider(visualStealthInSneakMode, (string)visualStealthInSneakMode.info.Tags[0]);
        AddFloatSlider(loudnessFac, (string)loudnessFac.info.Tags[0]);
        DrawFloatSliders(ref Tabs[tabIndex], 1.0f);

        AddNewLinesUntilEnd();
        DrawBox(ref Tabs[tabIndex]);



        AddTab(ref tabIndex, "Extras");

        AddDragger(slugpupHibernationFood, (string)slugpupHibernationFood.info.Tags[0]);
        AddDragger(slugpupExtraFood, (string)slugpupExtraFood.info.Tags[0]);
        DrawDraggers(ref Tabs[tabIndex]);

        AddCheckBox(forceGlow, (string)forceGlow.info.Tags[0]);
        AddCheckBox(instantNeedles, (string)instantNeedles.info.Tags[0]);
        DrawCheckBoxes(ref Tabs[tabIndex]);

        AddNewLine();

        AddFloatSlider(extraJumpBoost, (string)extraJumpBoost.info.Tags[0]);
        AddFloatSlider(pyroDeathThreshold, (string)pyroDeathThreshold.info.Tags[0]);
        DrawFloatSliders(ref Tabs[tabIndex]);

        AddIntSlider(needleExtractSpeedFirst, (string)needleExtractSpeedFirst.info.Tags[0], "1%", "1000%");
        AddIntSlider(needleExtractSpeedLast, (string)needleExtractSpeedLast.info.Tags[0], "1%", "1000%");
        DrawIntSliders(ref Tabs[tabIndex]);

        AddNewLinesUntilEnd();
        DrawBox(ref Tabs[tabIndex]);



        AddTab(ref tabIndex, "Normal");

        AddDragger(throwingSkill, (string)throwingSkill.info.Tags[0]);
        DrawDraggers(ref Tabs[tabIndex], 150.0f);

        AddNewLine();

        AddFloatSlider(bodyWeightFac, (string)bodyWeightFac.info.Tags[0]);
        AddFloatSlider(runspeedFac, (string)runspeedFac.info.Tags[0]);
        AddFloatSlider(poleClimbSpeedFac, (string)poleClimbSpeedFac.info.Tags[0]);
        AddFloatSlider(corridorClimbSpeedFac, (string)corridorClimbSpeedFac.info.Tags[0]);
        DrawFloatSliders(ref Tabs[tabIndex]);

        AddNewLinesUntilEnd();
        DrawBox(ref Tabs[tabIndex]);



        AddTab(ref tabIndex, "Starving");

        AddDragger(throwingSkillStarving, (string)throwingSkillStarving.info.Tags[0]);
        DrawDraggers(ref Tabs[tabIndex], 150.0f);

        AddNewLine();

        AddFloatSlider(bodyWeightFacStarving, (string)bodyWeightFacStarving.info.Tags[0]);
        AddFloatSlider(runspeedFacStarving, (string)runspeedFacStarving.info.Tags[0]);
        AddFloatSlider(poleClimbSpeedFacStarving, (string)poleClimbSpeedFacStarving.info.Tags[0]);
        AddFloatSlider(corridorClimbSpeedFacStarving, (string)corridorClimbSpeedFacStarving.info.Tags[0]);
        DrawFloatSliders(ref Tabs[tabIndex]);

        AddNewLinesUntilEnd();
        DrawBox(ref Tabs[tabIndex]);



        AddTab(ref tabIndex, "Spear Spawns");

        AddFloatSlider(spearSpawnChanceModifier, (string)spearSpawnChanceModifier.info.Tags[0]);
        AddFloatSlider(explosiveSpearSpawnChance, (string)explosiveSpearSpawnChance.info.Tags[0]);
        AddFloatSlider(electricSpearSpawnChance, (string)electricSpearSpawnChance.info.Tags[0]);
        DrawFloatSliders(ref Tabs[tabIndex]);

        AddNewLinesUntilEnd();
        DrawBox(ref Tabs[tabIndex]);
    }
}
