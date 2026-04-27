using System.Net;
using System.Text.Json;

namespace WallpaperDownloaderForm
{
    public partial class Form1 : Form
    {
        private const string baseUrl = "http://cn.bing.com";
        private static ImageModel entity;
        private static Image Img;
        private static readonly HttpClient _httpClient = new HttpClient();
        public Form1()
        {
            InitializeComponent();
            label2.Text = "正在加载...";
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            // 1. 取消订阅事件
            comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;

            string url = await GetBingImageAsync(0);
            List<ImagesItem> list = entity.images;
            comboBox1.DataSource = list;
            comboBox1.ValueMember = "urlStr";
            comboBox1.DisplayMember = "enddate";

            // 2. 重新订阅事件
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

            // 3.显示图片
            string imageUrl = baseUrl + comboBox1.SelectedValue.ToString().Split('|')[0];
            await ShowImageAsync(imageUrl);
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null) return;
            label2.Text = "正在加载...";
            string imageUrl = baseUrl + comboBox1.SelectedValue.ToString().Split('|')[0];
            await ShowImageAsync(imageUrl);
        }

        private async void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            string url = await GetBingImageAsync(Convert.ToInt32(this.numericUpDown1.Value));
            List<ImagesItem> list = entity.images;
            comboBox1.DataSource = list;
            comboBox1.ValueMember = "urlStr";
            comboBox1.DisplayMember = "enddate";
        }

        private async Task<string> ShowImageAsync(string imageUrl)
        {
            Image freshImg = await GetImageFromUrlAsync(imageUrl);
            if (freshImg != null)
            {

                // 先释放旧图片内存（如果有的话）
                this.BackgroundImage?.Dispose();
                this.BackgroundImage = freshImg;
                this.BackgroundImage.Tag = comboBox1.SelectedValue.ToString().Split('|')[1];
                label2.Text = comboBox1.SelectedValue.ToString().Split('|')[2] + " - " + comboBox1.SelectedValue.ToString().Split('|')[1];
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取某一天的Bing壁纸数据
        /// </summary>
        /// <returns></returns>
        static async Task<string> GetBingImageAsync(int day)
        {
            string url = $"http://cn.bing.com/HPImageArchive.aspx?format=js&idx={day}&n=100&mkt=zh-CN";
            string _ret = await ApiClient.SendPostApiAsync(url);
            entity = JsonSerializer.Deserialize<ImageModel>(_ret, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (entity?.images?.Count > 0)
            {
                return baseUrl + entity.images[0].url + "|" + entity.images[0].title;
            }
            return string.Empty;
        }

        #region 将图片Url转换为Image对象
        private static async Task<Image> GetImageFromUrlAsync(string url)
        {
            try
            {
                byte[] imageBytes = await _httpClient.GetByteArrayAsync(url);

                // 关键改进：不在这里对 MemoryStream 使用 using
                // 或者：直接将流转成一个解耦的 Bitmap
                MemoryStream ms = new MemoryStream(imageBytes);

                // 这样创建出来的 Image 会依赖 ms，ms 只要不 Dispose，图片就不会消失
                return Image.FromStream(ms);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"图片加载失败: {ex.Message}");
                return null;
            }
        }
        #endregion

        #region 右键下载
        private async void 下载1080PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".jpg";
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            sfd.Filter = "图片文件|*.jpg";
            sfd.FileName = comboBox1.Text + "(" + this.BackgroundImage.Tag + ")_1080P.jpg";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                bool b = await ApiClient.DownloadFileAsync(baseUrl + comboBox1.SelectedValue.ToString().Split('|')[0], sfd.FileName);
                if (b)
                    label2.Text = "下载完成！";
                else
                    label2.Text = "下载失败！";
            }
        }

        private async void 下载4KToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".jpg";
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            sfd.Filter = "图片文件|*.jpg";
            sfd.FileName = comboBox1.Text + "(" + this.BackgroundImage.Tag + ")_4K.jpg";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string imgUrl = comboBox1.SelectedValue.ToString().Split('|')[0].Replace("_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=hp", "_UHD.jpg&rf=LaDigue_UHD.jpg&pid=hp&w=3840&h=2160&rs=1&c=4");
                bool b = await ApiClient.DownloadFileAsync(baseUrl + imgUrl, sfd.FileName);
                if (b)
                    label2.Text = "下载完成！";
                else
                    label2.Text = "下载失败！";
            }
        }

        private async void 下载OriginalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".jpg";
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            sfd.Filter = "图片文件|*.jpg";
            sfd.FileName = comboBox1.Text + "(" + this.BackgroundImage.Tag + ")_Original.jpg";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string imgUrl = comboBox1.SelectedValue.ToString().Split('|')[0].Replace("_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=hp", "_UHD.jpg");
                bool b = await ApiClient.DownloadFileAsync(baseUrl + imgUrl, sfd.FileName);
                if (b)
                    label2.Text = "下载完成！";
                else
                    label2.Text = "下载失败！";
            }
        }

        private async void 保存到D盘根目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsDrive = Directory.Exists(@"D:\");
            if (IsDrive)
            {
                bool b = await ApiClient.DownloadFileAsync(baseUrl + comboBox1.SelectedValue.ToString().Split('|')[0], "d:\\" + this.BackgroundImage.Tag + ".jpg");
                if (b)
                    label2.Text = "下载完成，已成功保存到D:\\。";
                else
                    label2.Text = "下载失败。";
            }
            else
                MessageBox.Show("无法保存，因为D盘不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
        #endregion

        
    }
}
