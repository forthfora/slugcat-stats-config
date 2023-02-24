using System.Collections.Generic;
using Menu.Remix.MixedUI;
using UnityEngine;

namespace SlugcatStatsConfig
{
    // Based on the options script from SBCameraScroll by SchuhBaum
    // https://github.com/SchuhBaum/SBCameraScroll/blob/Rain-World-v1.9/SourceCode/MainModOptions.cs
    public class Options : OptionInterface
    {
        public static Options instance = new Options();
        private const string AUTHORS_NAME = "forthbridge";

        #region Options

        public static Configurable<bool> canMaul = instance.config.Bind("canMaul", false, new ConfigurableInfo(
            "Whether all slugcats can maul held creatures, normally exclusive to Artificer and Inv.",
            null, "", "Can Maul?"));

        public static Configurable<bool> autoGrabBatflies = instance.config.Bind("autoGrabBatflies", false, new ConfigurableInfo(
            "Whether all slugcats automatically grab batflies when nearby.",
            null, "", "Auto Grab Batflies?"));

        public static Configurable<float> spearSpawnChanceModifier = instance.config.Bind("spearSpawnChanceModifier", -0.1f, new ConfigurableInfo(
            "Determines chance modifier for a normal spear to spawn. Less is more likely." +
            "\nSurvivor = Pow 1, Hunter = Pow 0.85, Saint = Pow 1.4",
            new ConfigAcceptableRange<float>(-0.1f, 2.0f), "", "Spear Spawn Chance Modifier"));

        public static Configurable<float> explosiveSpearSpawnChance = instance.config.Bind("explosiveSpearSpawnChance", -0.1f, new ConfigurableInfo(
            "Determines chance for an explosive spear to spawn. Higher is more likely." +
            "\nSurvivor = 0.0, Artificer = 0.012",
            new ConfigAcceptableRange<float>(-0.1f, 1.0f), "", "Explosive Spear Spawn Chance"));

        public static Configurable<float> electricSpearSpawnChance = instance.config.Bind("electricSpearSpawnChance", -0.1f, new ConfigurableInfo(
            "Determines chance for an electric spear to spawn. Higher is more likely." +
            "\nSurvivor = 0.0, Artificer = 0.065",
            new ConfigAcceptableRange<float>(-0.1f, 1.0f), "", "Electric Spear Spawn Chance"));



        public static Configurable<int> hibernationFood = instance.config.Bind("hibernationFood", -1, new ConfigurableInfo(
            "Number of food pips necessary to hibernate.",
            new ConfigAcceptableRange<int>(-1, int.MaxValue / 2), "", "Hibernation Food"));

        public static Configurable<int> extraFood = instance.config.Bind("extraFood", -1, new ConfigurableInfo(
            "Number of food pips that can be stored as extra.",
            new ConfigAcceptableRange<int>(-1, int.MaxValue / 2), "", "Extra Food"));

        public static Configurable<float> lungsFac = instance.config.Bind("lungsFac", -0.1f, new ConfigurableInfo(
            "Determines lung capacity. LOWER values mean slugcat can hold their breath for longer." +
            "\nSurvivor = 1.0, Rivulet = 0.15, Monk = 1.2",
            new ConfigAcceptableRange<float>(-0.1f, 2.0f), "", "Lungs Factor"));

        public static Configurable<float> generalVisibilityBonus = instance.config.Bind("generalVisibilityBonus", -1.1f, new ConfigurableInfo(
            "A bonus on how easily slugcat is spotted by other creatures (minimum reverts to default). Lower is less visible." +
            "\nSurvivor = 0.0, Monk = -0.1, Gourmand = 0.3",
            new ConfigAcceptableRange<float>(-1.1f, 1.0f), "", "Visibility Bonus"));

        public static Configurable<float> visualStealthInSneakMode = instance.config.Bind("visualStealthInSneakMode", -0.1f, new ConfigurableInfo(
            "Determines how easily slugcat is spotted by other creatures when crawling. Higher is less visible." +
            "\nSurvivor = 0.5, Gourmand = 0.2, Monk = 0.6",
            new ConfigAcceptableRange<float>(-0.1f, 1.0f), "", "Crawling Stealth"));

        public static Configurable<float> loudnessFac = instance.config.Bind("loudnessFac", -0.1f, new ConfigurableInfo(
            "Determines how easily slugcat is heard by other creatures. Lower is more quiet." +
            "\nSurvivor = 1.0, Gourmand = 1.5, Monk = 0.75",
            new ConfigAcceptableRange<float>(-0.1f, 2.0f), "", "Loudness Factor"));



        public static Configurable<int> throwingSkill = instance.config.Bind("throwingSkill", -1, new ConfigurableInfo(
            "Determines throwing velocity, spear damage, throw delay, etc. Higher values are better" +
            "\nMonk = 0, Survivor = 1, Hunter = 2",
            new ConfigAcceptableRange<int>(-1, 2), "", "Throwing Skill"));

        public static Configurable<float> poleClimbSpeedFac = instance.config.Bind("poleClimbSpeedFac", -0.1f, new ConfigurableInfo(
            "Determines how quickly slugcat can climb poles." +
            "\nSurvivor = 1.0, Rivulet = 1.8",
            new ConfigAcceptableRange<float>(-0.1f, 5.0f), "", "Pole Climb Speed Factor"));

        public static Configurable<float> corridorClimbSpeedFac = instance.config.Bind("corridorClimbSpeedFac", -0.1f, new ConfigurableInfo(
            "Determines how quickly slugcat can climb through corridors." +
            "\nSurvivor = 1.0, Rivulet = 1.6",
            new ConfigAcceptableRange<float>(-0.1f, 5.0f), "", "Corridor Climb Speed Factor"));

        public static Configurable<float> runspeedFac = instance.config.Bind("runspeedFac", -0.1f, new ConfigurableInfo(
            "Determines how fast slugcat can run." +
            "\nSurvivor = 1.0, Rivulet = 1.75",
            new ConfigAcceptableRange<float>(-0.1f, 5.0f), "", "Run Speed Factor"));

        public static Configurable<float> bodyWeightFac = instance.config.Bind("bodyWeightFac", -0.1f, new ConfigurableInfo(
            "Determines slugcat's weight." +
            "\nSurvivor = 1.0, Spearmaster = 0.85, Gourmand = 1.35",
            new ConfigAcceptableRange<float>(-0.1f, 5.0f), "", "Body Weight Factor"));


        #endregion

        #region Options Starving

        public static Configurable<int> throwingSkillStarving = instance.config.Bind("throwingSkillStarving", -1, new ConfigurableInfo(
            "Determines throwing velocity, spear damage, throw delay, etc. Higher values are better" +
            "\nDefault = 0, Gourmand = 2",
            new ConfigAcceptableRange<int>(-1, 2), "", "Starving Throwing Skill"));

        public static Configurable<float> poleClimbSpeedFacStarving = instance.config.Bind("poleClimbSpeedFacStarving", -0.1f, new ConfigurableInfo(
            "Determines how quickly slugcat can climb poles." +
            "\nSurvivor = 0.8, Rivulet = 1.1",
            new ConfigAcceptableRange<float>(-0.1f, 5.0f), "", "Starving Pole Climb Speed Factor"));

        public static Configurable<float> corridorClimbSpeedFacStarving = instance.config.Bind("corridorClimbSpeedFacStarving", -0.1f, new ConfigurableInfo(
            "Determines how quickly slugcat can climb through corridors." +
            "\nSurvivor = 0.86, Rivulet = 1.2",
            new ConfigAcceptableRange<float>(-0.1f, 5.0f), "", "Starving Corridor Climb Speed Factor"));

        public static Configurable<float> runspeedFacStarving = instance.config.Bind("runspeedFacStarving", -0.1f, new ConfigurableInfo(
            "Determines how fast slugcat can run." +
            "\nSurvivor = 0.875, Rivulet = 1.27",
            new ConfigAcceptableRange<float>(-0.1f, 5.0f), "", "Starving Run Speed Factor"));

        public static Configurable<float> bodyWeightFacStarving = instance.config.Bind("bodyWeightFacStarving", -0.1f, new ConfigurableInfo(
            "Determines slugcat's weight." +
            "\nSurvivor = 0.9, Gourmand = 1.15",
            new ConfigAcceptableRange<float>(-0.1f, 5.0f), "", "Starving Body Weight Factor"));

        #endregion

        public static Configurable<bool> forceGlow = instance.config.Bind("forceGlow", false, new ConfigurableInfo(
            "Whether all slugcats will be forced to have the neuron glow effect. Not persistent.",
            null, "", "Force Glow?"));

        public static Configurable<float> extraJumpBoost = instance.config.Bind("extraJumpBoost", -10.1f, new ConfigurableInfo(
            "Determines extra jump power. Will only be added/subtracted ontop of the normal jump's power." +
            "\nBase Jump: Survivor = 8.0, Rivulet = 14.0",
            new ConfigAcceptableRange<float>(-10.1f, 10.0f), "", "Extra Jump Boost"));


        #region Parameters
        private readonly float spacing = 20f;
        private readonly float fontHeight = 20f;

        private float CheckBoxWithSpacing => checkBoxSize + 0.25f * spacing;
        private readonly int numberOfCheckboxes = 2;
        private readonly float checkBoxSize = 60.0f;

        private float DraggerWithSpacing => draggerSize + 0.25f * spacing;
        private readonly int numberOfDraggers = 2;
        private readonly float draggerSize = 60.0f;

        #endregion

        #region Variables
        private Vector2 marginX = new();
        private Vector2 pos = new();

        private readonly List<float> boxEndPositions = new();

        private readonly List<Configurable<float>> floatSliderConfigurables = new();
        private readonly List<string> floatSliderMainTextLabels = new();
        private readonly List<OpLabel> floatSliderTextLabelsLeft = new();
        private readonly List<OpLabel> floatSliderTextLabelsRight = new();

        private readonly List<Configurable<int>> draggerIntConfigurables = new();
        private readonly List<OpLabel> draggerIntTextLabels = new();

        private readonly List<Configurable<bool>> checkBoxConfigurables = new();
        private readonly List<OpLabel> checkBoxesTextLabels = new();

        private readonly List<Configurable<string>> comboBoxConfigurables = new();
        private readonly List<List<ListItem>> comboBoxLists = new();
        private readonly List<bool> comboBoxAllowEmpty = new();
        private readonly List<OpLabel> comboBoxesTextLabels = new();

        private readonly List<Configurable<int>> sliderConfigurables = new();
        private readonly List<string> sliderMainTextLabels = new();
        private readonly List<OpLabel> sliderTextLabelsLeft = new();
        private readonly List<OpLabel> sliderTextLabelsRight = new();

        private readonly List<OpLabel> textLabels = new();
        #endregion

        private const int NUMBER_OF_TABS = 5;

        public override void Initialize()
        {
            base.Initialize();
            Tabs = new OpTab[NUMBER_OF_TABS];
            int tabIndex = -1;

            // Miscellaneous stats that are unaffected by starving
            AddTab(ref tabIndex, "General");

            AddDragger(hibernationFood, (string)hibernationFood.info.Tags[0]);
            AddDragger(extraFood, (string)extraFood.info.Tags[0]);
            DrawDraggers(ref Tabs[tabIndex]);

            AddCheckBox(canMaul, (string)canMaul.info.Tags[0]);
            AddCheckBox(autoGrabBatflies, (string)autoGrabBatflies.info.Tags[0]);
            DrawCheckBoxes(ref Tabs[tabIndex]);

            AddNewLine(1);

            AddFloatSlider(lungsFac, (string)lungsFac.info.Tags[0]);

            AddFloatSlider(generalVisibilityBonus, (string)generalVisibilityBonus.info.Tags[0]);
            AddFloatSlider(visualStealthInSneakMode, (string)visualStealthInSneakMode.info.Tags[0]);
            AddFloatSlider(loudnessFac, (string)loudnessFac.info.Tags[0]);
            DrawFloatSliders(ref Tabs[tabIndex]);

            AddNewLine(0);
            DrawBox(ref Tabs[tabIndex]);

            AddTab(ref tabIndex, "Extras");

            AddCheckBox(forceGlow, (string)forceGlow.info.Tags[0]);
            DrawCheckBoxes(ref Tabs[tabIndex]);

            AddFloatSlider(extraJumpBoost, (string)extraJumpBoost.info.Tags[0]);
            DrawFloatSliders(ref Tabs[tabIndex]);

            AddNewLine(14);
            DrawBox(ref Tabs[tabIndex]);

            AddTab(ref tabIndex, "Normal");

            AddDragger(throwingSkill, (string)throwingSkill.info.Tags[0]);
            DrawDraggers(ref Tabs[tabIndex]);

            AddFloatSlider(bodyWeightFac, (string)bodyWeightFac.info.Tags[0]);
            AddFloatSlider(runspeedFac, (string)runspeedFac.info.Tags[0]);
            AddFloatSlider(poleClimbSpeedFac, (string)poleClimbSpeedFac.info.Tags[0]);
            AddFloatSlider(corridorClimbSpeedFac, (string)corridorClimbSpeedFac.info.Tags[0]);
            DrawFloatSliders(ref Tabs[tabIndex]);


            AddNewLine(4);
            DrawBox(ref Tabs[tabIndex]);

            AddTab(ref tabIndex, "Starving");

            AddDragger(throwingSkillStarving, (string)throwingSkillStarving.info.Tags[0]);
            DrawDraggers(ref Tabs[tabIndex]);

            AddFloatSlider(bodyWeightFacStarving, (string)bodyWeightFacStarving.info.Tags[0]);
            AddFloatSlider(runspeedFacStarving, (string)runspeedFacStarving.info.Tags[0]);
            AddFloatSlider(poleClimbSpeedFacStarving, (string)poleClimbSpeedFacStarving.info.Tags[0]);
            AddFloatSlider(corridorClimbSpeedFacStarving, (string)corridorClimbSpeedFacStarving.info.Tags[0]);
            DrawFloatSliders(ref Tabs[tabIndex]);

            AddNewLine(4);
            DrawBox(ref Tabs[tabIndex]);


            AddTab(ref tabIndex, "Spear Spawns");

            AddFloatSlider(spearSpawnChanceModifier, (string)spearSpawnChanceModifier.info.Tags[0]);
            AddFloatSlider(explosiveSpearSpawnChance, (string)explosiveSpearSpawnChance.info.Tags[0]);
            AddFloatSlider(electricSpearSpawnChance, (string)electricSpearSpawnChance.info.Tags[0]);
            DrawFloatSliders(ref Tabs[tabIndex]);

            AddNewLine(11);
            DrawBox(ref Tabs[tabIndex]);
        }

        #region UI Elements
        private void AddTab(ref int tabIndex, string tabName)
        {
            tabIndex++;
            Tabs[tabIndex] = new OpTab(this, tabName);
            InitializeMarginAndPos();

            AddNewLine();
            AddTextLabel(Plugin.MOD_NAME, bigText: true);
            DrawTextLabels(ref Tabs[tabIndex]);

            AddNewLine(0.5f);
            AddTextLabel("Version " + Plugin.VERSION, FLabelAlignment.Left);
            AddTextLabel("by " + AUTHORS_NAME, FLabelAlignment.Right);
            DrawTextLabels(ref Tabs[tabIndex]);

            AddNewLine();
            AddBox();
        }

        private void InitializeMarginAndPos()
        {
            marginX = new Vector2(50f, 550f);
            pos = new Vector2(50f, 600f);
        }

        private void AddNewLine(float spacingModifier = 1f)
        {
            pos.x = marginX.x; // left margin
            pos.y -= spacingModifier * spacing;
        }

        private void AddFloatSlider(Configurable<float> configurable, string text, string sliderTextLeft = "", string sliderTextRight = "")
        {
            floatSliderConfigurables.Add(configurable);
            floatSliderMainTextLabels.Add(text);
            floatSliderTextLabelsLeft.Add(new OpLabel(new Vector2(), new Vector2(), sliderTextLeft, alignment: FLabelAlignment.Right)); // set pos and size when drawing
            floatSliderTextLabelsRight.Add(new OpLabel(new Vector2(), new Vector2(), sliderTextRight, alignment: FLabelAlignment.Left));
        }

        private void AddDragger(Configurable<int> configurable, string text)
        {
            draggerIntConfigurables.Add(configurable);
            draggerIntTextLabels.Add(new OpLabel(new Vector2(), new Vector2(), text, FLabelAlignment.Left));
        }

        private void DrawFloatSliders(ref OpTab tab)
        {
            if (floatSliderConfigurables.Count != floatSliderMainTextLabels.Count) return;
            if (floatSliderConfigurables.Count != floatSliderTextLabelsLeft.Count) return;
            if (floatSliderConfigurables.Count != floatSliderTextLabelsRight.Count) return;

            float width = marginX.y - marginX.x;
            float sliderCenter = marginX.x + 0.5f * width;
            float sliderLabelSizeX = 0.2f * width;
            float sliderSizeX = width - 2f * sliderLabelSizeX - spacing;

            for (int sliderIndex = 0; sliderIndex < floatSliderConfigurables.Count; ++sliderIndex)
            {
                AddNewLine(2f);

                OpLabel opLabel = floatSliderTextLabelsLeft[sliderIndex];
                opLabel.pos = new Vector2(marginX.x, pos.y + 5f);
                opLabel.size = new Vector2(sliderLabelSizeX, fontHeight);
                tab.AddItems(opLabel);

                Configurable<float> configurable = floatSliderConfigurables[sliderIndex];
                OpFloatSlider slider = new(configurable, new Vector2(sliderCenter - 0.5f * sliderSizeX, pos.y), (int)sliderSizeX)
                {
                    size = new Vector2(sliderSizeX, fontHeight),
                    description = configurable.info?.description ?? ""
                };
                tab.AddItems(slider);

                opLabel = floatSliderTextLabelsRight[sliderIndex];
                opLabel.pos = new Vector2(sliderCenter + 0.5f * sliderSizeX + 0.5f * spacing, pos.y + 5f);
                opLabel.size = new Vector2(sliderLabelSizeX, fontHeight);
                tab.AddItems(opLabel);

                AddTextLabel(floatSliderMainTextLabels[sliderIndex]);
                DrawTextLabels(ref tab);

                if (sliderIndex < floatSliderConfigurables.Count - 1)
                {
                    AddNewLine();
                }
            }

            floatSliderConfigurables.Clear();
            floatSliderMainTextLabels.Clear();
            floatSliderTextLabelsLeft.Clear();
            floatSliderTextLabelsRight.Clear();
        }

        private void DrawDraggers(ref OpTab tab)
        {
            if (draggerIntConfigurables.Count != draggerIntTextLabels.Count) return;

            float width = marginX.y - marginX.x;
            float elementWidth = (width - (numberOfDraggers - 1) * 0.5f * spacing) / numberOfDraggers;
            pos.y -= draggerSize;
            float _posX = pos.x;

            for (int i = 0; i < draggerIntConfigurables.Count; ++i)
            {
                Configurable<int> configurable = draggerIntConfigurables[i];

                OpDragger dragger = new(configurable, new Vector2(_posX, pos.y))
                {
                    description = configurable.info?.description ?? ""
                };
                tab.AddItems(dragger);
                _posX += DraggerWithSpacing;

                OpLabel draggerLabel = draggerIntTextLabels[i];
                draggerLabel.pos = new Vector2(_posX, pos.y + 2f);
                draggerLabel.size = new Vector2(elementWidth - DraggerWithSpacing, fontHeight);
                tab.AddItems(draggerLabel);

                if (i < draggerIntConfigurables.Count - 1)
                {
                    if ((i + 1) % numberOfDraggers == 0)
                    {
                        AddNewLine();
                        pos.y -= draggerSize;
                        _posX = pos.x;
                    }
                    else
                    {
                        _posX += elementWidth - DraggerWithSpacing + 0.5f * spacing;
                    }
                }
            }

            draggerIntConfigurables.Clear();
            draggerIntTextLabels.Clear();
        }

        private void AddBox()
        {
            marginX += new Vector2(spacing, -spacing);
            boxEndPositions.Add(pos.y); // end position > start position
            AddNewLine();
        }

        private void DrawBox(ref OpTab tab)
        {
            marginX += new Vector2(-spacing, spacing);
            AddNewLine();

            float boxWidth = marginX.y - marginX.x;
            int lastIndex = boxEndPositions.Count - 1;

            tab.AddItems(new OpRect(pos, new Vector2(boxWidth, boxEndPositions[lastIndex] - pos.y)));
            boxEndPositions.RemoveAt(lastIndex);
        }

        private void AddCheckBox(Configurable<bool> configurable, string text)
        {
            checkBoxConfigurables.Add(configurable);
            checkBoxesTextLabels.Add(new OpLabel(new Vector2(), new Vector2(), text, FLabelAlignment.Left));
        }

        private void DrawCheckBoxes(ref OpTab tab) // changes pos.y but not pos.x
        {
            if (checkBoxConfigurables.Count != checkBoxesTextLabels.Count) return;

            float width = marginX.y - marginX.x;
            float elementWidth = (width - (numberOfCheckboxes - 1) * 0.5f * spacing) / numberOfCheckboxes;
            pos.y -= checkBoxSize;
            float _posX = pos.x;

            for (int checkBoxIndex = 0; checkBoxIndex < checkBoxConfigurables.Count; ++checkBoxIndex)
            {
                Configurable<bool> configurable = checkBoxConfigurables[checkBoxIndex];
                OpCheckBox checkBox = new(configurable, new Vector2(_posX, pos.y))
                {
                    description = configurable.info?.description ?? ""
                };
                tab.AddItems(checkBox);
                _posX += CheckBoxWithSpacing;

                OpLabel checkBoxLabel = checkBoxesTextLabels[checkBoxIndex];
                checkBoxLabel.pos = new Vector2(_posX, pos.y + 2f);
                checkBoxLabel.size = new Vector2(elementWidth - CheckBoxWithSpacing, fontHeight);
                tab.AddItems(checkBoxLabel);

                if (checkBoxIndex < checkBoxConfigurables.Count - 1)
                {
                    if ((checkBoxIndex + 1) % numberOfCheckboxes == 0)
                    {
                        AddNewLine();
                        pos.y -= checkBoxSize;
                        _posX = pos.x;
                    }
                    else
                    {
                        _posX += elementWidth - CheckBoxWithSpacing + 0.5f * spacing;
                    }
                }
            }

            checkBoxConfigurables.Clear();
            checkBoxesTextLabels.Clear();
        }

        private void AddComboBox(Configurable<string> configurable, List<ListItem> list, string text, bool allowEmpty = false)
        {
            OpLabel opLabel = new(new Vector2(), new Vector2(0.0f, fontHeight), text, FLabelAlignment.Center, false);
            comboBoxesTextLabels.Add(opLabel);
            comboBoxConfigurables.Add(configurable);
            comboBoxLists.Add(list);
            comboBoxAllowEmpty.Add(allowEmpty);
        }

        private void DrawComboBoxes(ref OpTab tab)
        {
            if (comboBoxConfigurables.Count != comboBoxesTextLabels.Count) return;
            if (comboBoxConfigurables.Count != comboBoxLists.Count) return;
            if (comboBoxConfigurables.Count != comboBoxAllowEmpty.Count) return;

            float offsetX = (marginX.y - marginX.x) * 0.1f;
            float width = (marginX.y - marginX.x) * 0.4f;

            for (int comboBoxIndex = 0; comboBoxIndex < comboBoxConfigurables.Count; ++comboBoxIndex)
            {
                AddNewLine(1.25f);
                pos.x += offsetX;

                OpLabel opLabel = comboBoxesTextLabels[comboBoxIndex];
                opLabel.pos = pos;
                opLabel.size += new Vector2(width, 2f); // size.y is already set
                pos.x += width;

                Configurable<string> configurable = comboBoxConfigurables[comboBoxIndex];
                OpComboBox comboBox = new(configurable, pos, width, comboBoxLists[comboBoxIndex])
                {
                    allowEmpty = comboBoxAllowEmpty[comboBoxIndex],
                    description = configurable.info?.description ?? ""
                };
                tab.AddItems(opLabel, comboBox);

                // don't add a new line on the last element
                if (comboBoxIndex < comboBoxConfigurables.Count - 1)
                {
                    AddNewLine();
                    pos.x = marginX.x;
                }
            }

            comboBoxesTextLabels.Clear();
            comboBoxConfigurables.Clear();
            comboBoxLists.Clear();
            comboBoxAllowEmpty.Clear();
        }

        private void AddSlider(Configurable<int> configurable, string text, string sliderTextLeft = "", string sliderTextRight = "")
        {
            sliderConfigurables.Add(configurable);
            sliderMainTextLabels.Add(text);
            sliderTextLabelsLeft.Add(new OpLabel(new Vector2(), new Vector2(), sliderTextLeft, alignment: FLabelAlignment.Right)); // set pos and size when drawing
            sliderTextLabelsRight.Add(new OpLabel(new Vector2(), new Vector2(), sliderTextRight, alignment: FLabelAlignment.Left));
        }

        private void DrawSliders(ref OpTab tab)
        {
            if (sliderConfigurables.Count != sliderMainTextLabels.Count) return;
            if (sliderConfigurables.Count != sliderTextLabelsLeft.Count) return;
            if (sliderConfigurables.Count != sliderTextLabelsRight.Count) return;

            float width = marginX.y - marginX.x;
            float sliderCenter = marginX.x + 0.5f * width;
            float sliderLabelSizeX = 0.2f * width;
            float sliderSizeX = width - 2f * sliderLabelSizeX - spacing;

            for (int sliderIndex = 0; sliderIndex < sliderConfigurables.Count; ++sliderIndex)
            {
                AddNewLine(2f);

                OpLabel opLabel = sliderTextLabelsLeft[sliderIndex];
                opLabel.pos = new Vector2(marginX.x, pos.y + 5f);
                opLabel.size = new Vector2(sliderLabelSizeX, fontHeight);
                tab.AddItems(opLabel);

                Configurable<int> configurable = sliderConfigurables[sliderIndex];
                OpSlider slider = new(configurable, new Vector2(sliderCenter - 0.5f * sliderSizeX, pos.y), (int)sliderSizeX)
                {
                    size = new Vector2(sliderSizeX, fontHeight),
                    description = configurable.info?.description ?? ""
                };
                tab.AddItems(slider);

                opLabel = sliderTextLabelsRight[sliderIndex];
                opLabel.pos = new Vector2(sliderCenter + 0.5f * sliderSizeX + 0.5f * spacing, pos.y + 5f);
                opLabel.size = new Vector2(sliderLabelSizeX, fontHeight);
                tab.AddItems(opLabel);

                AddTextLabel(sliderMainTextLabels[sliderIndex]);
                DrawTextLabels(ref tab);

                if (sliderIndex < sliderConfigurables.Count - 1)
                {
                    AddNewLine();
                }
            }

            sliderConfigurables.Clear();
            sliderMainTextLabels.Clear();
            sliderTextLabelsLeft.Clear();
            sliderTextLabelsRight.Clear();
        }

        private void AddTextLabel(string text, FLabelAlignment alignment = FLabelAlignment.Center, bool bigText = false)
        {
            float textHeight = (bigText ? 2f : 1f) * fontHeight;
            if (textLabels.Count == 0)
            {
                pos.y -= textHeight;
            }

            OpLabel textLabel = new(new Vector2(), new Vector2(20f, textHeight), text, alignment, bigText) // minimal size.x = 20f
            {
                autoWrap = true
            };
            textLabels.Add(textLabel);
        }

        private void DrawTextLabels(ref OpTab tab)
        {
            if (textLabels.Count == 0)
            {
                return;
            }

            float width = (marginX.y - marginX.x) / textLabels.Count;
            foreach (OpLabel textLabel in textLabels)
            {
                textLabel.pos = pos;
                textLabel.size += new Vector2(width - 20f, 0.0f);
                tab.AddItems(textLabel);
                pos.x += width;
            }

            pos.x = marginX.x;
            textLabels.Clear();
        }
        #endregion
    }
}