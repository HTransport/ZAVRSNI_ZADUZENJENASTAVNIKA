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
    public partial class NastavnikPicker : Form
    {
        public NastavnikPicker(Form owner, bool isChained)
        {
            InitializeComponent();

            Text = "Odabir nastavnika";
            Owner = owner;

            _skolskeGodineService = new SkolskeGodineService();
            _razredService = new RazredService();
            _nastavniciService = new NastavniciService();
            _predmetiService = new PredmetiService();
            _zamjeneService = new ZamjeneService();

            NastavniciContent = new List<string>();
            NastavniciLabels = new List<Label>();

            NastavnikLabelFont = new Font("Arial", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            NastavnikLabelListLoc = new Point(18, 18);
            NastavnikLabelListMargin = 40;

            IsChained = isChained;
        }

        private readonly SkolskeGodineService _skolskeGodineService;
        private readonly RazredService _razredService;
        private readonly NastavniciService _nastavniciService;
        private readonly PredmetiService _predmetiService;
        private readonly ZamjeneService _zamjeneService;

        private bool IsChained { get; set; }

        private Font NastavnikLabelFont { get; set; }
        private Point NastavnikLabelListLoc { get; set; }
        private int NastavnikLabelListMargin { get; set; }

        private bool ToolbarIsHeld { get; set; }
        public Point MousePos { get; set; }


        List<Nastavnik> Nastavnici { get; set; }
        public List<string> NastavniciContent { get; set; }
        public List<Label> NastavniciLabels { get; set; }

        private void NastavnikPicker_Load(object sender, EventArgs e)
        {
            Nastavnici = new List<Nastavnik>();

            Nastavnici.AddRange(_nastavniciService.GetList());

            foreach (Nastavnik nastavnik in Nastavnici)
            {
                NastavniciContent.Add(nastavnik.Ime + " " + nastavnik.Prezime + " - " + nastavnik.Kategorija);
            }

            for (int i = 0; i < NastavniciContent.Count; i++)
            {
                Label label = new Label();
                label.Text = NastavniciContent[i];
                Point loc = new Point(NastavnikLabelListLoc.X, NastavnikLabelListMargin * i + NastavnikLabelListLoc.Y);
                label.Location = loc;
                label.Size = new Size(498, 24);
                label.Font = NastavnikLabelFont;
                label.Name = "Nastavnik" + i;
                label.ForeColor = Color.Black;
                label.BackColor = Color.White;
                label.Parent = NastavnikLabelContainer;
                label.Click += NastavnikLabel_Click;
                NastavniciLabels.Add(label);
                NastavnikLabelContainer.Controls.Add(label);
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
            if(IsChained)
                (Owner as ZamjenaForm).NastavnikSelection = _nastavniciService.GetSingle(Nastavnici[i].OIB);
            else (Owner as PredmetForm).NastavnikSelection = _nastavniciService.GetSingle(Nastavnici[i].OIB);
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
