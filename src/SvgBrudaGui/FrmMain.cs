using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Windows.Forms;
using SvgNet.SvgGdi;

namespace SvgBrudaGui
{
    partial class FrmMain : Form
    {
        const int MAX_WIDTH = 1024;
        const int MAX_HEIGHT = 768;

        static readonly SmoothingMode[] _antitaliases = { SmoothingMode.None, SmoothingMode.AntiAlias };
        static readonly int[] _antialiasCycle = CycleArray(_antitaliases);
        static readonly string[] _imageExtensions = "jpg,jpeg,bmp,gif,png,tif".Split(new[] { ',' });
        static readonly FileInfo _lastJson = new FileInfo(Path.Combine(UserData, "last.json"));

        readonly Settings _settings;

        int _antialias;
        string _lastSvgFile;
        FrmSettings _settingsForm;
        Bitmap _target;

        static string UserData
        {
            get
            {
                var asm = Assembly.GetEntryAssembly();
                var attr = asm.GetCustomAttribute<GuidAttribute>();
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BrudaSvg", attr.Value);
            }
        }

        Bitmap Target
        {
            get => _target;
            set
            {
                _target?.Dispose();
                _target = value;
            }
        }

        FrmSettings SettingsForm => _settingsForm = _settingsForm ?? new FrmSettings(_settings);

        public FrmMain()
        {
            InitializeComponent();
            Icon = Properties.Resources.Dna;

            if (!_lastJson.Directory.Exists)
            {
                _lastJson.Directory.Create();
            }
            stsLabel.Text = UserData;
            if (_lastJson.Exists)
            {
                try
                {
                    _settings = Settings.ReadFromJson(_lastJson);
                }
                catch (SerializationException)
                {
                    _settings = new Settings();
                }
            }
            else
            {
                _settings = new Settings();
            }
            if (!string.IsNullOrWhiteSpace(_settings.TargetImagePath) && File.Exists(_settings.TargetImagePath))
            {
                LoadImageAndResize();
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _settings.Save2Json(_lastJson);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            string file;
            switch (e.KeyCode)
            {
                case Keys.A:
                    _antialias = _antialiasCycle[_antialias];
                    Invalidate();
                    break;
                case Keys.L:
                    file = GetImageFileName();
                    if (!string.IsNullOrEmpty(file))
                    {
                        _settings.TargetImagePath = file;
                        LoadImageAndResize();
                    }
                    break;
                case Keys.Q when e.Alt:
                    Application.Exit();
                    break;
                case Keys.S:
                    if (!SettingsForm.Visible)
                    {
                        SettingsForm.Show(this);
                    }
                    else
                    {
                        SettingsForm.Hide();
                    }
                    break;
                case Keys.W:
                    file = GetSaveFileName();
                    if (!string.IsNullOrEmpty(file))
                    {
                        var svg = new SvgGraphics(Color.Yellow);
                        PaintIt(svg, ClientRectangle);
                        File.WriteAllText(file, svg.WriteSVGString());
                        _lastSvgFile = file;
                    }
                    break;
                default:
                    base.OnKeyUp(e);
                    break;
            }
        }

        static int[] CycleArray<T>(IList<T> forWhat)
        {
            var re = new int[forWhat.Count];
            for (int i = 1; i < forWhat.Count; ++i)
            {
                re[i - 1] = i;
            }
            return re;
        }

        void LoadImageAndResize()
        {
            try
            {
                Target = new Bitmap(_settings.TargetImagePath);

                var size = Target.Size;
                while (size.Width >= MAX_WIDTH || size.Height >= MAX_HEIGHT)
                {
                    size = new Size(size.Width / 2, size.Height / 2);
                }
                Size = size;
            }
            catch
            {
                MessageBox.Show($"Couldn't load image '{_settings.TargetImagePath}'.", "Image Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        string GetImageFileName()
        {
            using (var lfd = new OpenFileDialog()
            {
                CheckFileExists = true,
                Filter = $"Image files|{string.Join(";", _imageExtensions.Select(e => "*." + e))}|All files|*.*",
                ReadOnlyChecked = true
            })
            {
                if (lfd.ShowDialog() == DialogResult.OK)
                {
                    return lfd.FileName;
                }
            }
            return null;
        }

        string GetSaveFileName()
        {
            if (string.IsNullOrEmpty(_lastSvgFile))
            {
                return GetSaveFileNameByDialog("svg");
            }
            if (File.Exists(_lastSvgFile))
            {
                DialogResult answer = MessageBox.Show("File exists, overwite?", "File Exists Warning", MessageBoxButtons.YesNoCancel);
                switch (answer)
                {
                    case DialogResult.Yes:
                        return _lastSvgFile;
                    case DialogResult.No:
                        return GetSaveFileNameByDialog("svg");
                }
            }
            else
            {
                return _lastSvgFile;
            }
            return null;
        }

        string GetSaveFileNameByDialog(string extension)
        {
            using (var sfd = new SaveFileDialog
            {
                AddExtension = true,
                CheckPathExists = true,
                DefaultExt = extension,
                Filter = $"{extension.ToUpper()} files|*.{extension}|All files|*.*",
                OverwritePrompt = true
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    return sfd.FileName;
                }
            }
            return null;
        }

        void PaintIt(IGraphics g, Rectangle rect)
        {
            g.SmoothingMode = _antitaliases[_antialias];
            g.Clear(Color.Black);

            if (g is GdiGraphics && Target != null)
            {
                // Only draw the background picture for GDI, not in the SVG.
                g.DrawImage(Target, 0, 0, rect.Width, rect.Height);
            }
        }

        void stsLabel_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(UserData))
            {
                using (var _ = Process.Start(UserData))
                {
                }
            }
        }

        void picDraw_Paint(object sender, PaintEventArgs e)
        {
            PaintIt(new GdiGraphics(e.Graphics), e.ClipRectangle);
        }

        void picDraw_Resize(object sender, EventArgs e)
        {
            picDraw.Invalidate();
        }
    }
}
