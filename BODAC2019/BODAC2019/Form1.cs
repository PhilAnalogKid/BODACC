using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Collections.ObjectModel;
using System.Data.SqlClient;



namespace BODAC2019
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
                   }

  

        
        private static void DisplayNodes(XmlNode node)
        {
            string value = "";
            string name = "";

                   //Print the node type, node name and node value of the node
            if (node.NodeType == XmlNodeType.Text)
                   {
                // list.Add(node.Value);
                 Console.WriteLine(node.Value);
               value = node.Value;
                   }
                   else
                   {
                // list.Add(node.Name);
                 Console.WriteLine(node.Name);
                name = node.Name;
                   }

            Console.WriteLine(name+" = "+value);



            //Print individual children of the node, gets only direct children of the node
            XmlNodeList children = node.ChildNodes;
                   foreach (XmlNode child in children)
                   {
                       DisplayNodes(child);
                   }               
        }


        private void button3_Click(object sender, EventArgs e)
        {
            int j = Int32.Parse(textBox1.Text);



            for (int i = j; i < listView1.Items.Count; i++)
            {
                try
                {
                    string uri=listView1.Items[i].SubItems[0].Text.Replace(" ", "+") + "+" + listView1.Items[i].SubItems[5].Text.Replace(" ", "+");


                    Console.WriteLine(uri);

                    webBrowser1.Navigate("https://www.google.com/search?q=" + uri);

                while (webBrowser1.ReadyState != WebBrowserReadyState.Complete || webBrowser1.IsBusy) Application.DoEvents();
                  
                    if (webBrowser1.Document != null)
                    {
                        HtmlElement elem1 = webBrowser1.Document.GetElementById("recaptcha");

                        if (elem1 != null)
                        {
                            MessageBox.Show("Google captcha");
                           break;
                        }

                        HtmlElementCollection elem = webBrowser1.Document.GetElementsByTagName("span");
                        foreach (HtmlElement link in elem)
                        {
                            if (link.GetAttribute("data-local-attribute") == "d3ph" && link.GetAttribute("data-ved")=="")
                            {
                               // MessageBox.Show(link.InnerText);
                                listView1.Items[i].SubItems[6].Text = link.InnerText.Trim();

                               // Console.WriteLine(link.InnerText.Trim());

                                Application.DoEvents();

                                if (link.InnerText.Trim() != "")
                                {
                                    SqlConnection connection = new SqlConnection("Data Source=JAMES\\SQLEXPRESS;Initial Catalog=bodac;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                                    connection.Open();

                                    SqlCommand cmd = new SqlCommand("UPDATE bodacc set phoneNumber=@Phone where numeroIdentification=@NumeroIdentification", connection);
                                    SqlParameter param = new SqlParameter();
                                    param.ParameterName = "@Phone";
                                    param.Value = link.InnerText.Trim();
                                    cmd.Parameters.Add(param);

                                    param = new SqlParameter();
                                    param.ParameterName = "@NumeroIdentification";
                                    param.Value = listView1.Items[i].SubItems[4].Text.Trim();
                                    cmd.Parameters.Add(param);

                                    cmd.ExecuteNonQuery();
                                    connection.Close();
                                }

                            }
                        }
                  


                    }
                }
                catch { Exception ex; }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


        
            SqlConnection connection = new SqlConnection("Data Source=JAMES\\SQLEXPRESS;Initial Catalog=bodac;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            connection.Open();

            SqlCommand cmd = new SqlCommand("SELECT top(1000) denomination,formeJuridique,nomCommercial,administration,numeroIdentification,adresse,activite FROM bodacc where [phoneNumber] IS NULL", connection);
         
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string denomination = (string)reader["denomination"];
                string formeJuridique = (string)reader["formeJuridique"];
                string nomCommercial = (string)reader["nomCommercial"];
                string administration = (string)reader["administration"];
                string numeroIdentification = (string)reader["numeroIdentification"];
                string adresse = (string)reader["adresse"];
                string activite = (string)reader["activite"];

                ListViewItem item = listView1.Items.Add(denomination);
                item.SubItems.Add(formeJuridique);
                item.SubItems.Add(nomCommercial);
                item.SubItems.Add(administration);
                item.SubItems.Add(numeroIdentification);
                item.SubItems.Add(adresse);
                item.SubItems.Add("");
                item.SubItems.Add(activite);

               


            }

            connection.Close();

            listView1.EnsureVisible(listView1.Items.Count - 1);
            label1.Text = listView1.Items.Count.ToString();
            Application.DoEvents();



        }

        private void button4_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                try
                {
                    string uri = "https://www.google.com/search?q=" + listView1.Items[i].SubItems[0].Text.Replace(" ", "+")+"+"+ listView1.Items[i].SubItems[5].Text.Replace(" ", "+");
                    
                   // Console.WriteLine(uri);

                    webBrowser1.Navigate(uri);

                    while (webBrowser1.ReadyState != WebBrowserReadyState.Complete || webBrowser1.IsBusy) Application.DoEvents();

                    if (webBrowser1.Document != null)
                    {
                        HtmlElement elem1 = webBrowser1.Document.GetElementById("recaptcha");

                        if (elem1 != null)
                        {
                            textBox1.Text = i.ToString();
                            i++;
                        }

                        HtmlElementCollection elem = webBrowser1.Document.GetElementsByTagName("a");
                        foreach (HtmlElement link in elem)
                        {
                     
                         
                            if (link.GetAttribute("href").IndexOf("www.pagesjaunes.fr/pros/")>0)
                            {
                                string uri1=link.GetAttribute("href");

               
                               // listView1.Items[i].SubItems[6].Text = link.InnerHtml.Trim();

                                webBrowser2.Navigate(uri1);

                                HtmlElementCollection elem2 = webBrowser2.Document.GetElementsByTagName("span");
                                foreach (HtmlElement link1 in elem2)
                                {


                                    if (link1.GetAttribute("className") == "coord-numero noTrad")
                                    {

                                        listView1.Items[i].SubItems[6].Text = link1.InnerText.Trim();
                                        listView1.EnsureVisible(i-1);
                                        Application.DoEvents();

                                        SqlConnection connection = new SqlConnection("Data Source=JAMES\\SQLEXPRESS;Initial Catalog=bodac;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                                        connection.Open();

                                        SqlCommand cmd = new SqlCommand("UPDATE bodacc set phoneNumber=@Phone where numeroIdentification=@NumeroIdentification", connection);
                                        SqlParameter param = new SqlParameter();
                                        param.ParameterName = "@Phone";
                                        param.Value = link1.InnerText.Trim();
                                        cmd.Parameters.Add(param);

                                        param = new SqlParameter();
                                        param.ParameterName = "@NumeroIdentification";
                                        param.Value = listView1.Items[i].SubItems[4].Text.Trim();
                                        cmd.Parameters.Add(param);

                                        cmd.ExecuteNonQuery();
                                        connection.Close();
                                    }
                                }

                            }
                        }



                    }
                }
                catch { i++; }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Text = "Please wait ...";

            label1.Text = "";

            listView1.Items.Clear();
         
            Application.DoEvents();

            String add = "";

           
            if (activite_search.Text != "" && lieu_search.Text == "")
            {
                add += "activite like '%'+@Activite+'%'";

            }
            else if (lieu_search.Text != "" && activite_search.Text != "")
            {
                add += " activite like '%'+@Activite+'%' and adresse like '%'+@Lieu+'%'";

            }
            else if (lieu_search.Text != "" && activite_search.Text == "")
            {               
                 add += "adresse like '%'+@Lieu+'%'";
            }



            if (denomination_search.Text != "" && (activite_search.Text != "" || lieu_search.Text != ""))
            {
                add += " and denomination like '%'+@Denomination+'%'";

            }
            else if (denomination_search.Text != "" && lieu_search.Text == "" && activite_search.Text == "")
            {
                add += "denomination like '%'+@Denomination+'%'";
            }

            //MessageBox.Show(add);

            SqlConnection connection = new SqlConnection("Data Source=JAMES\\SQLEXPRESS;Initial Catalog=bodac;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            connection.Open();

            SqlCommand cmd = new SqlCommand("SELECT denomination,administration,numeroIdentification,adresse,activite FROM bodacc where ("+add+") order by denomination", connection);

            SqlParameter param = new SqlParameter();
                   
                param.ParameterName = "@Activite";
                param.Value = activite_search.Text;
                cmd.Parameters.Add(param);         

         
                param = new SqlParameter();
                param.ParameterName = "@Lieu";
                param.Value = lieu_search.Text;
                cmd.Parameters.Add(param);
            

          
                param = new SqlParameter();
                param.ParameterName = "@Denomination";
                param.Value = denomination_search.Text;
                cmd.Parameters.Add(param);            

                SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string denomination = (string)reader["denomination"];
                //string formeJuridique = (string)reader["formeJuridique"];
              //  string nomCommercial = (string)reader["nomCommercial"];
                string administration = (string)reader["administration"];
                string numeroIdentification = (string)reader["numeroIdentification"];
                string adresse = (string)reader["adresse"];
                string activite = (string)reader["activite"];

                ListViewItem item = listView1.Items.Add(denomination);
              //  item.SubItems.Add(formeJuridique);
               // item.SubItems.Add(nomCommercial);
                item.SubItems.Add(administration);
                item.SubItems.Add(numeroIdentification);
                item.SubItems.Add(adresse);
                item.SubItems.Add("");
                item.SubItems.Add(activite);

              
            }

            connection.Close();

            if (listView1.Items.Count < 1) { }
            else
            {
              //  listView1.EnsureVisible(listView1.Items.Count - 1);
            }
            label1.Text = listView1.Items.Count.ToString() +" Results";
            button5.Text = "Rechercher";
            Application.DoEvents();

        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dirLoc = "c:\\Users\\Phil\\Desktop\\BODAC_files\\";
            string[] sitemaps = Directory.GetFiles(dirLoc);
            foreach (string sitemap in sitemaps)
            {
                try
                {

                    Console.WriteLine(sitemap);
                    Application.DoEvents();

                    //MessageBox.Show(sitemap);
                    // new xdoc instance 
                    XmlDocument doc = new XmlDocument();

                    //load up the xml from the location 
                    doc.Load(sitemap);

                    string denomination = "";
                    string formeJuridique = "";
                    string nomCommercial = "";
                    string administration = "";//liquidateur
                    string numeroIdentification = "";
                    string activite = "";
                    string adresse = "";

                    XmlNodeList nodes = doc.GetElementsByTagName("avis");

                    foreach (XmlNode node in nodes)
                    {
                        denomination = "";
                        formeJuridique = "";
                        nomCommercial = "";
                        administration = "";
                        numeroIdentification = "";
                        activite = "";
                        adresse = "";

                        foreach (XmlNode child in node.ChildNodes)
                        {
                            if (child.Name == "personnes")
                            {
                                foreach (XmlNode child1 in child)
                                {
                                    if (child1.Name == "personne")
                                    {
                                        foreach (XmlNode child2 in child1.ChildNodes)
                                        {
                                            if (child2.Name == "adresse")
                                            {
                                                foreach (XmlNode child3 in child2.ChildNodes)
                                                {
                                                    foreach (XmlNode child4 in child3.ChildNodes)
                                                    {
                                                        if (child4.Name == "numeroVoie")
                                                        {
                                                            adresse = child4.InnerText + " ";
                                                        }
                                                        if (child4.Name == "typeVoie")
                                                        {
                                                            adresse += child4.InnerText + " ";
                                                        }
                                                        if (child4.Name == "nomVoie")
                                                        {
                                                            adresse += child4.InnerText + " ";
                                                        }
                                                        if (child4.Name == "codePostal")
                                                        {
                                                            adresse += child4.InnerText + " ";
                                                        }
                                                        if (child4.Name == "ville")
                                                        {
                                                            adresse += child4.InnerText + " ";
                                                        }
                                                    }
                                                }
                                            }


                                            foreach (XmlNode child3 in child2.ChildNodes)
                                            {



                                                //MessageBox.Show(child3.Name);

                                                if (child3.Name == "denomination")
                                                {
                                                    denomination = child3.InnerText;
                                                }
                                                if (child3.Name == "formeJuridique")
                                                {
                                                    formeJuridique = child3.InnerText;
                                                }
                                                if (child3.Name == "nomCommercial")
                                                {
                                                    nomCommercial = child3.InnerText;
                                                }
                                                if (child3.Name == "administration")
                                                {
                                                    administration = child3.InnerText;
                                                }
                                                if (child3.Name == "numeroImmatriculation")
                                                {
                                                    foreach (XmlNode child4 in child3.ChildNodes)
                                                    {
                                                        if (child4.Name == "numeroIdentification")
                                                        {
                                                            numeroIdentification = child4.InnerText + " ";
                                                        }
                                                        if (child4.Name == "codeRCS")
                                                        {
                                                            numeroIdentification += child4.InnerText + " ";
                                                        }
                                                        if (child4.Name == "nomGreffeImmat")
                                                        {
                                                            numeroIdentification += child4.InnerText;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (child.Name == "etablissement")
                            {
                                foreach (XmlNode child1 in child.ChildNodes)
                                {
                                    if (child1.Name == "activite")
                                    {
                                        activite = child1.InnerText;
                                    }
                                }
                            }
                        }

                        if (denomination != "" && adresse != "")
                        {
                           /* ListViewItem item = listView1.Items.Add(denomination);

                            item.SubItems.Add(formeJuridique);
                            item.SubItems.Add(nomCommercial);
                            item.SubItems.Add(administration);
                            item.SubItems.Add(numeroIdentification);
                            item.SubItems.Add(adresse.Trim());
                            item.SubItems.Add("");
                            item.SubItems.Add(activite);

                            listView1.EnsureVisible(listView1.Items.Count - 1);

    */
                            SqlConnection connection = new SqlConnection("Data Source=JAMES\\SQLEXPRESS;Initial Catalog=bodac;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                            connection.Open();

                            SqlCommand cmd = new SqlCommand("SELECT count(*) FROM bodacc where numeroIdentification=@NumeroIdentification", connection);
                            SqlParameter param = new SqlParameter();
                            param.ParameterName = "@NumeroIdentification";
                            param.Value = numeroIdentification;
                            cmd.Parameters.Add(param);

                            int count = (int)cmd.ExecuteScalar();

                            if (count > 0) { }
                            else
                            {
                                //insert
                                cmd = new SqlCommand("INSERT INTO bodacc(denomination,formeJuridique,nomCommercial,administration,numeroIdentification,adresse,activite) " +
                                    "values (@Denomination, @FormeJuridique, @NomCommercial, @Administration, @NumeroIdentification, @Adresse,@Activite)", connection);
                                param = new SqlParameter();
                                param.ParameterName = "@Denomination";
                                param.Value = denomination;
                                cmd.Parameters.Add(param);

                                param = new SqlParameter();
                                param.ParameterName = "@FormeJuridique";
                                param.Value = formeJuridique;
                                cmd.Parameters.Add(param);

                                param = new SqlParameter();
                                param.ParameterName = "@NomCommercial";
                                param.Value = nomCommercial;
                                cmd.Parameters.Add(param);

                                param = new SqlParameter();
                                param.ParameterName = "@Administration";
                                param.Value = administration;
                                cmd.Parameters.Add(param);

                                param = new SqlParameter();
                                param.ParameterName = "@NumeroIdentification";
                                param.Value = numeroIdentification;
                                cmd.Parameters.Add(param);

                                param = new SqlParameter();
                                param.ParameterName = "@Adresse";
                                param.Value = adresse.Trim();
                                cmd.Parameters.Add(param);

                                param = new SqlParameter();
                                param.ParameterName = "@Activite";
                                param.Value = activite;
                                cmd.Parameters.Add(param);

                                cmd.ExecuteNonQuery();
                            }
                            connection.Close();



                        }
                    }


                    //  Console.WriteLine(denomination + " " + formeJuridique + " " + administration+ " "+capital);
                    label1.Text = listView1.Items.Count.ToString();
                    Application.DoEvents();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            MessageBox.Show("Mise à jour effectuée");
        }

        private void updateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            update update = new update();
            update.ShowDialog();
                 

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
