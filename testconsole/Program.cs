using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Excel = Microsoft.Office.Interop.Excel;
namespace testconsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //string FilePath = @"C:\Users\jinwa\source\repos\SeleniumTestProject\WindowsFormsApp1\bin\Debug\searFSC.bin";
            //using (FileStream FS = new FileStream(FilePath, FileMode.Open))
            //{
            //    BinaryFormatter bf = new BinaryFormatter();
            //    List<string[]> list = (List<string[]>)bf.Deserialize(FS);
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        Console.WriteLine(list[i][0]);
            //        Console.WriteLine(list[i][1]);
            //        Console.WriteLine();
            //    }
            //    Console.Read();
            //}

            //List<string[]> list = new List<string[]>();
            //List<string[]> list2 = new List<string[]>();
            //string FilePath = @"C:\Users\jinwa\source\repos\SeleniumTestProject\WindowsFormsApp1\bin\Debug\searFSC.bin";

            //using (FileStream FS = new FileStream(FilePath, FileMode.Open))
            //{
            //    BinaryFormatter bf = new BinaryFormatter();
            //    list = (List<string[]>)bf.Deserialize(FS);
            //}


            //FilePath = @"C:\Users\jinwa\source\repos\SeleniumTestProject\WindowsFormsApp1\bin\Debug\searFSC.bin";


            //list[4][0] = "===================바뀐파일===================";
            //list[4][1] = @"http://www.seojinwan.com/";

            //list[8][0] = "===================바뀐파일===================";
            //list[8][1] = @"http://www.seojinwan.com/";

            //list[12][0] = "===================바뀐파일===================";
            //list[12][1] = @"http://www.seojinwan.com/";

            //list[16][0] = "===================바뀐파일===================";
            //list[16][1] = @"http://www.seojinwan.com/";

            //list[20][0] = "===================바뀐파일===================";
            //list[20][1] = @"http://www.seojinwan.com/";

            //list[24][0] = "===================바뀐파일===================";
            //list[24][1] = @"http://www.seojinwan.com/";

            //list[28][0] = "===================바뀐파일===================";
            //list[28][1] = @"http://www.seojinwan.com/";

            //list[32][0] = "===================바뀐파일===================";
            //list[32][1] = @"http://www.seojinwan.com/";

            //list[36][0] = "===================바뀐파일===================";
            //list[36][1] = @"http://www.seojinwan.com/";


            //list[40][0] = "===================바뀐파일===================";
            //list[40][1] = @"http://www.seojinwan.com/";

            //using (FileStream FS = new FileStream(FilePath, FileMode.Create))
            //{
            //    BinaryFormatter fs = new BinaryFormatter();
            //    fs.Serialize(FS, list);
            //}

            //string FilePath = @"C:\Users\jinwa\source\repos\SeleniumTestProject\WindowsFormsApp1\bin\Debug\Reple.xlsx";
            //Excel.Application excelApp = new Excel.Application();
            //Excel.Workbook workBook = excelApp.Workbooks.Open(FilePath);
            //Excel.Worksheet workSheet = workBook.ActiveSheet;

            //Excel.Range range = workSheet.UsedRange;

            //object[,] excel_object = range.Value;
            //string[] excel_str = new string[excel_object.GetLength(0)];

            //for (int i = 1; i <= excel_object.GetLength(0); i++)
            //{
            //    Console.WriteLine(excel_object[i,1].ToString());

            //    excel_str[i - 1] = excel_object[i, 1].ToString().Replace(Convert.ToString((char)10), "<br>");
            //}

            //for (int i = 0; i < excel_str.Length; i++)
            //{
            //    Console.WriteLine(excel_str[i]); 
            //}

            //workBook.Close();
            //Console.Read();


            string str = "seo</br>jin</br>wan</br>";
            string str2 = str.Replace("</br>", "\r\n");
            Console.WriteLine(str2);
            Console.Read();
        }
    }
}
