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
using ZAVRSNI_ZADUZENJENASTAVNIKA.Models;

namespace ZAVRSNI_ZADUZENJENASTAVNIKA
{
    public partial class PredmetPicker : Form
    {
        public PredmetPicker(Form owner)
        {
            InitializeComponent();

            Text = "Odabir nastavnika";
            Owner = owner;

            _skolskeGodineService = new SkolskeGodineService();
            _razredService = new RazredService();
            _nastavniciService = new NastavniciService();
            _predmetiService = new PredmetiService();
            _zamjeneService = new ZamjeneService();

            PredmetiContent = new List<string>();
            PredmetiLabels = new List<Label>();

            PredmetLabelFont = new Font("Arial", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PredmetLabelListLoc = new Point(18, 18);
            PredmetLabelListMargin = 40;
        }

        private readonly SkolskeGodineService _skolskeGodineService;
        private readonly RazredService _razredService;
        private readonly NastavniciService _nastavniciService;
        private readonly PredmetiService _predmetiService;
        private readonly ZamjeneService _zamjeneService;

        private Font PredmetLabelFont { get; set; }
        private Point PredmetLabelListLoc { get; set; }
        private int PredmetLabelListMargin { get; set; }

        private bool ToolbarIsHeld { get; set; }
        public Point MousePos { get; set; }

        List<Predmet> Predmeti { get; set; }
        public List<string> PredmetiContent { get; set; }
        public List<Label> PredmetiLabels { get; set; }

        private void NastavnikPicker_Load(object sender, EventArgs e)
        {
            Predmeti = new List<Predmet>();
            Predmeti.AddRange(_predmetiService.GetList((Owner as ZamjenaForm).RazredGodinaInfo.Razina, (Owner as ZamjenaForm).RazredGodinaInfo.Oznaka, (Owner as ZamjenaForm).RazredGodinaInfo.SkolskaGodina.Godina));

            foreach (Predmet predmet in Predmeti)
            {
                PredmetiContent.Add(predmet.Naziv + " - " + predmet.Nastavnik.Ime + " " + predmet.Nastavnik.Prezime);
            }

            for (int i = 0; i < PredmetiContent.Count; i++)
            {
                Label label = new Label();
                label.Text = PredmetiContent[i];
                Point loc = new Point(PredmetLabelListLoc.X, PredmetLabelListMargin * i + PredmetLabelListLoc.Y);
                label.Location = loc;
                label.Size = new Size(498, 24);
                label.Font = PredmetLabelFont;
                label.Name = "Predmet" + i;
                label.ForeColor = Color.Black;
                label.BackColor = Color.White;
                label.Parent = PredmetLabelContainer;
                label.Click += NastavnikLabel_Click;
                PredmetiLabels.Add(label);
                PredmetLabelContainer.Controls.Add(label);
                label.BringToFront();
            }
        }

        private void Toolbar_MouseDown(object sender, MouseEventArgs e)
        {
            ToolbarIsHeld = true;
        }

        private void Toolbar_MouseUp(object sender, MouseEventArgs e)
        {
            ToolbarIsHeld = false;
            MousePos = Point.Empty;

            if (Location.Y < 0)
                Location = Point.Add(Location, new Size(0, -Location.Y));
        }

        private void Toolbar_MouseMove(object sender, MouseEventArgs e)
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

        private void NastavnikLabel_Click(object sender, EventArgs e)
        {
            int i = int.Parse((sender as Label).Name[(sender as Label).Name.Length - 1].ToString());
            (Owner as ZamjenaForm).PredmetSelection = _predmetiService.GetSingle(Predmeti[i].Naziv, Predmeti[i].Nastavnik.OIB, (Owner as ZamjenaForm).RazredGodinaInfo.Razina, (Owner as ZamjenaForm).RazredGodinaInfo.Oznaka, (Owner as ZamjenaForm).RazredGodinaInfo.SkolskaGodina.Godina);
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void NastavnikPicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode.ToString() == "Escape")
                Close();
        }
    }
}
