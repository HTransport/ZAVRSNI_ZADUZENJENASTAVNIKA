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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            _skolskeGodineService = new SkolskeGodineService();
            _razredService = new RazredService();
            _nastavniciService = new NastavniciService();
            _predmetiService = new PredmetiService();
            _zamjeneService = new ZamjeneService();

            IsMaximized = false;
            DefaultSelection = MenuSelection.Location;
            DarkerColor = Color.FromArgb(239, 192, 192);
            LighterColor = Color.FromArgb(255, 224, 224);
            BrightestColor = Color.FromArgb(255, 248, 248);
            RazredDropSize = new Size(85, 76);
            SkolskaGodinaDropSize = new Size(107, 76);
            RazinaDropStart = new Point(Size.Width - 361, 73);
            OznakaDropStart = new Point(Size.Width - 246, 73);
            SkolskaGodinaDropStart = new Point(Size.Width - 121, 73);
            DropMargin = 38;
            RazinaDropped = false;
            OznakaDropped = false;
            GodinaDropped = false;
            MousePos = Point.Empty;
            ToolbarIsHeld = false;
            RazinaDropPanels = new List<Panel>();
            OznakaDropPanels = new List<Panel>();
            SkolskaGodinaDropPanels = new List<Panel>();
            RazinaDropLabels = new List<Label>();
            OznakaDropLabels = new List<Label>();
            SkolskaGodinaDropLabels = new List<Label>();

            DataEntries = new List<Panel>();
            DataEntryLabels = new List<Label>();

            RazredDropLabelLocation = new Point(44, 44);
            GodinaDropLabelLocation = new Point(7, 44);
            RazredDropLabelFont = new Font(LabelInfo.Font.FontFamily, LabelInfo.Font.Size, LabelInfo.Font.Style);
            GodinaDropLabelFont = new Font(LabelInfoBig.Font.FontFamily, LabelInfoBig.Font.Size, LabelInfoBig.Font.Style);

            DataEntryLabelRegularFont = new Font("Consolas", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DataEntryLabelBoldFont = new Font("Consolas", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);

            RazredDropLabelForeColor = Color.FromArgb(207, 160, 160);

            DataEntryStart = new Point(13, 12);
            DataEntryLabel1Loc = new Point(10, 13);
            DataEntryLabel2Loc = new Point(10, 37);
            DataEntryLabel3Loc = new Point(10, 61);
            DataEntryLabel4Loc = new Point(10, 85);
            DataEntryLabel5Loc = new Point(382, 37);
            DataEntryLabel6Loc = new Point(382, 61);
            DataEntryLabel7Loc = new Point(382, 85);
            DataEntryLabel8Loc = new Point(610, 13);
            DataEntryLabel8Offset = 22;
            DataEntrySize = new Size(670, 122);
            DataEntryMargin = 133;

            foreach (Control control in Controls)
            {
                control.MouseDown += new MouseEventHandler(HideDrops);
                foreach (Control child in control.Controls)
                {
                    child.MouseDown += new MouseEventHandler(HideDrops);
                    foreach (Control child1 in child.Controls)
                        child1.MouseDown += new MouseEventHandler(HideDrops);
                }
            }
        }
        private readonly SkolskeGodineService _skolskeGodineService;
        private readonly RazredService _razredService;
        private readonly NastavniciService _nastavniciService;
        private readonly PredmetiService _predmetiService;
        private readonly ZamjeneService _zamjeneService;

        public List<Panel> RazinaDropPanels { get; set; }
        public List<Panel> OznakaDropPanels { get; set; }
        public List<Panel> SkolskaGodinaDropPanels { get; set; }

        public List<Label> RazinaDropLabels { get; set; }
        public List<Label> OznakaDropLabels { get; set; }
        public List<Label> SkolskaGodinaDropLabels { get; set; }

        public Point MousePos { get; set; }
        private bool IsMaximized { get; set; }
        private Point DefaultSelection { get; }
        private Color DarkerColor { get; }
        private Color LighterColor { get; }
        private Color BrightestColor { get; }
        private bool RazinaDropped { get; set; }
        private bool OznakaDropped { get; set; }
        private bool GodinaDropped { get; set; }

        private List<string> RazinaDropContent { get; set; }
        private List<string> OznakaDropContent { get; set; }
        private List<string> SkolskaGodinaDropContent { get; set; }

        private List<Panel> DataEntries { get; set; }
        private List<Label> DataEntryLabels { get; set; }

        private List<Nastavnik> DataNastavnici { get; set; }
        private List<Predmet> DataPredmeti { get; set; }
        private List<Zamjena> DataZamjene { get; set; }

        private Size RazredDropSize { get; }
        private Size SkolskaGodinaDropSize { get; }
        private Point RazinaDropStart { get; set; }
        private Point OznakaDropStart { get; set; }
        private Point SkolskaGodinaDropStart { get; set; }
        private int DropMargin { get; }
        private bool ToolbarIsHeld { get; set; }
        private Point RazredDropLabelLocation { get; set; }
        private Point GodinaDropLabelLocation { get; set; }
        private Font RazredDropLabelFont { get; set; }
        private Font GodinaDropLabelFont { get; set; }
        private Font DataEntryLabelBoldFont { get; set; }
        private Font DataEntryLabelRegularFont { get; set; }
        private Color RazredDropLabelForeColor { get; set; }
        private string CurrentGodina { get; set; }

        public Point DataEntryStart { get; set; }
        public Point DataEntryLabel1Loc { get; set; }
        public Point DataEntryLabel2Loc { get; set; }
        public Point DataEntryLabel3Loc { get; set; }
        public Point DataEntryLabel4Loc { get; set; }
        public Point DataEntryLabel5Loc { get; set; }
        public Point DataEntryLabel6Loc { get; set; }
        public Point DataEntryLabel7Loc { get; set; }
        public Point DataEntryLabel8Loc { get; set; }
        public int DataEntryLabel8Offset { get; set; }
        public int DataEntryMargin { get; set; }

        public int DataPropertyMargin { get; set; }
        public int DataListMargin { get; set; }
        private Size DataEntrySize { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            RazinaDropContent = new List<string>();
            OznakaDropContent = new List<string>();
            SkolskaGodinaDropContent = new List<string>();

            SkolskaGodinaDropContent = _skolskeGodineService.GetList();
            SkolskaGodinaDropContent.Sort();
            try
            {
                SkolskaGodinaDropDisplay.Text = SkolskaGodinaDropContent[0];
                CurrentGodina = SkolskaGodinaDropDisplay.Text;

                RazinaDropContent = _razredService.GetList(int.Parse(SkolskaGodinaDropContent[0][0].ToString() + SkolskaGodinaDropContent[0][1].ToString() + SkolskaGodinaDropContent[0][2].ToString() + SkolskaGodinaDropContent[0][3].ToString()));
                OznakaDropContent = _razredService.GetList(int.Parse(SkolskaGodinaDropContent[0][0].ToString() + SkolskaGodinaDropContent[0][1].ToString() + SkolskaGodinaDropContent[0][2].ToString() + SkolskaGodinaDropContent[0][3].ToString()));
                List<string> uniques = new List<string>();
                bool isUnique = true;
                foreach (string str in RazinaDropContent)
                {
                    foreach (string unique in uniques)
                        if (str[0].ToString() == unique)
                            isUnique = false;
                    if (isUnique)
                    {
                        uniques.Add(str[0].ToString());
                    }
                    isUnique = true;
                }
                RazinaDropContent.Clear();
                RazinaDropContent.AddRange(uniques);
                RazinaDropContent.Sort();
                RazinaDropDisplay.Text = RazinaDropContent[0] + ".";
                uniques.Clear();

                foreach (string str in OznakaDropContent)
                {
                    foreach (string unique in uniques)
                        if (str[str.Length - 1].ToString() == unique)
                            isUnique = false;
                    if (isUnique)
                    {
                        uniques.Add(str[str.Length - 1].ToString());
                    }
                    isUnique = true;
                }
                OznakaDropContent.Clear();
                OznakaDropContent.AddRange(uniques);
                OznakaDropContent.Sort();
                OznakaDropDisplay.Text = OznakaDropContent[0];

            }
            catch (ArgumentOutOfRangeException)
            {

            }

            DataNastavnici = _nastavniciService.GetList();
            DataPredmeti = _predmetiService.GetList();
            DataZamjene = _zamjeneService.GetList();

            MenuSelection.Location = DefaultSelection;
            Nastavnici.BackColor = LighterColor;
            Predmeti.BackColor = DarkerColor;
            Zamjene.BackColor = DarkerColor;

            UpdateNastavnici();
        }

        private void ToolbarDown(object sender, MouseEventArgs e)
        {
            ToolbarIsHeld = true;
        }

        private void Toolbar_CursorChanged(object sender,  MouseEventArgs e)
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

        public void UpdateNastavnici()
        {
            DataEntryList.Refresh();
            DataEntryList.VerticalScroll.Value = 0;

            SuspendLayout();
            DataEntryList.SuspendLayout();
            DataEntryList.Controls.Clear();
            DataEntryLabels.Clear();

            MenuSelection.Location = DefaultSelection;
            Nastavnici.BackColor = LighterColor;
            Predmeti.BackColor = DarkerColor;
            Zamjene.BackColor = DarkerColor;

            DataNastavnici = _nastavniciService.GetList();
            DataEntrySize = new Size(DataEntryList.Width - 25, 122);

            List<int> SearchResults = new List<int>();

            for (int i = 0; i < DataNastavnici.Count; i++)
            {
                Panel p = new Panel();
                p.Name = "DataEntry" + i;
                p.Size = DataEntrySize;
                p.Location = new Point(DataEntryStart.X, DataEntryMargin * i + DataEntryStart.Y);
                p.BackgroundImageLayout = ImageLayout.Stretch;
                p.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                string path = Path.GetDirectoryName(Application.CommonAppDataPath);
                p.BackgroundImage = Image.FromFile(path + "\\Assets\\DataEntry.png");
                p.Tag = i;
                DataEntryList.Controls.Add(p);
                DataEntries.Add(p);

                Label label1 = new Label();
                label1.Name = "Label1Entry" + i;
                label1.Text = "Ime: ";
                label1.Location = DataEntryLabel1Loc;
                label1.Size = new Size(10 + 12 * label1.Text.Length, 24);
                label1.Font = DataEntryLabelRegularFont;
                label1.ForeColor = BrightestColor;
                label1.BackColor = DarkerColor;
                label1.Tag = i;
                p.Controls.Add(label1);

                Label label2 = new Label();
                label2.Name = "Label2Entry" + i;
                label2.Text = "Prezime: ";
                label2.Location = DataEntryLabel2Loc;
                label2.Size = new Size(10 + 12 * label2.Text.Length, 24);
                label2.Font = DataEntryLabelRegularFont;
                label2.ForeColor = BrightestColor;
                label2.BackColor = DarkerColor;
                label2.Tag = i;
                p.Controls.Add(label2);

                Label label3 = new Label();
                label3.Name = "Label3Entry" + i;
                label3.Text = "OIB: ";
                label3.Location = DataEntryLabel3Loc;
                label3.Size = new Size(10 + 12 * label3.Text.Length, 24);
                label3.Font = DataEntryLabelRegularFont;
                label3.ForeColor = BrightestColor;
                label3.BackColor = DarkerColor;
                label3.Tag = i;
                p.Controls.Add(label3);

                Label label8 = new Label();
                label8.Name = "Label8Entry" + i;
                label8.Text = DataNastavnici[i].Kategorija;
                label8.TextAlign = ContentAlignment.TopRight;
                label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                label8.Location = DataEntryLabel8Loc;
                label8.Size = new Size(10 + 12 * label8.Text.Length, 24);
                if (p.Size.Width - label8.Location.X - label8.Size.Width != DataEntryLabel8Offset)
                    label8.Location = new Point(p.Size.Width - label8.Size.Width - DataEntryLabel8Offset, label8.Location.Y);
                label8.Font = DataEntryLabelBoldFont;
                label8.ForeColor = Color.FromArgb(175, 128, 128);
                label8.BackColor = DarkerColor;
                label8.Tag = i;
                p.Controls.Add(label8);
                DataEntryLabels.Add(label8);

                Label outputLabel1 = new Label();
                outputLabel1.Name = "OutputLabel1Entry" + i;
                outputLabel1.Text = DataNastavnici[i].Ime;
                outputLabel1.Size = new Size(10 + 12 * outputLabel1.Text.Length, 24);
                outputLabel1.Location = Point.Add(DataEntryLabel1Loc, new Size(label1.Size.Width, 0));
                outputLabel1.Font = DataEntryLabelBoldFont;
                outputLabel1.ForeColor = BrightestColor;
                outputLabel1.BackColor = DarkerColor;
                outputLabel1.Tag = i;
                p.Controls.Add(outputLabel1);
                DataEntryLabels.Add(outputLabel1);

                Label outputLabel2 = new Label();
                outputLabel2.Name = "OutputLabel2Entry" + i;
                outputLabel2.Text = DataNastavnici[i].Prezime;
                outputLabel2.Size = new Size(10 + 12 * outputLabel2.Text.Length, 24);
                outputLabel2.Location = Point.Add(DataEntryLabel2Loc, new Size(label2.Size.Width, 0));
                outputLabel2.Font = DataEntryLabelBoldFont;
                outputLabel2.ForeColor = BrightestColor;
                outputLabel2.BackColor = DarkerColor;
                outputLabel2.Tag = i;
                p.Controls.Add(outputLabel2);
                DataEntryLabels.Add(outputLabel2);

                Label outputLabel3 = new Label();
                outputLabel3.Name = "OutputLabel3Entry" + i;
                outputLabel3.Text = DataNastavnici[i].OIB;
                outputLabel3.Size = new Size(10 + 12 * outputLabel3.Text.Length, 24);
                outputLabel3.Location = Point.Add(DataEntryLabel3Loc, new Size(label3.Size.Width, 0));
                outputLabel3.Font = DataEntryLabelBoldFont;
                outputLabel3.ForeColor = BrightestColor;
                outputLabel3.Tag = i;
                outputLabel3.BackColor = DarkerColor;
                p.Controls.Add(outputLabel3);
                DataEntryLabels.Add(outputLabel3);

                p.Click += NastavniciDataEntry_Click;
                label1.Click += NastavniciDataEntry_Click;
                label2.Click += NastavniciDataEntry_Click;
                label3.Click += NastavniciDataEntry_Click;
                label8.Click += NastavniciDataEntry_Click;
                outputLabel1.Click += NastavniciDataEntry_Click;
                outputLabel2.Click += NastavniciDataEntry_Click;
                outputLabel3.Click += NastavniciDataEntry_Click;
            }

            if (SearchBox.Text != "")
            {
                SearchResults = SearchBoxCheck(DataEntryLabels);
                DataEntryList.Controls.Clear();

                if (SearchResults.Count == 0)
                    return;

                int offset = 0;

                for (int i = 0; i < DataNastavnici.Count; i++)
                {
                    while (!SearchResults.Contains(i + offset))
                    {
                        if (DataNastavnici.Count == i)
                            break;
                        DataNastavnici.RemoveAt(i);
                        offset++;
                    }
                }

                for (int i = 0; i < DataNastavnici.Count; i++)
                {
                    Panel p = new Panel();
                    p.Name = "DataEntry" + i;
                    p.Size = DataEntrySize;
                    p.Location = new Point(DataEntryStart.X, DataEntryMargin * i + DataEntryStart.Y);
                    p.BackgroundImageLayout = ImageLayout.Stretch;
                    p.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    string path = Path.GetDirectoryName(Application.CommonAppDataPath);
                    p.BackgroundImage = Image.FromFile(path + "\\Assets\\DataEntry.png");
                    p.Tag = i;
                    DataEntryList.Controls.Add(p);
                    DataEntries.Add(p);

                    Label label1 = new Label();
                    label1.Name = "Label1Entry" + i;
                    label1.Text = "Ime: ";
                    label1.Location = DataEntryLabel1Loc;
                    label1.Size = new Size(10 + 12 * label1.Text.Length, 24);
                    label1.Font = DataEntryLabelRegularFont;
                    label1.ForeColor = BrightestColor;
                    label1.BackColor = DarkerColor;
                    label1.Tag = i;
                    p.Controls.Add(label1);
                    DataEntryLabels.Add(label1);

                    Label label2 = new Label();
                    label2.Name = "Label2Entry" + i;
                    label2.Text = "Prezime: ";
                    label2.Location = DataEntryLabel2Loc;
                    label2.Size = new Size(10 + 12 * label2.Text.Length, 24);
                    label2.Font = DataEntryLabelRegularFont;
                    label2.ForeColor = BrightestColor;
                    label2.BackColor = DarkerColor;
                    label2.Tag = i;
                    p.Controls.Add(label2);
                    DataEntryLabels.Add(label2);

                    Label label3 = new Label();
                    label3.Name = "Label3Entry" + i;
                    label3.Text = "OIB: ";
                    label3.Location = DataEntryLabel3Loc;
                    label3.Size = new Size(10 + 12 * label3.Text.Length, 24);
                    label3.Font = DataEntryLabelRegularFont;
                    label3.ForeColor = BrightestColor;
                    label3.BackColor = DarkerColor;
                    label3.Tag = i;
                    p.Controls.Add(label3);
                    DataEntryLabels.Add(label3);

                    Label label8 = new Label();
                    label8.Name = "Label8Entry" + i;
                    label8.Text = DataNastavnici[i].Kategorija;
                    label8.TextAlign = ContentAlignment.TopRight;
                    label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                    label8.Location = DataEntryLabel8Loc;
                    label8.Size = new Size(10 + 12 * label8.Text.Length, 24);
                    if (p.Size.Width - label8.Location.X - label8.Size.Width != DataEntryLabel8Offset)
                        label8.Location = new Point(p.Size.Width - label8.Size.Width - DataEntryLabel8Offset, label8.Location.Y);
                    label8.Font = DataEntryLabelBoldFont;
                    label8.ForeColor = Color.FromArgb(175, 128, 128);
                    label8.BackColor = DarkerColor;
                    label8.Tag = i;
                    p.Controls.Add(label8);
                    DataEntryLabels.Add(label8);

                    Label outputLabel1 = new Label();
                    outputLabel1.Name = "OutputLabel1Entry" + i;
                    outputLabel1.Text = DataNastavnici[i].Ime;
                    outputLabel1.Size = new Size(10 + 12 * outputLabel1.Text.Length, 24);
                    outputLabel1.Location = Point.Add(DataEntryLabel1Loc, new Size(label1.Size.Width, 0));
                    outputLabel1.Font = DataEntryLabelBoldFont;
                    outputLabel1.ForeColor = BrightestColor;
                    outputLabel1.BackColor = DarkerColor;
                    outputLabel1.Tag = i;
                    p.Controls.Add(outputLabel1);
                    DataEntryLabels.Add(outputLabel1);

                    Label outputLabel2 = new Label();
                    outputLabel2.Name = "OutputLabel2Entry" + i;
                    outputLabel2.Text = DataNastavnici[i].Prezime;
                    outputLabel2.Size = new Size(10 + 12 * outputLabel2.Text.Length, 24);
                    outputLabel2.Location = Point.Add(DataEntryLabel2Loc, new Size(label2.Size.Width, 0));
                    outputLabel2.Font = DataEntryLabelBoldFont;
                    outputLabel2.ForeColor = BrightestColor;
                    outputLabel2.BackColor = DarkerColor;
                    outputLabel2.Tag = i;
                    p.Controls.Add(outputLabel2);
                    DataEntryLabels.Add(outputLabel2);

                    Label outputLabel3 = new Label();
                    outputLabel3.Name = "OutputLabel3Entry" + i;
                    outputLabel3.Text = DataNastavnici[i].OIB;
                    outputLabel3.Size = new Size(10 + 12 * outputLabel3.Text.Length, 24);
                    outputLabel3.Location = Point.Add(DataEntryLabel3Loc, new Size(label3.Size.Width, 0));
                    outputLabel3.Font = DataEntryLabelBoldFont;
                    outputLabel3.ForeColor = BrightestColor;
                    outputLabel3.Tag = i;
                    outputLabel3.BackColor = DarkerColor;
                    p.Controls.Add(outputLabel3);
                    DataEntryLabels.Add(outputLabel3);

                    p.Click += NastavniciDataEntry_Click;
                    label1.Click += NastavniciDataEntry_Click;
                    label2.Click += NastavniciDataEntry_Click;
                    label3.Click += NastavniciDataEntry_Click;
                    label8.Click += NastavniciDataEntry_Click;
                    outputLabel1.Click += NastavniciDataEntry_Click;
                    outputLabel2.Click += NastavniciDataEntry_Click;
                    outputLabel3.Click += NastavniciDataEntry_Click;
                }
            }

            DataEntryList.AutoScroll = true;

            ResumeLayout();
            DataEntryList.ResumeLayout();
        }

        public void UpdatePredmeti()
        {
            DataEntryList.Refresh();
            DataEntryList.VerticalScroll.Value = 0;

            SuspendLayout();
            DataEntryList.SuspendLayout();
            DataEntryList.Controls.Clear();
            DataEntryLabels.Clear();

            MenuSelection.Location = new Point(DefaultSelection.X, DefaultSelection.Y + 62);
            Nastavnici.BackColor = DarkerColor;
            Predmeti.BackColor = LighterColor;
            Zamjene.BackColor = DarkerColor;

            DataNastavnici = _nastavniciService.GetList();
            if (RazinaDropDisplay.Text == "" || OznakaDropDisplay.Text == "" || SkolskaGodinaDropDisplay.Text == "")
            {
                DataPredmeti = new List<Predmet>();
                DataEntryList.ResumeLayout();
                ResumeLayout();
                return;
            }
            DataPredmeti = _predmetiService.GetList(int.Parse(RazinaDropDisplay.Text[0].ToString()), OznakaDropDisplay.Text, int.Parse(SkolskaGodinaDropDisplay.Text.Remove(4)));
            DataEntrySize = new Size(DataEntryList.Width - 25, 122);

            List<int> SearchResults = new List<int>();

            for (int i = 0; i < DataPredmeti.Count; i++)
            {
                Panel p = new Panel();
                p.Name = "DataEntry" + i;
                p.Size = DataEntrySize;
                p.Location = new Point(DataEntryStart.X, DataEntryMargin * i + DataEntryStart.Y);
                p.BackgroundImageLayout = ImageLayout.Stretch;
                p.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                string path = Path.GetDirectoryName(Application.CommonAppDataPath);
                p.BackgroundImage = Image.FromFile(path + "\\Assets\\DataEntry.png");
                p.Tag = i;
                DataEntryList.Controls.Add(p);
                DataEntries.Add(p);

                Label label1 = new Label();
                label1.Name = "Label1Entry" + i;
                label1.Text = "Naziv: ";
                label1.Location = DataEntryLabel1Loc;
                label1.Size = new Size(10 + 12 * label1.Text.Length, 24);
                label1.Font = DataEntryLabelRegularFont;
                label1.ForeColor = BrightestColor;
                label1.BackColor = DarkerColor;
                label1.Tag = i;
                p.Controls.Add(label1);

                Label label2 = new Label();
                label2.Name = "Label2Entry" + i;
                label2.Text = "Nastavnik: ";
                label2.Location = DataEntryLabel2Loc;
                label2.Size = new Size(10 + 12 * label2.Text.Length, 24);
                label2.Font = DataEntryLabelRegularFont;
                label2.ForeColor = BrightestColor;
                label2.BackColor = DarkerColor;
                label2.Tag = i;
                p.Controls.Add(label2);

                Label label8 = new Label();
                label8.Name = "Label8Entry" + i;
                label8.Text = DataPredmeti[i].Kategorija;
                label8.TextAlign = ContentAlignment.TopRight;
                label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                label8.Location = DataEntryLabel8Loc;
                label8.Size = new Size(10 + 12 * label8.Text.Length, 24);
                if (p.Size.Width - label8.Location.X - label8.Size.Width != DataEntryLabel8Offset)
                    label8.Location = new Point(p.Size.Width - label8.Size.Width - DataEntryLabel8Offset, label8.Location.Y);
                label8.Font = DataEntryLabelBoldFont;
                label8.ForeColor = Color.FromArgb(175, 128, 128);
                label8.BackColor = DarkerColor;
                label8.Tag = i;
                p.Controls.Add(label8);
                DataEntryLabels.Add(label8);

                Label outputLabel1 = new Label();
                outputLabel1.Name = "OutputLabel1Entry" + i;
                outputLabel1.Text = DataPredmeti[i].Naziv;
                outputLabel1.Size = new Size(10 + 12 * outputLabel1.Text.Length, 24);
                outputLabel1.Location = Point.Add(DataEntryLabel1Loc, new Size(label1.Size.Width, 0));
                outputLabel1.Font = DataEntryLabelBoldFont;
                outputLabel1.ForeColor = BrightestColor;
                outputLabel1.BackColor = DarkerColor;
                outputLabel1.Tag = i;
                p.Controls.Add(outputLabel1);
                DataEntryLabels.Add(outputLabel1);

                Label outputLabel2 = new Label();
                outputLabel2.Name = "OutputLabel2Entry" + i;
                outputLabel2.Text = DataNastavnici.Find(n => n.Id == DataPredmeti[i].NastavnikId).Ime + " " + DataNastavnici.Find(n => n.Id == DataPredmeti[i].NastavnikId).Prezime;
                outputLabel2.Size = new Size(10 + 12 * outputLabel2.Text.Length, 24);
                outputLabel2.Location = Point.Add(DataEntryLabel2Loc, new Size(label2.Size.Width, 0));
                outputLabel2.Font = DataEntryLabelBoldFont;
                outputLabel2.ForeColor = BrightestColor;
                outputLabel2.BackColor = DarkerColor;
                outputLabel2.Tag = i;
                p.Controls.Add(outputLabel2);
                DataEntryLabels.Add(outputLabel2);

                p.Click += PredmetiDataEntry_Click;
                label1.Click += PredmetiDataEntry_Click;
                label2.Click += PredmetiDataEntry_Click;
                label8.Click += PredmetiDataEntry_Click;
                outputLabel1.Click += PredmetiDataEntry_Click;
                outputLabel2.Click += PredmetiDataEntry_Click;
            }

            if (SearchBox.Text != "")
            {
                SearchResults = SearchBoxCheck(DataEntryLabels);
                DataEntryList.Controls.Clear();

                if (SearchResults.Count == 0)
                    return;

                int offset = 0;

                for (int i = 0; i < DataPredmeti.Count; i++)
                {
                    while (!SearchResults.Contains(i + offset))
                    {
                        if (DataPredmeti.Count == i)
                            break;
                        DataPredmeti.RemoveAt(i);
                        offset++;
                    }
                }
            }

            for (int i = 0; i < DataPredmeti.Count; i++)
            {
                Panel p = new Panel();
                p.Name = "DataEntry" + i;
                p.Size = DataEntrySize;
                p.Location = new Point(DataEntryStart.X, DataEntryMargin * i + DataEntryStart.Y);
                p.BackgroundImageLayout = ImageLayout.Stretch;
                p.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                string path = Path.GetDirectoryName(Application.CommonAppDataPath);
                p.BackgroundImage = Image.FromFile(path + "\\Assets\\DataEntry.png");
                p.Tag = i;
                DataEntryList.Controls.Add(p);
                DataEntries.Add(p);

                Label label1 = new Label();
                label1.Name = "Label1Entry" + i;
                label1.Text = "Naziv: ";
                label1.Location = DataEntryLabel1Loc;
                label1.Size = new Size(10 + 12 * label1.Text.Length, 24);
                label1.Font = DataEntryLabelRegularFont;
                label1.ForeColor = BrightestColor;
                label1.BackColor = DarkerColor;
                label1.Tag = i;
                p.Controls.Add(label1);
                DataEntryLabels.Add(label1);

                Label label2 = new Label();
                label2.Name = "Label2Entry" + i;
                label2.Text = "Nastavnik: ";
                label2.Location = DataEntryLabel2Loc;
                label2.Size = new Size(10 + 12 * label2.Text.Length, 24);
                label2.Font = DataEntryLabelRegularFont;
                label2.ForeColor = BrightestColor;
                label2.BackColor = DarkerColor;
                label2.Tag = i;
                p.Controls.Add(label2);
                DataEntryLabels.Add(label2);

                Label label8 = new Label();
                label8.Name = "Label8Entry" + i;
                label8.Text = DataPredmeti[i].Kategorija;
                label8.TextAlign = ContentAlignment.TopRight;
                label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                label8.Location = DataEntryLabel8Loc;
                label8.Size = new Size(10 + 12 * label8.Text.Length, 24);
                if (p.Size.Width - label8.Location.X - label8.Size.Width != DataEntryLabel8Offset)
                    label8.Location = new Point(p.Size.Width - label8.Size.Width - DataEntryLabel8Offset, label8.Location.Y);
                label8.Font = DataEntryLabelBoldFont;
                label8.ForeColor = Color.FromArgb(175, 128, 128);
                label8.BackColor = DarkerColor;
                label8.Tag = i;
                p.Controls.Add(label8);
                DataEntryLabels.Add(label8);

                Label outputLabel1 = new Label();
                outputLabel1.Name = "OutputLabel1Entry" + i;
                outputLabel1.Text = DataPredmeti[i].Naziv;
                outputLabel1.Size = new Size(10 + 12 * outputLabel1.Text.Length, 24);
                outputLabel1.Location = Point.Add(DataEntryLabel1Loc, new Size(label1.Size.Width, 0));
                outputLabel1.Font = DataEntryLabelBoldFont;
                outputLabel1.ForeColor = BrightestColor;
                outputLabel1.BackColor = DarkerColor;
                outputLabel1.Tag = i;
                p.Controls.Add(outputLabel1);
                DataEntryLabels.Add(outputLabel1);

                Label outputLabel2 = new Label();
                outputLabel2.Name = "OutputLabel2Entry" + i;
                outputLabel2.Text = DataNastavnici.Find(n => n.Id == DataPredmeti[i].NastavnikId).Ime + " " + DataNastavnici.Find(n => n.Id == DataPredmeti[i].NastavnikId).Prezime;
                outputLabel2.Size = new Size(10 + 12 * outputLabel2.Text.Length, 24);
                outputLabel2.Location = Point.Add(DataEntryLabel2Loc, new Size(label2.Size.Width, 0));
                outputLabel2.Font = DataEntryLabelBoldFont;
                outputLabel2.ForeColor = BrightestColor;
                outputLabel2.BackColor = DarkerColor;
                outputLabel2.Tag = i;
                p.Controls.Add(outputLabel2);
                DataEntryLabels.Add(outputLabel2);

                p.Click += PredmetiDataEntry_Click;
                label1.Click += PredmetiDataEntry_Click;
                label2.Click += PredmetiDataEntry_Click;
                label8.Click += PredmetiDataEntry_Click;
                outputLabel1.Click += PredmetiDataEntry_Click;
                outputLabel2.Click += PredmetiDataEntry_Click;
            }
            DataEntryList.ResumeLayout();
            ResumeLayout();
        }

        public void UpdateZamjene()
        {
            DataEntryList.Refresh();
            DataEntryList.VerticalScroll.Value = 0;

            SuspendLayout();
            DataEntryList.SuspendLayout();
            DataEntryList.Controls.Clear();
            DataEntryLabels.Clear();

            MenuSelection.Location = new Point(DefaultSelection.X, DefaultSelection.Y + 124);
            Nastavnici.BackColor = DarkerColor;
            Predmeti.BackColor = DarkerColor;
            Zamjene.BackColor = LighterColor;

            DataNastavnici = _nastavniciService.GetList();
            if (RazinaDropDisplay.Text == "" || OznakaDropDisplay.Text == "" || SkolskaGodinaDropDisplay.Text == "")
            {
                DataZamjene = new List<Zamjena>();
                DataEntryList.ResumeLayout();
                ResumeLayout();
                return;
            }
            DataPredmeti = _predmetiService.GetList(int.Parse(RazinaDropDisplay.Text[0].ToString()), OznakaDropDisplay.Text, int.Parse(SkolskaGodinaDropDisplay.Text.Remove(4)));
            DataZamjene = _zamjeneService.GetList(int.Parse(RazinaDropDisplay.Text[0].ToString()), OznakaDropDisplay.Text, int.Parse(SkolskaGodinaDropDisplay.Text.Remove(4)));
            DataEntrySize = new Size(DataEntryList.Width - 25, 122);

            List<int> SearchResults = new List<int>();

            for (int i = 0; i < DataZamjene.Count; i++)
            {
                Panel p = new Panel();
                p.Name = "DataEntry" + i;
                p.Size = DataEntrySize;
                p.Location = new Point(DataEntryStart.X, DataEntryMargin * i + DataEntryStart.Y);
                p.BackgroundImageLayout = ImageLayout.Stretch;
                p.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                string path = Path.GetDirectoryName(Application.CommonAppDataPath);
                p.BackgroundImage = Image.FromFile(path + "\\Assets\\DataEntry.png");
                p.Tag = i;
                DataEntryList.Controls.Add(p);
                DataEntries.Add(p);

                Label label1 = new Label();
                label1.Name = "Label1Entry" + i;
                label1.Text = "Nastavnik: ";
                label1.Location = DataEntryLabel1Loc;
                label1.Size = new Size(10 + 12 * label1.Text.Length, 24);
                label1.Font = DataEntryLabelRegularFont;
                label1.ForeColor = BrightestColor;
                label1.BackColor = DarkerColor;
                label1.Tag = i;
                p.Controls.Add(label1);

                Label label2 = new Label();
                label2.Name = "Label2Entry" + i;
                label2.Text = "Predmet: ";
                label2.Location = DataEntryLabel2Loc;
                label2.Size = new Size(10 + 12 * label2.Text.Length, 24);
                label2.Font = DataEntryLabelRegularFont;
                label2.ForeColor = BrightestColor;
                label2.BackColor = DarkerColor;
                label2.Tag = i;
                p.Controls.Add(label2);

                Label label3 = new Label();
                label3.Name = "Label3Entry" + i;
                label3.Text = "Od: ";
                label3.Location = DataEntryLabel3Loc;
                label3.Size = new Size(10 + 12 * label3.Text.Length, 24);
                label3.Font = DataEntryLabelRegularFont;
                label3.ForeColor = BrightestColor;
                label3.BackColor = DarkerColor;
                label3.Tag = i;
                p.Controls.Add(label3);

                Label label4 = new Label();
                label4.Name = "Label4Entry" + i;
                label4.Text = "Do: ";
                label4.Location = DataEntryLabel4Loc;
                label4.Size = new Size(10 + 12 * label4.Text.Length, 24);
                label4.Font = DataEntryLabelRegularFont;
                label4.ForeColor = BrightestColor;
                label4.BackColor = DarkerColor;
                label4.Tag = i;
                p.Controls.Add(label4);

                Label outputLabel1 = new Label();
                outputLabel1.Name = "OutputLabel1Entry" + i;
                outputLabel1.Text = DataNastavnici.Find(n => n.Id == DataZamjene[i].NastavnikId).Ime + " " + DataNastavnici.Find(n => n.Id == DataZamjene[i].NastavnikId).Prezime;
                outputLabel1.Size = new Size(10 + 12 * outputLabel1.Text.Length, 24);
                outputLabel1.Location = Point.Add(DataEntryLabel1Loc, new Size(label1.Size.Width, 0));
                outputLabel1.Font = DataEntryLabelBoldFont;
                outputLabel1.ForeColor = BrightestColor;
                outputLabel1.BackColor = DarkerColor;
                outputLabel1.Tag = i;
                p.Controls.Add(outputLabel1);
                DataEntryLabels.Add(outputLabel1);

                Label outputLabel2 = new Label();
                outputLabel2.Name = "OutputLabel2Entry" + i;
                outputLabel2.Text = DataPredmeti.Find(P => P.Id == DataZamjene[i].PredmetId).Naziv;
                outputLabel2.Size = new Size(10 + 12 * outputLabel2.Text.Length, 24);
                outputLabel2.Location = Point.Add(DataEntryLabel2Loc, new Size(label2.Size.Width, 0));
                outputLabel2.Font = DataEntryLabelBoldFont;
                outputLabel2.ForeColor = BrightestColor;
                outputLabel2.BackColor = DarkerColor;
                outputLabel2.Tag = i;
                p.Controls.Add(outputLabel2);
                DataEntryLabels.Add(outputLabel2);

                Label outputLabel3 = new Label();
                outputLabel3.Name = "OutputLabel3Entry" + i;
                outputLabel3.Text = DataZamjene[i].DatumOd.ToShortDateString();
                outputLabel3.Size = new Size(10 + 12 * outputLabel3.Text.Length, 24);
                outputLabel3.Location = Point.Add(DataEntryLabel3Loc, new Size(label3.Size.Width, 0));
                outputLabel3.Font = DataEntryLabelBoldFont;
                outputLabel3.ForeColor = BrightestColor;
                outputLabel3.BackColor = DarkerColor;
                outputLabel3.Tag = i;
                p.Controls.Add(outputLabel3);
                DataEntryLabels.Add(outputLabel3);

                Label outputLabel4 = new Label();
                outputLabel4.Name = "OutputLabel4Entry" + i;
                outputLabel4.Text = DataZamjene[i].DatumDo.ToShortDateString();
                outputLabel4.Size = new Size(10 + 12 * outputLabel4.Text.Length, 24);
                outputLabel4.Location = Point.Add(DataEntryLabel4Loc, new Size(label4.Size.Width, 0));
                outputLabel4.Font = DataEntryLabelBoldFont;
                outputLabel4.ForeColor = BrightestColor;
                outputLabel4.BackColor = DarkerColor;
                outputLabel4.Tag = i;
                p.Controls.Add(outputLabel4);
                DataEntryLabels.Add(outputLabel4);

                p.Click += ZamjeneDataEntry_Click;
                label1.Click += ZamjeneDataEntry_Click;
                label2.Click += ZamjeneDataEntry_Click;
                label3.Click += ZamjeneDataEntry_Click;
                label4.Click += ZamjeneDataEntry_Click;
                outputLabel1.Click += ZamjeneDataEntry_Click;
                outputLabel2.Click += ZamjeneDataEntry_Click;
                outputLabel3.Click += ZamjeneDataEntry_Click;
                outputLabel4.Click += ZamjeneDataEntry_Click;
            }

            if (SearchBox.Text != "")
            {
                SearchResults = SearchBoxCheck(DataEntryLabels);
                DataEntryList.Controls.Clear();

                if (SearchResults.Count == 0)
                    return;

                int offset = 0;

                for (int i = 0; i < DataZamjene.Count; i++)
                {
                    while (!SearchResults.Contains(i + offset))
                    {
                        if (DataZamjene.Count == i)
                            break;
                        DataZamjene.RemoveAt(i);
                        offset++;
                    }
                }
            }
            else
            {
                DataEntryList.ResumeLayout();
                ResumeLayout();
                return;
            }
            for (int i = 0; i < DataZamjene.Count; i++)
            {
                Panel p = new Panel();
                p.Name = "DataEntry" + i;
                p.Size = DataEntrySize;
                p.Location = new Point(DataEntryStart.X, DataEntryMargin * i + DataEntryStart.Y);
                p.BackgroundImageLayout = ImageLayout.Stretch;
                p.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                string path = Path.GetDirectoryName(Application.CommonAppDataPath);
                p.BackgroundImage = Image.FromFile(path + "\\Assets\\DataEntry.png");
                p.Tag = i;
                DataEntryList.Controls.Add(p);
                DataEntries.Add(p);

                Label label1 = new Label();
                label1.Name = "Label1Entry" + i;
                label1.Text = "Nastavnik: ";
                label1.Location = DataEntryLabel1Loc;
                label1.Size = new Size(10 + 12 * label1.Text.Length, 24);
                label1.Font = DataEntryLabelRegularFont;
                label1.ForeColor = BrightestColor;
                label1.BackColor = DarkerColor;
                label1.Tag = i;
                p.Controls.Add(label1);
                DataEntryLabels.Add(label1);

                Label label2 = new Label();
                label2.Name = "Label2Entry" + i;
                label2.Text = "Predmet: ";
                label2.Location = DataEntryLabel2Loc;
                label2.Size = new Size(10 + 12 * label2.Text.Length, 24);
                label2.Font = DataEntryLabelRegularFont;
                label2.ForeColor = BrightestColor;
                label2.BackColor = DarkerColor;
                label2.Tag = i;
                p.Controls.Add(label2);
                DataEntryLabels.Add(label2);

                Label label3 = new Label();
                label3.Name = "Label3Entry" + i;
                label3.Text = "Od: ";
                label3.Location = DataEntryLabel3Loc;
                label3.Size = new Size(10 + 12 * label3.Text.Length, 24);
                label3.Font = DataEntryLabelRegularFont;
                label3.ForeColor = BrightestColor;
                label3.BackColor = DarkerColor;
                label3.Tag = i;
                p.Controls.Add(label3);
                DataEntryLabels.Add(label3);

                Label label4 = new Label();
                label4.Name = "Label4Entry" + i;
                label4.Text = "Do: ";
                label4.Location = DataEntryLabel4Loc;
                label4.Size = new Size(10 + 12 * label4.Text.Length, 24);
                label4.Font = DataEntryLabelRegularFont;
                label4.ForeColor = BrightestColor;
                label4.BackColor = DarkerColor;
                label4.Tag = i;
                p.Controls.Add(label4);
                DataEntryLabels.Add(label4);

                Label outputLabel1 = new Label();
                outputLabel1.Name = "OutputLabel1Entry" + i;
                outputLabel1.Text = DataNastavnici.Find(n => n.Id == DataZamjene[i].NastavnikId).Ime + " " + DataNastavnici.Find(n => n.Id == DataZamjene[i].NastavnikId).Prezime;
                outputLabel1.Size = new Size(10 + 12 * outputLabel1.Text.Length, 24);
                outputLabel1.Location = Point.Add(DataEntryLabel1Loc, new Size(label1.Size.Width, 0));
                outputLabel1.Font = DataEntryLabelBoldFont;
                outputLabel1.ForeColor = BrightestColor;
                outputLabel1.BackColor = DarkerColor;
                outputLabel1.Tag = i;
                p.Controls.Add(outputLabel1);
                DataEntryLabels.Add(outputLabel1);

                Label outputLabel2 = new Label();
                outputLabel2.Name = "OutputLabel2Entry" + i;
                outputLabel2.Text = DataPredmeti.Find(P => P.Id == DataZamjene[i].PredmetId).Naziv;
                outputLabel2.Size = new Size(10 + 12 * outputLabel2.Text.Length, 24);
                outputLabel2.Location = Point.Add(DataEntryLabel2Loc, new Size(label2.Size.Width, 0));
                outputLabel2.Font = DataEntryLabelBoldFont;
                outputLabel2.ForeColor = BrightestColor;
                outputLabel2.BackColor = DarkerColor;
                outputLabel2.Tag = i;
                p.Controls.Add(outputLabel2);
                DataEntryLabels.Add(outputLabel2);

                Label outputLabel3 = new Label();
                outputLabel3.Name = "OutputLabel3Entry" + i;
                outputLabel3.Text = DataZamjene[i].DatumOd.ToShortDateString();
                outputLabel3.Size = new Size(10 + 12 * outputLabel3.Text.Length, 24);
                outputLabel3.Location = Point.Add(DataEntryLabel3Loc, new Size(label3.Size.Width, 0));
                outputLabel3.Font = DataEntryLabelBoldFont;
                outputLabel3.ForeColor = BrightestColor;
                outputLabel3.BackColor = DarkerColor;
                outputLabel3.Tag = i;
                p.Controls.Add(outputLabel3);
                DataEntryLabels.Add(outputLabel3);

                Label outputLabel4 = new Label();
                outputLabel4.Name = "OutputLabel4Entry" + i;
                outputLabel4.Text = DataZamjene[i].DatumDo.ToShortDateString();
                outputLabel4.Size = new Size(10 + 12 * outputLabel4.Text.Length, 24);
                outputLabel4.Location = Point.Add(DataEntryLabel4Loc, new Size(label4.Size.Width, 0));
                outputLabel4.Font = DataEntryLabelBoldFont;
                outputLabel4.ForeColor = BrightestColor;
                outputLabel4.BackColor = DarkerColor;
                outputLabel4.Tag = i;
                p.Controls.Add(outputLabel4);
                DataEntryLabels.Add(outputLabel4);

                p.Click += ZamjeneDataEntry_Click;
                label1.Click += ZamjeneDataEntry_Click;
                label2.Click += ZamjeneDataEntry_Click;
                label3.Click += ZamjeneDataEntry_Click;
                label4.Click += ZamjeneDataEntry_Click;
                outputLabel1.Click += ZamjeneDataEntry_Click;
                outputLabel2.Click += ZamjeneDataEntry_Click;
                outputLabel3.Click += ZamjeneDataEntry_Click;
                outputLabel4.Click += ZamjeneDataEntry_Click;
            }
            DataEntryList.ResumeLayout();
            ResumeLayout();
        }

        private List<int> SearchBoxCheck(List<Label> labels)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < labels.Count; i++)
            {
                try
                {
                    if (labels[i].Text.Contains(SearchBox.Text))
                        result.Add(Convert.ToInt32(labels[i].Tag));
                }
                catch (IndexOutOfRangeException)
                {
                    continue;
                }
            }

            result = result.Distinct().ToList();
            return result;
        }

        private void ToolbarUp(object sender, MouseEventArgs e)
        {
            ToolbarIsHeld = false;
            MousePos = Point.Empty;

            if (Location.Y < 0)
                Location = Point.Add(Location,new Size(0, -Location.Y));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
                WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(Application.CommonAppDataPath);

            RazinaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredDropNew.png");
            RazinaDropped = false;
            OznakaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredDropNew.png");
            OznakaDropped = false;
            SkolskaGodinaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\GodinaDropNew.png");
            GodinaDropped = false;

            foreach (Panel p in RazinaDropPanels)
                Controls.Remove(p);
            RazinaDropPanels.Clear();
            foreach (Panel p in OznakaDropPanels)
                Controls.Remove(p);
            OznakaDropPanels.Clear();
            foreach (Panel p in SkolskaGodinaDropPanels)
                Controls.Remove(p);
            SkolskaGodinaDropPanels.Clear();
            foreach (Label l in OznakaDropLabels)
                Controls.Remove(l);
            OznakaDropLabels.Clear();
            foreach (Label l in RazinaDropLabels)
                Controls.Remove(l);
            RazinaDropLabels.Clear();
            foreach (Label l in SkolskaGodinaDropLabels)
                Controls.Remove(l);
            SkolskaGodinaDropLabels.Clear();

            if (IsMaximized)
                IsMaximized = false;
            else IsMaximized = true;

            if (IsMaximized)
                WindowState = FormWindowState.Maximized;
            else WindowState = FormWindowState.Normal;

            RazinaDropStart = new Point(Size.Width - 361, 73);
            OznakaDropStart = new Point(Size.Width - 246, 73);
            SkolskaGodinaDropStart = new Point(Size.Width - 121, 73);
        }

        private void Nastavnici_Click(object sender, EventArgs e)
        {
            SearchBox.Clear();

            UpdateNastavnici();
        }

        private void Predmeti_Click(object sender, EventArgs e)
        {
            SearchBox.Clear();

            UpdatePredmeti();
        }

        private void Zamjene_Click(object sender, EventArgs e)
        {
            SearchBox.Clear();

            UpdateZamjene();
        }

        private void OznakaDrop_Click(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(Application.CommonAppDataPath);
            SkolskaGodinaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\GodinaDropNew.png");
            GodinaDropped = false;
            RazinaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredDropNew.png");
            foreach (Panel p in SkolskaGodinaDropPanels)
                Controls.Remove(p);
            SkolskaGodinaDropPanels.Clear();
            foreach (Panel p in RazinaDropPanels)
                Controls.Remove(p);
            RazinaDropPanels.Clear();
            foreach (Label l in RazinaDropLabels)
                Controls.Remove(l);
            RazinaDropLabels.Clear();
            foreach (Label l in SkolskaGodinaDropLabels)
                Controls.Remove(l);
            SkolskaGodinaDropLabels.Clear();
            if (!OznakaDropped)
            {
                OznakaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredDropped.png");
                OznakaDropped = true;
                int i;
                try
                {
                    OznakaDropContent = _razredService.GetList(int.Parse(SkolskaGodinaDropDisplay.Text.Remove(4)));
                    List<string> uniques = new List<string>();
                    bool isUnique = true;
                    foreach (string str in OznakaDropContent)
                    {
                        foreach (string unique in uniques)
                            if (str[str.Length - 1].ToString() == unique)
                                isUnique = false;
                        if (isUnique)
                        {
                            uniques.Add(str[str.Length - 1].ToString());
                        }
                        isUnique = true;
                    }
                    OznakaDropContent.Clear();
                    OznakaDropContent.AddRange(uniques);

                    i = DropOznaka(path);
                } catch (Exception)
                {
                    i = 0;
                }

                Panel addOznaka = new Panel();
                int x = OznakaDropStart.X;
                int y = DropMargin * i + OznakaDropStart.Y;
                addOznaka.Name = "OznakaAddPanel";
                addOznaka.Location = new Point(x, y);
                addOznaka.Size = RazredDropSize;
                addOznaka.BackgroundImage = Image.FromFile(path + "\\Assets\\AddRazred.png");
                addOznaka.BackgroundImageLayout = ImageLayout.Zoom;
                addOznaka.Click += AddRazred;
                addOznaka.SendToBack();
                OznakaDrop.BringToFront();
                OznakaDropPanels.Add(addOznaka);
                Controls.Add(addOznaka);

                DataEntryList.SendToBack();
            }
            else
            {
                OznakaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredDropNew.png");
                foreach (Panel p in OznakaDropPanels)
                    Controls.Remove(p);
                OznakaDropPanels.Clear();
                foreach (Label l in OznakaDropLabels)
                    Controls.Remove(l);
                OznakaDropLabels.Clear();
                OznakaDropped = false;
            }

        }

        private void RazinaDrop_Click(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(Application.CommonAppDataPath);
            SkolskaGodinaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\GodinaDropNew.png");
            GodinaDropped = false;
            OznakaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredDropNew.png");
            OznakaDropped = false;
            foreach (Panel p in SkolskaGodinaDropPanels)
                Controls.Remove(p);
            SkolskaGodinaDropPanels.Clear();
            foreach (Panel p in OznakaDropPanels)
                Controls.Remove(p);
            OznakaDropPanels.Clear();
            foreach (Label l in OznakaDropLabels)
                Controls.Remove(l);
            OznakaDropLabels.Clear();
            foreach (Label l in SkolskaGodinaDropLabels)
                Controls.Remove(l);
            SkolskaGodinaDropLabels.Clear();
            if (!RazinaDropped)
            {
                RazinaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredDropped.png");
                RazinaDropped = true;
                int i;
                try
                {
                    RazinaDropContent = _razredService.GetList(int.Parse(SkolskaGodinaDropDisplay.Text.Remove(4)));
                    List<string> uniques = new List<string>();
                    bool isUnique = true;
                    foreach (string str in RazinaDropContent)
                    {
                        foreach (string unique in uniques)
                            if (str[0].ToString() == unique)
                                isUnique = false;
                        if (isUnique)
                        {
                            uniques.Add(str[0].ToString());
                        }
                        isUnique = true;
                    }
                    RazinaDropContent.Clear();
                    RazinaDropContent.AddRange(uniques);

                    i = DropRazina(path);
                } catch (Exception)
                {
                    i = 0;
                }

                Panel addRazina = new Panel();
                int x = RazinaDropStart.X;
                int y = DropMargin * i + RazinaDropStart.Y;
                addRazina.Name = "RazinaAddPanel";
                addRazina.Location = new Point(x, y);
                addRazina.Size = RazredDropSize;
                addRazina.BackgroundImage = Image.FromFile(path + "\\Assets\\AddRazred.png");
                addRazina.BackgroundImageLayout = ImageLayout.Zoom;
                addRazina.Click += AddRazred;
                addRazina.SendToBack();
                RazinaDrop.BringToFront();
                RazinaDropPanels.Add(addRazina);
                Controls.Add(addRazina); 
                
                DataEntryList.SendToBack();
            }
            else
            {
                RazinaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredDropNew.png");
                foreach (Panel p in RazinaDropPanels)
                    Controls.Remove(p);
                RazinaDropPanels.Clear();
                foreach (Label l in RazinaDropLabels)
                    Controls.Remove(l);
                RazinaDropLabels.Clear();
                RazinaDropped = false;
            }
        }

        private void SkolskaGodinaDrop_Click(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(Application.CommonAppDataPath);
            OznakaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredDropNew.png");
            RazinaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredDropNew.png");
            RazinaDropped = false;
            OznakaDropped = false;
            foreach (Panel p in RazinaDropPanels)
                Controls.Remove(p);
            RazinaDropPanels.Clear();
            foreach (Panel p in OznakaDropPanels)
                Controls.Remove(p);
            OznakaDropPanels.Clear();
            foreach (Label l in OznakaDropLabels)
                Controls.Remove(l);
            OznakaDropLabels.Clear();
            foreach (Label l in RazinaDropLabels)
                Controls.Remove(l);
            RazinaDropLabels.Clear();
            if (!GodinaDropped)
            {
                SkolskaGodinaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\GodinaDropped.png");
                GodinaDropped = true;

                SkolskaGodinaDropContent = _skolskeGodineService.GetList();

                int i = DropGodina(path);

                Panel addGodina = new Panel();
                int x = SkolskaGodinaDropStart.X;
                int y = DropMargin * i + SkolskaGodinaDropStart.Y;
                addGodina.Name = "SkolskaGodinaAddPanel";
                addGodina.Location = new Point(x, y);
                addGodina.Size = SkolskaGodinaDropSize;
                addGodina.BackgroundImage = Image.FromFile(path + "\\Assets\\AddGodina.png");
                addGodina.BackgroundImageLayout = ImageLayout.Zoom;
                addGodina.Click += AddSkolskaGodina;
                addGodina.SendToBack();
                SkolskaGodinaDrop.BringToFront();
                SkolskaGodinaDropPanels.Add(addGodina);
                Controls.Add(addGodina);

                DataEntryList.SendToBack();
            }
            else
            {
                SkolskaGodinaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\GodinaDropNew.png");
                GodinaDropped = false;
                foreach (Panel p in SkolskaGodinaDropPanels)
                    Controls.Remove(p);
                SkolskaGodinaDropPanels.Clear();
                foreach (Label l in SkolskaGodinaDropLabels)
                    Controls.Remove(l);
                SkolskaGodinaDropLabels.Clear();
            }
        }

        private int DropOznaka(string path)
        {
            Panel p;
            Label l;
            if (OznakaDropContent is null)
                return 0;
            OznakaDropContent.Sort();
            int i;
            for (i = 0; i < OznakaDropContent.Count; i++)
            {
                p = new Panel();
                int x = OznakaDropStart.X;
                int y = DropMargin * i + DropMargin + OznakaDropStart.Y;
                p.Name = "OznakaDropPanel" + (i + 1);
                p.Location = new Point(x, y);
                p.BackColor = Color.FromArgb(0,0,0,0);

                if (i == OznakaDropContent.Count - 1)
                {
                    p.Size = new Size(RazredDropSize.Width, RazredDropSize.Height / 2 + 6);
                    p.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredComponentSmall.png");
                }
                else
                {
                    p.Size = RazredDropSize;
                    p.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredComponent.png");
                }
                p.BackgroundImageLayout = ImageLayout.Zoom;
                OznakaDropPanels.Add(p);
                Controls.Add(p);
                l = new Label { Parent = p };
                l.BringToFront();
                l.Font = RazredDropLabelFont;
                l.BackColor = LighterColor;
                l.ForeColor = RazredDropLabelForeColor;
                l.Size = new Size(25, 25);
                x = OznakaDropStart.X + RazredDropLabelLocation.X;
                y = OznakaDropStart.Y + DropMargin * i + RazredDropLabelLocation.Y;
                l.Location = new Point(x, y);
                l.Name = "OznakaDropLabel" + (i + 1);
                l.Text = OznakaDropContent[i];
                OznakaDropLabels.Add(l);
                Controls.Add(l);
                l.Click += SelectFromDrop;
                p.Click += SelectFromDrop;
            }
            int s = i;
            for (i = OznakaDropPanels.Count - 1; i >= 0; i--)
            {
                OznakaDropPanels[i].SendToBack();
            }
            return s;
        }

        private int DropRazina(string path)
        {
            Panel p;
            if (RazinaDropContent is null)
                return 0;
            RazinaDropContent.Sort();
            int i;
            for (i = 0; i < RazinaDropContent.Count; i++)
            {
                p = new Panel();
                int x = RazinaDropStart.X;
                int y = DropMargin * i + DropMargin + RazinaDropStart.Y;
                p.Name = "RazinaDropPanel" + (i + 1);
                p.Location = new Point(x, y);
                if (i == RazinaDropContent.Count - 1)
                {
                    p.Size = new Size(RazredDropSize.Width, RazredDropSize.Height / 2 + 6);
                    p.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredComponentSmall.png");
                }
                else
                {
                    p.Size = RazredDropSize;
                    p.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredComponent.png");
                }
                p.BackgroundImageLayout = ImageLayout.Zoom;
                RazinaDropPanels.Add(p);
                Controls.Add(p);
                Label l = new Label { Parent = p };
                l.BringToFront();
                l.Font = RazredDropLabelFont;
                l.BackColor = LighterColor;
                l.ForeColor = RazredDropLabelForeColor;
                l.Size = new Size(30, 25);
                x = RazinaDropStart.X + RazredDropLabelLocation.X;
                y = RazinaDropStart.Y + DropMargin * i + RazredDropLabelLocation.Y;
                l.Location = new Point(x, y);
                l.Name = "RazinaDropLabel" + (i + 1);
                l.Text = RazinaDropContent[i] + ".";
                RazinaDropLabels.Add(l);
                Controls.Add(l);
                l.Click += SelectFromDrop;
                p.Click += SelectFromDrop;
            }
            int s = i;
            for (i = RazinaDropPanels.Count - 1; i >= 0; i--)
            {
                RazinaDropPanels[i].SendToBack();
            }
            return s;
        }

        private int DropGodina(string path)
        {
            Panel p;
            if (SkolskaGodinaDropContent is null)
                return 0;
            SkolskaGodinaDropContent.Sort();
            int i;
            for (i = 0; i < SkolskaGodinaDropContent.Count; i++)
            {
                p = new Panel();
                int x = SkolskaGodinaDropStart.X;
                int y = DropMargin * i + DropMargin + SkolskaGodinaDropStart.Y;
                p.Name = "SkolskaGodinaDropPanel" + (i + 1);
                p.Location = new Point(x, y);
                if (i == SkolskaGodinaDropContent.Count - 1)
                {
                    p.Size = new Size(SkolskaGodinaDropSize.Width, SkolskaGodinaDropSize.Height / 2 + 6);
                    p.BackgroundImage = Image.FromFile(path + "\\Assets\\GodinaComponentSmall.png");
                }
                else
                {
                    p.Size = SkolskaGodinaDropSize;
                    p.BackgroundImage = Image.FromFile(path + "\\Assets\\GodinaComponent.png");
                }
                p.BackgroundImageLayout = ImageLayout.Zoom;
                SkolskaGodinaDropPanels.Add(p);
                Controls.Add(p);
                Label l = new Label { Parent = p };
                l.BringToFront();
                l.Font = GodinaDropLabelFont;
                l.BackColor = LighterColor;
                l.ForeColor = RazredDropLabelForeColor;
                l.Size = new Size(95, 25);
                x = SkolskaGodinaDropStart.X + GodinaDropLabelLocation.X;
                y = SkolskaGodinaDropStart.Y + DropMargin * i + GodinaDropLabelLocation.Y;
                l.Location = new Point(x, y);
                l.Name = "SkolskaGodinaDropLabel" + (i + 1);
                l.Text = SkolskaGodinaDropContent[i];
                SkolskaGodinaDropLabels.Add(l);
                Controls.Add(l);
                l.Click += SelectFromDrop;
                p.Click += SelectFromDrop;
            }
            int s = i;
            for (i = SkolskaGodinaDropPanels.Count - 1; i >= 0; i--)
            {
                SkolskaGodinaDropPanels[i].SendToBack();
            }
            return s;
        }

        private void SelectFromDrop(object sender, EventArgs e)
        {
            string controlName = (sender as Control).Name;
            string index = controlName[controlName.Length - 1].ToString();
            SearchBox.Clear();
            switch (controlName[0].ToString().ToUpper())
            {
                case "R": 
                    RazinaDropDisplay.Text = RazinaDropContent[Convert.ToInt32(index) - 1] + "."; 
                    break;
                case "O": 
                    OznakaDropDisplay.Text = OznakaDropContent[Convert.ToInt32(index) - 1]; 
                    break;
                case "S":
                    SkolskaGodinaDropDisplay.Text = SkolskaGodinaDropContent[Convert.ToInt32(index) - 1];
                    RazinaDropContent = _razredService.GetList(int.Parse(SkolskaGodinaDropDisplay.Text[0].ToString() + SkolskaGodinaDropDisplay.Text[1].ToString() + SkolskaGodinaDropDisplay.Text[2] + SkolskaGodinaDropDisplay.Text[3].ToString()));
                    OznakaDropContent = _razredService.GetList(int.Parse(SkolskaGodinaDropDisplay.Text[0].ToString() + SkolskaGodinaDropDisplay.Text[1].ToString() + SkolskaGodinaDropDisplay.Text[2] + SkolskaGodinaDropDisplay.Text[3].ToString()));
                    if (CurrentGodina != SkolskaGodinaDropDisplay.Text)
                        RazinaDropDisplay.Text = OznakaDropDisplay.Text = "";
                    CurrentGodina = SkolskaGodinaDropDisplay.Text;
                    break;
                default:
                    break;
            }
            string path = Path.GetDirectoryName(Application.CommonAppDataPath);

            RazinaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredDropNew.png");
            OznakaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredDropNew.png");
            SkolskaGodinaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\GodinaDropNew.png");
            RazinaDropped = false;
            OznakaDropped = false;
            GodinaDropped = false;

            foreach (Panel p in RazinaDropPanels)
                Controls.Remove(p);
            RazinaDropPanels.Clear();
            foreach (Panel p in OznakaDropPanels)
                Controls.Remove(p);
            OznakaDropPanels.Clear();
            foreach (Panel p in SkolskaGodinaDropPanels)
                Controls.Remove(p);
            SkolskaGodinaDropPanels.Clear();
            foreach (Label l in OznakaDropLabels)
                Controls.Remove(l);
            OznakaDropLabels.Clear();
            foreach (Label l in RazinaDropLabels)
                Controls.Remove(l);
            RazinaDropLabels.Clear();
            foreach (Label l in SkolskaGodinaDropLabels)
                Controls.Remove(l);
            SkolskaGodinaDropLabels.Clear();

            switch (MenuSelection.Location.Y)
            {
                case 161:
                    UpdatePredmeti();
                    break;
                case 223:
                    UpdateZamjene();
                    break;
                default:
                    break;
            }
        }

        private void HideDrops(object sender, MouseEventArgs e)
        {
            string path = Path.GetDirectoryName(Application.CommonAppDataPath);

            RazinaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredDropNew.png");
            OznakaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredDropNew.png");
            SkolskaGodinaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\GodinaDropNew.png");
            RazinaDropped = false;
            OznakaDropped = false;
            GodinaDropped = false;

            foreach (Panel p in RazinaDropPanels)
                Controls.Remove(p);
            RazinaDropPanels.Clear();
            foreach (Panel p in OznakaDropPanels)
                Controls.Remove(p);
            OznakaDropPanels.Clear();
            foreach (Panel p in SkolskaGodinaDropPanels)
                Controls.Remove(p);
            SkolskaGodinaDropPanels.Clear();
            foreach (Label l in OznakaDropLabels)
                Controls.Remove(l);
            OznakaDropLabels.Clear();
            foreach (Label l in RazinaDropLabels)
                Controls.Remove(l);
            RazinaDropLabels.Clear();
            foreach (Label l in SkolskaGodinaDropLabels)
                Controls.Remove(l);
            SkolskaGodinaDropLabels.Clear();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(Application.CommonAppDataPath);

            RazinaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredDropNew.png");
            OznakaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredDropNew.png");
            SkolskaGodinaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\GodinaDropNew.png");
            RazinaDropped = false;
            OznakaDropped = false;
            GodinaDropped = false;

            foreach (Panel p in RazinaDropPanels)
                Controls.Remove(p);
            RazinaDropPanels.Clear();
            foreach (Panel p in OznakaDropPanels)
                Controls.Remove(p);
            OznakaDropPanels.Clear();
            foreach (Panel p in SkolskaGodinaDropPanels)
                Controls.Remove(p);
            SkolskaGodinaDropPanels.Clear();
            foreach (Label l in OznakaDropLabels)
                Controls.Remove(l);
            OznakaDropLabels.Clear();
            foreach (Label l in RazinaDropLabels)
                Controls.Remove(l);
            RazinaDropLabels.Clear();
            foreach (Label l in SkolskaGodinaDropLabels)
                Controls.Remove(l);
            SkolskaGodinaDropLabels.Clear();
        }

        private void AddRazred(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(224,224,224);
            Toolbar.BackColor = Color.FromArgb(224, 224, 224);
            DataEntryList.BackColor = Color.FromArgb(224, 224, 224);

            AddGodinaRazred addGodinaRazred;

            if (SkolskaGodinaDropDisplay.Text == "")
                addGodinaRazred = new AddGodinaRazred();
            else addGodinaRazred = new AddGodinaRazred(SkolskaGodinaDropDisplay.Text);

            addGodinaRazred.StartPosition = FormStartPosition.CenterScreen;

            addGodinaRazred.ShowDialog();

            var s = _skolskeGodineService.GetSingle(int.Parse(CurrentGodina.Remove(4)));

            if (s is null)
            {
                CurrentGodina = null;
                SkolskaGodinaDropDisplay.Text = "";
                RazinaDropDisplay.Text = "";
                OznakaDropDisplay.Text = "";
            }

            BackColor = Color.FromArgb(255, 255, 255);
            Toolbar.BackColor = Color.FromArgb(255, 255, 255);
            DataEntryList.BackColor = Color.FromArgb(255, 255, 255);


            string path = Path.GetDirectoryName(Application.CommonAppDataPath);

            RazinaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredDropNew.png");
            RazinaDropped = false;
            OznakaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredDropNew.png");
            OznakaDropped = false;
            SkolskaGodinaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\GodinaDropNew.png");
            GodinaDropped = false;

            foreach (Panel p in RazinaDropPanels)
                Controls.Remove(p);
            RazinaDropPanels.Clear();
            foreach (Panel p in OznakaDropPanels)
                Controls.Remove(p);
            OznakaDropPanels.Clear();
            foreach (Panel p in SkolskaGodinaDropPanels)
                Controls.Remove(p);
            SkolskaGodinaDropPanels.Clear();
            foreach (Label l in OznakaDropLabels)
                Controls.Remove(l);
            OznakaDropLabels.Clear();
            foreach (Label l in RazinaDropLabels)
                Controls.Remove(l);
            RazinaDropLabels.Clear();
            foreach (Label l in SkolskaGodinaDropLabels)
                Controls.Remove(l);
            SkolskaGodinaDropLabels.Clear();
        }

        private void AddSkolskaGodina(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(224, 224, 224);
            Toolbar.BackColor = Color.FromArgb(224, 224, 224);
            DataEntryList.BackColor = Color.FromArgb(224, 224, 224);

            AddGodinaRazred addGodinaRazred;
            if (SkolskaGodinaDropDisplay.Text == "")
            {
                addGodinaRazred = new AddGodinaRazred();
                addGodinaRazred.StartPosition = FormStartPosition.CenterScreen;
                addGodinaRazred.ShowDialog();
            }
            else 
            {
                addGodinaRazred = new AddGodinaRazred(SkolskaGodinaDropDisplay.Text);
                addGodinaRazred.StartPosition = FormStartPosition.CenterScreen;
                addGodinaRazred.ShowDialog();
                var s = _skolskeGodineService.GetSingle(int.Parse(CurrentGodina.Remove(4)));
                if (s is null)
                {
                    CurrentGodina = null;
                    SkolskaGodinaDropDisplay.Text = "";
                    RazinaDropDisplay.Text = "";
                    OznakaDropDisplay.Text = "";
                    OznakaDropContent = new List<string>();
                    RazinaDropContent = new List<string>();
                }
            }

            BackColor = Color.FromArgb(255, 255, 255);
            Toolbar.BackColor = Color.FromArgb(255, 255, 255);
            DataEntryList.BackColor = Color.FromArgb(255, 255, 255);

            string path = Path.GetDirectoryName(Application.CommonAppDataPath);

            RazinaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredDropNew.png");
            RazinaDropped = false;
            OznakaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredDropNew.png");
            OznakaDropped = false;
            SkolskaGodinaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\GodinaDropNew.png");
            GodinaDropped = false;

            foreach (Panel p in RazinaDropPanels)
                Controls.Remove(p);
            RazinaDropPanels.Clear();
            foreach (Panel p in OznakaDropPanels)
                Controls.Remove(p);
            OznakaDropPanels.Clear();
            foreach (Panel p in SkolskaGodinaDropPanels)
                Controls.Remove(p);
            SkolskaGodinaDropPanels.Clear();
            foreach (Label l in OznakaDropLabels)
                Controls.Remove(l);
            OznakaDropLabels.Clear();
            foreach (Label l in RazinaDropLabels)
                Controls.Remove(l);
            RazinaDropLabels.Clear();
            foreach (Label l in SkolskaGodinaDropLabels)
                Controls.Remove(l);
            SkolskaGodinaDropLabels.Clear();
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //MessageBox.Show(e.KeyCode + " " + e.KeyData + " " + e.KeyValue);
            //if(e.KeyCode.ToString() == "Tab")
            if(e.KeyCode.ToString() == "Escape")
            {
                string path = Path.GetDirectoryName(Application.CommonAppDataPath);

                RazinaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredDropNew.png");
                RazinaDropped = false;
                OznakaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\RazredDropNew.png");
                OznakaDropped = false;
                SkolskaGodinaDrop.BackgroundImage = Image.FromFile(path + "\\Assets\\GodinaDropNew.png");
                GodinaDropped = false;

                foreach (Panel p in RazinaDropPanels)
                    Controls.Remove(p);
                RazinaDropPanels.Clear();
                foreach (Panel p in OznakaDropPanels)
                    Controls.Remove(p);
                OznakaDropPanels.Clear();
                foreach (Panel p in SkolskaGodinaDropPanels)
                    Controls.Remove(p);
                SkolskaGodinaDropPanels.Clear();
                foreach (Label l in OznakaDropLabels)
                    Controls.Remove(l);
                OznakaDropLabels.Clear();
                foreach (Label l in RazinaDropLabels)
                    Controls.Remove(l);
                RazinaDropLabels.Clear();
                foreach (Label l in SkolskaGodinaDropLabels)
                    Controls.Remove(l);
                SkolskaGodinaDropLabels.Clear();

                IsMaximized = false;
                WindowState = FormWindowState.Normal;

                RazinaDropStart = new Point(Size.Width - 361, 73);
                OznakaDropStart = new Point(Size.Width - 246, 73);
                SkolskaGodinaDropStart = new Point(Size.Width - 121, 73);
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(224, 224, 224);
            Toolbar.BackColor = Color.FromArgb(224, 224, 224);
            DataEntryList.BackColor = Color.FromArgb(224, 224, 224);

            switch (MenuSelection.Location.Y)
            {
                case 99:
                    NastavnikForm n = new NastavnikForm();
                    n.StartPosition = FormStartPosition.CenterScreen;
                    n.ShowDialog();

                    UpdateNastavnici();

                    break;
                case 161:
                    if(RazinaDropDisplay.Text == "" || OznakaDropDisplay.Text == "" || SkolskaGodinaDropDisplay.Text == "")
                    {
                        MessageBox.Show("Da biste dodali predmet, prvo odaberite/dodajte godinu i razred");
                        break;
                    }

                    PredmetForm p = new PredmetForm(this);
                    p.StartPosition = FormStartPosition.CenterScreen;
                    p.ShowDialog();

                    UpdatePredmeti();
                    break;
                case 223:
                    if (RazinaDropDisplay.Text == "" || OznakaDropDisplay.Text == "" || SkolskaGodinaDropDisplay.Text == "")
                    {
                        MessageBox.Show("Da biste dodali zamjenu, prvo odaberite/dodajte godinu i razred");
                        break;
                    }
                    ZamjenaForm z = new ZamjenaForm(this);
                    z.StartPosition = FormStartPosition.CenterScreen;
                    z.ShowDialog();

                    UpdateZamjene();
                    break;
                default:
                    NotImplementedException ex = new NotImplementedException();
                    MessageBox.Show(ex.ToString());
                    break;
            }

            BackColor = Color.FromArgb(255, 255, 255);
            Toolbar.BackColor = Color.FromArgb(255, 255, 255);
            DataEntryList.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void NastavniciDataEntry_Click(object sender, EventArgs e)
        {
            Control c = sender as Control;
            NastavnikForm nastavnikForm = new NastavnikForm(DataNastavnici[Convert.ToInt32(c.Tag.ToString())], this);
            nastavnikForm.StartPosition = FormStartPosition.CenterScreen;

            BackColor = Color.FromArgb(224, 224, 224);
            Toolbar.BackColor = Color.FromArgb(224, 224, 224);
            DataEntryList.BackColor = Color.FromArgb(224, 224, 224);

            nastavnikForm.ShowDialog();

            BackColor = Color.FromArgb(255, 255, 255);
            Toolbar.BackColor = Color.FromArgb(255, 255, 255);
            DataEntryList.BackColor = Color.FromArgb(255, 255, 255);

            UpdateNastavnici();
        }

        private void PredmetiDataEntry_Click(object sender, EventArgs e)
        {
            Control c = sender as Control;
            PredmetForm predmetForm = new PredmetForm(DataPredmeti[int.Parse(c.Tag.ToString())], DataPredmeti[int.Parse(c.Tag.ToString())].Nastavnik.OIB, this);
            predmetForm.StartPosition = FormStartPosition.CenterScreen;

            BackColor = Color.FromArgb(224, 224, 224);
            Toolbar.BackColor = Color.FromArgb(224, 224, 224);
            DataEntryList.BackColor = Color.FromArgb(224, 224, 224);

            predmetForm.ShowDialog();

            BackColor = Color.FromArgb(255, 255, 255);
            Toolbar.BackColor = Color.FromArgb(255, 255, 255);
            DataEntryList.BackColor = Color.FromArgb(255, 255, 255);

            UpdatePredmeti();
        }

        private void ZamjeneDataEntry_Click(object sender, EventArgs e)
        {
            Control c = sender as Control;
            ZamjenaForm zamjenaForm = new ZamjenaForm(DataZamjene[int.Parse(c.Tag.ToString())], this);
            zamjenaForm.StartPosition = FormStartPosition.CenterScreen;

            BackColor = Color.FromArgb(224, 224, 224);
            Toolbar.BackColor = Color.FromArgb(224, 224, 224);
            DataEntryList.BackColor = Color.FromArgb(224, 224, 224);

            zamjenaForm.ShowDialog();

            BackColor = Color.FromArgb(255, 255, 255);
            Toolbar.BackColor = Color.FromArgb(255, 255, 255);
            DataEntryList.BackColor = Color.FromArgb(255, 255, 255);

            UpdateZamjene();
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            switch (MenuSelection.Location.Y)
            {
                case 99:

                    UpdateNastavnici();

                    break;
                case 161:
                    UpdatePredmeti();

                    break;
                case 223:
                    UpdateZamjene();

                    break;
                default:
                    break;
            }
        }
    }
}
