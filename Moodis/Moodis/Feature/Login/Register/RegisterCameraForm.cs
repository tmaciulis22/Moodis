using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moodis.Feature.Login.Register
{
    public partial class RegistrationCameraForm : Form
    {
        private RegisterViewModel registerViewModel;

        public RegistrationCameraForm(RegisterViewModel viewModel)
        {
            registerViewModel = viewModel;
            InitializeComponent();
        }
    }
}
