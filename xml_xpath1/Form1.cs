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
using System.Xml.XPath;

namespace xml_xpath1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            System.Diagnostics.Debug.WriteLine(Application.StartupPath);
            
            XPathDocument doc = new XPathDocument(new StreamReader(Application.StartupPath + @"\invoices.xml"));
            XPathNavigator nav = doc.CreateNavigator();
            XPathNodeIterator iter = nav.SelectDescendants("invoice", "", false);
            // iter points to parent of invoice which is invoices
            // first call to MoveNext takes it to invoice element.

            while(iter.MoveNext()) 
            {
                sb.AppendLine(); // blank line.

                sb.AppendLine("Name: " +
                        iter.Current.SelectSingleNode("./patient/@firstname").Value);

                sb.AppendLine("SSN: " +
                        iter.Current.SelectSingleNode("./patient/@SSN").Value);

                sb.AppendLine("Procedures and Cost:");

                XPathNodeIterator navProcedures = iter.Current.SelectDescendants("procedure","",false);
            
                // navProcedures points to parent of all procedure which is invoice.
                // First call to movenext takes it to first procedure.
                while(navProcedures.MoveNext())
                {
                    sb.AppendLine(  navProcedures.Current.GetAttribute("name","") + ", " + navProcedures.Current.GetAttribute("cost",""));
                } // iterate all procedure elements.


            } // iterate all invoice elements.

            txbResults.Text = sb.ToString();

        }
    }
}
