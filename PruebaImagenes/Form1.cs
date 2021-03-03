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
using System.IO;

namespace PruebaImagenes
{
    public partial class Form1 : Form
    {
        MySqlConnection connection;
        Image imagenConseguida;
        byte[] arrayBytes;

        public Form1(MySqlConnection connection)
        {
            this.connection = connection;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            pictureBox1.Image = Image.FromFile(fileDialog.FileName);
            arrayBytes = imageToArray(pictureBox1.Image);
            insertarImagen();
        }

        private byte[] imageToArray (Image imagen)
        {
            MemoryStream ms = new MemoryStream();
            imagen.Save(ms, imagen.RawFormat);
            byte[] res = ms.ToArray();
            return res;
        }

        private void insertarImagen()
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand("INSERT INTO contiene (id_inmueble, foto) VALUES (1, @img)", connection);
            command.Parameters.Add("@img", MySqlDbType.LongBlob, arrayBytes.Length).Value = arrayBytes;
            command.ExecuteNonQuery();
            connection.Close();
        }

        private byte[] obtenerImagen()
        {
            connection.Open();
            byte[] res = null;
            MySqlCommand command = new MySqlCommand("SELECT foto FROM contiene WHERE id_contiene = 4", connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                res = (byte[])reader["foto"];
            }
            connection.Close();

            return res;
        }

        private Image arrayToImage()
        {
            Image res = null;
            MemoryStream ms = new MemoryStream(arrayBytes);
            res = Image.FromStream(ms);
            return res;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            arrayBytes = obtenerImagen();
            imagenConseguida = arrayToImage();
            pictureBox1.Image = imagenConseguida;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }
    }
}
