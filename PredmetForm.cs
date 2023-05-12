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
    public partial class PredmetForm : Form
    {
        public PredmetForm(Form owner)
        {
            InitializeComponent();

            Owner = owner;
            Text = "Dodavanje predmeta";

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
        
        public PredmetForm(Predmet predmet, string oib, Form owner)
        {
            InitializeComponent();

            Owner = owner;
            Text = "Uređivanje predmeta";

            _skolskeGodineService = new SkolskeGodineService();
            _razredService = new RazredService();
            _nastavniciService = new NastavniciService();
            _predmetiService = new PredmetiService();
            _zamjeneService = new ZamjeneService();

            PredmetEdit = predmet;
            NastavnikEdit = NastavnikSelection = _nastavniciService.GetSingle(oib);
            string path = Path.GetDirectoryName(Application.CommonAppDataPath);

            ToolbarIsHeld = false;
            MousePos = Point.Empty;

            DarkerColor = Color.FromArgb(239, 192, 192);
            BrightestColor = Color.FromArgb(255, 248, 248);

            NazivUnos.Text = predmet.Naziv;
            KategorijaUnos.Text = predmet.Kategorija;

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

        private Predmet PredmetEdit { get; set; }
        public Nastavnik NastavnikSelection { get; set; }
        public Nastavnik NastavnikEdit { get; set; }

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
            if (NazivUnos.Text == ""|| KategorijaUnos.Text == "" || NastavnikSelection is null)
            {
                MessageBox.Show("Nedostaje jedan ili više argumenata. Provjerite jeste li odabrali nastavnika.");
                return;
            }
            NazivUnos.Text = NazivUnos.Text[0].ToString().ToUpper() + NazivUnos.Text.Remove(0,1).ToLower();
            KategorijaUnos.Text = KategorijaUnos.Text[0].ToString().ToUpper() + KategorijaUnos.Text.Remove(0, 1);

            List<Predmet> Predmeti = new List<Predmet>();
            List<string> Nazivi = new List<string>();

            Predmeti.AddRange(_predmetiService.GetList(int.Parse((Owner as Form1).RazinaDropDisplay.Text[0].ToString()), (Owner as Form1).OznakaDropDisplay.Text, int.Parse((Owner as Form1).SkolskaGodinaDropDisplay.Text.Remove(4))));

            foreach (Predmet predmet in Predmeti)
            {
                Nazivi.Add(predmet.Naziv);
            }
            foreach (string naziv in Nazivi)
            {
                if (NazivUnos.Text == naziv)
                {
                    MessageBox.Show("Unos sa danim nazivom već postoji.");
                    NazivUnos.Clear();
                    return;
                }
            }

            _predmetiService.Add(NazivUnos.Text, KategorijaUnos.Text, NastavnikSelection.OIB, int.Parse((Owner as Form1).RazinaDropDisplay.Text[0].ToString()), (Owner as Form1).OznakaDropDisplay.Text, int.Parse((Owner as Form1).SkolskaGodinaDropDisplay.Text.Remove(4)));
            Close();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (NazivUnos.Text == "" || KategorijaUnos.Text == "" || NastavnikSelection is null)
            {
                MessageBox.Show("Nedostaje jedan ili više argumenata. Provjerite jeste li odabrali nastavnika.");
                return;
            }


            List<Predmet> Predmeti = new List<Predmet>();
            List<string> Nazivi = new List<string>();

            Predmeti.AddRange(_predmetiService.GetList(int.Parse((Owner as Form1).RazinaDropDisplay.Text[0].ToString()), (Owner as Form1).OznakaDropDisplay.Text, int.Parse((Owner as Form1).SkolskaGodinaDropDisplay.Text.Remove(4))));
            Predmeti.Remove(Predmeti.Where(c => c.Naziv == PredmetEdit.Naziv).FirstOrDefault());

            foreach (Predmet predmet in Predmeti)
            {
                Nazivi.Add(predmet.Naziv);
            }
            foreach (string naziv in Nazivi)
            {
                if (NazivUnos.Text == naziv)
                {
                    MessageBox.Show("Unos sa danim nazivom već postoji.");
                    NazivUnos.Clear();
                    return;
                }
            }

            NazivUnos.Text = NazivUnos.Text[0].ToString().ToUpper() + NazivUnos.Text.Remove(0, 1).ToLower();
            KategorijaUnos.Text = KategorijaUnos.Text[0].ToString().ToUpper() + KategorijaUnos.Text.Remove(0, 1);

            _predmetiService.Update(PredmetEdit.Naziv, NazivUnos.Text, KategorijaUnos.Text, NastavnikEdit.OIB, NastavnikSelection.OIB, int.Parse((Owner as Form1).RazinaDropDisplay.Text[0].ToString()), (Owner as Form1).OznakaDropDisplay.Text, int.Parse((Owner as Form1).SkolskaGodinaDropDisplay.Text.Remove(4)));
            Close();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            _predmetiService.Delete(PredmetEdit.Naziv, PredmetEdit.Nastavnik.OIB, PredmetEdit.Razred.Razina, PredmetEdit.Razred.Oznaka, PredmetEdit.Razred.SkolskaGodina.Godina);
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

        private void NastavnikPickButton_Click(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(224, 224, 224);
            Toolbar.BackColor = Color.FromArgb(224, 224, 224);

            NastavnikPicker nastavnikPicker = new NastavnikPicker(this, false);
            nastavnikPicker.ShowDialog();

            BackColor = Color.FromArgb(255, 255, 255);
            Toolbar.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void NastavnikPreviewer_Click(object sender, EventArgs e)
        {
            if (NastavnikSelection is null)
            {
                MessageBox.Show("Nastavnik nije odabran.");
                return;
            }
            NastavnikForm nastavnikForm = new NastavnikForm(NastavnikSelection, this);
            nastavnikForm.AddButton.Visible = false;
            nastavnikForm.DeleteButton.Visible = false;
            nastavnikForm.ImeUnos.Enabled = false;
            nastavnikForm.PrezimeUnos.Enabled = false;
            nastavnikForm.KategorijaUnos.Enabled = false;
            nastavnikForm.OibUnos.Enabled = false;
            nastavnikForm.label4.DoubleClick -= nastavnikForm.KategorijaUnos_DoubleClick;
            nastavnikForm.MoreButton.Visible = false;

            nastavnikForm.ShowDialog();
        }
    }
}
