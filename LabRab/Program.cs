using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LabRab
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Введите путь для создания подкаталогов");
            DirectoryInfo directory = new DirectoryInfo(Console.ReadLine() ?? throw new InvalidOperationException());
            DirectoryInfo k1 = directory.CreateSubdirectory("K1");
            DirectoryInfo k2 = directory.CreateSubdirectory("K2");

            FileInfo file = new FileInfo(directory.FullName + @"\K1\t1.txt");
            if (!file.Exists)
            {
                FileStream fs = file.Create();
                fs.Close();
                using (StreamWriter sw = file.AppendText())
                {
                    sw.Write("Иванов Иван Иванович, 1965 года рождения, место жительства г. Саратов");
                }
            }

            FileInfo file2 = new FileInfo(directory.FullName +@"\K1\t2.txt");
            if (!file2.Exists)
            {
                FileStream fs = file2.Create();
                fs.Close();
                using (StreamWriter sw = file2.AppendText())
                {
                    sw.Write("Петров Сергей Федорович, 1966 года рождения, место жительства г.Энгельс");
                }
            }

            FileInfo file3 = new FileInfo(directory.FullName +@"\K2\t3.txt");
            string fromfile1 = "";
            string fromfile2 = "";
                using (StreamReader sr = new StreamReader(file.Open(FileMode.Open, FileAccess.Read)))
                {
                    fromfile1 = sr.ReadToEnd();
                }

                using (StreamReader sr = new StreamReader(file2.Open(FileMode.Open, FileAccess.Read)))
                {
                    fromfile2 = sr.ReadToEnd();
                }
            

            if (!file3.Exists)
            {
                FileStream fs = file3.Create();
                fs.Close();
                
                using (StreamWriter sw = file3.AppendText())
                {
                    sw.WriteLine(fromfile1);
                    sw.WriteLine(fromfile2);
                    
                }
            }

            PrintInfo(file);
            PrintInfo(file2);
            PrintInfo(file3);

            try
            {
                file2.MoveTo(directory.FullName + @"\K2\t2.txt");
                file.CopyTo(directory.FullName + @"\K2\t.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
          

            try
            {
                Directory.Move(directory.FullName + @"\K2", directory.FullName + @"\All");
                Directory.Delete(directory.FullName + @"\K1", true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ;
            }
            


            string[] str = Directory.GetFiles(directory.FullName + @"\All");
            Console.WriteLine("Информацио о всех файлах в папке ALL");
            foreach (string s in str)
            {
                Console.WriteLine(s);
            }

            Console.ReadLine();
        }

        static void PrintInfo(FileInfo file)
        {
            Console.WriteLine($"Имя файла {file.FullName} Расширение {file.Extension} Дата создания файла{file.CreationTime}");
        }
    }
}
