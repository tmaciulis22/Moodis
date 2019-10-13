using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moodis.Feature.Group
{
    public partial class GroupForm : Form
    {

        private GroupViewModel groupViewModel;

        public GroupForm()
        {
            InitializeComponent();
            groupViewModel = new GroupViewModel();
        }
    }
}
