using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serialexpample {
    public partial class PropertyPage : Form {
        //variables for storing values of baud rate and stop bits
        private string baudR = "";
        private string stopB = "";

        //property for setting and getting baud rate and stop bits
        public string bRate {
            get {
                return baudR;
            }
            set {
                baudR = value;
            }
        }

        public string sBits {
            get {
                return stopB;
            }
            set {
                stopB = value;
            }
        }

        public PropertyPage( ) {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            this.bRate = "";
            this.sBits = "";
            //close form
            this.Close();
        }

        private void okButton_Click_1(object sender, EventArgs e) {
            //here we set the value for stop bits and baud rate.
            this.bRate = BaudRateComboBox.Text;
            this.sBits = stopBitComboBox.Text;
            //
            this.Close();

        }
    }
}
