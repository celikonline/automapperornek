using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Xml.Serialization;

namespace ekitapintegration.EkipdagitimToErdem.quartz
{
    public class EkipdagitimToErdemJob : IJob
    {
        public static string baseUri = "https://testapi.ekipdagitim.com/";
        public void Execute(IJobExecutionContext context)
        {
            object data = new
            {
                username = "fuattek",
                password = "666666"
            };
            string jwtToken = POST("", "Account/login", data);

            string productsString = GET(jwtToken, "api/Product/GetProducts?page=1&size=1000");

            dynamic productObject = JsonConvert.DeserializeObject(productsString);
            Erdem_Sonuc Erdem_Sonuc = new Erdem_Sonuc();

            Erdem_Sonuc.Kitap = new List<Kitap>();

            foreach (dynamic item in productObject.items)
            {

                Kitap kitap = new Kitap();
                kitap.Barkod = item.barcode1;
                kitap.Eseradi = item.productName;
                kitap.Fiyat = item.salesPrice;
                kitap.Stokdurum = item.warehouseStock > 0 ? "Stokta" : "StoktaYok";
                kitap.Yasgrubu = "";
                kitap.Sinif = "";
                kitap.Kategori = item.category1;
                kitap.Yayinci = item.publisher;
                kitap.Yazari = item.writer;
                kitap.Sayfa = item.numberOfPages;
                kitap.Basimtarihi = item.yearOfPrinting;
                kitap.Dil = "Türkçe";
                kitap.Ebat = "";
                kitap.Kapakres = item.images[0];
                kitap.Ozet = "<![CDATA[" + item.productDescription + "]]>";
                kitap.Sno = item.productCode;
                Erdem_Sonuc.Kitap.Add(kitap);

                //String productCode = item.productCode;
                //item.productDescription;
                //item.productName;
                //item.publisher;
                //item.netDiscount;
                //item.impressionsStatus;
                //item.writer;
                //item.translator;
                //item.paperType;
                //item.coverCase;
                //item.yearOfPrinting;
                //item.numberOfPages;
                //item.numberOfImpressions;
                //item.category1;
                //item.category1Code;
                //item.category2;
                //item.category2Code;
                //item.category3;
                //item.category3Code;
                //item.category4;
                //item.category4Code;
                //item.warehouseStock;
                //item.salesPrice;
                //item.taxRate;
                //item.customsTariffCode;
                //item.images;
                //item.xmlCurrentCode;
                //item.barcode1;
                //item.barcode2;
                //item.barcode3;
                //item.createdDate;
                //item.modifedDate;
                //item.id;


            }
            Serialize(Erdem_Sonuc);

        }

        public static void Serialize<T>(T dataToSerialize)
        {
           XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            //var path = HostingEnvironment.MapPath("~/ekipdagitim/ekipdagitim.xml");
            System.IO.FileStream file = System.IO.File.Create(@"D:\vhosts\revzenkitap.com\httpdocs\ekipdagitim\ekipdagitim.xml");
            xmlSerializer.Serialize(file, dataToSerialize);
            file.Close();

        }


        public static string GET(string token, string apiadres)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = client.GetAsync(apiadres).Result;
                string resultContent = result.Content.ReadAsStringAsync().Result;
                return resultContent;
            }

        }

        public static string POST(string token, string apiadres, object data)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("token", token);


                var myContent = JsonConvert.SerializeObject(data);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = client.PostAsync(apiadres, byteContent).Result;
                string resultContent = result.Content.ReadAsStringAsync().Result;
                dynamic tokenObject = JsonConvert.DeserializeObject(resultContent);

                return tokenObject.jwtToken;
            }

        }


        //{"userName":"fuattek","jwtToken":"


        //"username": "fuattek",
        //"password": "666666"

        public class Login
        {
            public string username;
            public string password;

        }


    }
}