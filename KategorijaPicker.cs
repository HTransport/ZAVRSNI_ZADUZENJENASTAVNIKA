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
    public partial class KategorijaPicker : Form
    {
        public KategorijaPicker(Form f)
        {
            InitializeComponent();

            Text = "Odabir kategorije";
            Owner = f;

            _skolskeGodineService = new SkolskeGodineService();
            _razredService = new RazredService();
            _nastavniciService = new NastavniciService();
            _predmetiService = new PredmetiService();
            _zamjeneService = new ZamjeneService();

            KategorijeContent = new List<string>();
            KategorijeLabels = new List<Label>();

            KategorijaLabelFont = new Font("Arial", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            KategorijaLabelListLoc = new Point(18, 18);
            KategorijaLabelListMargin = 40;
        }

        private readonly SkolskeGodineService _skolskeGodineService;
        private readonly RazredService _razredService;
        private readonly NastavniciService _nastavniciService;
        private readonly PredmetiService _predmetiService;
        private readonly ZamjeneService _zamjeneService;

        private Font KategorijaLabelFont { get; set; }
        private Point KategorijaLabelListLoc { get; set; }
        private int KategorijaLabelListMargin { get; set; }

        private bool ToolbarIsHeld { get; set; }
        public Point MousePos { get; set; }

        public List<string> KategorijeContent { get; set; }
        public List<Label> KategorijeLabels { get; set; }

        private void KategorijaPicker_Load(object sender, EventArgs e)
        {
            List<Nastavnik> Nastavnici = new List<Nastavnik>();
            List<Predmet> Predmeti = new List<Predmet>();

            Nastavnici.AddRange(_nastavniciService.GetList());
            Predmeti.AddRange(_predmetiService.GetList());

            foreach (Nastavnik nastavnik in Nastavnici)
            {
                KategorijeContent.Add(nastavnik.Kategorija);
            }
            foreach (Predmet predmet in Predmeti)
            {
                KategorijeContent.Add(predmet.Kategorija);
            }
            List<string> TempList = new List<string>();
            
            foreach (string kategorija in KategorijeContent) 
            {
                bool IsUnique = true;
                foreach (string unique in TempList)
                    if (kategorija == unique)
                        IsUnique = false;
                if (IsUnique)
                    TempList.Add(kategorija);
            }
            KategorijeContent.Clear();
            KategorijeContent.AddRange(TempList);
            KategorijeContent.Sort();

            for (int i = 0; i < KategorijeContent.Count; i++)
            {
                Label label = new Label();
                label.Text = KategorijeContent[i];
                Point loc = new Point(KategorijaLabelListLoc.X, KategorijaLabelListMargin * i + KategorijaLabelListLoc.Y);
                label.Location = loc;
                label.Size = new Size(498, 24);
                label.Font = KategorijaLabelFont;
                label.Name = "Kategorija" + i;
                label.ForeColor = Color.Black;
                label.BackColor = Color.White;
                label.Parent = KategorijaLabelContainer;
                label.Click += KategorijaLabel_Click;
                KategorijeLabels.Add(label);
                KategorijaLabelContainer.Controls.Add(label);
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

        private void KategorijaLabel_Click(object sender, EventArgs e)
        {
            Control[] controls = Owner.Controls.Find("KategorijaUnos", false);
            if (controls.Length > 0 && controls[0] is TextBox kategorijaTextBox)
            {
                kategorijaTextBox.Text = (sender as Label).Text;
                kategorijaTextBox.Enabled = false;
            }
            else
            {
                throw new NoNullAllowedException();
            }
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void KategorijaPicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode.ToString() == "Escape")
                Close();
        }
    }
}
