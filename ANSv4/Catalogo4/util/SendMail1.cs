using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace Catalogo.util
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(
                  "yourid@gmail.com", "yourgmailpassword");
                MailMessage msg = new MailMessage();
                msg.To.Add(textBox_To.Text);
                msg.From = new MailAddress("yourid@gmail.com");
                msg.Subject = textBox_Subject.Text;
                msg.Body = textBox_Message.Text;
                Attachment data = new Attachment(textBox_Attachment.Text);
                msg.Attachments.Add(data);
                client.Send(msg);
                MessageBox.Show("Successfully Sent Message.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_Upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_Attachment.Text = dlg.FileName.ToString();
            }
        }
    }
}