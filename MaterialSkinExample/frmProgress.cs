using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;

using MaterialSkin.Controls;



namespace MaterialSkinExample
{
    public partial class frmProgress : Form
    {
        

        public static frmProgress sForm = null;
        public static frmProgress Instance()
        {
            if (sForm == null) { sForm = new frmProgress(); }

            return sForm;
        }
        //private string filePath = @"gif1.gif";

        public frmProgress()
        {
            InitializeComponent();

           //// string filePath = @"gif1.gif";
           // pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
           // pictureBox1.Image = Image.FromFile(filePath);


            //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            //Bitmap myBitmap = new Bitmap(filePath);
            //myBitmap.MakeTransparent();
            //this.pictureBox1.Image = myBitmap;

            //  pictureBox1.Image = Image.FromFile(filePath);

            /*
             *   pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            string filePath = @"logo.JPg";
            //  pictureBox2.Image = Image.FromFile(filePath);
            
            Bitmap myBitmap = new Bitmap(filePath);
            myBitmap.MakeTransparent();
            this.pictureBox2.Image = myBitmap;
             * */

        }

        /*
         * 
         *  public partial class C_Deposito : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;

        public C_Deposito()
        {

            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey900, Primary.BlueGrey100, Primary.gris500, Accent.LightBlue200, TextShade.WHITE);

         * */

        private void frmProgress_Load(object sender, EventArgs e)
        {
            this.Refresh();


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //string gif = Application.StartupPath + @"\loading.gif";


            //pictureBox1.Image = Image.FromFile(gif);
        }
    }
}
