using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using i4Ds.LanguageToolkit;
using net.zemberek.erisim;
using net.zemberek.tr.yapi;
using net.zemberek.yapi;
using TurkishCollocation.Classes;
using TurkishCollocation.Model;

namespace TurkishCollocation
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        Zemberek zemberek= new Zemberek(new TurkiyeTurkcesi());

        private void button1_Click(object sender, EventArgs e)
        {
           
          

           
            
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();

            string text = richTextBox1.Text;
            text = Utilities.ClearCharacter(text);// karakterleri temizler

            string[] array = text.Split(' ');//bütün yazıyı boşluklara göre ayırıp bir diziye dolduruyor

            string yazi = "";
            int cc = 0;
            for (int i = 0; i < array.Length-1; i++)
            {
                cc = 0;
                yazi = "";
                char c = array[i][0];
                while ((c >= 'A' && c <= 'Z' && i<array.Length-1) || array[i] == "ve")
                {

                    if (!String.IsNullOrEmpty(array[i].Trim()))
                    {
                        if (array[i] == "ve")
                        {
                            yazi += "ve ";
                            cc++;
                           
                        }
                        else
                        {
                             yazi += array[i] + " ";
                             cc++;
                        }
                       

                    }
                        

                    i++;

                    string aaa = zemberek.asciiyeDonustur(array[i]);
                        c = aaa[0];
                   
                    
                }

                if (!String.IsNullOrEmpty(yazi) && cc>1)
                    listBox4.Items.Add(yazi);
                
            }



            text = text.ToLower();
            text = text.Trim();


            array = text.Split(' ');//bütün yazıyı boşluklara göre ayırıp bir diziye dolduruyor

            //string[] array2 = new String[array.Length];
            //for (int i = 0; i < array.Length; i++)
            //{
            //    if (array[i].Trim() != "")
            //        array2[i] = array[i];
            //}


            for (int i = 0; i < array.Length - 3; i++)
            {

                string val1 = array[i].ToString().Trim();
                string val2 = array[i + 1].ToString().Trim();



                string value = val1 + " " + val2;

                if (Utilities.IsNonLexi(value.Trim().Split(' ')))
                {
                    listBox1.Items.Add(value);
                }
                else if(Utilities.IsSemiLexi(zemberek,value.Trim().Split(' ')))
                {
                    listBox3.Items.Add(value);
                }
                else
                {

                    double benzerHeceSayisi = 0;
                    string [] val1Heceler = zemberek.heceleyici().hecele(val1);
                    string[] val2Heceler = zemberek.heceleyici().hecele(val2);
                    double kelimeSayisi = val2Heceler.Length>=val1Heceler.Length? val2Heceler.Length: val1Heceler.Length;


                    if (val1Heceler.Length!=0 || val2Heceler.Length != 0)
                    {
                        if (Utilities.IsTurkish(zemberek, value.Split(' ')))
                        {

                            foreach (string item1 in val1Heceler)
                            {
                                foreach (string item2 in val2Heceler)
                                {
                                    if (item2 == item1)
                                        benzerHeceSayisi++;

                                }
                            }

                            double z = Convert.ToInt64(benzerHeceSayisi * 100 / kelimeSayisi);
                            if (z >= 50.0)
                            {
                                listBox2.Items.Add(value);
                            }
                        }
                    }
                }

            }

            label1.Text = listBox1.Items.Count.ToString();
            label2.Text = listBox2.Items.Count.ToString();
            label3.Text = listBox3.Items.Count.ToString();


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            //CollocationDBEntities db = new CollocationDBEntities();

            //foreach (var item in db.Collocation)
            //{
            //    richTextBox1.Text += item.CollocationName + " ";


            //}     

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
