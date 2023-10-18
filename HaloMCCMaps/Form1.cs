using System.Diagnostics;
using System.Windows.Forms;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using HaloMCCMaps.Properties;
using System.Reflection.Metadata.Ecma335;

namespace HaloMCCMaps
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            checkedListBox1.CheckOnClick = true;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
            DirectoryInfo dinfo = new DirectoryInfo(@"c:\Program Files (x86)\Steam\steamapps\common\Halo The Master Chief Collection\");

            if (dinfo.Exists)
            {

                FileInfo[] smFiles = dinfo.GetFiles("*.map");

                foreach (FileInfo fi in smFiles)
                {
                    checkedListBox1.Items.Add(Path.GetFileNameWithoutExtension(fi.Name));
                }
                installPath.Text = dinfo.ToString();
                createTabs();
                refreshChecklist();
                checkAllItems();
                createTabs();
            }

            else
            {
                MessageBox.Show("Please Browse for your Installation Folder - usually found in c:\\Program Files (x86)\\Steam\\steamapps\\common\\Halo The Master Chief Collection\\");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);
                    //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                    installPath.Text = fbd.SelectedPath.ToString();
                }
                createTabs();
                refreshChecklist();
                checkAllItems();
            }

        }




        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = checkedListBox1.SelectedIndex;

            if (index != -1)
            {
                var isChecked = checkedListBox1.GetItemChecked(index);

                var mapName = checkedListBox1.SelectedItem as string;
                var mapPath = installPath.Text + tabControl1.SelectedTab.Text + "\\maps";
                var fileToEdit = mapPath + "\\" + mapName + ".map";

                var h2MapPath = installPath.Text + "\\" + tabControl1.SelectedTab.Text + "\\h2_maps_win64_dx11";
                var h2fileToEdit = h2MapPath + "\\" + mapName + ".map";
                var h2fileBackup = h2MapPath + "\\" + mapName + "REMOVED.map";

                pathLabel.Text = fileToEdit;

                mapMaker();

                if (isChecked == true && tabControl1.SelectedIndex != 1 && mapName.Contains("chill") == false)
                {
                    if (mapName.Contains("REMOVED"))
                    {
                        var fixedFile = fileToEdit.Replace("REMOVED", "");
                        File.Move(fileToEdit, fixedFile);
                        refreshChecklist();
                        checkAllItems();
                    }
                    //MessageBox.Show(mapName + " was fixed!");
                }
                else if (tabControl1.SelectedIndex != 1 && mapName.Contains("chill") == false)
                {
                    //MessageBox.Show(mapName + " was deleted from " + fileToEdit);
                    var fixedFile = fileToEdit.Replace(mapName, mapName + "REMOVED");
                    File.Move(fileToEdit, fixedFile);
                    refreshChecklist();
                    checkAllItems();
                    checkedListBox1.SetItemChecked(index, false);
                }


                if (isChecked == true && tabControl1.SelectedIndex != 1 && mapName.Contains("chill"))
                {
                    if (mapName.Contains("REMOVED"))
                    {
                        var fixedFile = fileToEdit.Replace("aREMOVED", "");
                        File.Move(fileToEdit, fixedFile);
                        refreshChecklist();
                        checkAllItems();
                    }
                    //MessageBox.Show(mapName + " was fixed!");
                }
                else if (tabControl1.SelectedIndex != 1 && mapName.Contains("chill"))
                {
                    //MessageBox.Show(mapName + " was deleted from " + fileToEdit);
                    var fixedFile = fileToEdit.Replace(mapName, mapName + "aREMOVED");
                    File.Move(fileToEdit, fixedFile);
                    refreshChecklist();
                    checkAllItems();
                    checkedListBox1.SetItemChecked(index, false);
                }


                //Special Cases for Halo 2
                if (isChecked == true && tabControl1.SelectedIndex == 1)
                {
                    // MessageBox.Show("HEY");
                    if (mapName.Contains("REMOVED"))
                    {
                        var fixedFile = h2fileToEdit.Replace("REMOVED", "");
                        File.Move(h2fileToEdit, fixedFile);
                        checkedListBox1.Items.Clear();
                        DirectoryInfo dinfo = new DirectoryInfo(@installPath.Text + "\\" + tabControl1.SelectedTab.Text + "\\h2_maps_win64_dx11");
                        FileInfo[] smFiles = dinfo.GetFiles("*.map");
                        foreach (FileInfo fi in smFiles)
                        {
                            checkedListBox1.Items.Add(Path.GetFileNameWithoutExtension(fi.Name));
                        }
                        checkAllItems();
                    }
                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    File.Move(h2fileToEdit, h2fileBackup);
                    checkedListBox1.Items.Clear();
                    DirectoryInfo dinfo = new DirectoryInfo(@installPath.Text + "\\" + tabControl1.SelectedTab.Text + "\\h2_maps_win64_dx11");
                    FileInfo[] smFiles = dinfo.GetFiles("*.map");

                    foreach (FileInfo fi in smFiles)
                    {
                        checkedListBox1.Items.Add(Path.GetFileNameWithoutExtension(fi.Name));
                    }
                    checkAllItems();
                    checkedListBox1.SetItemChecked(index, false);
                }
            }
        }


        private void checkAllItems()
        {
            int i = 0;
            while (i < checkedListBox1.Items.Count)
            {
                try
                {
                    if (checkedListBox1.Items[i].ToString().Contains("REMOVED"))
                    {
                        checkedListBox1.SetItemChecked(i, false);
                        i++;
                    }
                    if (checkedListBox1.Items[i].ToString().Contains("REMOVED") == false)
                    {
                        checkedListBox1.SetItemChecked(i, true);
                        i++;
                    }
                }
                catch
                {
                    break;
                }
            }
        }

        private void refreshChecklist()
        {
            checkedListBox1.Items.Clear();
            DirectoryInfo dinfo = new DirectoryInfo(installPath.Text + "\\" + tabControl1.SelectedTab.Text + "\\maps");
            FileInfo[] smFiles = dinfo.GetFiles("*.map");

            foreach (FileInfo fi in smFiles)
            {
                checkedListBox1.Items.Add(Path.GetFileNameWithoutExtension(fi.Name));
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {

                checkedListBox1.Items.Clear();
                DirectoryInfo dinfo = new DirectoryInfo(@installPath.Text + "\\" + tabControl1.SelectedTab.Text + "\\maps");
                FileInfo[] smFiles = dinfo.GetFiles("*.map");

                foreach (FileInfo fi in smFiles)
                {
                    checkedListBox1.Items.Add(Path.GetFileNameWithoutExtension(fi.Name));
                }
                checkAllItems();

            }
            if (tabControl1.SelectedIndex == 1)
            {

                checkedListBox1.Items.Clear();
                DirectoryInfo dinfo = new DirectoryInfo(@installPath.Text + "\\" + tabControl1.SelectedTab.Text + "\\h2_maps_win64_dx11");
                FileInfo[] smFiles = dinfo.GetFiles("*.map");

                foreach (FileInfo fi in smFiles)
                {
                    checkedListBox1.Items.Add(Path.GetFileNameWithoutExtension(fi.Name));
                }
                checkAllItems();

            }
            if (tabControl1.SelectedIndex == 2)
            {

                checkedListBox1.Items.Clear();
                DirectoryInfo dinfo = new DirectoryInfo(@installPath.Text + "\\" + tabControl1.SelectedTab.Text + "\\maps");
                FileInfo[] smFiles = dinfo.GetFiles("*.map");

                foreach (FileInfo fi in smFiles)
                {
                    checkedListBox1.Items.Add(Path.GetFileNameWithoutExtension(fi.Name));
                }
                checkAllItems();

            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void createTabs()
        {
            tabPage1.Text = "halo1";
            ceLabel.Text = "Campaign" + "\n" +
            "The Pillar of Autumn  A10" + "\n" +
            "Halo  A30" + "\n" +
            "The Truth and Reconciliation  A50" + "\n" +
            "The Silent Cartographer  B30" + "\n" +
            "Assault on the Control Room  B40" + "\n" +
            "343 Guilty Spark  C10" + "\n" + 
            "The Library  C20" + "\n" +
            "Two Betrayals  C40" + "\n" +
            "Keyes  D20" + "\n" +
            "The Maw  D40" + "\n" + "\n" +
            "Multiplayer" + "\n" +
            "Battle Creek  beavercreek" + "\n" +
            "Blood Gulch  bloodgulch" + "\n" +
            "Boarding Action  boardingaction" + "\n" +
            "Chill Out  chillout" + "\n" +
            "Chiron TL-34  putput" + "\n" +
            "Damnation  damnation" + "\n" +
            "Danger Canyon  dangercanyon" + "\n" +
            "Death Island  deathisland" + "\n" +
            "Derelict  carousel" + "\n" +
            "Gephyrophobia  gephyrophobia" + "\n" +
            "Hang 'Em High  hangemhigh" + "\n" +
            "Ice Fields  icefields" + "\n" +
            "Infinity  infinity" + "\n" +
            "Longest  longest" + "\n" +
            "Prisoner  prisoner" + "\n" +
            "Rat Race  ratrace" + "\n" +
            "Sidewinder  sidewinder" + "\n" +
            "Timberland  timberland" + "\n" +
            "Wizard  wizard" + "\n";

            tabPage2.Text = "halo2";
            h2Label.Text = "Campaign" + "\n" +
            "The Heretic  00a_introduction" + "\n" +
            "The Armory  01a_tutorial" + "\n" +
            "Cairo Station  1b_spacestation" + "\n" +
            "Outskirts  03a_oldmombasa" + "\n" +
            "Metropolis  03b_newmombasa" + "\n" +
            "The Arbiter  04a_gasgiant" + "\n" +
            "The Oracle  04b_floodlab" + "\n" +
            "Delta Halo  05a_deltaapproach" + "\n" +
            "Regret  05b_deltatowers" + "\n" +
            "Sacred Icon  06a_sentinelwalls" + "\n" +
            "Quarantine Zone  06b_floodzone" + "\n" +
            "Gravemind  07a_highcharity" + "\n" +
            "Uprising  08a_deltacliffs" + "\n" +
            "High Charity  07b_forerunnership" + "\n" +
            "The Great Journey  08b_deltacontrol" + "\n" +
            "Cut levels" + "\n" +
            "alphamoon  alphamoon" + "\n" +
            "Earth Ark  Earth_Ark_09" + "\n" +
            "deltatemple  deltatemple" + "\n" + "\n" +

            "Multiplayer" + "\n" +
            "Ascension  ascension" + "\n" +
            "Backwash  backwash" + "\n" +
            "Beaver Creek  beaver_creek" + "\n" +
            "Burial Mounds  burial_mounds" + "\n" +
            "Coagulation  coagulation" + "\n" +
            "Colossus  colossus" + "\n" +
            "Containment  containment" + "\n" +
            "Desolation  derelict" + "\n" +
            "District  street_sweeper" + "\n" +
            "Elongation  elongation" + "\n" +
            "Foundation  foundation	" + "\n" +
            "Gemini  gemini" + "\n" +
            "Headlong  headlong" + "\n" +
            "Ivory Tower  cyclotron" + "\n" +
            "Lockout  lockout" + "\n" +
            "Midship  midship" + "\n" +
            "Relic  dune" + "\n" +
            "Sanctuary  deltatap" + "\n" +
            "Tombstone  highplains" + "\n" +
            "Terminal  triplicate" + "\n" +
            "Turf  turf" + "\n" +
            "Uplift  needle" + "\n" +
            "Waterworks  waterworks" + "\n" +
            "Warlock  warlock" + "\n" +
            "Zanzibar  zanzibar" + "\n" + "\n" +

            "Halo 2: Anniversary" + "\n" +
            "Awash  ca_forge_skybox01" + "\n" +
            "Bloodline  ca_coagulation" + "\n" +
            "Lockdown  ca_lockout" + "\n" +
            "Nebula  ca_forge_skybox02" + "\n" +
            "Shrine  ca_sanctuary" + "\n" +
            "Skyward  ca_forge_skybox03" + "\n" +
            "Stonetown  ca_zanzibar" + "\n" +
            "Warlord  ca_warlock" + "\n" +
            "Zenith  ca_ascension" + "\n" +
            "Remnant  ca_relic" + "\n";

            tabPage3.Text = "halo3";
            h3Label.Text = "Campaign" + "\n" +
            "Arrival  005_intro" + "\n" +
            "Sierra 117  010_jungle" + "\n" +
            "Crow's Nest  020_base" + "\n" +
            "Tsavo Highway  030_outskirts" + "\n" +
            "The Storm  040_voi" + "\n" +
            "Floodgate  050_floodvoi" + "\n" +
            "The Ark  070_waste" + "\n" +
            "The Covenant  100_citadel" + "\n" +
            "Cortana  110_hc" + "\n" +
            "Halo  120_halo" + "\n" +
            "Epilogue  130_epilogue" + "\n" +
            "Cut levels" + "\n" +
            "Flood Ship  060_floodship" + "\n" +
            "Guardian Forest 080_forest" + "\n" +
            "alpine	090_alpine" + "\n" +
            "High Charity 110_hc_old" + "\n" +
            "Forerunner City 100_forecity" + "\n" + "\n" +
            "Multiplayer" + "\n" +
            "Assembly  descent" + "\n" +
            "Avalanche  sidewinder" + "\n" +
            "Blackout  lockout" + "\n" +
            "Citadel  citadel" + "\n" +
            "Cold Storage  chillout" + "\n" +
            "Construct construct" + "\n" +
            "Longshore  docks" + "\n" +
            "Edge  s3d_edge" + "\n" +
            "Epitaph  salvation" + "\n" +
            "Foundry  warehouse" + "\n" +
            "Ghost Town  ghosttown" + "\n" +
            "Guardian guardian" + "\n" +
            "Heretic midship" + "\n" +
            "High Ground deadlock" + "\n" +
            "Icebox	s3d_turf" + "\n" +
            "Isolation isolation" + "\n" +
            "Last Resort zanzibar" + "\n" +
            "Narrows chill" + "\n" +
            "Orbital spacecamp" + "\n" +
            "Rat's Nest  armory" + "\n" +
            "Sandbox sandbox" + "\n" +
            "Sandtrap shrine" + "\n" +
            "Snowbound snowbound" + "\n" +
            "Standoff bunkerworld" + "\n" +
            "The Pit cyberdyne" + "\n" +
            "Valhalla riverworld" + "\n" +
            "Waterfall s3d_waterfall" + "\n";
        }

        private void label1_Click_2(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void mapMaker()
        {
            var mapName = checkedListBox1.SelectedItem as string;
            pictureBox1.Visible = true;
            switch (mapName)
            {
                //Halo CE
                case "beavercreek":
                    pictureBox1.Load("https://i.imgur.com/NT2BJDM.jpeg");
                    break;
                case "bloodgulch":
                    pictureBox1.Load("https://i.imgur.com/HvCC1f6.jpeg");
                    break;
                case "boardingaction":
                    pictureBox1.Load("https://i.imgur.com/2HHKtyh.png");
                    break;
                case "34putput":
                    pictureBox1.Load("https://i.imgur.com/t3TxXEE.jpeg");
                    break;
                case "damnation":
                    pictureBox1.Load("https://i.imgur.com/VFkUdui.jpeg");
                    break;
                case "dangercanyon":
                    pictureBox1.Load("https://i.imgur.com/JUZ1xau.jpeg");
                    break;
                case "deathisland":
                    pictureBox1.Load("https://i.imgur.com/cwnLbAE.jpeg");
                    break;
                case "carousel":
                    pictureBox1.Load("https://i.imgur.com/8fXm4Mu.png");
                    break;
                case "gephyrophobia":
                    pictureBox1.Load("https://i.imgur.com/t37ap1n.jpeg");
                    break;
                case "sidewinder":
                    pictureBox1.Load("https://i.imgur.com/oiGK9et.jpeg");
                    break;
                case "ratrace":
                    pictureBox1.Load("https://i.imgur.com/9ybQw1K.png");
                    break;
                case "prisoner":
                    pictureBox1.Load("https://i.imgur.com/eIwY7PJ.jpeg");
                    break;
                case "hangemhigh":
                    pictureBox1.Load("https://i.imgur.com/ZyWaIIg.png");
                    break;
                case "wizard":
                    pictureBox1.Load("https://i.imgur.com/8fXm4Mu.png");
                    break;
                case "longest":
                    pictureBox1.Load("https://i.imgur.com/rHV3WvO.jpeg");
                    break;
                case "timerbland":
                    pictureBox1.Load("https://i.imgur.com/C6uDQDC.jpeg");
                    break;
                case "icefields":
                    pictureBox1.Load("https://i.imgur.com/OT0uwUe.jpeg");
                    break;
                case "infinity":
                    pictureBox1.Load("https://i.imgur.com/3WGCQ4z.jpeg");
                    break;
                case "putput":
                    pictureBox1.Load("https://i.imgur.com/t3TxXEE.jpeg");
                    break;
                case "timberland":
                    pictureBox1.Load("https://i.imgur.com/C6uDQDC.jpeg");
                    break;
                //Halo 2
                case "ascension":
                    pictureBox1.Load("https://i.imgur.com/JG1Rgic.png");
                    break;
                case "backwash":
                    pictureBox1.Load("https://i.imgur.com/U7Anq98.jpeg");
                    break;
                case "burial_mounds":
                    pictureBox1.Load("https://i.imgur.com/EaxHBPf.jpeg");
                    break;
                case "coagulation":
                    pictureBox1.Load("https://i.imgur.com/v7LBcxn.jpeg");
                    break;
                case "colossus":
                    pictureBox1.Load("https://i.imgur.com/S9Z6Urr.jpeg");
                    break;
                case "containment":
                    pictureBox1.Load("https://i.imgur.com/osOyTXD.jpeg");
                    break;
                case "derelict":
                    pictureBox1.Load("https://i.imgur.com/ssyDz8U.jpeg");
                    break;
                case "street_sweeper":
                    pictureBox1.Load("https://i.imgur.com/eXTLvX0.jpeg");
                    break;
                case "elongation":
                    pictureBox1.Load("https://i.imgur.com/6zvZYhF.gif");
                    break;
                case "foundation":
                    pictureBox1.Load("https://i.imgur.com/aqFRgdw.jpeg");
                    break;
                case "gemini":
                    pictureBox1.Load("https://i.imgur.com/O0np7A4.jpeg");
                    break;
                case "headlong":
                    pictureBox1.Load("https://i.imgur.com/zpKHFVa.jpeg");
                    break;
                case "cyclotron":
                    pictureBox1.Load("https://i.imgur.com/i5x91dK.png");
                    break;
                //case "lockout":
                //    pictureBox1.Load("https://i.imgur.com/PPPzNZ9.jpeg");
                //    break;
                //case "midship":
                //    pictureBox1.Load("https://i.imgur.com/qnyXHZn.jpeg");
                //    break;
                case "dune":
                    pictureBox1.Load("https://i.imgur.com/5yY9Ay6.jpeg");
                    break;
                case "deltatap":
                    pictureBox1.Load("https://i.imgur.com/h2MDx7E.jpeg");
                    break;
                case "highplains":
                    pictureBox1.Load("https://i.imgur.com/7BvwtgW.jpeg");
                    break;
                case "turf":
                    pictureBox1.Load("https://i.imgur.com/TlDDVYe.png");
                    break;
                case "triplicate":
                    pictureBox1.Load("https://i.imgur.com/8f3bKID.jpeg");
                    break;
                case "waterworks":
                    pictureBox1.Load("https://i.imgur.com/ta3BzHs.jpeg");
                    break;
                case "warlock":
                    pictureBox1.Load("https://i.imgur.com/QensFVv.jpeg");
                    break;
                case "zanzibar":
                    pictureBox1.Load("https://i.imgur.com/vcqAHOu.jpeg");
                    break;

                //Halo 3
                case "construct":
                    pictureBox1.Load("https://i.imgur.com/rCzh1oo.jpeg");
                    break;
                case "salvation":
                    pictureBox1.Load("https://i.imgur.com/yneZlcp.jpeg");
                    break;
                case "guardian":
                    pictureBox1.Load("https://i.imgur.com/olJrhE4.jpeg");
                    break;
                case "deadlock":
                    pictureBox1.Load("https://i.imgur.com/xMFhuXd.jpeg");
                    break;
                case "isolation":
                    pictureBox1.Load("https://i.imgur.com/TrM3FfA.jpeg");
                    break;
                case "chill":
                    pictureBox1.Load("https://i.imgur.com/tVS8kJH.jpeg");
                    break;
                case "shrine":
                    pictureBox1.Load("https://i.imgur.com/2e6ZJel.jpeg");
                    break;
                case "snowbound":
                    pictureBox1.Load("https://i.imgur.com/FPwCfKp.jpeg");
                    break;
                case "cyberdyne":
                    pictureBox1.Load("https://i.imgur.com/E39veGY.jpeg");
                    break;
                case "riverworld":
                    pictureBox1.Load("https://i.imgur.com/Z9vTn6G.jpeg");
                    break;
                case "warehouse":
                    pictureBox1.Load("https://i.imgur.com/vl8phC3.jpeg");
                    break;
                case "armory":
                    pictureBox1.Load("https://i.imgur.com/Ql9cJ58.jpeg");
                    break;
                case "bunkerworld":
                    pictureBox1.Load("https://i.imgur.com/UXZUkfF.jpeg");
                    break;
                case "ghosttown":
                    pictureBox1.Load("https://i.imgur.com/qVHnvpa.jpeg");
                    break;
                case "lockout":
                    pictureBox1.Load("https://i.imgur.com/J8LJJ34.jpeg");
                    break;
                case "avalanche":
                    pictureBox1.Load("https://i.imgur.com/4tgB72S.jpeg");
                    break;
                case "chillout":
                    pictureBox1.Load("https://i.imgur.com/RFjbuOV.jpeg");
                    break;
                case "descent":
                    pictureBox1.Load("https://i.imgur.com/g23FPCU.jpeg");
                    break;
                case "spacecamp":
                    pictureBox1.Load("https://i.imgur.com/gginWYT.jpeg");
                    break;
                case "sandbox":
                    pictureBox1.Load("https://i.imgur.com/a6f3XS3.jpeg");
                    break;
                case "citadel":
                    pictureBox1.Load("https://i.imgur.com/LTRZhCy.jpeg");
                    break;
                case "midship":
                    pictureBox1.Load("https://i.imgur.com/dVgGaJA.jpeg");
                    break;
                case "docks":
                    pictureBox1.Load("https://i.imgur.com/7EJxOpa.jpeg");
                    break;
                //default:
                //    pictureBox1.Load("https://pbs-prod.linustechtips.com/monthly_2021_04/386889645_Screenshot2021-04-18175739.png.dc4cbd1a5158bc2b9fe46037c0f8f277.png");
                //    break;
                default:
                    pictureBox1.Image = Resources.brave_0ETtJE95R5;
                    break;
            }
        }
    }
}