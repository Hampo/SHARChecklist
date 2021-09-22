using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHARChecklist
{
    public partial class FrmSetLevelCount : Form
    {
        public FrmSetLevelCount()
        {
            InitializeComponent();
        }

        private void FrmSetLevelCount_Load(object sender, EventArgs e)
        {
            NUDLevel.Value = FrmMain.S.LevelCount;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            FrmMain.S.LevelCount = (uint)NUDLevel.Value;
        }
    }
}
