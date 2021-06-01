using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaHoa_AES
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static string decryptedData; // String giải mã

        static string encryptedData;// String mã hoá

        static Bitmap originalImage; // ảnh ban đầu

        static Bitmap imageWithHiddenData; // ảnh đã giấu thông tin

        static string inPath1, inPath2; // Đường dẫn in textbox //  

        static string pathOriginalImage; 

        static string pathResultImage;


        // Giấu thông tin vào ảnh
        public static Bitmap EncryptImage(string data, string password)
        {
            SteganographyHelper sgp = new SteganographyHelper();

            encryptedData = StringCipher.Encrypt(data, password);    // Mã hoá thông tin

            originalImage = SteganographyHelper.CreateNonIndexedImage(Image.FromFile(pathOriginalImage));
            sgp.grayImage(ref originalImage);
            imageWithHiddenData = SteganographyHelper.MergeText(encryptedData, originalImage);
           

            //imageWithHiddenData.Save(pathResultImage);
            //imageWithHiddenData.Save(@"D:\images\Harry_encrypt1.png");
            return imageWithHiddenData;

        }
        // Tách thông tin từ ảnh
        public static string DecryptImage(string _PASSWORD, Bitmap image)
        {
            //string _PASSWORD = "password";
            //string pathImageWithHiddenInformation = @"D:\images\Harry_encrypt.png";

            string encryptedData = SteganographyHelper.ExtractText(image);

            decryptedData = StringCipher.Decrypt(encryptedData, _PASSWORD);// Giải mã thông tin
            return decryptedData;

        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            
            try
            {
                pathOriginalImage = txtpathFileOld.Text;
                if (string.IsNullOrEmpty(pathOriginalImage))
                {
                    MessageBox.Show("Vui lòng chọn ảnh trước khi giấu tin !");
                    return;
                }
                pathResultImage = txtpathFileNew.Text;
                Bitmap newBitmap =   EncryptImage(richData.Text, txtpass.Text);           
                //txtpathFileOld.Text = inPath2;
                pictureAfter.Image = newBitmap;
                MessageBox.Show("Giấu tin thành công !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Giấu tin thất bại !");
            }
        }
        private void button2_Click_1(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Choose an Image";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                inPath1 = openFileDialog1.FileName; // get pathFile ảnh dùng để giấu thông tin 
                Bitmap oldBitmap = new Bitmap(inPath1);
                txtpathFileOld.Text = inPath1;
                pictureBefor.Image = oldBitmap;
            }
            else
            {
                //inPath1 = "";
            }
        }

        private void pictureBefor_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Choose an Image";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                inPath1 = openFileDialog1.FileName; // get pathFile ảnh dùng để giấu thông tin 
                Bitmap oldBitmap = new Bitmap(inPath1);
                txtpathFileOld.Text = inPath1;
                pictureBefor.Image = oldBitmap;
            }
            else
            {
                //inPath2 = "";
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureAfter.Image == null)
            {
                MessageBox.Show("Vui lòng giấu tin trước khi lưu ảnh !");
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Title = "Save Image";

            saveFileDialog.Filter = "Image Files (*.png;*.jpg; *.bmp)|*.png;*.jpg;*.bmp" + "|" + "All Files (*.*)|*.*";
            //openFileDialog1.Filter = "txt files (*.jpg)|*.jpg|All files (*.*)|*.*";
            //SaveFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                inPath2 = saveFileDialog.FileName; // get pathFile ảnh đã giấu tin

                pictureAfter.Image.Save(inPath2);
                MessageBox.Show("Lưu ảnh thành công");
            }
            else
            {
                inPath2 = "";
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Choose an Image ";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                inPath1 = openFileDialog1.FileName; // get pathFile ảnh dùng để giấu thông tin 
                Bitmap oldBitmap = new Bitmap(inPath1);
                textBox1.Text = inPath1;
                pictureBox1.Image = oldBitmap;
            }
            else
            {
                //inPath1 = "";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            richData.Clear();
            txtpass.Clear();
            pictureBefor.Image = null;          
            pictureAfter.Image = null;
            txtpathFileOld.Clear();
            txtpathFileNew.Clear();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            pictureBox1.Image = null;
            textBox1.Clear();
            textBox2.Clear();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu giải mã !");
                    return;
                }
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Vui lòng chọn ảnh trước khi giải mã !");
                    return;
                }
                //Image.FromFile(pathOriginalImage)
                richTextBox1.Text = DecryptImage(textBox2.Text, pictureBox1.Image as Bitmap);
                MessageBox.Show("Giải mã thành công !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Giải mã thất bại !");
            }

        }

    }
}
