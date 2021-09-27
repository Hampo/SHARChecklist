
namespace SHARChecklist
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.LblCredits = new System.Windows.Forms.Label();
            this.CMSMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.topmostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formBorderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LblPercentageCompleteLabel = new System.Windows.Forms.Label();
            this.LblStoryMissionsLabel = new System.Windows.Forms.Label();
            this.LblBonusMissionsLabel = new System.Windows.Forms.Label();
            this.LblStreetRacesLabel = new System.Windows.Forms.Label();
            this.LblCollectorCardsLabel = new System.Windows.Forms.Label();
            this.LblCharacterClothingLabel = new System.Windows.Forms.Label();
            this.LblVehiclesLabel = new System.Windows.Forms.Label();
            this.LblWaspCamerasLabel = new System.Windows.Forms.Label();
            this.LblGagsLabel = new System.Windows.Forms.Label();
            this.LblMoviesLabel = new System.Windows.Forms.Label();
            this.LblPercentageComplete = new System.Windows.Forms.Label();
            this.LblStoryMissions = new System.Windows.Forms.Label();
            this.LblBonusMissions = new System.Windows.Forms.Label();
            this.LblStreetRaces = new System.Windows.Forms.Label();
            this.LblCollectorCards = new System.Windows.Forms.Label();
            this.LblCharacterClothing = new System.Windows.Forms.Label();
            this.LblVehicles = new System.Windows.Forms.Label();
            this.LblWaspCameras = new System.Windows.Forms.Label();
            this.LblGags = new System.Windows.Forms.Label();
            this.LblMovies = new System.Windows.Forms.Label();
            this.LblStoryMissionsTotal = new System.Windows.Forms.Label();
            this.LblBonusMissionsTotal = new System.Windows.Forms.Label();
            this.LblStreetRacesTotal = new System.Windows.Forms.Label();
            this.LblCollectorCardsTotal = new System.Windows.Forms.Label();
            this.LblCharacterClothingTotal = new System.Windows.Forms.Label();
            this.LblVehiclesTotal = new System.Windows.Forms.Label();
            this.LblWaspCamerasTotal = new System.Windows.Forms.Label();
            this.LblGagsTotal = new System.Windows.Forms.Label();
            this.LblMoviesTotal = new System.Windows.Forms.Label();
            this.TmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.CMSMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblCredits
            // 
            this.LblCredits.ContextMenuStrip = this.CMSMain;
            this.LblCredits.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LblCredits.Location = new System.Drawing.Point(0, 198);
            this.LblCredits.Name = "LblCredits";
            this.LblCredits.Size = new System.Drawing.Size(201, 39);
            this.LblCredits.TabIndex = 0;
            this.LblCredits.Text = "Lucas Cardellini: Memory mapping\r\nzoton2: Original checklist\r\nProddy: This checkl" +
    "ist";
            this.LblCredits.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.LblCredits.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // CMSMain
            // 
            this.CMSMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.topmostToolStripMenuItem,
            this.formBorderToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.CMSMain.Name = "CMSMain";
            this.CMSMain.Size = new System.Drawing.Size(141, 76);
            // 
            // topmostToolStripMenuItem
            // 
            this.topmostToolStripMenuItem.CheckOnClick = true;
            this.topmostToolStripMenuItem.Name = "topmostToolStripMenuItem";
            this.topmostToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.topmostToolStripMenuItem.Text = "Topmost";
            this.topmostToolStripMenuItem.CheckedChanged += new System.EventHandler(this.topmostToolStripMenuItem_CheckedChanged);
            // 
            // formBorderToolStripMenuItem
            // 
            this.formBorderToolStripMenuItem.Checked = true;
            this.formBorderToolStripMenuItem.CheckOnClick = true;
            this.formBorderToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.formBorderToolStripMenuItem.Name = "formBorderToolStripMenuItem";
            this.formBorderToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.formBorderToolStripMenuItem.Text = "Form border";
            this.formBorderToolStripMenuItem.CheckedChanged += new System.EventHandler(this.formBorderToolStripMenuItem_CheckedChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(137, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // LblPercentageCompleteLabel
            // 
            this.LblPercentageCompleteLabel.AutoSize = true;
            this.LblPercentageCompleteLabel.ContextMenuStrip = this.CMSMain;
            this.LblPercentageCompleteLabel.Location = new System.Drawing.Point(12, 9);
            this.LblPercentageCompleteLabel.Name = "LblPercentageCompleteLabel";
            this.LblPercentageCompleteLabel.Size = new System.Drawing.Size(118, 13);
            this.LblPercentageCompleteLabel.TabIndex = 1;
            this.LblPercentageCompleteLabel.Text = "Percentage Completed:";
            this.LblPercentageCompleteLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblStoryMissionsLabel
            // 
            this.LblStoryMissionsLabel.AutoSize = true;
            this.LblStoryMissionsLabel.ContextMenuStrip = this.CMSMain;
            this.LblStoryMissionsLabel.Location = new System.Drawing.Point(12, 27);
            this.LblStoryMissionsLabel.Name = "LblStoryMissionsLabel";
            this.LblStoryMissionsLabel.Size = new System.Drawing.Size(77, 13);
            this.LblStoryMissionsLabel.TabIndex = 2;
            this.LblStoryMissionsLabel.Text = "Story Missions:";
            this.LblStoryMissionsLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblBonusMissionsLabel
            // 
            this.LblBonusMissionsLabel.AutoSize = true;
            this.LblBonusMissionsLabel.ContextMenuStrip = this.CMSMain;
            this.LblBonusMissionsLabel.Location = new System.Drawing.Point(12, 45);
            this.LblBonusMissionsLabel.Name = "LblBonusMissionsLabel";
            this.LblBonusMissionsLabel.Size = new System.Drawing.Size(83, 13);
            this.LblBonusMissionsLabel.TabIndex = 3;
            this.LblBonusMissionsLabel.Text = "Bonus Missions:";
            this.LblBonusMissionsLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblStreetRacesLabel
            // 
            this.LblStreetRacesLabel.AutoSize = true;
            this.LblStreetRacesLabel.ContextMenuStrip = this.CMSMain;
            this.LblStreetRacesLabel.Location = new System.Drawing.Point(12, 63);
            this.LblStreetRacesLabel.Name = "LblStreetRacesLabel";
            this.LblStreetRacesLabel.Size = new System.Drawing.Size(72, 13);
            this.LblStreetRacesLabel.TabIndex = 4;
            this.LblStreetRacesLabel.Text = "Street Races:";
            this.LblStreetRacesLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblCollectorCardsLabel
            // 
            this.LblCollectorCardsLabel.AutoSize = true;
            this.LblCollectorCardsLabel.ContextMenuStrip = this.CMSMain;
            this.LblCollectorCardsLabel.Location = new System.Drawing.Point(12, 81);
            this.LblCollectorCardsLabel.Name = "LblCollectorCardsLabel";
            this.LblCollectorCardsLabel.Size = new System.Drawing.Size(81, 13);
            this.LblCollectorCardsLabel.TabIndex = 5;
            this.LblCollectorCardsLabel.Text = "Collector Cards:";
            this.LblCollectorCardsLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblCharacterClothingLabel
            // 
            this.LblCharacterClothingLabel.AutoSize = true;
            this.LblCharacterClothingLabel.ContextMenuStrip = this.CMSMain;
            this.LblCharacterClothingLabel.Location = new System.Drawing.Point(12, 99);
            this.LblCharacterClothingLabel.Name = "LblCharacterClothingLabel";
            this.LblCharacterClothingLabel.Size = new System.Drawing.Size(97, 13);
            this.LblCharacterClothingLabel.TabIndex = 6;
            this.LblCharacterClothingLabel.Text = "Character Clothing:";
            this.LblCharacterClothingLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblVehiclesLabel
            // 
            this.LblVehiclesLabel.AutoSize = true;
            this.LblVehiclesLabel.ContextMenuStrip = this.CMSMain;
            this.LblVehiclesLabel.Location = new System.Drawing.Point(12, 117);
            this.LblVehiclesLabel.Name = "LblVehiclesLabel";
            this.LblVehiclesLabel.Size = new System.Drawing.Size(50, 13);
            this.LblVehiclesLabel.TabIndex = 7;
            this.LblVehiclesLabel.Text = "Vehicles:";
            this.LblVehiclesLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblWaspCamerasLabel
            // 
            this.LblWaspCamerasLabel.AutoSize = true;
            this.LblWaspCamerasLabel.ContextMenuStrip = this.CMSMain;
            this.LblWaspCamerasLabel.Location = new System.Drawing.Point(12, 135);
            this.LblWaspCamerasLabel.Name = "LblWaspCamerasLabel";
            this.LblWaspCamerasLabel.Size = new System.Drawing.Size(82, 13);
            this.LblWaspCamerasLabel.TabIndex = 8;
            this.LblWaspCamerasLabel.Text = "Wasp Cameras:";
            this.LblWaspCamerasLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblGagsLabel
            // 
            this.LblGagsLabel.AutoSize = true;
            this.LblGagsLabel.ContextMenuStrip = this.CMSMain;
            this.LblGagsLabel.Location = new System.Drawing.Point(12, 153);
            this.LblGagsLabel.Name = "LblGagsLabel";
            this.LblGagsLabel.Size = new System.Drawing.Size(35, 13);
            this.LblGagsLabel.TabIndex = 9;
            this.LblGagsLabel.Text = "Gags:";
            this.LblGagsLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblMoviesLabel
            // 
            this.LblMoviesLabel.AutoSize = true;
            this.LblMoviesLabel.ContextMenuStrip = this.CMSMain;
            this.LblMoviesLabel.Location = new System.Drawing.Point(12, 171);
            this.LblMoviesLabel.Name = "LblMoviesLabel";
            this.LblMoviesLabel.Size = new System.Drawing.Size(44, 13);
            this.LblMoviesLabel.TabIndex = 10;
            this.LblMoviesLabel.Text = "Movies:";
            this.LblMoviesLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblPercentageComplete
            // 
            this.LblPercentageComplete.AutoSize = true;
            this.LblPercentageComplete.ContextMenuStrip = this.CMSMain;
            this.LblPercentageComplete.Location = new System.Drawing.Point(128, 9);
            this.LblPercentageComplete.Name = "LblPercentageComplete";
            this.LblPercentageComplete.Size = new System.Drawing.Size(21, 13);
            this.LblPercentageComplete.TabIndex = 11;
            this.LblPercentageComplete.Text = "0%";
            this.LblPercentageComplete.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblStoryMissions
            // 
            this.LblStoryMissions.AutoSize = true;
            this.LblStoryMissions.ContextMenuStrip = this.CMSMain;
            this.LblStoryMissions.Location = new System.Drawing.Point(128, 27);
            this.LblStoryMissions.Name = "LblStoryMissions";
            this.LblStoryMissions.Size = new System.Drawing.Size(13, 13);
            this.LblStoryMissions.TabIndex = 12;
            this.LblStoryMissions.Text = "0";
            this.LblStoryMissions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblBonusMissions
            // 
            this.LblBonusMissions.AutoSize = true;
            this.LblBonusMissions.ContextMenuStrip = this.CMSMain;
            this.LblBonusMissions.Location = new System.Drawing.Point(128, 45);
            this.LblBonusMissions.Name = "LblBonusMissions";
            this.LblBonusMissions.Size = new System.Drawing.Size(13, 13);
            this.LblBonusMissions.TabIndex = 13;
            this.LblBonusMissions.Text = "0";
            this.LblBonusMissions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblStreetRaces
            // 
            this.LblStreetRaces.AutoSize = true;
            this.LblStreetRaces.ContextMenuStrip = this.CMSMain;
            this.LblStreetRaces.Location = new System.Drawing.Point(128, 63);
            this.LblStreetRaces.Name = "LblStreetRaces";
            this.LblStreetRaces.Size = new System.Drawing.Size(13, 13);
            this.LblStreetRaces.TabIndex = 14;
            this.LblStreetRaces.Text = "0";
            this.LblStreetRaces.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblCollectorCards
            // 
            this.LblCollectorCards.AutoSize = true;
            this.LblCollectorCards.ContextMenuStrip = this.CMSMain;
            this.LblCollectorCards.Location = new System.Drawing.Point(128, 81);
            this.LblCollectorCards.Name = "LblCollectorCards";
            this.LblCollectorCards.Size = new System.Drawing.Size(13, 13);
            this.LblCollectorCards.TabIndex = 15;
            this.LblCollectorCards.Text = "0";
            this.LblCollectorCards.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblCharacterClothing
            // 
            this.LblCharacterClothing.AutoSize = true;
            this.LblCharacterClothing.ContextMenuStrip = this.CMSMain;
            this.LblCharacterClothing.Location = new System.Drawing.Point(128, 99);
            this.LblCharacterClothing.Name = "LblCharacterClothing";
            this.LblCharacterClothing.Size = new System.Drawing.Size(13, 13);
            this.LblCharacterClothing.TabIndex = 16;
            this.LblCharacterClothing.Text = "0";
            this.LblCharacterClothing.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblVehicles
            // 
            this.LblVehicles.AutoSize = true;
            this.LblVehicles.ContextMenuStrip = this.CMSMain;
            this.LblVehicles.Location = new System.Drawing.Point(128, 117);
            this.LblVehicles.Name = "LblVehicles";
            this.LblVehicles.Size = new System.Drawing.Size(13, 13);
            this.LblVehicles.TabIndex = 17;
            this.LblVehicles.Text = "0";
            this.LblVehicles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblWaspCameras
            // 
            this.LblWaspCameras.AutoSize = true;
            this.LblWaspCameras.ContextMenuStrip = this.CMSMain;
            this.LblWaspCameras.Location = new System.Drawing.Point(128, 135);
            this.LblWaspCameras.Name = "LblWaspCameras";
            this.LblWaspCameras.Size = new System.Drawing.Size(13, 13);
            this.LblWaspCameras.TabIndex = 18;
            this.LblWaspCameras.Text = "0";
            this.LblWaspCameras.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblGags
            // 
            this.LblGags.AutoSize = true;
            this.LblGags.ContextMenuStrip = this.CMSMain;
            this.LblGags.Location = new System.Drawing.Point(128, 153);
            this.LblGags.Name = "LblGags";
            this.LblGags.Size = new System.Drawing.Size(13, 13);
            this.LblGags.TabIndex = 19;
            this.LblGags.Text = "0";
            this.LblGags.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblMovies
            // 
            this.LblMovies.AutoSize = true;
            this.LblMovies.ContextMenuStrip = this.CMSMain;
            this.LblMovies.Location = new System.Drawing.Point(128, 171);
            this.LblMovies.Name = "LblMovies";
            this.LblMovies.Size = new System.Drawing.Size(13, 13);
            this.LblMovies.TabIndex = 20;
            this.LblMovies.Text = "0";
            this.LblMovies.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblStoryMissionsTotal
            // 
            this.LblStoryMissionsTotal.AutoSize = true;
            this.LblStoryMissionsTotal.ContextMenuStrip = this.CMSMain;
            this.LblStoryMissionsTotal.Location = new System.Drawing.Point(161, 27);
            this.LblStoryMissionsTotal.Name = "LblStoryMissionsTotal";
            this.LblStoryMissionsTotal.Size = new System.Drawing.Size(18, 13);
            this.LblStoryMissionsTotal.TabIndex = 21;
            this.LblStoryMissionsTotal.Text = "/?";
            this.LblStoryMissionsTotal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblBonusMissionsTotal
            // 
            this.LblBonusMissionsTotal.AutoSize = true;
            this.LblBonusMissionsTotal.ContextMenuStrip = this.CMSMain;
            this.LblBonusMissionsTotal.Location = new System.Drawing.Point(161, 45);
            this.LblBonusMissionsTotal.Name = "LblBonusMissionsTotal";
            this.LblBonusMissionsTotal.Size = new System.Drawing.Size(18, 13);
            this.LblBonusMissionsTotal.TabIndex = 22;
            this.LblBonusMissionsTotal.Text = "/?";
            this.LblBonusMissionsTotal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblStreetRacesTotal
            // 
            this.LblStreetRacesTotal.AutoSize = true;
            this.LblStreetRacesTotal.ContextMenuStrip = this.CMSMain;
            this.LblStreetRacesTotal.Location = new System.Drawing.Point(161, 63);
            this.LblStreetRacesTotal.Name = "LblStreetRacesTotal";
            this.LblStreetRacesTotal.Size = new System.Drawing.Size(18, 13);
            this.LblStreetRacesTotal.TabIndex = 23;
            this.LblStreetRacesTotal.Text = "/?";
            this.LblStreetRacesTotal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblCollectorCardsTotal
            // 
            this.LblCollectorCardsTotal.AutoSize = true;
            this.LblCollectorCardsTotal.ContextMenuStrip = this.CMSMain;
            this.LblCollectorCardsTotal.Location = new System.Drawing.Point(161, 81);
            this.LblCollectorCardsTotal.Name = "LblCollectorCardsTotal";
            this.LblCollectorCardsTotal.Size = new System.Drawing.Size(18, 13);
            this.LblCollectorCardsTotal.TabIndex = 24;
            this.LblCollectorCardsTotal.Text = "/?";
            this.LblCollectorCardsTotal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblCharacterClothingTotal
            // 
            this.LblCharacterClothingTotal.AutoSize = true;
            this.LblCharacterClothingTotal.ContextMenuStrip = this.CMSMain;
            this.LblCharacterClothingTotal.Location = new System.Drawing.Point(161, 99);
            this.LblCharacterClothingTotal.Name = "LblCharacterClothingTotal";
            this.LblCharacterClothingTotal.Size = new System.Drawing.Size(18, 13);
            this.LblCharacterClothingTotal.TabIndex = 25;
            this.LblCharacterClothingTotal.Text = "/?";
            this.LblCharacterClothingTotal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblVehiclesTotal
            // 
            this.LblVehiclesTotal.AutoSize = true;
            this.LblVehiclesTotal.ContextMenuStrip = this.CMSMain;
            this.LblVehiclesTotal.Location = new System.Drawing.Point(161, 117);
            this.LblVehiclesTotal.Name = "LblVehiclesTotal";
            this.LblVehiclesTotal.Size = new System.Drawing.Size(18, 13);
            this.LblVehiclesTotal.TabIndex = 26;
            this.LblVehiclesTotal.Text = "/?";
            this.LblVehiclesTotal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblWaspCamerasTotal
            // 
            this.LblWaspCamerasTotal.AutoSize = true;
            this.LblWaspCamerasTotal.ContextMenuStrip = this.CMSMain;
            this.LblWaspCamerasTotal.Location = new System.Drawing.Point(161, 135);
            this.LblWaspCamerasTotal.Name = "LblWaspCamerasTotal";
            this.LblWaspCamerasTotal.Size = new System.Drawing.Size(18, 13);
            this.LblWaspCamerasTotal.TabIndex = 27;
            this.LblWaspCamerasTotal.Text = "/?";
            this.LblWaspCamerasTotal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblGagsTotal
            // 
            this.LblGagsTotal.AutoSize = true;
            this.LblGagsTotal.ContextMenuStrip = this.CMSMain;
            this.LblGagsTotal.Location = new System.Drawing.Point(161, 153);
            this.LblGagsTotal.Name = "LblGagsTotal";
            this.LblGagsTotal.Size = new System.Drawing.Size(18, 13);
            this.LblGagsTotal.TabIndex = 28;
            this.LblGagsTotal.Text = "/?";
            this.LblGagsTotal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // LblMoviesTotal
            // 
            this.LblMoviesTotal.AutoSize = true;
            this.LblMoviesTotal.ContextMenuStrip = this.CMSMain;
            this.LblMoviesTotal.Location = new System.Drawing.Point(161, 171);
            this.LblMoviesTotal.Name = "LblMoviesTotal";
            this.LblMoviesTotal.Size = new System.Drawing.Size(18, 13);
            this.LblMoviesTotal.TabIndex = 29;
            this.LblMoviesTotal.Text = "/?";
            this.LblMoviesTotal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            // 
            // TmrUpdate
            // 
            this.TmrUpdate.Interval = 1000;
            this.TmrUpdate.Tick += new System.EventHandler(this.TmrUpdate_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(201, 237);
            this.ContextMenuStrip = this.CMSMain;
            this.Controls.Add(this.LblMoviesTotal);
            this.Controls.Add(this.LblGagsTotal);
            this.Controls.Add(this.LblWaspCamerasTotal);
            this.Controls.Add(this.LblVehiclesTotal);
            this.Controls.Add(this.LblCharacterClothingTotal);
            this.Controls.Add(this.LblCollectorCardsTotal);
            this.Controls.Add(this.LblStreetRacesTotal);
            this.Controls.Add(this.LblBonusMissionsTotal);
            this.Controls.Add(this.LblStoryMissionsTotal);
            this.Controls.Add(this.LblMovies);
            this.Controls.Add(this.LblGags);
            this.Controls.Add(this.LblWaspCameras);
            this.Controls.Add(this.LblVehicles);
            this.Controls.Add(this.LblCharacterClothing);
            this.Controls.Add(this.LblCollectorCards);
            this.Controls.Add(this.LblStreetRaces);
            this.Controls.Add(this.LblBonusMissions);
            this.Controls.Add(this.LblStoryMissions);
            this.Controls.Add(this.LblPercentageComplete);
            this.Controls.Add(this.LblMoviesLabel);
            this.Controls.Add(this.LblGagsLabel);
            this.Controls.Add(this.LblWaspCamerasLabel);
            this.Controls.Add(this.LblVehiclesLabel);
            this.Controls.Add(this.LblCharacterClothingLabel);
            this.Controls.Add(this.LblCollectorCardsLabel);
            this.Controls.Add(this.LblStreetRacesLabel);
            this.Controls.Add(this.LblBonusMissionsLabel);
            this.Controls.Add(this.LblStoryMissionsLabel);
            this.Controls.Add(this.LblPercentageCompleteLabel);
            this.Controls.Add(this.LblCredits);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.Text = "SHAR Checklist";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Shown += new System.EventHandler(this.FrmMain_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctrl_MouseDown);
            this.CMSMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblCredits;
        private System.Windows.Forms.Label LblPercentageCompleteLabel;
        private System.Windows.Forms.Label LblStoryMissionsLabel;
        private System.Windows.Forms.Label LblBonusMissionsLabel;
        private System.Windows.Forms.Label LblStreetRacesLabel;
        private System.Windows.Forms.Label LblCollectorCardsLabel;
        private System.Windows.Forms.Label LblCharacterClothingLabel;
        private System.Windows.Forms.Label LblVehiclesLabel;
        private System.Windows.Forms.Label LblWaspCamerasLabel;
        private System.Windows.Forms.Label LblGagsLabel;
        private System.Windows.Forms.Label LblMoviesLabel;
        private System.Windows.Forms.Label LblPercentageComplete;
        private System.Windows.Forms.Label LblStoryMissions;
        private System.Windows.Forms.Label LblBonusMissions;
        private System.Windows.Forms.Label LblStreetRaces;
        private System.Windows.Forms.Label LblCollectorCards;
        private System.Windows.Forms.Label LblCharacterClothing;
        private System.Windows.Forms.Label LblVehicles;
        private System.Windows.Forms.Label LblWaspCameras;
        private System.Windows.Forms.Label LblGags;
        private System.Windows.Forms.Label LblMovies;
        private System.Windows.Forms.Label LblStoryMissionsTotal;
        private System.Windows.Forms.Label LblBonusMissionsTotal;
        private System.Windows.Forms.Label LblStreetRacesTotal;
        private System.Windows.Forms.Label LblCollectorCardsTotal;
        private System.Windows.Forms.Label LblCharacterClothingTotal;
        private System.Windows.Forms.Label LblVehiclesTotal;
        private System.Windows.Forms.Label LblWaspCamerasTotal;
        private System.Windows.Forms.Label LblGagsTotal;
        private System.Windows.Forms.Label LblMoviesTotal;
        private System.Windows.Forms.ContextMenuStrip CMSMain;
        private System.Windows.Forms.ToolStripMenuItem topmostToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formBorderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Timer TmrUpdate;
    }
}

