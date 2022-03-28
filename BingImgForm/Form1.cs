using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BingImgForm
{
    public partial class Form1 : Form
    {
        private const string baseUrl = "http://cn.bing.com";
        private static ImageModel entity;
        private static Image Img;
        public Form1()
        {
            InitializeComponent();
            label2.Text = "正在加载...";
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string url = await GetBingImageAsync(0);
            List<ImageList> list= entity.images;
            comboBox1.DataSource = list;
            comboBox1.ValueMember = "urlStr";
            comboBox1.DisplayMember = "enddate";
            
            pictureBox1.Image= UrlToImage(url.Split('|')[0]);
            pictureBox1.Tag = url.Split('|')[1];
            label2.Text = url.Split('|')[1];
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        }

        private async void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            string url = await GetBingImageAsync(Convert.ToInt32(this.numericUpDown1.Value));
            List<ImageList> list = entity.images;
            comboBox1.DataSource = list;
            comboBox1.ValueMember = "urlStr";
            comboBox1.DisplayMember = "enddate";
        }

        private async void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = "正在加载...";
            string result = await ShowImageAsync(baseUrl + comboBox1.SelectedValue.ToString().Split('|')[0]);
            pictureBox1.Image = Img;
            pictureBox1.Tag = comboBox1.SelectedValue.ToString().Split('|')[1];
            label2.Text = comboBox1.SelectedValue.ToString().Split('|')[1];
        }


        /// <summary>
        /// 异步获取Image实体
        /// </summary>
        /// <returns></returns>
        static Task<string> GetBingImageAsync(int day)
        {
            return Task<string>.Run(() =>
            {
                string url = "http://cn.bing.com/HPImageArchive.aspx?format=js&idx="+day+"&n=100";//修改idx=0 1=昨天 2=前天
                string _ret = HttpHelper.SendPostApi(url, null);
                entity = JsonHelper.ConvertToObject<ImageModel>(_ret);  
                return baseUrl + entity.images[0].url+"|"+ entity.images[0].ImageTitie;
            });
        }

        /// <summary>
        /// 异步转换图片Url到Image对象
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        static Task<string> ShowImageAsync(string url)
        {
            return Task<string>.Run(() =>
            {
                Img= UrlToImage(url);
                return "ok";
            });
        }

        #region 将图片Url转换为Image对象
        private static Image UrlToImage(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            using (WebResponse response = request.GetResponse())
            {
                Image img = Image.FromStream(response.GetResponseStream());
                return img;
            }
        }
        #endregion

        #region 下载
        private void 下载ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".jpg";
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            sfd.Filter = "图片文件|*.jpg";
            sfd.FileName = comboBox1.Text + "(" + pictureBox1.Tag + ").jpg";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                bool b = HttpHelper.DownloadFile(baseUrl + comboBox1.SelectedValue.ToString().Split('|')[0], sfd.FileName);
                if (b)
                    label2.Text = "下载完成！";
                else
                    label2.Text = "下载失败！";
            }
        }

        private void 下载ToolStripMenuItem_4K_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".jpg";
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            sfd.Filter = "图片文件|*.jpg";
            sfd.FileName = comboBox1.Text + "(" + pictureBox1.Tag + ")_4K.jpg";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string imgUrl = comboBox1.SelectedValue.ToString().Split('|')[0].Replace("_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=hp", "_UHD.jpg&rf=LaDigue_UHD.jpg&pid=hp&w=3840&h=2160&rs=1&c=4");
                bool b = HttpHelper.DownloadFile(baseUrl + imgUrl, sfd.FileName);
                if (b)
                    label2.Text = "下载完成！";
                else
                    label2.Text = "下载失败！";
            }
        }

        private void 下载ToolStripMenuItem_Full_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".jpg";
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            sfd.Filter = "图片文件|*.jpg";
            sfd.FileName = comboBox1.Text + "(" + pictureBox1.Tag + ")_原始.jpg";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string imgUrl = comboBox1.SelectedValue.ToString().Split('|')[0].Replace("_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=hp", "_UHD.jpg");
                bool b = HttpHelper.DownloadFile(baseUrl + imgUrl, sfd.FileName);
                if (b)
                    label2.Text = "下载完成！";
                else
                    label2.Text = "下载失败！";
            }
        }

        private void 保存到D盘根目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsDrive= Directory.Exists(@"D:\");
            if (IsDrive)
            {
                bool b = HttpHelper.DownloadFile(baseUrl + comboBox1.SelectedValue.ToString().Split('|')[0], "d:\\" + pictureBox1.Tag + ".jpg");
                if (b)
                    label2.Text = "下载完成，已成功保存到D:\\。";
                else
                    label2.Text = "下载失败。";
            }
            else
                MessageBox.Show("无法保存，因为D盘不存在！","提示",MessageBoxButtons.OK,MessageBoxIcon.Stop);    
        }

        #endregion

        
    }
}
