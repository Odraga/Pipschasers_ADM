using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pipschasers_ADM
{
    public partial class Form1 : Form
    {
        private byte[] data;
        private string imgPath = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCrearPDF_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;

            string filePath = @"Template\";

            string fileNameExisting = @"impuestos.pdf";

            string fileNameNew = @"prueba_" + nombre.Replace(" ", "").Trim() + ".pdf";

            string fullNewPath = filePath + fileNameNew;

            string fullExistingPath = filePath + fileNameExisting;

            using (var existingFileStream = new FileStream(fullExistingPath, FileMode.Open))

            using (var newFileStream = new FileStream(fullNewPath, FileMode.Create))
            {
                var pdfReader = new PdfReader(existingFileStream);

                var stamper = new PdfStamper(pdfReader, newFileStream);

                AcroFields fields = stamper.AcroFields;

                BaseColor color;
                BaseFont font = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

                fields.SetFieldProperty("fullName", "textcolor", color = new BaseColor(23,116,131), null);
                fields.SetFieldProperty("fullName", "textfont", font, null);
                fields.SetField("fullName", nombre);

                fields.SetFieldProperty("valorDop", "textcolor", color = new BaseColor(255, 0, 0), null);
                fields.SetField("valorDop", nombre);

                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(imgPath);

                PushbuttonField button = fields.GetNewPushbuttonFromField("imgTransferencia");
                button.Layout = PushbuttonField.LAYOUT_ICON_ONLY;
                button.ProportionalIcon = true;
                button.Image = imagen;

                fields.ReplacePushbuttonField("imgTransferencia", button.Field);

                stamper.FormFlattening = true;

                stamper.Close();
                pdfReader.Close();



            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Buscar Imagen";
            openFileDialog1.Filter = "Image files (*.jpeg, *.png) | *.jpeg; *.png";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                data = File.ReadAllBytes(openFileDialog1.FileName);
                imgPath = openFileDialog1.FileName;
            }
        }
    }
}
