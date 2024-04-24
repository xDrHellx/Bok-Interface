using System.Collections.Generic;
using System.Windows.Forms;

using BokInterface.Addresses;
using BokInterface.All;

/**
 * File for the Zoktai (Boktai 2) interface itself
 */

namespace BokInterface {

    partial class BokInterface {

        #region Properties

        private readonly ZoktaiAddresses zoktaiAddresses = new();
        private Label bok2_currentStatusHpValue = new();
        private Label bok2_currentStatusEneValue = new();
        private Label bok2_djangoLevel = new();
        private Label bok2_djangoExp = new();
        private Label bok2_djangoVit = new();
        private Label bok2_djangoSpr = new();
        private Label bok2_djangoStr = new();
        private Label bok2_djangoAgi = new();
        private Label bok2_djangoStatPoints = new();
        private Label bok2_djangoSwordSkill = new();
        private Label bok2_djangoSpearSkill = new();
        private Label bok2_djangoHammerSkill = new();
        private Label bok2_djangoFistsSkill = new();
        private Label bok2_djangoGunSkill = new();
        private Button bok2_editStatusBtn = new();
        private Button bok2_editInventoryBtn = new();
        private Button bok2_editEquipsBtn = new();
        private Button bok2_editWeaponsBtn = new();
        private Button bok2_editMagicsBtn = new();
        private GroupBox bok2_currentSkillGroupBox = new();
        private readonly List<Label> bok2_currentSkillLabels = [];

        #endregion

        private void ShowZoktaiInterface() {

            // If JP version, update the game name label to add the version
            string version = "";
            int gameNameLabelWidth = 145;
            if (currentGameId == 1244803925) {
                version = Utilities.GetGameVersion() == 1 ? " (v1.1)" : " (v1.0)";
                gameNameLabelWidth = 180;
            }

            // Current game name
            CreateLabel("currentGameName", currentGameName + version, 5, 5, gameNameLabelWidth, 20, true);

            // Current status section
            AddZoktaiCurrentStatusSection();

            // Stats section
            AddZoktaiCurrentStatsSection();

            // Skill sections
            AddZoktaiCurrentSkillSection();

            // Values setting / editing section
            AddZoktaiEditSection();

            // Extras / misc tools section
            AddToolsSection();

            // Main window
            SetMainWindow("Bok Interface" + (shorterGameName != "" ? " - " + shorterGameName : ""), 345, 500);

            ResumeLayout(false);
        }

        private void UpdateZoktaiInterface() {

            // Get one of the values used for reading current stats
            uint currentStat = zoktaiAddresses.Misc["current_stat"].Value;
            /**
             * Update values by retrieving from memory addresses
             * 
             * In some cases we only update when the values are "valid"
             * For example "stat" is 0 during room transitions or at the title screen
             */
            if (currentStat > 0) {
                bok2_currentStatusHpValue.Text = memoryValues.Django["current_hp"].Value.ToString();
                bok2_currentStatusEneValue.Text = memoryValues.Django["current_ene"].Value.ToString();

                bok2_djangoLevel.Text = memoryValues.Django["level"].Value.ToString();
                bok2_djangoExp.Text = memoryValues.Django["exp"].Value.ToString();

                bok2_djangoVit.Text = memoryValues.Misc["vit"].Value.ToString();
                bok2_djangoSpr.Text = memoryValues.Misc["spr"].Value.ToString();
                bok2_djangoStr.Text = memoryValues.Misc["str"].Value.ToString();
                bok2_djangoAgi.Text = memoryValues.Misc["agi"].Value.ToString();
                bok2_djangoStatPoints.Text = memoryValues.Django["stat_points"].Value.ToString();

                bok2_djangoSwordSkill.Text = Utilities.ExpToLevel(memoryValues.Django["sword_skill"].Value).ToString();
                bok2_djangoSpearSkill.Text = Utilities.ExpToLevel(memoryValues.Django["spear_skill"].Value).ToString();
                bok2_djangoHammerSkill.Text = Utilities.ExpToLevel(memoryValues.Django["hammer_skill"].Value).ToString();
                bok2_djangoFistsSkill.Text = Utilities.ExpToLevel(memoryValues.Django["fists_skill"].Value).ToString();
                bok2_djangoGunSkill.Text = Utilities.ExpToLevel(memoryValues.Django["gun_skill"].Value).ToString();

                // Add / refresh tooltips for skill containing the EXP amount
                AddToolTip(bok2_djangoSwordSkill, memoryValues.Django["sword_skill"].Value + " EXP");
                AddToolTip(bok2_djangoSpearSkill, memoryValues.Django["spear_skill"].Value + " EXP");
                AddToolTip(bok2_djangoHammerSkill, memoryValues.Django["hammer_skill"].Value + " EXP");
                AddToolTip(bok2_djangoFistsSkill, memoryValues.Django["fists_skill"].Value + " EXP");
                AddToolTip(bok2_djangoGunSkill, memoryValues.Django["gun_skill"].Value + " EXP");
            }
        }

        private void AddZoktaiCurrentStatusSection() {

            // Section
            currentStatusGroupBox = CreateGroupBox("currentStatus", "Current status", 5, 25, 226, 55, true);

            // Current status labels
            currentStatusLabels.Add(CreateLabel("djangoCurrentHpLabel", "LIFE :", 7, 19, 34, 15));
            currentStatusLabels.Add(CreateLabel("djangoCurrentEneLabel", "ENE :", 7, 34, 34, 15));
            currentStatusLabels.Add(CreateLabel("djangoCurrentLevelLabel", "Level :", 93, 19, 40, 15));
            currentStatusLabels.Add(CreateLabel("djangoCurrentExpLabel", "EXP :", 93, 34, 33, 15));

            // Current status values
            bok2_currentStatusHpValue = CreateLabel("djangoCurrentHpValue", "", 44, 19, 31, 15);
            bok2_currentStatusEneValue = CreateLabel("djangoCurrentHpValue", "", 44, 34, 31, 15);
            bok2_djangoLevel = CreateLabel("djangoCurrentLevelValue", "", 132, 19, 31, 15);
            bok2_djangoExp = CreateLabel("djangoCurrentExpValue", "", 132, 34, 43, 15);

            // Add values labels to group
            currentStatusLabels.Add(bok2_currentStatusHpValue);
            currentStatusLabels.Add(bok2_currentStatusEneValue);
            currentStatusLabels.Add(bok2_djangoLevel);
            currentStatusLabels.Add(bok2_djangoExp);

            // Add elements to group
            for (int i = 0; i < currentStatusLabels.Count; i++) {
                currentStatusGroupBox.Controls.Add(currentStatusLabels[i]);
            }
        }

        private void AddZoktaiCurrentSkillSection() {

            // Section
            bok2_currentSkillGroupBox = CreateGroupBox("currentSkill", "Skill", 92, 86, 110, 104, true);

            // Sword
            bok2_currentSkillLabels.Add(CreateLabel("swordSkillLabel", "Sword", 6, 20, 54, 15, textAlignment: "MiddleLeft"));
            bok2_djangoSwordSkill = CreateLabel("djangoSwordSkill", "", 60, 19, 41, 15, colorHex: totalStatColor, textAlignment: "MiddleLeft");

            // Spear
            bok2_currentSkillLabels.Add(CreateLabel("spearSkillLabel", "Spear", 6, 34, 54, 15, textAlignment: "MiddleLeft"));
            bok2_djangoSpearSkill = CreateLabel("djangoSpearSkill", "", 60, 34, 41, 15, colorHex: totalStatColor, textAlignment: "MiddleLeft");

            // Hammer
            bok2_currentSkillLabels.Add(CreateLabel("hammerSkillLabel", "Hammer", 6, 49, 54, 15, textAlignment: "MiddleLeft"));
            bok2_djangoHammerSkill = CreateLabel("djangoHammerSkill", "", 60, 49, 41, 15, colorHex: totalStatColor, textAlignment: "MiddleLeft");

            // Fists
            bok2_currentSkillLabels.Add(CreateLabel("fistsSkillLabel", "Fists", 6, 64, 54, 15, textAlignment: "MiddleLeft"));
            bok2_djangoFistsSkill = CreateLabel("djangoFistsSkill", "", 60, 64, 41, 15, colorHex: totalStatColor, textAlignment: "MiddleLeft");

            // Gun
            bok2_currentSkillLabels.Add(CreateLabel("gunSkillLabel", "Gun", 6, 79, 54, 15, textAlignment: "MiddleLeft"));
            bok2_djangoGunSkill = CreateLabel("djangoGunSkill", "", 60, 79, 41, 15, colorHex: totalStatColor, textAlignment: "MiddleLeft");

            // Add values labels to group
            bok2_currentSkillLabels.Add(bok2_djangoSwordSkill);
            bok2_currentSkillLabels.Add(bok2_djangoSpearSkill);
            bok2_currentSkillLabels.Add(bok2_djangoHammerSkill);
            bok2_currentSkillLabels.Add(bok2_djangoFistsSkill);
            bok2_currentSkillLabels.Add(bok2_djangoGunSkill);

            for (int i = 0; i < bok2_currentSkillLabels.Count; i++) {
                edit_statusLabels.Add(bok2_currentSkillLabels[i]);
            }

            // Add elements to group
            for (int i = 0; i < bok2_currentSkillLabels.Count; i++) {
                bok2_currentSkillGroupBox.Controls.Add(bok2_currentSkillLabels[i]);
            }
        }

        private void AddZoktaiCurrentStatsSection() {

            // Section
            currentStatsGroupBox = CreateGroupBox("currentStats", "Stats", 5, 86, 75, 106, true);

            // VIT
            currentStatsLabels.Add(CreateLabel("vitRowLabel", "VIT", 6, 19, 27, 15, textAlignment: "MiddleLeft"));
            bok2_djangoVit = CreateLabel("djangoVit", "", 35, 19, 31, 15, colorHex: baseStatColor, textAlignment: "MiddleRight");

            // SPR
            currentStatsLabels.Add(CreateLabel("sprRowLabel", "SPR", 6, 34, 27, 15, textAlignment: "MiddleLeft"));
            bok2_djangoSpr = CreateLabel("djangoSpr", "", 35, 34, 31, 15, colorHex: baseStatColor, textAlignment: "MiddleRight");

            // STR
            currentStatsLabels.Add(CreateLabel("strRowLabel", "STR", 6, 49, 27, 15, textAlignment: "MiddleLeft"));
            bok2_djangoStr = CreateLabel("djangoStr", "", 35, 49, 31, 15, colorHex: baseStatColor, textAlignment: "MiddleRight");

            // AGI
            currentStatsLabels.Add(CreateLabel("agiRowLabel", "AGI", 6, 64, 27, 15, textAlignment: "MiddleLeft"));
            bok2_djangoAgi = CreateLabel("djangoAgi", "", 35, 64, 31, 15, colorHex: baseStatColor, textAlignment: "MiddleRight");

            // Stat points to allocate
            currentStatsLabels.Add(CreateLabel("statPointsLabel", "Add", 6, 84, 29, 15, textAlignment: "MiddleLeft"));
            bok2_djangoStatPoints = CreateLabel("djangoStatPoints", "", 35, 84, 31, 15, colorHex: totalStatColor, textAlignment: "MiddleRight");

            // Add values labels to group
            currentStatsLabels.Add(bok2_djangoVit);
            currentStatsLabels.Add(bok2_djangoSpr);
            currentStatsLabels.Add(bok2_djangoStr);
            currentStatsLabels.Add(bok2_djangoAgi);
            currentStatsLabels.Add(bok2_djangoStatPoints);

            // Add elements to group
            for (int i = 0; i < currentStatsLabels.Count; i++) {
                currentStatsGroupBox.Controls.Add(currentStatsLabels[i]);
            }
        }

        private void AddZoktaiEditSection() {

            // Section
            editGroupBox = CreateGroupBox("editButtons", "Edit", 237, 25, 87, 157, true);

            bok2_editStatusBtn = CreateButton("editStatuts", "Status", 6, 19, 75, 23);
            bok2_editInventoryBtn = CreateButton("editItems", "Items", 6, 46, 75, 23);
            bok2_editEquipsBtn = CreateButton("editEquips", "Equips", 6, 73, 75, 23);
            bok2_editWeaponsBtn = CreateButton("editWeapons", "Weapons", 6, 100, 75, 23);
            bok2_editMagicsBtn = CreateButton("editMagics", "Magics", 6, 127, 75, 23);

            // WIP features are disabled for now
            bok2_editInventoryBtn.Enabled = false;
            bok2_editEquipsBtn.Enabled = false;
            bok2_editWeaponsBtn.Enabled = false;
            bok2_editMagicsBtn.Enabled = false;

            // Add onclick events
            bok2_editStatusBtn.Click += new System.EventHandler(OpenStatusEditor);
            // this.bok2_editInventoryBtn.Click += new System.EventHandler(this.OpenInventoryEditor);
            // this.bok2_editEquipsBtn.Click += new System.EventHandler(this.OpenEquipsEditor);
            // this.bok2_editWeaponsBtn.Click += new System.EventHandler(this.OpenWeaponsEditor);
            // this.bok2_editMagicsBtn.Click += new System.EventHandler(this.OpenMagicsEditor);

            // Add buttons to group
            editButtons.Add(bok2_editStatusBtn);
            editButtons.Add(bok2_editInventoryBtn);
            editButtons.Add(bok2_editEquipsBtn);
            editButtons.Add(bok2_editWeaponsBtn);
            editButtons.Add(bok2_editMagicsBtn);

            for (int i = 0; i < editButtons.Count; i++) {
                editGroupBox.Controls.Add(editButtons[i]);
            }
        }
    }
}
