using Microsoft.VisualBasic;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            double Ind = 0;
            Interaction.Choose(Ind, "Speedy", "United", "Federal");

            Ind = Ind + 0;
        }
    }
}