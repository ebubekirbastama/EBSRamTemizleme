using BekraRamclear;
using System;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace EBSRamTemizleme
{
    public partial class Form1 : Form
    {
        private System.Timers.Timer timer;
        private bool dragging = false;
        private int dragStartX = 0;
        private int dragStartY = 0;

        public Form1()
        {
            InitializeComponent();

            // Arka plan rengini ayarla
            this.BackColor = System.Drawing.Color.FromArgb(255, 28, 35, 42); // A: 255, R: 28, G: 35, B: 42

            // Form Ayarları
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
    
            //this.TopMost = true;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new System.Drawing.Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, 0);

            // Klavye olaylarını yakalamak için
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) // ESC tuşuna basıldığında
            {
                this.Close(); // Formu kapat
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Zamanlayıcı ayarları
            this.timer = new System.Timers.Timer();
            this.timer.Interval = 5 * 1000; // 5 saniye
            this.timer.Elapsed += new ElapsedEventHandler(this.TimerElapsed);
            this.timer.Start();

            // Drag & Drop olayları
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);
            this.Paint += ModernForm_Paint; // Formu özel olarak çizeceğiz.
            label1.Text = GetRamBilgisiGB();
        }
        private void ModernForm_Paint(object sender, PaintEventArgs e)
        {
            // Formun yuvarlak köşeli tasarımı
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Yuvarlak köşe oluştur
            GraphicsPath path = new GraphicsPath();
            int borderRadius = 30;
            path.AddArc(0, 0, borderRadius, borderRadius, 180, 90);
            path.AddArc(this.Width - borderRadius, 0, borderRadius, borderRadius, 270, 90);
            path.AddArc(this.Width - borderRadius, this.Height - borderRadius, borderRadius, borderRadius, 0, 90);
            path.AddArc(0, this.Height - borderRadius, borderRadius, borderRadius, 90, 90);
            path.CloseAllFigures();

            // Gradient arkaplan
            LinearGradientBrush gradientBrush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(45, 45, 48), // Gradient başlangıç rengi
                Color.FromArgb(28, 28, 30), // Gradient bitiş rengi
                LinearGradientMode.ForwardDiagonal);

            g.FillPath(gradientBrush, path);

            // Kenar efekti (parlayan hat)
            Pen borderPen = new Pen(Color.FromArgb(128, 255, 255, 255), 2);
            g.DrawPath(borderPen, path);

            // Form bölgesini ayarla
            this.Region = new Region(path);

            // Gölgeler
            AddShadow(g);
        }
        public static string GetRamBilgisiGB()
        {
            var toplamBellekSayacı = new PerformanceCounter("Memory", "Available MBytes");
            var kullanılabilirRamGB = toplamBellekSayacı.NextValue() / 1024.0; // GB'ye çevir
            var toplamRamGB = GetirToplamFizikselBellekBayt() / (1024.0 * 1024.0 * 1024.0); // GB'ye çevir
            var kullanılanRamGB = toplamRamGB - kullanılabilirRamGB;

            return $"Kullanılan RAM: {kullanılanRamGB:F2} GB\n";
        }

        private static long GetirToplamFizikselBellekBayt()
        {
            using (var arama = new System.Management.ManagementObjectSearcher("SELECT TotalVisibleMemorySize FROM Win32_OperatingSystem"))
            {
                foreach (var obje in arama.Get())
                {
                    return Convert.ToInt64(obje["TotalVisibleMemorySize"]) * 1024; // Bayt cinsinden
                }
            }

            return 0; // Varsayılan değer
        }
        private void AddShadow(Graphics g)
        {
            int shadowSize = 15;
            Rectangle shadowRect = new Rectangle(
                shadowSize / 2,
                shadowSize / 2,
                this.Width - shadowSize,
                this.Height - shadowSize);

            using (GraphicsPath shadowPath = new GraphicsPath())
            {
                shadowPath.AddRectangle(shadowRect);

                using (PathGradientBrush shadowBrush = new PathGradientBrush(shadowPath))
                {
                    shadowBrush.CenterColor = Color.FromArgb(120, 0, 0, 0); // Ortada daha yoğun gölge
                    shadowBrush.SurroundColors = new Color[] { Color.Transparent }; // Kenarlarda şeffaflık

                    g.FillRectangle(shadowBrush, shadowRect);
                }
            }
        }
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                label1.Text = GetRamBilgisiGB();
                BekraRamClearr.temizle();
            });
        }

        // Form sürükleme için olaylar
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                dragStartX = e.X;
                dragStartY = e.Y;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                this.Left += e.X - dragStartX;
                this.Top += e.Y - dragStartY;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = false;
            }
        }
    }
}
