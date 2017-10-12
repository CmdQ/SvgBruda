using System.Windows.Forms;
using SvgBrudaGui.Properties;

namespace SvgBrudaGui
{
    partial class FrmSettings : Form
    {
        readonly Settings _settings;

        public FrmSettings(Settings settings)
        {
            _settings = settings;
            InitializeComponent();
            prpGrid.SelectedObject = _settings;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Hide();
                    break;
                default:
                    base.OnKeyDown(e);
                    break;
            }
        }
    }
}
