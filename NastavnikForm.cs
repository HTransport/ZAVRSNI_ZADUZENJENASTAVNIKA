using System;
using System.IO;
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
    public partial class NastavnikForm : Form
    {
        public NastavnikForm()
        {
            InitializeComponent();

            ToolbarIsHeld = false;
            MousePos = Point.Empty;

            DarkerColor = Color.FromArgb(239, 192, 192);
            BrightestColor = Color.FromArgb(255, 248, 248);

            _skolskeGodineService = new SkolskeGodineService();
            _razredService = new RazredService();
            _nastavniciService = new NastavniciService();
            _predmetiService = new PredmetiService();
            _zamjeneService = new ZamjeneService();
        }

        public NastavnikForm(Nastavnik nastavnik, Form owner)
        {
            InitializeComponent();

            Owner = owner;

            NastavnikEdit = nastavnik;
            string path = Path.GetDirectoryName(Application.CommonAppDataPath);

            ToolbarIsHeld = false;
            MousePos = Point.Empty;

            DarkerColor = Color.FromArgb(239, 192, 192);
            BrightestColor = Color.FromArgb(255, 248, 248);

            _skolskeGodineService = new SkolskeGodineService();
            _razredService = new RazredService();
            _nastavniciService = new NastavniciService();
            _predmetiService = new PredmetiService();
            _zamjeneService = new ZamjeneService();

            ImeUnos.Text = nastavnik.Ime;
            PrezimeUnos.Text = nastavnik.Prezime;
            KategorijaUnos.Text = nastavnik.Kategorija;
            OibUnos.Text = nastavnik.OIB;

            AddButtonText.Text = "Potvrdi";
            AddButtonText.Location = new Point(AddButtonText.Location.X - 8, AddButtonText.Location.Y);

            AddButton.Location = new Point(AddButton.Location.X - 100, AddButton.Location.Y);

            AddButton.Click -= AddButton_Click;
            AddButtonText.Click -= AddButton_Click;

            DeleteButton = new Panel();
            Label DeleteButtonText = new Label();

            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = AddButton.Size;
            DeleteButton.BackgroundImageLayout = ImageLayout.Zoom;
            DeleteButton.Location = new Point(Size.Width - 80 - DeleteButton.Size.Width, AddButton.Location.Y);
            DeleteButton.BackgroundImage = Image.FromFile(path + "\\Assets\\Delete.png");

            DeleteButtonText.Name = "DeleteButtonText";
            DeleteButtonText.Size = AddButtonText.Size;
            DeleteButtonText.Location = AddButtonText.Location;
            DeleteButtonText.BackColor = DarkerColor;
            DeleteButtonText.ForeColor = BrightestColor;
            DeleteButtonText.Font = AddButtonText.Font;
            DeleteButtonText.Text = "Obriši";

            DeleteButton.Controls.Clear();
            DeleteButton.Controls.Add(DeleteButtonText);

            Controls.Add(DeleteButton);

            AddButton.Click += EditButton_Click;
            AddButtonText.Click += EditButton_Click;
            DeleteButton.Click += DeleteButton_Click;
            DeleteButtonText.Click += DeleteButton_Click;
        }
        public Panel DeleteButton;

        private Color DarkerColor { get; set; }
        private Color BrightestColor { get; set; }
        private readonly SkolskeGodineService _skolskeGodineService;
        private readonly RazredService _razredService;
        private readonly NastavniciService _nastavniciService;
        private readonly PredmetiService _predmetiService;
        private readonly ZamjeneService _zamjeneService;

        private Nastavnik NastavnikEdit { get; set; }

        private bool ToolbarIsHeld { get; set; }
        public Point MousePos { get; set; }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
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

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (ImeUnos.Text == "" || PrezimeUnos.Text == "" || KategorijaUnos.Text == "" || OibUnos.Text == "")
            {
                MessageBox.Show("Nedostaje jedan ili više argumenata.");
                return;
            }
            ImeUnos.Text = ImeUnos.Text[0].ToString().ToUpper() + ImeUnos.Text.Remove(0,1).ToLower();
            PrezimeUnos.Text = PrezimeUnos.Text[0].ToString().ToUpper() + PrezimeUnos.Text.Remove(0,1).ToLower();
            KategorijaUnos.Text = KategorijaUnos.Text[0].ToString().ToUpper() + KategorijaUnos.Text.Remove(0,1);

            try
            {
                foreach (char c in OibUnos.Text)
                {
                    int oib = int.Parse(c.ToString());
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("OIB nije napisan u ispravnom formatu.");
                OibUnos.Clear();
                return;
            }

            List<Nastavnik> Nastavnici = new List<Nastavnik>();
            List<string> OIBs = new List<string>();

            Nastavnici.AddRange( _nastavniciService.GetList());

            foreach (Nastavnik nastavnik in Nastavnici)
            {
                OIBs.Add(nastavnik.OIB);
            }
            foreach (string oib in OIBs)
            {
                if (OibUnos.Text == oib)
                {
                    MessageBox.Show("Unos sa danim OIB-om već postoji.");
                    OibUnos.Clear();
                    return;
                }
            }

            if(OibUnos.Text.Length != 11)
            {
                MessageBox.Show("OIB nije ispravan (netočan broj znamenki).");
                return;
            }

             _nastavniciService.Add(ImeUnos.Text, PrezimeUnos.Text, OibUnos.Text, KategorijaUnos.Text);
            Close();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (ImeUnos.Text == "" || PrezimeUnos.Text == "" || KategorijaUnos.Text == "" || OibUnos.Text == "")
            {
                MessageBox.Show("Nedostaje jedan ili više argumenata.");
                return;
            }
            ImeUnos.Text = ImeUnos.Text[0].ToString().ToUpper() + ImeUnos.Text.Remove(0, 1).ToLower();
            PrezimeUnos.Text = PrezimeUnos.Text[0].ToString().ToUpper() + PrezimeUnos.Text.Remove(0, 1).ToLower();
            KategorijaUnos.Text = KategorijaUnos.Text[0].ToString().ToUpper() + KategorijaUnos.Text.Remove(0, 1);

            List<Nastavnik> Nastavnici = new List<Nastavnik>();
            List<string> OIBs = new List<string>();

            Nastavnici.AddRange(_nastavniciService.GetList());
            Nastavnici.Remove(Nastavnici.Where(c => c.OIB == NastavnikEdit.OIB).FirstOrDefault());

            foreach (Nastavnik nastavnik in Nastavnici)
            {
                OIBs.Add(nastavnik.OIB);
            }
            foreach (string oib in OIBs)
            {
                if (OibUnos.Text == oib)
                {
                    MessageBox.Show("Unos sa danim OIB-om već postoji.");
                    OibUnos.Clear();
                    return;
                }
            }

            try
            {
                foreach (char c in OibUnos.Text)
                {
                    int oib = int.Parse(c.ToString());
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("OIB nije napisan u ispravnom formatu.");
                OibUnos.Clear();
                return;
            }

            if (OibUnos.Text.Length != 11)
            {
                MessageBox.Show("OIB nije ispravan (netočan broj znamenki).");
                return;
            }

             _nastavniciService.Update(ImeUnos.Text, PrezimeUnos.Text, OibUnos.Text, NastavnikEdit.OIB, KategorijaUnos.Text);
            Close();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
             _nastavniciService.Delete(NastavnikEdit.OIB);
            Owner.Refresh();
            Close();
        }

        private void MoreButton_Click(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(224, 224, 224);
            Toolbar.BackColor = Color.FromArgb(224, 224, 224);

            KategorijaPicker kategorijaPicker = new KategorijaPicker(this);
            kategorijaPicker.ShowDialog();

            BackColor = Color.FromArgb(255, 255, 255);
            Toolbar.BackColor = Color.FromArgb(255, 255, 255);
        }

        public void KategorijaUnos_DoubleClick(object sender, EventArgs e)
        {
            if (KategorijaUnos.Enabled)
                return;
            KategorijaUnos.Clear();
            KategorijaUnos.Enabled = true;
            KategorijaUnos.Focus();
        }
    }
}
