using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PruebaImagenes
{
    public partial class Form1 : Form
    {
        MySqlConnection connection;

        public Form1(MySqlConnection connection)
        {
            this.connection = connection;
            MySqlCommand comm = new MySqlCommand("SELECT * FROM inmueble", connection);
            InitializeComponent();

            connection.Open();

            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                textBox1.Text += reader["descripcion"].ToString();
                textBox1.Text += "\r\n";
            }
        }
    }
}
