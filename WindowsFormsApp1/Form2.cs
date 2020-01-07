using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Excel = Microsoft.Office.Interop.Excel;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        List<string[]> reply_List = new List<string[]>();
        List<string> checkedList = new List<string>();
        int visit_num = 0;
        string[] user_Info = new string[2];
        bool userAuto = false;
        int Search_Num=0;
        string keyWord;
        public Form2(List<string[]> data,string[] userinformation,bool Auto,int Search_type, string keyWord)
        {
            //신규 항목 : reply_List, data
            //유저 정보 : userData, userinformation
            //오토 여부 : Auto, userAuto
            //블로그 카페 지식인 여부 : Search_type, Search_Num (1:블로그 2:카페 3: 지식인)
            InitializeComponent();
            reply_List = data;
            user_Info = userinformation;
            userAuto = Auto;
            Search_Num = Search_type;
            this.keyWord = keyWord;
            default_Screen();

            dataGridView1.CellClick += DataGridView_CellClick;
        }
        private void DataGridView_CellClick(object sender,DataGridViewCellEventArgs e)
        {
            if(sender.Equals(dataGridView1)&&e.RowIndex>=0 && e.ColumnIndex == dataGridView1.Columns["속성_버튼"].Index)
            {
                string title = dataGridView1[dataGridView1.Columns["속성_타이틀"].Index, e.RowIndex].Value.ToString().Trim();
                string link = dataGridView1[dataGridView1.Columns["속성_링크"].Index, e.RowIndex].Value.ToString().Trim();

                //새로운 스레드 생성하면서 링크에 접속
                Thread thread = new Thread(new ParameterizedThreadStart(GoTourl_Data));
                thread.Start(link);

                //현재 log 상황 출력
                if (!(checkedList.Contains(title)))
                {
                    visit_num++;
                    logListBox.Items.Add(title);
                    logListBox.Items.Add(reply_List.Count + "개의 신규 목록 중 " + visit_num.ToString() + "개 방문 완료");
                    logListBox.SelectedIndex = logListBox.Items.Count - 1;
                    checkedList.Add(title);
                }
            }
        }
        private void GoTourl_Data(object url)
        {
            try
            {
                IWebDriver driver = new ChromeDriver();

                //접속
                driver.Navigate().GoToUrl(url.ToString());
            }
            catch (Exception Err)
            {
                Console.WriteLine("form2의 chrome 스레드 종료" + "\n" + Err.Message.ToString());
            }
            finally
            {
                if (Thread.CurrentThread.IsAlive)
                {
                    Thread.CurrentThread.Abort();
                }
            }

        }
        private void default_Screen()
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

            for (int i = 0; i < reply_List.Count; i++)
            {
                int rowIndex = dataGridView1.Rows.Add();
                dataGridView1[0, rowIndex].Value = reply_List[i][0];
                dataGridView1[1, rowIndex].Value = reply_List[i][1];
                dataGridView1.Rows[rowIndex].Cells["속성_버튼"] = new DataGridViewButtonCell();
                dataGridView1.Rows[rowIndex].Cells["속성_버튼"].Value = "바로가기";
            }
            if (userAuto == true)
            {
                logListBox.Items.Add("자동 댓글 사용중...");
                logListBox.Items.Add("레이더에 포착된 글에 댓글을 남기는 중..");
                logListBox.Items.Add("댓글 개수는 하루 50개로 제한되어 있습니다.");
                Thread th = new Thread(new ThreadStart(Auto_Reply));
                th.Start();
            }
            else
            {
                logListBox.Items.Add("수동 댓글 사용중..");
                logListBox.Items.Add("컴퓨터가 사용자의 댓글을 학습합니다..");
                logListBox.Items.Add("내용을 최대한 포괄적으로 기재해주세요.");
            }
        }
        
        private void Auto_Reply()
        {
            string[] reply_text = ExcelOpen();
            int reply_Comp = 0;

            IWebDriver driver = new ChromeDriver();
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            if (Search_Num == 1)
            {//블로그 레이더인 경우
                Auto_Reply_Start(driver);

                //통합검색 후 블로그 블록 찾고 블로그 더보기 클릭
                IWebElement blg_Section = driver.FindElement(By.CssSelector(".blog.section._blogBase._prs_blg"));
                blg_Section.FindElement(By.CssSelector(".go_more")).Click();
                Thread.Sleep(random_sec());

                for (int i = 0; i < 10; i++)
                {//10페이지 기준 10회 반복 

                    //블로그 타이틀 li 리스트 저장
                    var lis = driver.FindElements(By.CssSelector(".sh_blog_top"));
                    Console.WriteLine(i + "번째 페이지 작업중");
                    Thread.Sleep(random_sec());

                    foreach (var item in lis)
                    {
                        IWebElement go_to_title = item.FindElement(By.CssSelector(".sh_blog_title._sp_each_url._sp_each_title"));
                        //타이틀 뽑기
                        string title = go_to_title.GetAttribute("title").ToString();
                        //링크 뽑기
                        string blg_link = go_to_title.GetAttribute("href").ToString();

                        //전달받은 새로 편입리스트와 비교
                        foreach (var item2 in reply_List)
                        {
                            if (title.Equals(item2[0]))
                            {//파싱 제목과 편입리스트의 제목과 같은경우 클릭
                                Console.WriteLine(title);
                                go_to_title.Click();
                                Thread.Sleep(random_sec());

                                reply_Comp++;
                                //댓글가능여부 확인후 댓글남기는 메소드 호출
                                Console.WriteLine("현재 작업 개수 : " + reply_Comp);


                                //새창으로 드라이버 핸들바꾸기
                                driver.SwitchTo().Window(driver.WindowHandles.Last());
                                Thread.Sleep(random_sec());
                                Console.WriteLine("핸들바꾸기 여부 ok");

                                if (blg_link.Contains("blog"))
                                {
                                    //iframe인 경우 iframe으로 핸들바꾸기
                                    try
                                    {
                                        IWebElement element = driver.FindElement(By.Id("mainFrame"));
                                        driver.SwitchTo().Frame(element);
                                        Console.WriteLine("iframe핸들바꾸기 여부 ok");
                                        Thread.Sleep(random_sec());

                                    }
                                    catch (Exception err)
                                    {
                                        Console.WriteLine("예외발생 : " + err.Message.ToString());
                                        //다른 iframe 열기
                                        IWebElement element = driver.FindElement(By.Id("screenFrame"));
                                        driver.SwitchTo().Frame(element);
                                        Console.WriteLine("iframe핸들바꾸기 여부 1번 ok");
                                        Thread.Sleep(random_sec());

                                        //핸들바꾸기
                                        IWebElement element2 = driver.FindElement(By.Id("mainFrame"));
                                        driver.SwitchTo().Frame(element2);
                                        Console.WriteLine("iframe핸들바꾸기 여부 2번 ok");
                                        Thread.Sleep(random_sec());
                                    }

                                    //댓글보기 클릭
                                    //try
                                    //{
                                    IWebElement FirstBtn = driver.FindElement(By.CssSelector(".area_comment.pcol2"));
                                    FirstBtn.FindElement(By.TagName("a")).Click();

                                    Thread.Sleep(random_sec());
                                    Console.WriteLine("댓글보기 클릭 ok");
                                    //}
                                    //catch (Exception err)
                                    //{
                                    // Console.WriteLine("예외발생 : " + err.Message.ToString());
                                    //IWebElement FirstBtn2 = driver.FindElement(By.CssSelector(".area_comment.pcol3"));
                                    //FirstBtn2.FindElement(By.TagName("a")).Click();

                                    //Thread.Sleep(random_sec());
                                    //Console.WriteLine("댓글보기 클릭 ok");
                                    //}
                                    string reple_comment = reply_text[random_int(reply_text.Length)];
                                    //댓글 박스 요소 찾기 -자바스크립트
                                    js.ExecuteScript($"document.querySelector('.u_cbox_text.u_cbox_text_mention').innerHTML='{reple_comment}'");
                                    Thread.Sleep(random_sec());
                                    Console.WriteLine("댓글박스 요소 찾기 클릭 ok");


                                    //핸들 원래대로
                                    driver.SwitchTo().DefaultContent();
                                    driver.Close();
                                    Thread.Sleep(random_sec());


                                    driver.SwitchTo().Window(driver.WindowHandles.First());
                                    Thread.Sleep(random_sec());

                                    break;
                                }
                                else if (blg_link.Contains("tistory"))
                                {
                                    Console.WriteLine("티스토리는 추후 업데이트 예정");
                                    driver.Close();
                                    Thread.Sleep(random_sec());


                                    driver.SwitchTo().Window(driver.WindowHandles.First());
                                    Thread.Sleep(random_sec());

                                    break;
                                }

                            }
                            else
                            {//파싱 제목과 편입리스트와 제목이 서로 다른경우
                                Console.WriteLine(title + " " + item2[0]);
                                Thread.Sleep(200);
                            }
                        }
                    }
                    //작업 개수와 편입리스트의 개수가 같을경우 나가기
                    if (reply_Comp >= reply_List.Count) break;
                    else
                    {//같지 않을 경우 다음페이지 ㄱㄱ
                        IWebElement pageBlock = driver.FindElement(By.CssSelector(".paging"));
                        pageBlock.FindElement(By.CssSelector(".next")).Click();
                        Thread.Sleep(random_sec());
                    }
                }
            }
            else if (Search_Num == 3)
            {//지식인 레이더인 경우 - 키워드 입력 과정까지는 마친상태
                Auto_Reply_Start(driver);

                //더보기 버튼 클릭
                IWebElement zin_Section = driver.FindElement(By.CssSelector(".kinn.section._kinBase"));

                zin_Section.FindElement(By.CssSelector(".go_more")).Click();
                Thread.Sleep(random_sec());

                for(int i = 0; i < 10; i++)
                {//10페이지 기준 10회 반복 

                    //지식인 타이틀 li 리스트 저장
                    IWebElement title_block = driver.FindElement(By.CssSelector("#elThumbnailResultArea"));
                    var title_a_tags = title_block.FindElements(By.TagName("a"));
                    Console.WriteLine((i+1).ToString() + "번째 페이지 작업중");
                    Thread.Sleep(random_sec());

                    foreach (var item in title_a_tags)
                    {
                        //현재 지식인 타이틀
                        string title = item.Text.Trim();
                        //새 편입리스트 reply_List와 비교
                        foreach (var item2 in reply_List)
                        {
                            if (title.Equals(item2[0].ToString().Trim()))
                            {
                                Console.WriteLine(title);
                                item.Click();
                                Thread.Sleep(random_sec());

                                reply_Comp++;
                                //댓글가능여부 확인후 댓글남기는 메소드 호출
                                Console.WriteLine("현재 작업 개수 : " + reply_Comp);


                                //새창으로 드라이버 핸들바꾸기
                                driver.SwitchTo().Window(driver.WindowHandles.Last());
                                Thread.Sleep(random_sec());
                                Console.WriteLine("핸들바꾸기 여부 ok");

                                try
                                {//답변하기가 있다면,
                                    IWebElement reply_btn = driver.FindElement(By.CssSelector("#answerWriteButton"));
                                    //답변하기 버튼 클릭
                                    reply_btn.FindElement(By.CssSelector(".c-button-default__title")).Click();
                                    Thread.Sleep(random_sec());

                                    //답변 블록찾기
                                    js.ExecuteScript(@"var smartEdit = document.getElementById('smartEditor');
                                                        function Find_PTag(seed)
                                                        {
                                                            if(seed.nodeName==='P')
                                                            {
                                                               seed.firstChild.innerHTML = '"
                                                                        + keyWord +
                                                                   @"';
                                                            }
                                                            for(var i=0;i<seed.childNodes.length;i++)
                                                            {
                                                                if(seed.nodeType === document.ELEMENT_NODE)
                                                                {
                                                                    Find_PTag(seed.childNodes[i]);
                                                                }
                                                            }
                                                        }
                                                        Find_PTag(smartEdit);");
                                    Thread.Sleep(random_sec());
                                }
                                catch (Exception Err)
                                {//답변하기가 없다면, 댓글달기
                                    Console.WriteLine("예외상황 : (답변하기 없음)");
                                    Console.WriteLine("답변하기 대신 댓글로 대체" + Err.Message.ToString());

                                    driver.FindElement(By.CssSelector(".icon.icon_compose_opinion")).Click();
                                    Thread.Sleep(random_sec());

                                    //행바꿈을 기준으로 나누어 문자열 배열로 저장 
                                    string reple_comment = reply_text[random_int(reply_text.Length)];
                                    string reple_comment2 = reple_comment.Replace("<br>", ";");
                                    string[] reple_comment3 = reple_comment2.Split(';');

                                    //댓글내용
                                    IWebElement iweb = driver.FindElement(By.CssSelector(".c-opinion__write-textarea.placeholder"));

                                    for (int k = 0; k < reple_comment3.Length; k++)
                                    {
                                        iweb.SendKeys(reple_comment3[i]);
                                        if (k == reple_comment3.Length - 1)
                                        {
                                            iweb.
                                        }
                                    }

                                    Thread.Sleep(random_sec());

                                    //입력버튼 클릭

                                }

                                //핸들 원래대로
                                driver.SwitchTo().DefaultContent();
                                driver.Close();
                                Thread.Sleep(random_sec());


                                driver.SwitchTo().Window(driver.WindowHandles.First());
                                Thread.Sleep(random_sec());

                                break;
                            }
                            else
                            {//파싱 제목과 편입리스트와 제목이 서로 다른경우
                                Console.WriteLine(title + " " + item2[0]);
                                Thread.Sleep(200);
                            }
                        }
                    }
                    //작업 개수와 편입리스트의 개수가 같을경우 나가기
                    if (reply_Comp >= reply_List.Count) break;
                    else
                    {//같지 않을 경우 다음페이지 ㄱㄱ
                        IWebElement pageBlock = driver.FindElement(By.CssSelector(".paging"));
                        pageBlock.FindElement(By.CssSelector(".next")).Click();
                        Thread.Sleep(random_sec());
                    }
                }
            }
            else if (Search_Num == 2)
            {//카페 레이더인 경우 -처음부터
                List<string[]> join_CafeList = new List<string[]>();
                string naver_url = @"https://www.naver.com";

                //접속
                driver.Navigate().GoToUrl(naver_url);
                Thread.Sleep(random_sec());

                driver.FindElement(By.CssSelector(".ico_local_login.lang_ko")).Click();
                Thread.Sleep(random_sec());

                //사용자 정보 입력
                js = (IJavaScriptExecutor)driver;
                js.ExecuteScript($"document.querySelector('#id').value='{user_Info[0]}'");
                Thread.Sleep(random_sec());

                js.ExecuteScript($"document.querySelector('#pw').value='{user_Info[1]}'");
                Thread.Sleep(random_sec());

                driver.FindElement(By.CssSelector(".btn_global")).Click();
                Thread.Sleep(random_sec());

                //네이버 접속시 등록 안함처리
                IWebElement uploadBtn = driver.FindElement(By.CssSelector(".btn_cancel"));
                uploadBtn.FindElement(By.CssSelector(".btn")).Click();
                Thread.Sleep(random_sec());

                //카페탭이동
                IWebElement cafeTab = driver.FindElement(By.CssSelector(".an_a.mn_cafe"));
                cafeTab.FindElement(By.CssSelector(".an_icon")).Click();
                Thread.Sleep(random_sec());

                //내카페관리 클릭
                driver.FindElement(By.CssSelector(".btn_mycafe_edit")).Click();
                Thread.Sleep(random_sec());

                //가입카페 목록확인
                var join_titles = driver.FindElements(By.CssSelector(".cafe_name"));
                Thread.Sleep(random_sec());

                foreach (var item in join_titles)
                {
                    Console.WriteLine($"{item.Text} || "+item.GetAttribute("href"));
                    join_CafeList.Add(new string[] {item.Text, item.GetAttribute("href").ToString()});
                }
                Thread.Sleep(random_sec());

                //메인으로
                driver.FindElement(By.CssSelector(".logo_naver")).Click();
                Thread.Sleep(random_sec());

                //키워드 입력
                js.ExecuteScript($"document.querySelector('#query').value='{keyWord}'");
                Thread.Sleep(random_sec());

                driver.FindElement(By.CssSelector("#search_btn")).Click();
                Thread.Sleep(random_sec());

                //더보기 클릭
                IWebElement cafe_block = driver.FindElement(By.CssSelector(".cafe.section._cafeBase._prs_caf"));
                cafe_block.FindElement(By.CssSelector(".go_more")).Click();
                Thread.Sleep(random_sec());

                //(현재목록 && 편입타이틀과 비교) && 가입카페목록과 비교
                for (int i = 0; i < 10; i++)
                {
                    var titles = driver.FindElements(By.CssSelector(".sh_cafe_title"));
                    Console.WriteLine((i + 1).ToString() + "번째 페이지 작업중");
                    Thread.Sleep(random_sec());

                    foreach (var item in titles)
                    {
                        //타이틀,링크 뽑기
                        string cafe_title = item.Text.Trim();
                        string cafe_link = item.GetAttribute("href").ToString();
                        foreach (var item2 in reply_List)
                        {
                            if (cafe_title.Equals(item2[0].ToString()))
                            {
                                Console.WriteLine("편입목록 찾았음..");
                                Console.WriteLine("가입 카페 목록과 비교...");
                                Thread.Sleep(random_sec());

                                foreach (var item3 in join_CafeList)
                                {
                                    if (cafe_link.Contains(item3[1]))
                                    {
                                        Console.WriteLine(cafe_title);
                                        item.Click();
                                        Thread.Sleep(random_sec());

                                        reply_Comp++;
                                        //댓글가능여부 확인후 댓글남기는 메소드 호출
                                        Console.WriteLine("현재 작업 개수 : " + reply_Comp);


                                        //새창으로 드라이버 핸들바꾸기
                                        driver.SwitchTo().Window(driver.WindowHandles.Last());
                                        Thread.Sleep(random_sec());
                                        Console.WriteLine("핸들바꾸기 여부 ok");

                                        IWebElement element = driver.FindElement(By.Id("cafe_main"));
                                        driver.SwitchTo().Frame(element);


                                        string reple_comment = reply_text[random_int(reply_text.Length)];

                                        js = (IJavaScriptExecutor)driver;
                                        js.ExecuteScript($"document.querySelector('#comment_text').innerHTML='{reple_comment}'");
                                        Thread.Sleep(random_sec());

                                        //확인버튼 클릭


                                        //핸들 원래대로
                                        driver.SwitchTo().DefaultContent();
                                        driver.Close();
                                        Thread.Sleep(random_sec());


                                        driver.SwitchTo().Window(driver.WindowHandles.First());
                                        Thread.Sleep(random_sec());
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"현재 링크 : {cafe_link}    가입 링크 : {item3[1]}");
                                        Thread.Sleep(200);
                                    }
                                }
                            }
                            else
                            {//파싱 제목과 편입리스트와 제목이 서로 다른경우
                                Console.WriteLine(cafe_title + " " + item2[0]);
                                Thread.Sleep(200);
                            }
                        }
                    }
                    //작업 개수와 편입리스트의 개수가 같을경우 나가기
                    if (reply_Comp >= reply_List.Count) break;
                    else
                    {//같지 않을 경우 다음페이지 ㄱㄱ
                        IWebElement pageBlock = driver.FindElement(By.CssSelector(".paging"));
                        pageBlock.FindElement(By.CssSelector(".next")).Click();
                        Thread.Sleep(random_sec());
                    }
                }
            }
            Thread.CurrentThread.Abort();
        }

        private void Auto_Reply_Start(IWebDriver driver)
        {
            string naver_url = @"https://www.naver.com";

            //접속
            driver.Navigate().GoToUrl(naver_url);
            Thread.Sleep(random_sec());

            driver.FindElement(By.CssSelector(".ico_local_login.lang_ko")).Click();
            Thread.Sleep(random_sec());

            //사용자 정보 입력
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript($"document.querySelector('#id').value='{user_Info[0]}'");
            Thread.Sleep(random_sec());

            js.ExecuteScript($"document.querySelector('#pw').value='{user_Info[1]}'");
            Thread.Sleep(random_sec());

            driver.FindElement(By.CssSelector(".btn_global")).Click();
            Thread.Sleep(random_sec());

            //네이버 접속시 등록 안함처리
            IWebElement uploadBtn = driver.FindElement(By.CssSelector(".btn_cancel"));
            uploadBtn.FindElement(By.CssSelector(".btn")).Click();
            Thread.Sleep(random_sec());

            //키워드 입력
            js.ExecuteScript($"document.querySelector('#query').value='{keyWord}'");
            Thread.Sleep(random_sec());

            driver.FindElement(By.CssSelector("#search_btn")).Click();
            Thread.Sleep(random_sec());
        }

        private int random_sec()
        {
            Random rand_sec = new Random();
            return rand_sec.Next(150, 300) * 10;
        }
        private int random_int(int strlen)
        {
            Random rand_int = new Random();
            return rand_int.Next(1, strlen);
        }
        private string[] ExcelOpen()
        {
            string FilePath = @"C:\Users\jinwa\source\repos\SeleniumTestProject\WindowsFormsApp1\bin\Debug\Reple.xlsx";
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workBook = excelApp.Workbooks.Open(FilePath);
            Excel.Worksheet workSheet = workBook.ActiveSheet;

            Excel.Range range = workSheet.UsedRange;

            object[,] excel_object = range.Value;
            string[] excel_str = new string[excel_object.GetLength(0)];

            for (int i = 1; i <= excel_object.GetLength(0); i++)
            {
                excel_str[i - 1] = excel_object[i, 1].ToString().Replace(Convert.ToString((char)10), "<br>");
            }

            workBook.Close();

            return excel_str;
        }

    }
}
