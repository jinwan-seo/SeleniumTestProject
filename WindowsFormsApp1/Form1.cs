using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Web;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Thread th = null;
        bool searched = false;
        public string[] user_Info = null;
        public bool autoUser = false;
        int Search_Num = 0;

        public Form1()
        {
            InitializeComponent();

            searchButton.Click += Button_Click;
            stopSearchButton.Click += Button_Click;
            dataGridView1.CellClick += DataGridViewCell_Click;
        }
        private void DataGridViewCell_Click(object sender, DataGridViewCellEventArgs e)
        {
            if (sender.Equals(dataGridView1) && e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["속성_버튼"].Index)
            {
                string url = dataGridView1[dataGridView1.Columns["속성_링크"].Index, e.RowIndex].Value.ToString();

                Thread thread = new Thread(new ParameterizedThreadStart(GoToUrl_Data));
                thread.Start(url);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender.Equals(searchButton))
            {//검색하기 버튼이 클릭되었을 경우
                
                if (autoReplyCheckBox.Checked)
                {//오토 댓글 유저인 경우
                    string naver_id = uIdTextBox.Text;
                    string naver_pw = uPwTextBox.Text;

                    user_Info = new string[2] { naver_id, naver_pw };

                    autoUser = true;
                }

                if (searched == true)
                {
                    MessageBox.Show("검색을 먼저 중단하세요","검색중단요청",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                else
                {                    
                    searched = true;
                    if (blogRadioButton.Checked)
                    {//블로그 라디오 버튼 체크되었을 경우
                        ThreadStart threadStart = new ThreadStart(Blog_Search);
                        th = new Thread(threadStart);
                        th.Start();
                    }
                    else if (cafeRadioButton.Checked)
                    {//카페 라디오 버튼이 체크되었을 경우
                        ThreadStart threadStart = new ThreadStart(Cafe_Search);
                        th = new Thread(threadStart);
                        th.Start();
                    }
                    else if (kinRadioButton.Checked)
                    {//지식인 라디오 버튼이 체크되었을 경우
                        ThreadStart threadStart = new ThreadStart(Kin_Search);
                        th = new Thread(threadStart);
                        th.Start();
                    }
                }
            }
            else if (sender.Equals(stopSearchButton))
            {//검색 중단 버튼 클릭시
                searched = false;
                if (th != null)
                {
                    th.Abort();
                }
            }
        }
        private void Blog_Search()
        {
            try
            {
                int time = 0;

                if (oneHour_Radiobutton.Checked)
                {
                    time = 60000;
                }
                else if (threehour_RadioButton.Checked)
                {
                    time = 10800000;
                }
                else if (sixHour_RadioButton.Checked)
                {
                    time = 21600000;
                }

                for (int j = 1; j <= 1000; j++)
                {

                    //데이터 그리드뷰 Head column 작성
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            //그리드뷰 리셋
                            dataGridView1.Rows.Clear();
                            dataGridView1.Columns.Clear();
                            dataGridView1.Refresh();
                            //column 설정
                            dataGridView1.Columns.Add("속성_타이틀", "타이틀");
                            dataGridView1.Columns[0].Width = 305;
                            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                            dataGridView1.Columns.Add("속성_링크", "링크");
                            dataGridView1.Columns[1].Width = 305;
                            dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                            dataGridView1.Columns.Add("속성_버튼", "버튼");
                            dataGridView1.Columns[2].Width = 65;
                            dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                        }));
                    }
                    else
                    {
                        //그리드뷰 리셋
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();
                        dataGridView1.Refresh();
                        //column 설정
                        dataGridView1.Columns.Add("속성_타이틀", "타이틀");
                        dataGridView1.Columns[0].Width = 305;
                        dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dataGridView1.Columns.Add("속성_링크", "링크");
                        dataGridView1.Columns[1].Width = 305;
                        dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dataGridView1.Columns.Add("속성_버튼", "버튼");
                        dataGridView1.Columns[2].Width = 65;
                        dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    //블로그 데이터 수집

                    for (int i = 1; i <= 10; i++)
                    {
                        string keyWord = HttpUtility.UrlEncode(keyWordTextBox.Text);
                        int pageNum = i * 10 - 9;
                        var url = $"https://search.naver.com/search.naver?date_from=&date_option=0&date_to=&dup_remove=1&nso=&post_blogurl=&post_blogurl_without=&query={keyWord}&sm=tab_pge&srchby=all&st=sim&where=post&start={pageNum.ToString()}";
                        HtmlWeb web = new HtmlWeb();
                        var doc = web.Load(url);

                        HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//ul[@id='elThumbnailResultArea']");

                        var NODE = nodes[0].SelectNodes("//a[@class='sh_blog_title _sp_each_url _sp_each_title']");

                        //순위 권 내 타이틀, 링크 수집후 datagridview1에 업로드
                        foreach (var item in NODE)
                        {
                            if (this.InvokeRequired)
                            {
                                this.Invoke(new MethodInvoker(delegate ()
                                {
                                    int rowIndex = dataGridView1.Rows.Add();
                                    dataGridView1[dataGridView1.Columns["속성_타이틀"].Index, rowIndex].Value = item.GetAttributeValue("title", "").Trim();
                                    dataGridView1[dataGridView1.Columns["속성_링크"].Index, rowIndex].Value = item.GetAttributeValue("href", "").Trim();
                                    dataGridView1.Rows[rowIndex].Cells["속성_버튼"] = new DataGridViewButtonCell();
                                    dataGridView1.Rows[rowIndex].Cells["속성_버튼"].Value = "바로가기";
                                }));
                            }
                            else
                            {
                                int rowIndex = dataGridView1.Rows.Add();
                                dataGridView1[dataGridView1.Columns["속성_타이틀"].Index, rowIndex].Value = item.GetAttributeValue("title", "").Trim();
                                dataGridView1[dataGridView1.Columns["속성_링크"].Index, rowIndex].Value = item.GetAttributeValue("href", "").Trim();
                                dataGridView1.Rows[rowIndex].Cells["속성_버튼"] = new DataGridViewButtonCell();
                                dataGridView1.Rows[rowIndex].Cells["속성_버튼"].Value = "바로가기";
                            }
                        }
                    }
                    //데이터저장소 호출
                    Search_Num = 1;
                    SearchFile(j);
                    Thread.Sleep(time);
                }
            }
            catch (Exception Err)
            {
                Console.WriteLine("스레드 종료" + "\n" + Err.Message.ToString());
            }
            finally
            {
                Thread.CurrentThread.Abort();
            }
        }
        private void Cafe_Search()
        {
            try
            {
                int time = 0;

                if (oneHour_Radiobutton.Checked)
                {
                    time = 60000;
                }
                else if (threehour_RadioButton.Checked)
                {
                    time = 10800000;
                }
                else if (sixHour_RadioButton.Checked)
                {
                    time = 21600000;
                }


                for (int j = 1; j <= 1000; j++)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            //그리드뷰 리셋
                            dataGridView1.Rows.Clear();
                            dataGridView1.Columns.Clear();
                            dataGridView1.Refresh();
                            //column 설정
                            dataGridView1.Columns.Add("속성_타이틀", "타이틀");
                            dataGridView1.Columns[0].Width = 305;
                            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                            dataGridView1.Columns.Add("속성_링크", "링크");
                            dataGridView1.Columns[1].Width = 305;
                            dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                            dataGridView1.Columns.Add("속성_버튼", "버튼");
                            dataGridView1.Columns[2].Width = 65;
                            dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                        }));
                    }
                    else
                    {
                        //그리드뷰 리셋
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();
                        dataGridView1.Refresh();
                        //column 설정
                        dataGridView1.Columns.Add("속성_타이틀", "타이틀");
                        dataGridView1.Columns[0].Width = 305;
                        dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dataGridView1.Columns.Add("속성_링크", "링크");
                        dataGridView1.Columns[1].Width = 305;
                        dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dataGridView1.Columns.Add("속성_버튼", "버튼");
                        dataGridView1.Columns[2].Width = 65;
                        dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    for (int i = 1; i <= 10; i++)
                    {
                        string keyWord = HttpUtility.UrlEncode(keyWordTextBox.Text);
                        int pageNum = i * 10 - 9;
                        string url = $"https://search.naver.com/search.naver?where=article&ie=utf8&query={keyWord}&prdtype=0&t=0&st=rel&date_option=0&date_from=&date_to=&srchby=text&dup_remove=1&cafe_url=&without_cafe_url=&board=&sm=tab_pge&start={pageNum.ToString()}";
                        HtmlWeb web = new HtmlWeb();
                        var doc = web.Load(url);

                        var nodes = doc.DocumentNode.SelectNodes("//ul[@id='elThumbnailResultArea']");
                        var NODE = nodes[0].SelectNodes("//a[@class='sh_cafe_title']");
                        foreach (var item in NODE)
                        {
                            if (this.InvokeRequired)
                            {
                                this.Invoke(new MethodInvoker(delegate ()
                                {
                                    int rowIndex = dataGridView1.Rows.Add();
                                    dataGridView1[dataGridView1.Columns["속성_타이틀"].Index, rowIndex].Value = item.InnerText;
                                    dataGridView1[dataGridView1.Columns["속성_링크"].Index, rowIndex].Value = item.GetAttributeValue("href", "").Trim();
                                    dataGridView1.Rows[rowIndex].Cells["속성_버튼"] = new DataGridViewButtonCell();
                                    dataGridView1.Rows[rowIndex].Cells["속성_버튼"].Value = "바로가기";
                                }));
                            }
                            else
                            {
                                int rowIndex = dataGridView1.Rows.Add();
                                dataGridView1[dataGridView1.Columns["속성_타이틀"].Index, rowIndex].Value = item.InnerText;
                                dataGridView1[dataGridView1.Columns["속성_링크"].Index, rowIndex].Value = item.GetAttributeValue("href", "").Trim();
                                dataGridView1.Rows[rowIndex].Cells["속성_버튼"] = new DataGridViewButtonCell();
                                dataGridView1.Rows[rowIndex].Cells["속성_버튼"].Value = "바로가기";
                            }
                        }
                    }
                    Search_Num = 2;
                    SearchFile(j);
                    Thread.Sleep(time);
                }
            }
            catch (Exception Err)
            {
                Console.WriteLine("스레드 종료" + "\n" + Err.Message.ToString());
            }
            finally
            {
                Thread.CurrentThread.Abort();
            }

        }
        private void Kin_Search()
        {
            try
            {
                int time = 0;

                if (oneHour_Radiobutton.Checked)
                {
                    time = 60000;
                }
                else if (threehour_RadioButton.Checked)
                {
                    time = 10800000;
                }
                else if (sixHour_RadioButton.Checked)
                {
                    time = 21600000;
                }

                for (int j = 1; j <= 1000; j++)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            //그리드뷰 리셋
                            dataGridView1.Rows.Clear();
                            dataGridView1.Columns.Clear();
                            dataGridView1.Refresh();
                            //column 설정
                            dataGridView1.Columns.Add("속성_타이틀", "타이틀");
                            dataGridView1.Columns[0].Width = 305;
                            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                            dataGridView1.Columns.Add("속성_링크", "링크");
                            dataGridView1.Columns[1].Width = 305;
                            dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                            dataGridView1.Columns.Add("속성_버튼", "버튼");
                            dataGridView1.Columns[2].Width = 65;
                            dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                        }));
                    }
                    else
                    {
                        //그리드뷰 리셋
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();
                        dataGridView1.Refresh();
                        //column 설정
                        dataGridView1.Columns.Add("속성_타이틀", "타이틀");
                        dataGridView1.Columns[0].Width = 305;
                        dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dataGridView1.Columns.Add("속성_링크", "링크");
                        dataGridView1.Columns[1].Width = 305;
                        dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dataGridView1.Columns.Add("속성_버튼", "버튼");
                        dataGridView1.Columns[2].Width = 65;
                        dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    for (int i = 1; i <= 10; i++)
                    {
                        string keyWord = HttpUtility.UrlEncode(keyWordTextBox.Text);
                        int pageNum = i * 10 - 9;
                        string url = $"https://search.naver.com/search.naver?where=kin&kin_display=10&qt=&title=0&&answer=0&grade=0&choice=0&sec=0&nso=so%3Ar%2Ca%3Aall%2Cp%3Aall&query={keyWord}&c_id=&c_name=&sm=tab_pge&kin_start={pageNum.ToString()}";

                        HtmlWeb html = new HtmlWeb();
                        var doc = html.Load(url);

                        var node = doc.DocumentNode.SelectNodes("//ul[@id='elThumbnailResultArea']");
                        var NODE = node[0].SelectNodes("//dt[@class='question']//a");

                        foreach (var item in NODE)
                        {
                            if (this.InvokeRequired)
                            {
                                this.Invoke(new MethodInvoker(delegate ()
                                {
                                    int rowIndex = dataGridView1.Rows.Add();
                                    dataGridView1[dataGridView1.Columns["속성_타이틀"].Index, rowIndex].Value = item.InnerText;
                                    dataGridView1[dataGridView1.Columns["속성_링크"].Index, rowIndex].Value = item.GetAttributeValue("href", "").Trim();
                                    dataGridView1.Rows[rowIndex].Cells["속성_버튼"] = new DataGridViewButtonCell();
                                    dataGridView1.Rows[rowIndex].Cells["속성_버튼"].Value = "바로가기";
                                }));
                            }
                            else
                            {
                                int rowIndex = dataGridView1.Rows.Add();
                                dataGridView1[dataGridView1.Columns["속성_타이틀"].Index, rowIndex].Value = item.InnerText;
                                dataGridView1[dataGridView1.Columns["속성_링크"].Index, rowIndex].Value = item.GetAttributeValue("href", "").Trim();
                                dataGridView1.Rows[rowIndex].Cells["속성_버튼"] = new DataGridViewButtonCell();
                                dataGridView1.Rows[rowIndex].Cells["속성_버튼"].Value = "바로가기";
                            }
                        }
                    }
                    Search_Num = 3;
                    SearchFile(j);
                    Thread.Sleep(time);
                }
            }
            catch (Exception Err)
            {
                Console.WriteLine("스레드 종료" + "\n" + Err.Message.ToString());
            }
            finally
            {
                Thread.CurrentThread.Abort();
            }
        }
        private void SearchFile(int SearchNum)
        {
            List<string[]> dataList = new List<string[]>();
            int rowCount = dataGridView1.Rows.Count;
            
            if(SearchNum == 1)
            {//처음 파일을 저장하는 경우
                for (int i = 0; i < rowCount; i++)
                {
                    string title = dataGridView1[dataGridView1.Columns["속성_타이틀"].Index, i].Value.ToString();
                    string link = dataGridView1[dataGridView1.Columns["속성_링크"].Index, i].Value.ToString();
                    dataList.Add(new string[2] { title, link });
                }
                SearchFileSave(dataList);
                Console.WriteLine("순위 파일을 처음 저장하였습니다.");
            }
            else
            {
                //SearchFileOpen 함수를 통해 
                //파일 오픈 후의 list dataList에 저장
                dataList = (List<string[]>)SearchFileOpen();
                //(기존리스트)를 새로 업데이트된 리스트와 비교 후
                //새로 편입된 리스트 받아오기

                List<string[]> dList = dataCompare(dataList);

                if (dList != null && dList.Count > 0)
                {
                    for (int i = 0; i < rowCount; i++)
                    {
                        string title = dataGridView1[dataGridView1.Columns["속성_타이틀"].Index, i].Value.ToString();
                        string link = dataGridView1[dataGridView1.Columns["속성_링크"].Index, i].Value.ToString();
                        dataList.Add(new string[2] { title, link });
                    }
                    SearchFileSave(dataList);

                    //dList 댓글 작업 함수로 보내기 
                    reply_Write(dList);
                }
                else
                {
                    Console.WriteLine(SearchNum + "번째 검색중이고, 현재순위 변함 없습니다.");
                }
            }
        }
        private void reply_Write(List<string[]> reply_data)
        {//댓글 자동 or 수동 적기
            Console.WriteLine("새로 편입된 순위가 존재합니다.");
            Console.WriteLine("편입된 목록은 다음과 같습니다.");
            Console.WriteLine();
            for (int i = 0; i < reply_data.Count; i++)
            {
                Console.WriteLine((i + 1).ToString() + "번째 편입된 목록");
                Console.WriteLine("타이틀 : " + reply_data[i][0]);
                Console.WriteLine("링크 : " + reply_data[i][1]);
                Console.WriteLine();
            }
            ParameterizedThreadStart ts = new ParameterizedThreadStart(Call_NewForm);
            Thread thread = new Thread(ts);
            thread.Start(reply_data);
        }
        private void Call_NewForm(object reply_data)
        {
            try
            {
                Form2 form2 = new Form2((List<string[]>)reply_data, user_Info, autoUser, Search_Num, keyWordTextBox.Text);
                form2.ShowDialog();
            }
            catch(Exception Err)
            {
                Console.WriteLine("form2 스레드 종료" + "\n" + Err.Message.ToString());
            }
            finally
            {
                if (Thread.CurrentThread.IsAlive)
                {
                    Thread.CurrentThread.Abort();
                }
            }
        }
        private List<string[]> dataCompare(List<string[]> dataList)
        {
            //기존 데이터 : dataList
            //새로운 데이터 : dataGridView1

            List<string[]> dAttr = new List<string[]>();

            for (int i = 0; i < dataGridView1.Rows.Count ; i++)
            {
                string newtitle = dataGridView1[0, i].Value.ToString(); //새로운타이틀
                string newlink = dataGridView1[1, i].Value.ToString();  //새로운링크
                bool imsi1 = false;  //기존데이터 존재여부

                foreach (var item in dataList)
                {
                    if ((newlink.Equals(item[1])))
                    {
                        imsi1 = true;
                        break;
                    }
                }
                if (imsi1 == false)
                {
                    dAttr.Add(new string[2] { newtitle, newlink });
                }
            }
            return dAttr;
        }
        private void SearchFileSave(List<string[]> dataList)
        {
            string fileName = string.Empty;
            switch (Search_Num)
            {
                case 1:
                    fileName = "SearFSB.bin";
                    break;
                case 2:
                    fileName = "SearFSC.bin";
                    break;
                case 3:
                    fileName = "SearFSZ.bin";
                    break;
            }

            using (FileStream FS = new FileStream(fileName, FileMode.Create))
            {
                BinaryFormatter BF = new BinaryFormatter();
                BF.Serialize(FS, dataList);
            }
        }
        private object SearchFileOpen()
        {
            string fileName = string.Empty;
            switch (Search_Num)
            {
                case 1:
                    fileName = "SearFSB.bin";
                    break;
                case 2:
                    fileName = "SearFSC.bin";
                    break;
                case 3:
                    fileName = "SearFSZ.bin";
                    break;
            }
            using (FileStream FS = new FileStream(fileName, FileMode.Open))
            {
                BinaryFormatter BF = new BinaryFormatter();
                object data = BF.Deserialize(FS);
                return data;
            }
        }
        private void GoToUrl_Data(object url)
        {
            try
            {
                IWebDriver driver = new ChromeDriver();
                driver.Navigate().GoToUrl(url.ToString());
            }
            catch(Exception Err)
            {
                Console.WriteLine("form1의 chrome 스레드 종료"+"\n"+Err.Message.ToString());
                
            }
            finally
            {
                if (Thread.CurrentThread.IsAlive)
                {
                    Thread.CurrentThread.Abort();
                }
            }
        }

        private void autoReplyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (autoReplyCheckBox.Checked)
            {
                uIdTextBox.ReadOnly = false;
                uPwTextBox.ReadOnly = false;
            }
            else
            {
                uIdTextBox.ReadOnly = true;
                uPwTextBox.ReadOnly = true;
            }
        }
    }
}
