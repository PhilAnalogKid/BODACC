using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace BODAC2019
{
    public partial class update : Form
    {
        public update()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
                            
        List<string> denomination = new List<string>();
        List<string> address = new List<string>();
        List<string> formeJuridique = new List<string>();
        List<string> administration = new List<string>();
        List<string> dateImmatriculation = new List<string>();
        List<string> activite = new List<string>();
        List<string> montantCapital = new List<string>();
        List<string> numeroIdentification = new List<string>();
        List<string> nomCommercial = new List<string>();
        List<string> enseigne = new List<string>();

        int d = 0;
        private void read() { 
            string data = textBox1.Text;
            
            Regex reg = new Regex("<avis>", RegexOptions.None);
            int occ = reg.Matches(data).Count;

            int index = 0;

            int f = 0;
            

            for (int i = 0; i < occ; i++)
            {
                int st1 = data.IndexOf("<avis>", index) + 6;
                int en1 = data.IndexOf("</avis>", st1);
                string avis = data.Substring(st1, en1 - st1);


                int st = avis.IndexOf("<denomination>") + 14;
                int en = avis.IndexOf("</denomination>", st);

                if (en > st)
                {
                    denomination.Add(avis.Substring(st, en - st).Trim());
                  //  Console.WriteLine(avis.Substring(st, en - st));
                }
                else
                {//personne
                    int st2 = avis.IndexOf("<nom>") + 5;
                    int en2 = avis.IndexOf("</nom>", st2);

                    int st3 = avis.IndexOf("<prenom>") + 8;
                    int en3 = avis.IndexOf("</prenom>", st3);

                    string nom = avis.Substring(st2, en2 - st2).Trim();
                    string prenom = avis.Substring(st3, en3 - st3).Trim();

                    denomination.Add(nom + " " + prenom);
                  //  Console.WriteLine(nom + " " + prenom);


                }

                st = avis.IndexOf("<nomCommercial>") + 15;
                en = avis.IndexOf("</nomCommercial>", st);

                if (en > st)
                {
                    nomCommercial.Add(avis.Substring(st, en - st).Trim());
                }
                else
                {
                    nomCommercial.Add("");

                }

                string address1 = "";

                st = avis.IndexOf("<numeroVoie>") + 12;
                en = avis.IndexOf("</numeroVoie>", st);

                if (en > st)
                {
                    address1 += avis.Substring(st, en - st).Trim();
                }

                st = avis.IndexOf("<typeVoie>") + 10;
                en = avis.IndexOf("</typeVoie>", st);

                if (en > st)
                {
                    address1 += " " + avis.Substring(st, en - st).Trim();
                }

                st = avis.IndexOf("<nomVoie>") + 9;
                en = avis.IndexOf("</nomVoie>", st);

                if (en > st)
                {
                    address1 += " " + avis.Substring(st, en - st).Trim();
                }

                st = avis.IndexOf("<codePostal>") + 12;
                en = avis.IndexOf("</codePostal>", st);

                if (en > st)
                {
                    address1 += " " + avis.Substring(st, en - st).Trim();
                }

                st = avis.IndexOf("<ville>") + 7;
                en = avis.IndexOf("</ville>", st);

                if (en > st)
                {
                    address1 += " " + avis.Substring(st, en - st).Trim();
                }

                address.Add(address1.Trim());
               // Console.WriteLine(address1);


                st = avis.IndexOf("<activite>") + 10;
                en = avis.IndexOf("</activite>", st);

                if (en > st)
                {
                    activite.Add(avis.Substring(st, en - st).Trim());
                   // Console.WriteLine(avis.Substring(st, en - st).Trim());
                }
                else
                {
                    activite.Add("");
                }

                st = avis.IndexOf("<montantCapital>") + 16;
                en = avis.IndexOf("</montantCapital>", st);

                if (en > st)
                {
                    montantCapital.Add(avis.Substring(st, en - st).Trim() + "€");
                   // Console.WriteLine(avis.Substring(st, en - st).Trim() + "€");
                }
                else
                {
                    montantCapital.Add("");
                }
                st = avis.IndexOf("<administration>") + 16;
                en = avis.IndexOf("</administration>", st);

                if (en > st)
                {
                    administration.Add(avis.Substring(st, en - st).Trim());
                   // Console.WriteLine(avis.Substring(st, en - st).Trim());
                }
                else
                {
                    administration.Add("");
                }
                st = avis.IndexOf("<numeroIdentification>") + 22;
                en = avis.IndexOf("</numeroIdentification>", st);

                if (en > st)
                {
                    numeroIdentification.Add(avis.Substring(st, en - st).Trim());
                   // Console.WriteLine(avis.Substring(st, en - st).Trim());
                }
                else
                {
                    numeroIdentification.Add("");
                }

                st = avis.IndexOf("<dateImmatriculation>") + 21;
                en = avis.IndexOf("</dateImmatriculation>", st);

                if (en > st)
                {
                    dateImmatriculation.Add(avis.Substring(st, en - st).Trim());
                  //  Console.WriteLine(avis.Substring(st, en - st).Trim());
                }
                else
                {
                    dateImmatriculation.Add("");
                }

                st = avis.IndexOf("<formeJuridique>") + 16;
                en = avis.IndexOf("</formeJuridique>", st);

                if (en > st)
                {
                    formeJuridique.Add(avis.Substring(st, en - st).Trim());
                  //  Console.WriteLine(avis.Substring(st, en - st).Trim());
                }
                else
                {
                    formeJuridique.Add("");
                }

                st = avis.IndexOf("<enseigne>") + 10;
                en = avis.IndexOf("</enseigne>", st);

                if (en > st)
                {
                    enseigne.Add(avis.Substring(st, en - st).Trim());
                    // Console.WriteLine(avis.Substring(st, en - st).Trim());
                }
                else
                {
                    enseigne.Add("");
                }

                label2.Text = d.ToString();
                Application.DoEvents();

             
                index = en1 + 7;

            }
         
            for (int i = 0; i < occ; i++)
            {
                d++;          

                SqlConnection connection = new SqlConnection("Data Source=JAMES\\SQLEXPRESS;Initial Catalog=bodac;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT count(*) FROM bodacc where numeroIdentification=@NumeroIdentification", connection);
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@NumeroIdentification";
                param.Value = numeroIdentification[i];
                cmd.Parameters.Add(param);

               int count = (int)cmd.ExecuteScalar();

                if (count > 0) { }
                else
                {
                    f++;

                    Console.WriteLine(f);
                    //insert
                    cmd = new SqlCommand("INSERT INTO bodacc(denomination,formeJuridique,nomCommercial,administration,numeroIdentification,adresse,activite,dateImmatriculation,montantCapital,enseigne) " +
                        "values (@Denomination, @FormeJuridique, @NomCommercial, @Administration, @NumeroIdentification, @Adresse,@Activite,@DateImmatriculation,@MontantCapital,@Enseigne)", connection);
                    param = new SqlParameter();
                    param.ParameterName = "@Denomination";
                    param.Value = denomination[i];
                    cmd.Parameters.Add(param);

                    param = new SqlParameter();
                    param.ParameterName = "@FormeJuridique";
                    param.Value = formeJuridique[i];
                    cmd.Parameters.Add(param);

                
                        param = new SqlParameter();
                        param.ParameterName = "@NomCommercial";
                        param.Value = nomCommercial[i];
                        cmd.Parameters.Add(param);
                    

                    param = new SqlParameter();
                    param.ParameterName = "@Administration";
                    param.Value = administration[i];
                    cmd.Parameters.Add(param);

                    param = new SqlParameter();
                    param.ParameterName = "@NumeroIdentification";
                    param.Value = numeroIdentification[i];
                    cmd.Parameters.Add(param);

                    param = new SqlParameter();
                    param.ParameterName = "@DateImmatriculation";
                    param.Value = dateImmatriculation[i];
                    cmd.Parameters.Add(param);

                    param = new SqlParameter();
                    param.ParameterName = "@MontantCapital";
                    param.Value = montantCapital[i];
                    cmd.Parameters.Add(param);

                    param = new SqlParameter();
                    param.ParameterName = "@Adresse";
                    param.Value = address[i];
                    cmd.Parameters.Add(param);

                    param = new SqlParameter();
                    param.ParameterName = "@Activite";
                    param.Value = activite[i];
                    cmd.Parameters.Add(param);

                    param = new SqlParameter();
                    param.ParameterName = "@Enseigne";
                    param.Value = enseigne[i];
                    cmd.Parameters.Add(param);

                    cmd.ExecuteNonQuery();
                }
                connection.Close();



            }
           
          
        }
        string[] files;
      

        //mise à jour
        private void button4_Click(object sender, EventArgs e)
        {
            PasswordForm frm = new PasswordForm();
            if (frm.ShowDialog() != DialogResult.OK)
            {
                // The user canceled.
                frm.Close();
            }
            else
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "XML files *.xml | *.xml";
                open.Multiselect = true;
                open.Title = "Open XML Files";

                if (open.ShowDialog() == DialogResult.OK)
                {

                    files = open.FileNames;


                    foreach (string file in files)
                    {

                        label3.Text = file;
                        StreamReader reader;
                        try
                        {
                            FileStream myStream = new FileStream(file, FileMode.Open);
                            {
                                using (myStream)
                                {
                                    reader = new StreamReader(myStream, Encoding.GetEncoding("iso-8859-1"));
                                    textBox1.Text = reader.ReadToEnd();
                                }
                            }
                        }
                        catch (Exception ex) { }
                        read();

                    }
                    MessageBox.Show("Mise à jour achevée !");
                }
            }
        }

    }
    }

