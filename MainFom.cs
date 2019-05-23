using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using net.zemberek.araclar.turkce;
using net.zemberek.bilgi.araclar;
using net.zemberek.bilgi.kokler;
using net.zemberek.erisim;
using net.zemberek.islemler;
using net.zemberek.islemler.cozumleme;
using net.zemberek.tr.islemler;
using net.zemberek.tr.yapi;
using net.zemberek.tr.yapi.ek;
using net.zemberek.tr.yapi.kok;
using net.zemberek.yapi;
using net.zemberek.yapi.ek;
using TurkishCollocation.Classes;
using TurkishCollocation.Model;
using i4Ds.LanguageToolkit;
using net.zemberek.bilgi;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TurkishCollocation
{
    public partial class MainFom : Form
    {
        public char[] AYIRICI_PATTERN { get; private set; }

        public MainFom()
        {
            InitializeComponent();
        }
        CollocationDBEntities db = new CollocationDBEntities();

        private void button1_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document doc = new Microsoft.Office.Interop.Word.Document();
            object fileName = @"C:\Users\334262\Desktop\test2.docx";

            // Define an object to pass to the API for missing parameters
            object missing = System.Type.Missing;
            doc = word.Documents.Open(ref fileName,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing);
            string ReadValue = string.Empty;
            // Activate the document
            doc.Activate();

            foreach (Microsoft.Office.Interop.Word.Range tmpRange in doc.StoryRanges)
            {
                ReadValue += tmpRange.Text;
            }

            richTextBox1.Text = ReadValue;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //richTextBox1.Text = "abidik gubidik el ele ıvır zıvır yiyip yutmak ara ara";
            ////var list = db.Collocation.ToList();
            //listBoxOurList.Items.Clear();
            //foreach (Collocation item in list)
            //{
            //    listBoxOurList.Items.Add(item.CollocationName.Trim());
            //}

            //label2.Text = listBoxOurList.Items.Count.ToString();

        }


        private void button2_Click(object sender, EventArgs e)
        {


            listBoxInArticle.Items.Clear();
            listBoxLExi.Items.Clear();
            listBoxNonLexi.Items.Clear();
            listBoxSemiLexi.Items.Clear();

            try
            {

                string text = richTextBox1.Text;
                text = Utilities.ClearCharacter(text);// karakterleri temizler

                text = text.Trim();

                string[] array = text.Split(' ');//bütün yazıyı boşluklara göre ayırıp bir diziye dolduruyor
                string[] array2 = new String[array.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].Trim() != "")
                        array2[i] = array[i];
                }

                Collocation c = new Collocation();
                for (int i = 0; i < array.Length - 3; i++)
                {

                    string value1 = array[i].ToString().Trim() + " " + array[i + 1].ToString().Trim();
                    string value2 = array[i].ToString().Trim() + " " + array[i + 1].ToString().Trim() + " " + array[i + 2].ToString().Trim();
                    string value3 = array[i].ToString().Trim() + " " + array[i + 1].ToString().Trim() + " " + array[i + 2].ToString().Trim() + " " + array[i + 3].ToString().Trim();

                    if (!String.IsNullOrEmpty(value1))
                    {
                        //foreach (var val in listBoxOurList.Items)
                        //{
                        //    if (val.ToString().Trim().ToLower() == value1.Trim().ToLower())
                        //    {
                        //        //listBoxInArticle.Items.Add(value1);

                        //        ////if (value1.Trim() != "")
                        //        ////    c = db.Collocation.Where(x => x.CollocationName == value1).FirstOrDefault();//veri tabanından bulunan ikilemeyi çeker ve grubunu tespit için gerekli

                        //        //if (c.Type == 1)//Lexicalized
                        //        //    listBoxLExi.Items.Add(value1);
                        //        //if (c.Type == 2)//Semi-lexicalized
                        //        //    listBoxSEmiLexi.Items.Add(value1);
                        //        //if (c.Type == 3)//Non-lexicalized
                        //        //    listBoxNonLexi.Items.Add(value1);
                        //    }
                        //    else if (val.ToString().Trim().ToLower() == value2.Trim().ToLower())
                        //    {
                        //        listBoxInArticle.Items.Add(value2);
                        //        if (value2.Trim() != "")
                        //            c = db.Collocation.Where(x => x.CollocationName == value2).FirstOrDefault();

                        //        if (c.Type == 1)
                        //            listBoxLExi.Items.Add(value2);
                        //        if (c.Type == 2)
                        //            listBoxSEmiLexi.Items.Add(value2);
                        //        if (c.Type == 3)
                        //            listBoxNonLexi.Items.Add(value2);
                        //    }
                        //    else if (val.ToString().Trim().ToLower() == value3.Trim().ToLower())
                        //    {
                        //        listBoxInArticle.Items.Add(value3);
                        //        if (value3.Trim() != "")
                        //            c = db.Collocation.Where(x => x.CollocationName == value3).FirstOrDefault();

                        //        if (c.Type == 1)
                        //            listBoxSEmiLexi.Items.Add(value2);
                        //        if (c.Type == 2)
                        //            listBoxSEmiLexi.Items.Add(value2);
                        //        if (c.Type == 3)
                        //            listBoxSEmiLexi.Items.Add(value2);
                        //    }
                        //}

                    }
                }

                string last2Value = array[array.Length - 2] + " " + array[array.Length - 1];
                string last3value = array[array.Length - 3] + " " + array[array.Length - 2] + " " + array[array.Length - 1];

                foreach (var val in listBoxOurList.Items)
                {
                    if (val.ToString().Trim().ToLower() == last2Value.Trim().ToLower())
                    {
                        if (last2Value.Trim() != "")
                            c = db.Collocation.Where(x => x.CollocationName == last2Value).FirstOrDefault();
                        listBoxInArticle.Items.Add(last2Value);

                        if (c.Type == 1)
                            listBoxLExi.Items.Add(last2Value);
                        if (c.Type == 2)
                            listBoxSemiLexi.Items.Add(last2Value);
                        if (c.Type == 3)
                            listBoxNonLexi.Items.Add(last2Value);
                    }
                }

                foreach (var val in listBoxOurList.Items)
                {
                    if (val.ToString().Trim().ToLower() == last3value.Trim().ToLower())
                    {
                        if (last3value.Trim() != "")
                            c = db.Collocation.Where(x => x.CollocationName == last3value).FirstOrDefault();

                        listBoxInArticle.Items.Add(last2Value);
                        if (c.Type == 1)
                            listBoxLExi.Items.Add(last3value);
                        if (c.Type == 2)
                            listBoxSemiLexi.Items.Add(last3value);
                        if (c.Type == 3)
                            listBoxNonLexi.Items.Add(last3value);
                    }
                }

                listBoxNonLexi.Items.Add(listBoxNonLexi.Items.Count);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected problem! " + ex.Message);
            }

            label1.Text = listBoxInArticle.Items.Count.ToString();
            //buttonAnalysis.PerformClick();

        }




        public String FindRoot(string text)
        {
            String kok = "";

            Zemberek zemberek = new Zemberek(new TurkiyeTurkcesi());


            if (!String.IsNullOrEmpty(text))
            {
                if (zemberek.kelimeDenetle(text))
                {
                    kok = zemberek.kelimeCozumle(text)[0].kok().icerik();
                }
            }


            return kok;
        }

        private void buttonFillListbox2_Click(object sender, EventArgs e)
        {

        }

        private void buttonFillListbox3_Click(object sender, EventArgs e)
        {
            Zemberek zemberek = new Zemberek(new TurkiyeTurkcesi());
            int i = 0;
            foreach (var item in listBoxLExi.Items)
            {
                if (i < 100)
                {
                    string value = item.ToString().Trim();
                    if (!String.IsNullOrEmpty(value))
                    {
                        if (zemberek.kelimeDenetle(value))
                        {
                            listBoxSemiLexi.Items.Add(FindRoot(value));
                        }
                    }

                    i++;
                }
                else
                {
                    break;

                }

            }

            MessageBox.Show("Bitti");
        }
        Zemberek zemberek = new Zemberek(new TurkiyeTurkcesi());
        private void button3_Click(object sender, EventArgs e)
        {




            List<WordList> ResultList = new List<WordList>();


            List<YaziBirimi> analizDizisi;
            foreach (var item in listBoxInArticle.Items)
            {
                analizDizisi = YaziIsleyici.analizDizisiOlustur(item.ToString()).ToList();
                ResultList.Add(Utilities.FindRoot2(zemberek, analizDizisi));

            }
            foreach (var item in ResultList)
            {
                if (item.collocationAnalaysis.TypeList2.Count == 1)
                {
                    if (item.collocationAnalaysis.TypeList2[0] == "FIIL")
                        listBoxSemiLexiAnalysis.Items.Add(item.collocationFull);
                    else
                        listBoxLexiAnalaysis.Items.Add(item.collocationFull);
                }
                else
                    listBoxLexiAnalaysis.Items.Add(item.collocationFull);
            }

            foreach (var item in listBoxLexiAnalaysis.Items)
            {
                listBoxInArticle.Items.Remove(item);
            }
            listBoxInArticle.Refresh();

            foreach (var item in listBoxSemiLexiAnalysis.Items)
            {
                listBoxInArticle.Items.Remove(item);
            }
            listBoxInArticle.Refresh();


            labelINArticle.Text += listBoxInArticle.Items.Count;
            labelLexi.Text += listBoxLExi.Items.Count;
            labelLexiA.Text += listBoxLexiAnalaysis.Items.Count;
            labelNonLexi.Text += listBoxNonLexi.Items.Count;
            labelNonLexiA.Text += listBoxNonLexiAnalaysis.Items.Count;
            labelSemiLexi.Text += listBoxSemiLexi.Items.Count;
            labelSemiLexiA.Text += listBoxSemiLexiAnalysis.Items.Count;


        }

        private void buttonAnalysis_Click(object sender, EventArgs e)
        {

            listBoxInArticleFromArticle.Items.Clear();


            string text = richTextBox1.Text;
            text = Utilities.ClearCharacter(text);// karakterleri temizler

            string[] array = text.Split(' ');//bütün yazıyı boşluklara göre ayırıp bir diziye dolduruyor




            List<string> y = array.ToList<string>();
            y.RemoveAll(p => string.IsNullOrEmpty(p));

            string[] array2 = new String[y.Count];
            array2 = y.ToArray();


            //for (int i = 0; i < array.Length; i++)
            //{
            //    if (array[i].Length > 1)
            //    { 
            //            string aaaaa = array[i];
            //            array2[i] = aaaaa;

            //    }
            //}


            string yazi = "";
            int cc = 0;
            for (int i = 0; i < array2.Length - 1; i++)
            {
                cc = 0;
                yazi = "";
                char c = array2[i][0];
                while ((c >= 'A' && c <= 'Z' && i < array2.Length - 1) || array[i] == "ve" || array2[i] != null)
                {

                    if (!String.IsNullOrEmpty(array2[i].Trim()))
                    {
                        if (array2[i] == "ve")
                        {
                            yazi += "ve ";
                            cc++;

                        }
                        else
                        {
                            yazi += array2[i] + " ";
                            cc++;
                        }


                    }


                    i++;

                    if (i >= array2.Length)
                        break;
                    string aaa = zemberek.asciiyeDonustur(array2[i]);
                    c = aaa[0];


                }

                if (!String.IsNullOrEmpty(yazi) && cc > 1)
                    listBoxInArticleFromArticle.Items.Add(yazi);

            }



            //text = text.ToLower();
            text = text.Trim();


            array2 = text.Split(' ');//bütün yazıyı boşluklara göre ayırıp bir diziye dolduruyor



            for (int i = 0; i < array2.Length - 3; i++)
            {

                string val1 = array2[i].ToString().Trim();
                string val2 = array2[i + 1].ToString().Trim();



                string value = val1 + " " + val2;

                if (Utilities.IsNonLexi(value.Trim().Split(' ')))
                {
                    listBoxInArticleFromArticle.Items.Add(value);
                }
                else if (Utilities.IsSemiLexi(zemberek, value.Trim().Split(' ')))
                {
                    listBoxInArticleFromArticle.Items.Add(value);
                }
                else
                {
                    double benzerHeceSayisi = 0;
                    string[] val1Heceler = zemberek.heceleyici().hecele(val1);
                    string[] val2Heceler = zemberek.heceleyici().hecele(val2);
                    double kelimeSayisi = val2Heceler.Length >= val1Heceler.Length ? val2Heceler.Length : val1Heceler.Length;


                    if (val1Heceler.Length != 0 || val2Heceler.Length != 0)
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
                            listBoxInArticleFromArticle.Items.Add(value);
                        }
                    }
                }

            }

           



        }
        List<string> listNon = new List<string>();
        List<string> listLexi = new List<string>();
        List<string> listSemi = new List<string>();

        private void button4_Click(object sender, EventArgs e)
        {


            TestForm test = new TestForm();
            test.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            #region clear
            listBoxInArticleFromArticle.Items.Clear();
            listBoxLexiAnalaysis.Items.Clear();
            listBoxSemiLexiAnalysis.Items.Clear();
            listBoxLExi.Items.Clear();
            listBoxSemiLexi.Items.Clear();
            listBoxNonLexi.Items.Clear();

            labelLexi.Text = "Count:0";
            labelNonLexi.Text = "Count:0";
            labelSemiLexi.Text = "Count:0";

            listLexi.Clear();
            listNon.Clear();
            listSemi.Clear();
            #endregion




            List<string> liste = new List<string>();//dinamik stringtipinde liste. tespit edilen bütün ikilelemler burada tutuluyor.

            try
            {
                string text = richTextBox1.Text;
                text = Utilities.ClearCharacter(text); // karakterleri temizler

                string[] array = text.Split(' '); //bütün yazıyı boşluklara göre ayırıp bir diziye dolduruyor

                int start = 0;
                int finish = 5;
                int c = 0;

                if (array.Length <= 5)
                {
                   
                    MessageBox.Show("Please enter text has less 6 word!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    richTextBox1.Text = "";
                    return;
                }

                if (finish < array.Length)
                {
                    for (int i = 0; i < finish; i++)
                    {
                        if (!Utilities.IsTurkish1(zemberek, array[i].Trim()))
                        {
                            c++;
                        }

                    }

                }

                finish = array.Length - 1;
                start = array.Length - 6;

                if (start > 0)
                {
                    for (int i = start; i < finish; i++)
                    {
                        if (!Utilities.IsTurkish1(zemberek, array[i].Trim()))
                        {
                            c++;
                        }

                    }

                }

                if (c > 5)
                {
                    MessageBox.Show("Please enter turkish text!","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    richTextBox1.Text = "";
                    return;
                }

                string yazi = "";
                int cc = 0;

                #region nameentities
                //for (int i = 0; i < array.Length - 1; i++)
                //{
                //    cc = 0;
                //    yazi = "";
                //    char c = array[i][0];
                //    while ((c >= 'A' && c <= 'Z' && i < array.Length - 1) || array[i] == "ve")
                //    {

                //        if (!String.IsNullOrEmpty(array[i].Trim()))
                //        {
                //            if (array[i] == "ve")
                //            {
                //                yazi += "ve ";
                //                cc++;

                //            }
                //            else
                //            {
                //                yazi += array[i] + " ";
                //                cc++;
                //            }


                //        }


                //        i++;

                //        string aaa = zemberek.asciiyeDonustur(array[i]);
                //        c = aaa[0];


                //    }

                //    if (!String.IsNullOrEmpty(yazi) && cc > 1)
                //        listBoxInArticleFromArticle.Items.Add(yazi);

                //}
                #endregion

                text = text.ToLower();

                text = text.Trim();//başında ve sonundaki boşlukları siler.
                richTextBox1.Text = text;

                array = text.Split(' ');//bütün yazıyı boşluklara göre ayırıp bir diziye dolduruyor

                List<JsonClass> list = Utilities.ReadWordList();//wordlist teki kelimeleri list isimindeli dinamik listeye dolduruyor.

                for (int k = 0; k < array.Length - 1; k++)
                {

                    string val1 = array[k].ToString().Trim().ToLower();
                    string val2 = array[k + 1].ToString().Trim().ToLower();



                    string value = val1 + " " + val2;

                    if (Utilities.IsNonLexi(value.Trim().Split(' ')))//algoritma 1 birbirini içeriyor mu?
                    {
                        liste.Add(value);
                        listNon.Add(value);
                    }

                    else if (Utilities.IsSemiLexi(zemberek, value.Trim().Split(' ')))//algoritma 2  -ip eki ve mastar testi
                    {
                        liste.Add(value);
                        listSemi.Add(value);

                    }
                    else if (Utilities.IsNonLexi2(zemberek, value.Trim().Split(' '))) //algoritma 3 hece benzerlik oranı
                    {
                        liste.Add(value);
                        listLexi.Add(value);
                    }
                    else if (Utilities.IsSemiLexiLI(zemberek, value.Trim().Split(' ')))//ek kontrolü
                    {
                        liste.Add(value);
                        listLexi.Add(value);
                    }
                    else//wordlist analizi.
                    {
                        if (Utilities.IsTurkish(zemberek, value.Trim().Split(' ')))
                        {
                            var ddd = list.Where(x => x.s1 == val1.ToLower()).ToList();
                            if (ddd.Count > 0)
                            {
                                foreach (var item in ddd)
                                {
                                    string hhh = item.s2;
                                    if (hhh == val2)
                                    {
                                        liste.Add(value);
                                        listLexi.Add(value);
                                    }
                                }

                            }
                        }

                    }

                }

            }
            catch
            {

            }
            finally
            {

                int i = 0;
                foreach (var item in liste)
                {

                    string[] value = item.ToString().Trim().Split(' ');
                    //if (value[0].Length* 2 < value[1].Length || value[1].Length* 2 < value[0].Length)
                    //{
                    //}
                    //else
                    //{
                    listBoxInArticleFromArticle.Items.Add(item.ToLower());
                    //}


                }
                labelINArticle.Text = "Count:" + listBoxInArticleFromArticle.Items.Count.ToString();
                labelSemiLexiA.Text = "Accuracy Rate:100%";
                button8.PerformClick();


            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {

                List<JsonClass> table = new List<JsonClass>();

                var a = db.Collocation.Where(x => x.Type == 1).ToList();
                foreach (var item in a)
                {
                    JsonClass cc = new JsonClass();
                    cc.s1 = item.CollocationName.Trim().Split(' ')[0].Trim().ToLower();
                    cc.s2 = item.CollocationName.Trim().Split(' ')[1].Trim().ToLower();
                    table.Add(cc);
                }

                Utilities.DataTableToJSONWithStringBuilder(table);//json oluşturuuyor

                // Check if file already exists. If yes, delete it. 

            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }


        }

        private void button7_Click(object sender, EventArgs e)
        {
            //List<JsonClass> list = Utilities.ReadWordList();
            //string u = "alt";
            //if (list.Where(x => x.s1 == u).Count() > 0)
            //{
            //    var ddd = list.Where(x => x.s1 == u).GetEnumerator();
            //    ddd.Current.Equals()
            //    for (int i = 0; i < list.Count; i++)
            //    {

            //    }
            //}
        }

        public string calculateRate(double trueValues, double falseValues)
        {
            double result;
            result = (1 - falseValues / (trueValues + falseValues)) * 100.0;
            return "Accuracy Rate:" + String.Format("{0:00.0}", result) + "%";
        }


        public void RemoveItem(string item)
        {
            listSemi.Remove(item.Trim().ToLower());
            listNon.Remove(item);
            listLexi.Remove(item);
        }

        private void listBoxInArticleFromArticle_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var text = listBoxInArticleFromArticle.SelectedItem;
                listBoxInArticleFromArticle.Items.Remove(text);
                listBoxLexiAnalaysis.Items.Add(text);

                double trueValues = listBoxInArticleFromArticle.Items.Count;
                double falseValues = listBoxLexiAnalaysis.Items.Count;
                labelINArticle.Text = "Count:" + trueValues.ToString();
                labelLexiA.Text = "Count:" + falseValues.ToString();
                double result = 0.0;

                listBoxSemiLexiAnalysis.Items.Clear();

                result = (1 - falseValues / (trueValues + falseValues)) * 100.0;
                string rrr = "Rate:" + String.Format("{0:00.0}", result) + "%";
                labelSemiLexiA.Text = rrr;

                RemoveItem(text.ToString());
            }
            catch
            {

            }


        }

        private void listBoxLexiAnalaysis_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var text = listBoxLexiAnalaysis.SelectedItem;
                listBoxLexiAnalaysis.Items.Remove(text);
                listBoxInArticleFromArticle.Items.Add(text);
                listBoxSemiLexiAnalysis.Items.Clear();
                double trueValues = listBoxInArticleFromArticle.Items.Count;
                double falseValues = listBoxLexiAnalaysis.Items.Count;
                labelINArticle.Text = "Count:" + trueValues.ToString();
                labelLexiA.Text = "Count:" + falseValues.ToString();
                labelSemiLexiA.Text = calculateRate(trueValues, falseValues);
            }
            catch
            {

            }

        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {

            foreach (var item in listBoxLexiAnalaysis.Items)
            {
                listBoxInArticleFromArticle.Items.Add(item);
            }
            listBoxLexiAnalaysis.Items.Clear();
            labelINArticle.Text = "Count:" + listBoxInArticleFromArticle.Items.Count;

            //labelINArticle.Text = "Count:" + trueValues.ToString();
            labelLexiA.Text = "Count:0";

            labelSemiLexiA.Text = calculateRate(listBoxInArticleFromArticle.Items.Count, 0);
        }

        private void buttonGrouping_Click(object sender, EventArgs e)
        {
            foreach (var item in listLexi)
            {
                listBoxLExi.Items.Add(item);
            }
            foreach (var item in listSemi)
            {
                listBoxSemiLexi.Items.Add(item);
            }
            foreach (var item in listNon)
            {
                listBoxNonLexi.Items.Add(item);
            }

            labelSemiLexi.Text = "Count:" + listBoxSemiLexi.Items.Count.ToString();
            labelLexi.Text = "Count:" + listBoxLExi.Items.Count.ToString();
            labelNonLexi.Text = "Count:" + listBoxNonLexi.Items.Count.ToString();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            foreach (var item in listBoxInArticleFromArticle.Items)
            {
                string input = item.ToString();
                int length = input.Length;
                int index = richTextBox1.Text.IndexOf(input);
                richTextBox1.Select(index, length);
                richTextBox1.SelectionBackColor = Color.Orange;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }
    }


}


