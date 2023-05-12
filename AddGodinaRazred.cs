using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZAVRSNI_ZADUZENJENASTAVNIKA.Services;

namespace ZAVRSNI_ZADUZENJENASTAVNIKA
{
    public partial class AddGodinaRazred : Form
    {
        public AddGodinaRazred()
        {
            InitializeComponent();

            EditSkolskaGodinaSection.Enabled = false;
            AddSkolskaGodinaSection.Checked = true;

            GodinaPickerExtension.Text = "/ " + (GodinaPicker.Value + 1).ToString();

            _skolskeGodineService = new SkolskeGodineService();
            _razredService = new RazredService();
            _nastavniciService = new NastavniciService();
            _predmetiService = new PredmetiService();
            _zamjeneService = new ZamjeneService();

            Branch = 0;
        }

        public AddGodinaRazred(string godina)
        {
            InitializeComponent();

            EditSkolskaGodinaSection.Enabled = true;
            AddSkolskaGodinaSection.Enabled = true;
            AddSkolskaGodinaSection.Checked = true;
            EditSkolskaGodinaSection.Checked = false;

            GodinaPicker.Value = DateTime.Today.Year;
            GodinaPickerExtension.Text = "/ " + (GodinaPicker.Value + 1).ToString();

            SelectedGodina = godina;

            RazineDB = new List<string>();
            OznakeDB = new List<string>();

            Razine = new List<CheckBox>();
            Oznake = new List<CheckBox>();

            List<Control> TempControls = new List<Control>();

            for (int i = 0; i < EditControls.Controls.Count; i++)
            {
                if (EditControls.Controls[i].Tag as string == "razinaE")
                    TempControls.Add(EditControls.Controls[i]);
            }
            for (int i = 0; i < TempControls.Count; i++)
            {
                Razine.Add(TempControls[i] as CheckBox);
            }
            TempControls.Clear();
            for (int i = 0; i < EditControls.Controls.Count; i++)
            {
                if (EditControls.Controls[i].Tag as string == "oznakaE")
                    TempControls.Add(EditControls.Controls[i]);
            }
            for (int i = 0; i < TempControls.Count; i++)
            {
                Oznake.Add(TempControls[i] as CheckBox);
            }

            _skolskeGodineService = new SkolskeGodineService();
            _razredService = new RazredService();
            _nastavniciService = new NastavniciService();
            _predmetiService = new PredmetiService();
            _zamjeneService = new ZamjeneService();

            Branch = 1;
        }

        private readonly SkolskeGodineService _skolskeGodineService;
        private readonly RazredService _razredService;
        private readonly NastavniciService _nastavniciService;
        private readonly PredmetiService _predmetiService;
        private readonly ZamjeneService _zamjeneService;

        private List<string> RazineDB { get; set; }
        private List<string> OznakeDB { get; set; }
        private List<CheckBox> Razine { get; set; }
        private List<CheckBox> Oznake { get; set; }

        public Point MousePos { get; set; }
        private bool ToolbarIsHeld { get; set; }
        private string SelectedGodina { get; set; }
        private int Branch { get; set; }

        private void ToolbarDown(object sender, MouseEventArgs e)
        {
            ToolbarIsHeld = true;
            //MessageBox.Show(MousePos.X + "\n" + MousePos.Y + "\n" + DesktopLocation.X + "\n" + DesktopLocation.Y);
        }

        private void Toolbar_CursorChanged(object sender, MouseEventArgs e)
        {
            if (!ToolbarIsHeld)
                return;
            if (MousePos.IsEmpty)
                MousePos = Cursor.Position;
            Point Loc = new Point(Location.X, Location.Y);
            int x = MousePosition.X - MousePos.X;
            int y = MousePosition.Y - MousePos.Y;
            Loc.X += x;
            Loc.Y += y;
            Location = Loc;
            MousePos = MousePosition;
        }

        private void ToolbarUp(object sender, MouseEventArgs e)
        {
            ToolbarIsHeld = false;
            MousePos = Point.Empty;

            if (Location.Y < 0)
                Location = Point.Add(Location, new Size(0, -Location.Y));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddSkolskaGodinaSection_CheckedChanged(object sender, EventArgs e)
        {
            if (AddSkolskaGodinaSection.Checked)
                AddControls.Visible = true;
            else AddControls.Visible = false;

            if (EditSkolskaGodinaSection.Checked)
                EditControls.Visible = true;
            else EditControls.Visible = false;
        }

        private void EditSkolskaGodinaSection_CheckedChanged(object sender, EventArgs e)
        {
            if (AddSkolskaGodinaSection.Checked)
                AddControls.Visible = true;
            else AddControls.Visible = false;

            if (EditSkolskaGodinaSection.Checked)
                EditControls.Visible = true;
            else EditControls.Visible = false;
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            List<Control> TempControls = new List<Control>();
            if(AddSkolskaGodinaSection.Checked)
            {
                if (GodinaPickerWarning.Text != "")
                    return;
                
                Razine = new List<CheckBox>();
                Oznake = new List<CheckBox>();

                 _skolskeGodineService.Add(Convert.ToInt32(GodinaPicker.Value));
                TempControls.Clear();
                for (int i = 0; i < AddControls.Controls.Count; i++)
                {
                    if (AddControls.Controls[i].Tag as string == "razinaA")
                        TempControls.Add(AddControls.Controls[i]);
                }
                for (int i = 0; i < TempControls.Count; i++)
                {
                    Razine.Add(TempControls[i] as CheckBox);
                }
                TempControls.Clear();
                for (int i = 0; i < AddControls.Controls.Count; i++)
                {
                    if (AddControls.Controls[i].Tag as string == "oznakaA")
                        TempControls.Add(AddControls.Controls[i]);
                }
                for (int i = 0; i < TempControls.Count; i++)
                {
                    Oznake.Add(TempControls[i] as CheckBox);
                }
                foreach (CheckBox razina in Razine)
                    foreach (CheckBox oznaka in Oznake)
                        if (razina.Checked && oznaka.Checked)
                             _razredService.Add(Convert.ToInt32(razina.Text), oznaka.Text, Convert.ToInt32(GodinaPicker.Value));
                Close();
            }
            else
            {
                TempControls.Clear();
                for (int i = 0; i < EditControls.Controls.Count; i++)
                {
                    if (EditControls.Controls[i].Tag as string == "razinaE")
                        TempControls.Add(EditControls.Controls[i]);
                }
                for (int i = 0; i < TempControls.Count; i++)
                {
                    Razine.Add(TempControls[i] as CheckBox);
                }
                TempControls.Clear();
                for(int i = 0; i < EditControls.Controls.Count; i++)
                {
                    if (EditControls.Controls[i].Tag as string == "oznakaE")
                        TempControls.Add(EditControls.Controls[i]);
                }
                for (int i = 0; i < TempControls.Count; i++)
                {
                    Oznake.Add(TempControls[i] as CheckBox);
                }
                foreach (CheckBox razina in Razine)
                    foreach (CheckBox oznaka in Oznake)
                    {
                        int n = 0;
                        if (razina.Checked)
                            n += 2;
                        if (oznaka.Checked)
                            n++;

                        switch (n)
                        {
                            case 0:
                            case 1:
                            case 2:
                                var r =  _razredService.GetSingle(int.Parse(razina.Text[0].ToString()), oznaka.Text, int.Parse(SelectedGodina.Remove(4)));
                                if (r != null)
                                     _razredService.Delete(r.Razina, r.Oznaka, int.Parse(SelectedGodina.Remove(4)));
                                break;
                            case 3:
                                r =  _razredService.GetSingle(int.Parse(razina.Text[0].ToString()), oznaka.Text, int.Parse(SelectedGodina.Remove(4)));
                                if (r is null)
                                     _razredService.Add(int.Parse(razina.Text[0].ToString()), oznaka.Text, int.Parse(SelectedGodina.Remove(4)));
                                break;
                            default:
                                break;
                        }
                        if (razina.Checked && oznaka.Checked)
                             _razredService.Add(Convert.ToInt32(razina.Text), oznaka.Text, Convert.ToInt32(GodinaPicker.Value));
                    }
                Dispose();
                Close();
            }

            
        }

        private void GodinaPicker_ValueChanged(object sender, EventArgs e)
        {
            GodinaPickerExtension.Text = "/ " + (GodinaPicker.Value + 1).ToString();

            var s =  _skolskeGodineService.GetSingle(Convert.ToInt32(GodinaPicker.Value));

            if (s != null)
                GodinaPickerWarning.Text = "Godina već postoji.";
            else
                GodinaPickerWarning.Text = "";
        }

        private void AddGodinaRazred_Load(object sender, EventArgs e)
        {
            if (AddSkolskaGodinaSection.Checked)
                AddControls.Visible = true;
            else AddControls.Visible = false;

            if (EditSkolskaGodinaSection.Checked)
                EditControls.Visible = true;
            else EditControls.Visible = false;

            var s =  _skolskeGodineService.GetSingle(Convert.ToInt32(GodinaPicker.Value));

            if (s != null)
                GodinaPickerWarning.Text = "Godina već postoji.";
            else
                GodinaPickerWarning.Text = "";

            if (Branch == 0)
            {
                RazineDB = new List<string>();
                OznakeDB = new List<string>();
            }
            else
            {
                RazineDB =  _razredService.GetList(int.Parse(SelectedGodina.Remove(4)));
                OznakeDB =  _razredService.GetList(int.Parse(SelectedGodina.Remove(4)));

                for (int i = 0; i < RazineDB.Count; i++)
                    RazineDB[i] = RazineDB[i][0].ToString();
                for (int i = 0; i < OznakeDB.Count; i++)
                    OznakeDB[i] = OznakeDB[i][OznakeDB[i].Length - 1].ToString();

                foreach (CheckBox razina in Razine)
                    foreach (string razinaDb in RazineDB)
                        if (razina.Text[razina.Text.Length - 1].ToString() == razinaDb)
                            razina.Checked = true;

                foreach (CheckBox oznaka in Oznake)
                    foreach (string oznakaDb in OznakeDB)
                        if (oznaka.Text[oznaka.Text.Length - 1].ToString().ToUpper() == oznakaDb.ToUpper())
                            oznaka.Checked = true;
            }
        }

        private void DeleteSkolskaGodina(object sender, EventArgs e)
        {
             _skolskeGodineService.Delete(int.Parse(SelectedGodina.Remove(4)));
            Close();
        }
    }
}
