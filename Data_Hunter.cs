using OpenQA.Selenium; 
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

// Selenium Classından Yararlanarak Web Scraping Yaptık

namespace Data_Hunter_Tool
{
    abstract class imzalar
    {
        public abstract void islem1();
        public abstract void islem2();
        public abstract void islem3();
        public abstract void islem4();
        public abstract void islem5();
        

        public abstract void hakkimizda();
        
    }
    class İslemler:imzalar
    {
        public string tel_no;

        public string domain;
        public string web_waf;
        public string web_sub;

        private string kullanici_ipadress;
        public string Kullanici_ipadress
        {
            get
            {
                return kullanici_ipadress;
            }
            set
            {
                // IP doğrulama: xxx.xxx.xxx.xxx formatında olmalı
                if (Regex.IsMatch(value, @"^(\d{1,3}\.){3}\d{1,3}$"))
                {
                    kullanici_ipadress = value;
                }
                else
                {
                    kullanici_ipadress = null; // Hatalı giriş durumunda null yap
                    Console.WriteLine("Geçersiz IP adresi girdiniz. Lütfen tekrar deneyin.");
                }
            }
        }
        public virtual void Selam_Ver() // Polymorphism kullanarak İki aynı metoda sahip farklı classlar tanımladık
        {
            Console.WriteLine("İslemler sinifindan Selamlar !");
        }
        public override void islem1()
        {
            Console.WriteLine("Lütfen İp Adresini Girin :");
            
            
            kullanici_ipadress = Convert.ToString(Console.ReadLine());
            
            
            if (string.IsNullOrEmpty(kullanici_ipadress)) // IP adresi doğrulanmamışsa işlemi durdur
            {
                Console.WriteLine("Geçerli bir IP adresi girilmedi. İşlem durduruldu.");
                return;
            }
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Minimize();
            driver.Navigate().GoToUrl("https://www.ipsorgu.com/");
            var element = driver.FindElement(By.XPath("/html/body/table/tbody/tr/td/table/tbody/tr[11]/td[2]/form/input[1]")); // internetten veri çektiğimiz için 'var' veri tipini kullandık
            element.Clear();
            element.SendKeys(kullanici_ipadress);
            element.Submit();

            var bilgi = driver.FindElements(By.XPath("/html/body/table/tbody/tr/td/table/tbody/tr[5]/td/div/div[1]/div[2]"));

            if (bilgi.Count > 0)
            {
                foreach (var bil in bilgi)
                {
                    Console.WriteLine("Bilgi: " + bil.Text);
                }
            }
            else
            {
                Console.WriteLine("Belirtilen XPath ile eşleşen bilgi bulunamadı. XPath doğruluğunu kontrol edin.");
            }
            driver.Close();
        }
        public override void islem2()
        {
            IWebDriver driver2 = new ChromeDriver();
            driver2.Manage().Window.Minimize();
            driver2.Navigate().GoToUrl("https://www.ipsorgu.com/");
            var element2 = driver2.FindElement(By.XPath("/html/body/table/tbody/tr/td/table/tbody/tr[11]/td[2]/form/input[1]")); // internetten veri çektiğimiz için var veri tipini kullandık
            element2.Submit();
            var bilgi2 = driver2.FindElements(By.XPath("/html/body/table/tbody/tr/td/table/tbody/tr[5]/td/div/div[1]/div[2]/strong[3]"));
            if (bilgi2.Count > 0)
            {
                foreach (var bil in bilgi2)
                {
                    Console.WriteLine("Bilgi: " + bil.Text);
                }
            }
            else
            {
                Console.WriteLine("Belirtilen XPath ile eşleşen bilgi bulunamadı. XPath doğruluğunu kontrol edin.");
            }
            driver2.Close();
        }

        public override void islem3()
        {

            
            Console.WriteLine("Lütfen İncelemek İstediğiniz Web Sitesinin Domain'ini Girin (www.example.com)");
            domain = Convert.ToString(Console.ReadLine());
            IWebDriver driver3 = new ChromeDriver();
            driver3.Manage().Window.Minimize();
            driver3.Navigate().GoToUrl("https://sitereport.netcraft.com/");
            var element = driver3.FindElement(By.XPath("/html/body/div[2]/div[2]/div/button[1]"));
            element.Click();
            var element3 = driver3.FindElement(By.XPath("/html/body/div[1]/main/header/div/div/div/form/div/input"));
            element3.SendKeys(domain);
            var elementt = driver3.FindElement(By.XPath("/html/body/div[1]/main/header/div/div/div/form/input"));
            elementt.Click();
            var bilgi3 = driver3.FindElements(By.XPath("/html/body/div[1]/main/div[1]/div/section[2]/div[2]"));
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (bilgi3.Count > 0)
            {
                foreach (var bil in bilgi3)
                {
                    Console.WriteLine("Bilgi: " + bil.Text);
                }
            }
            else
            {
                Console.WriteLine("Belirtilen XPath ile eşleşen bilgi bulunamadı. XPath doğruluğunu kontrol edin.");
            }
            Console.ResetColor();
            driver3.Close();
            element3.Submit();
            
            
        }
        
        public override void islem4()
        {
            Console.WriteLine("Lütfen Geçerli Bir Telefon Numarası Giriniz : (örn : 5523522855)");
            tel_no = Convert.ToString(Console.ReadLine());
            IWebDriver driver4 = new ChromeDriver();
            driver4.Manage().Window.Minimize();   
            driver4.Navigate().GoToUrl("https://messente.com/carrier-lookup");
            var element4_ = driver4.FindElement(By.XPath("/html/body/div[2]/div/div[4]/div/div[2]/button[4]"));
            element4_.Click();
            var element4 = driver4.FindElement(By.XPath("/html/body/section[2]/div/div[1]/div/form/div[1]/div/input"));
            element4.SendKeys(tel_no);
            var element_ = driver4.FindElement(By.XPath("/html/body/section[2]/div/div[2]/div/div[1]/div"));
            element_.Click();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("İşleminiz Gerçekleştiriliyor...");
            Console.ResetColor();
            Thread.Sleep(15000);
            Console.Clear();
            var bilgi4 = driver4.FindElements(By.XPath("/html/body/section[2]/div/div[4]/div/div/div"));
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (bilgi4.Count > 0)
            {
                foreach (var bil in bilgi4)
                {
                    Console.WriteLine("Bilgi: " + bil.Text);
                }
            }
            else
            {
                Console.WriteLine("Belirtilen XPath ile eşleşen bilgi bulunamadı. XPath doğruluğunu kontrol edin.");
            }
            Console.ResetColor();
            Console.ResetColor();
            driver4.Close();
            element4.Submit();

        }
        public override void islem5()
        {
            Console.WriteLine("Lütfen Bir Site Giriniz:  (örn www.youtube.com)");
            web_waf = Convert.ToString(Console.ReadLine());
            IWebDriver driver5 = new ChromeDriver();
            driver5.Manage().Window.Minimize();
            driver5.Navigate().GoToUrl("https://www.urlvoid.com/");
            var element5 = driver5.FindElement(By.XPath("/html/body/div[1]/div[1]/div[1]/form/div/input"));
            element5.SendKeys(web_waf);
            var element5_click = driver5.FindElement(By.XPath("/html/body/div[1]/div[1]/div[1]/form/div/span/button"));
            element5_click.Click();
            
            
            var bilgi10 = driver5.FindElements(By.XPath("/html/body/div[1]/div[1]/div[2]/div/main/div/div/div"));
            if (bilgi10.Count > 0)
            {
                foreach (var bil in bilgi10)
                {
                    Console.WriteLine("Bilgi: " + bil.Text);
                }
            }
            else
            {
                Console.WriteLine("Belirtilen XPath ile eşleşen bilgi bulunamadı. XPath doğruluğunu kontrol edin.");
            }
            element5.Submit();
            driver5.Close();
            


        }

        public override void hakkimizda()
        {
            List<string> list = new List<string>();
            list.Add("Ege Şahin");
            list.Add("Berkay Öztürk");
            Console.WriteLine("Bu Uygulamayı Geliştirenler : \n");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }

    }

    class Data_Hunter : İslemler // İnheritance Kullanarak Bir Class'ı Ana Class'a Bağladık
    {
        public byte secenek { get; set; } //prop kullandık

        public override void Selam_Ver() // Polymorphism kullanarak İki aynı metoda sahip farklı classlar tanımladık
        {
            Console.WriteLine("Data_Hunter sinifindan Selamlar !");
        }
        public Data_Hunter() // Class Çalıştığı Anda Constructor Çalışarak Yazıyı Yazar
        {

           
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine("\r\n /$$$$$$$              /$$                      /$$   /$$                       /$$                        \r\n| $$__  $$            | $$                     | $$  | $$                      | $$                        \r\n| $$  \\ $$  /$$$$$$  /$$$$$$    /$$$$$$        | $$  | $$ /$$   /$$ /$$$$$$$  /$$$$$$    /$$$$$$   /$$$$$$ \r\n| $$  | $$ |____  $$|_  $$_/   |____  $$       | $$$$$$$$| $$  | $$| $$__  $$|_  $$_/   /$$__  $$ /$$__  $$\r\n| $$  | $$  /$$$$$$$  | $$      /$$$$$$$       | $$__  $$| $$  | $$| $$  \\ $$  | $$    | $$$$$$$$| $$  \\__/\r\n| $$  | $$ /$$__  $$  | $$ /$$ /$$__  $$       | $$  | $$| $$  | $$| $$  | $$  | $$ /$$| $$_____/| $$      \r\n| $$$$$$$/|  $$$$$$$  |  $$$$/|  $$$$$$$       | $$  | $$|  $$$$$$/| $$  | $$  |  $$$$/|  $$$$$$$| $$      \r\n|_______/  \\_______/   \\___/   \\_______//$$$$$$|__/  |__/ \\______/ |__/  |__/   \\___/   \\_______/|__/      \r\n                                       |______/                                                            \r\n                                                                                                           \r\n                                                                                                           \r\n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Thread.Sleep(2000);
            Console.WriteLine("\r\n  ___         _                  _   ___        ___              ___          _             \r\n |   \\ ___ __(_)__ _ _ _  ___ __| | | _ )_  _  | __|__ _ ___ ___| _ ) ___ _ _| |____ _ _  _ \r\n | |) / -_|_-< / _` | ' \\/ -_) _` | | _ \\ || | | _|/ _` / -_)___| _ \\/ -_) '_| / / _` | || |\r\n |___/\\___/__/_\\__, |_||_\\___\\__,_| |___/\\_, | |___\\__, \\___|   |___/\\___|_| |_\\_\\__,_|\\_, |\r\n               |___/                     |__/      |___/                               |__/ \r\n");
            Console.ResetColor();
            Console.WriteLine("\n\n");
            Secenekler();

        }

        ~Data_Hunter()
        {
            Console.WriteLine("");
            Console.WriteLine("© Telif Hakları Web_Hunter Şirketi Bünyesine Dahildir!"); // Yıkıcı Metod Kullandık
        }

        public void Secenekler()
        {
            Console.WriteLine("Lütfen işlem Seçiniz");
            Console.WriteLine("----------------------------");
            Console.WriteLine("[1] IP Adres Sorgula");
            Console.WriteLine("[2] IP Adresim Ne?");
            Console.WriteLine("[3] Web siteden Bilgi Toplama");
            Console.WriteLine("[4] Telefon Numarasından Bilgi Toplama");
            Console.WriteLine("[5] Web Site FireWall Tespiti");
            Console.WriteLine("[6] Hakkımızda");
            try                                                           //TRY-CATCH YAPISI KULLANILARAK HATA YAKALANDI 
            {
                secenek = Convert.ToByte(Console.ReadLine());
            }
            catch                                                         //TRY-CATCH YAPISI KULLANILARAK HATA YAKALANDI 
            {
                Console.WriteLine("HATA !! , String formatında değer girdiniz !"); 
            }
            switch (secenek)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Green;
                    islem1();
                    Console.ResetColor();
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Green;
                    islem2();
                    Console.ResetColor();
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Green;
                    islem3();
                    Console.ResetColor();
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Green;
                    islem4();
                    Console.ResetColor();
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.Green;
                    islem5();
                    Console.ResetColor();
                    break;
                case 6:
                    Console.ForegroundColor = ConsoleColor.Red;
                    hakkimizda();
                    Console.ResetColor();
                    break;
                default:
                    Console.WriteLine("Lütfen (1-6) sayı giriniz");
                    break;

               


            }
            
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        { 
           Data_Hunter data_Hunter = new Data_Hunter();
        }
    }
}
