using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caso2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString);

        public void ListaNombres()
        {
            using (SqlCommand cmd = new SqlCommand("usp_listanombres", cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    using (DataSet df = new DataSet())
                    {
                        da.Fill(df, "empleados");
                        cbNombre.DataSource = df.Tables["empleados"];
                        cbNombre.DisplayMember = "nombrecompleto";
                        cbNombre.ValueMember = "idempleado";
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            ListaNombres();

        }

        private void cbNombre_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using (SqlCommand cmd = new SqlCommand("usp_listapedidos", cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idempleado", cbNombre.SelectedValue);

                    using (DataSet df = new DataSet())
                    {
                        da.Fill(df, "Pedidos");
                        dgPedidos.DataSource = df.Tables["Pedidos"];
                        //lblPedido.Text = df.Tables["Pedidos"].Rows.Count.ToString();
                    }
                }
            }
        }
    }
}
