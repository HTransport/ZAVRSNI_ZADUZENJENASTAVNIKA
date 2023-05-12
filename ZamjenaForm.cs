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
    public partial class ZamjenaForm : Form
    {
        public ZamjenaForm(Form owner)
        {
            InitializeComponent();

            Owner = owner;
            Text = "Unos zamjene";

            ToolbarIsHeld = false;
            MousePos = Point.Empty;

            DarkerColor = Color.FromArgb(239, 192, 192);
            BrightestColor = Color.FromArgb(255, 248, 248);

            _skolskeGodineService = new SkolskeGodineService();
            _razredService = new RazredService();
            _nastavniciService = new NastavniciService();
            _predmetiService = new PredmetiService();
            _zamjeneService = new ZamjeneService();

            RazredGodinaInfo = _razredService.GetSingle(int.Parse((Owner as Form1).RazinaDropDisplay.Text[0].ToString()), (Owner as Form1).OznakaDropDisplay.Text, int.Parse((Owner as Form1).SkolskaGodinaDropDisplay.Text.Remove(4)));
        }

        public ZamjenaForm(Zamjena zamjena, Form owner)
        {
            InitializeComponent();

            Owner = owner;
            Text = "Uređivanje zamjene";

            _skolskeGodineService = new SkolskeGodineService();
            _razredService = new RazredService();
            _nastavniciService = new NastavniciService();
            _predmetiService = new PredmetiService();
            _zamjeneService = new ZamjeneService();

            ZamjenaEdit = zamjena;
            string path = Path.GetDirectoryName(Application.CommonAppDataPath);

            ToolbarIsHeld = false;
            MousePos = Point.Empty;

            DarkerColor = Color.FromArgb(239, 192, 192);
            BrightestColor = Color.FromArgb(255, 248, 248);

            RazredGodinaInfo = _razredService.GetSingle(int.Parse((Owner as Form1).RazinaDropDisplay.Text[0].ToString()), (Owner as Form1).OznakaDropDisplay.Text, int.Parse((Owner as Form1).SkolskaGodinaDropDisplay.Text.Remove(4)));

            NastavnikSelection = ZamjenaEdit.Nastavnik;
            PredmetSelection = ZamjenaEdit.Predmet;
            DatumOdPicker.Value = ZamjenaEdit.DatumOd;
            DatumDoPicker.Value = ZamjenaEdit.DatumDo;

            AddButtonText.Text = "Potvrdi";
            AddButtonText.Location = new Point(AddButtonText.Location.X - 8, AddButtonText.Location.Y);

            AddButton.Location = new Point(AddButton.Location.X - 150, AddButton.Location.Y);

            AddButton.Click -= AddButton_Click;
            AddButtonText.Click -= AddButton_Click;

            Panel DeleteButton = new Panel();
            Label DeleteButtonText = new Label();

            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = AddButton.Size;
            DeleteButton.BackgroundImageLayout = ImageLayout.Zoom;
            DeleteButton.Location = new Point(Size.Width - 250 - DeleteButton.Size.Width, AddButton.Location.Y);
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

        private void ZamjenaForm_Load(object sender, EventArgs e)
        {
            DatumOdPicker.MinDate = new DateTime(RazredGodinaInfo.SkolskaGodina.Godina, 9, 1);
            DatumOdPicker.MaxDate = new DateTime(RazredGodinaInfo.SkolskaGodina.Godina + 1, 6, 30);
            DatumDoPicker.MinDate = new DateTime(RazredGodinaInfo.SkolskaGodina.Godina, 9, 1);
            DatumDoPicker.MaxDate = new DateTime(RazredGodinaInfo.SkolskaGodina.Godina + 1, 6, 30);
        }

        private Color DarkerColor { get; set; }
        private Color BrightestColor { get; set; }
        private readonly SkolskeGodineService _skolskeGodineService;
        private readonly RazredService _razredService;
        private readonly NastavniciService _nastavniciService;
        private readonly PredmetiService _predmetiService;
        private readonly ZamjeneService _zamjeneService;

        public Razred RazredGodinaInfo { get; set; }

        public Predmet PredmetSelection { get; set; }
        public Nastavnik NastavnikSelection { get; set; }
        private Zamjena ZamjenaEdit { get; set; }

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
            if (NastavnikSelection is null || PredmetSelection is null)
            {
                MessageBox.Show("Nedostaje jedan ili više argumenata. Provjerite jeste li odabrali nastavnika i/ili predmet.");
                return;
            }
            if (DatumOdPicker.Value > DatumDoPicker.Value)
            {
                MessageBox.Show("Zadani raspon nije moguć (Početni datum je postavljen poslije konačnog datuma).");
                return;
            }
            List<Zamjena> Zamjene = new List<Zamjena>();
            Zamjene.AddRange(_zamjeneService.GetList(NastavnikSelection.OIB, PredmetSelection.Naziv, RazredGodinaInfo.Razina, RazredGodinaInfo.Oznaka, RazredGodinaInfo.SkolskaGodina.Godina));
            foreach (Zamjena zamjena in Zamjene)
            {
                if (DatumOdPicker.Value >= zamjena.DatumOd && DatumOdPicker.Value <= zamjena.DatumDo || DatumDoPicker.Value >= zamjena.DatumOd && DatumDoPicker.Value <= zamjena.DatumDo || DatumOdPicker.Value < zamjena.DatumOd && DatumDoPicker.Value > zamjena.DatumDo)
                {
                    MessageBox.Show("Zadani raspon se preklapa sa već unesenom zamjenom za odabranog nastavnika ovog predmeta u ovom razredu");
                    return;
                }
            }
            if (NastavnikSelection.OIB == PredmetSelection.Nastavnik.OIB)
            {
                MessageBox.Show("Za zamjenu je postavljen nastavnik koji trenutno predaje zadani predmet.");
                NastavnikSelection = null;
                return;
            }

            _zamjeneService.Add(NastavnikSelection.OIB, DatumOdPicker.Value, DatumDoPicker.Value, PredmetSelection.Naziv, RazredGodinaInfo.Razina, RazredGodinaInfo.Oznaka, RazredGodinaInfo.SkolskaGodina.Godina);
            Close();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {

            if (NastavnikSelection is null || PredmetSelection is null)
            {
                MessageBox.Show("Nedostaje jedan ili više argumenata. Provjerite jeste li odabrali nastavnika i/ili predmet.");
                return;
            }
            if (DatumOdPicker.Value > DatumDoPicker.Value)
            {
                MessageBox.Show("Zadani raspon nije moguć (Početni datum je postavljen poslije konačnog datuma).");
                return;
            }
            List<Zamjena> Zamjene = new List<Zamjena>();
            Zamjene.AddRange(_zamjeneService.GetList(NastavnikSelection.OIB, PredmetSelection.Naziv, RazredGodinaInfo.Razina, RazredGodinaInfo.Oznaka, RazredGodinaInfo.SkolskaGodina.Godina));
            Zamjene.Remove(Zamjene.Where(Z => Z.Predmet.Naziv == ZamjenaEdit.Predmet.Naziv && Z.Nastavnik.OIB == ZamjenaEdit.Nastavnik.OIB && Z.Predmet.Razred.Razina == RazredGodinaInfo.Razina && Z.Predmet.Razred.Oznaka == RazredGodinaInfo.Oznaka && Z.Predmet.Razred.SkolskaGodina.Godina == RazredGodinaInfo.SkolskaGodina.Godina).FirstOrDefault());
            foreach (Zamjena zamjena in Zamjene)
            {
                if (DatumOdPicker.Value >= zamjena.DatumOd && DatumOdPicker.Value <= zamjena.DatumDo || DatumDoPicker.Value >= zamjena.DatumOd && DatumDoPicker.Value <= zamjena.DatumDo || DatumOdPicker.Value < zamjena.DatumOd && DatumDoPicker.Value > zamjena.DatumDo)
                {
                    MessageBox.Show("Zadani raspon se preklapa sa već unesenom zamjenom za odabranog nastavnika ovog predmeta u ovom razredu");
                    return;
                }
            }
            if (NastavnikSelection.OIB == PredmetSelection.Nastavnik.OIB)
            {
                MessageBox.Show("Za zamjenu je postavljen nastavnik koji trenutno predaje zadani predmet.");
                NastavnikSelection = null;
                return;
            }

            _zamjeneService.Update(DatumOdPicker.Value, DatumDoPicker.Value, PredmetSelection.Naziv, ZamjenaEdit.Predmet.Naziv, NastavnikSelection.OIB, ZamjenaEdit.Nastavnik.OIB, RazredGodinaInfo.Razina, RazredGodinaInfo.Oznaka, RazredGodinaInfo.SkolskaGodina.Godina);
            Close();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            _zamjeneService.Delete(ZamjenaEdit.Predmet.Naziv, ZamjenaEdit.Nastavnik.OIB, ZamjenaEdit.Predmet.Razred.Razina, ZamjenaEdit.Predmet.Razred.Oznaka, ZamjenaEdit.Predmet.Razred.SkolskaGodina.Godina);
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

        private void NastavnikPickButton_Click(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(224, 224, 224);
            Toolbar.BackColor = Color.FromArgb(224, 224, 224);

            NastavnikPicker nastavnikPicker = new NastavnikPicker(this, true);
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
            nastavnikForm.MoreButton.Enabled = false;

            nastavnikForm.ShowDialog();
        }

        private void PredmetPickButton_Click(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(224, 224, 224);
            Toolbar.BackColor = Color.FromArgb(224, 224, 224);

            PredmetPicker predmetPicker = new PredmetPicker(this);
            predmetPicker.ShowDialog();

            BackColor = Color.FromArgb(255, 255, 255);
            Toolbar.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void PredmetPreviewer_Click(object sender, EventArgs e)
        {
            if (PredmetSelection is null)
            {
                MessageBox.Show("Predmet nije odabran.");
                return;
            }
            PredmetForm predmetForm = new PredmetForm(PredmetSelection, PredmetSelection.Nastavnik.OIB, this);
            predmetForm.AddButton.Visible = false;
            predmetForm.DeleteButton.Visible = false;
            predmetForm.NazivUnos.Enabled = false;
            predmetForm.KategorijaUnos.Enabled = false;
            predmetForm.MoreButton.Visible = false;
            predmetForm.NastavnikPickButton.Visible = false;
            predmetForm.label4.DoubleClick -= predmetForm.KategorijaUnos_DoubleClick;

            predmetForm.ShowDialog();
        }
    }
}
