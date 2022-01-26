using System;
using System.Windows.Forms;

namespace Probabilite_Variables_Aleatoires
{
    public partial class Main : Form
    {
        float[] table = new float[9];

        public Main()
        {
            InitializeComponent();
        }

        private void Calculer_BTN_Click(object sender, EventArgs e)
        {
            int len = FillTable();

            if (len != -1)
            {
                FonctionDeRepartition(len);
                Esperance_TB.Text = Esperance(1, 0, len).ToString("0.00000");
                Variance_TB.Text = Variance(1, 0, len).ToString("0.00000");
                Ecart_TB.Text = Ecart(1, 0, len).ToString("0.00000");

                //check if the Variable aleatoire Y has values
                try
                {
                    var a = float.Parse(ValueA_TB.Text);
                    var b = float.Parse(ValueB_TB.Text);

                    //Variable Aleatoire Y output
                    EsperanceY_TB.Text = Esperance(a, b, len).ToString("0.00000");
                    VarianceY_TB.Text = Variance(a, b, len).ToString("0.00000");
                    EcartY_TB.Text = Ecart(a, b, len).ToString("0.00000");
                }
                catch
                {
                    MessageBox.Show("values 'a' or 'b' are not numbers");
                    //Variable Aleatoire Y output
                    EsperanceY_TB.Text = VarianceY_TB.Text = EcartY_TB.Text = "−";
                }
            }
        }
        private int FillTable()
        {
            int count = 0;
            try
            {
                //from text box to array
                table[count++] = float.Parse(Case1_TB.Text);
                table[count++] = float.Parse(Case2_TB.Text);
                table[count++] = float.Parse(Case3_TB.Text);
                table[count++] = float.Parse(Case4_TB.Text);
                table[count++] = float.Parse(Case5_TB.Text);
                table[count++] = float.Parse(Case6_TB.Text);
                table[count++] = float.Parse(Case7_TB.Text);
                table[count++] = float.Parse(Case8_TB.Text);
                table[count] = float.Parse(Case9_TB.Text);

                return count;
            }
            catch (Exception ex)
            {
                bool check = CheckValues(count);

                if (count > 2 && check)
                    return count;
                else if(!check)
                {
                    MessageBox.Show("One or multiple Values entered are wrong");
                    return -1;
                }

                if(count > 1)
                    MessageBox.Show("At least two values should be added");
                else
                    MessageBox.Show(ex.Message);

                return -1;
            }
        }
        private bool CheckValues(int len)
        {
            float sum = 0;
            for (int i = 0; i < len; i++)
            {
                sum += table[i];
            }

            if (sum == 1)
                return true;

            return false;
        }
    
        //Calcul
        private float Esperance(float a, float b, int len)
        {
            float sum = 0;
            for (int i = 1; i <= len; i++)
            {
                sum += (i * table[i-1]);
            }
            return a*sum+b;
        }
        private float Variance(float a, float b, int len)
        {
            //E(X^2)
            float sum = 0;
            for (int i = 1; i <= len; i++)
            {
                sum += (float)Math.Pow(i, 2) * table[i-1];
            }

            var varX = sum - (float)Math.Pow(Esperance(1, 0, len), 2);

            return (float)Math.Pow(a, 2) * varX ;
        }
        private float Ecart(float a, float b, int len)
        {
            return (float)Math.Sqrt(Variance(a, b, len));
        }
        private void FonctionDeRepartition(int len)
        {
            try
            {
                CaseFx1_TB.Text = Case1_TB.Text;
                CaseFx2_TB.Text = (float.Parse(CaseFx1_TB.Text) + float.Parse(Case2_TB.Text)).ToString();
                if (len < 4) return;
                CaseFx3_TB.Text = (float.Parse(CaseFx2_TB.Text) + float.Parse(Case3_TB.Text)).ToString();
                if (len < 5) return;
                CaseFx4_TB.Text = (float.Parse(CaseFx3_TB.Text) + float.Parse(Case4_TB.Text)).ToString();
                if (len < 6) return;
                CaseFx5_TB.Text = (float.Parse(CaseFx4_TB.Text) + float.Parse(Case5_TB.Text)).ToString();
                if (len < 7) return;
                CaseFx6_TB.Text = (float.Parse(CaseFx5_TB.Text) + float.Parse(Case6_TB.Text)).ToString();
                if (len < 8) return;
                CaseFx7_TB.Text = (float.Parse(CaseFx6_TB.Text) + float.Parse(Case7_TB.Text)).ToString();
                if (len < 9) return;
                CaseFx8_TB.Text = (float.Parse(CaseFx7_TB.Text) + float.Parse(Case8_TB.Text)).ToString();
                if (len < 10) return;
                CaseFx9_TB.Text = (float.Parse(CaseFx8_TB.Text) + float.Parse(Case9_TB.Text)).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}