using AutoMapper;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Xml.Serialization;
using System.Linq;

namespace ekitapintegration.EkipdagitimToTcimax.quartz
{
    public class EkipdagitimToTcimaxJob : IJob
    {
        private static string getUrl(string siteUrl, String UrunAdi, string Yazar)
        {
            return string.Format($"{siteUrl}/{UrunAdi.ToLower()}--{Yazar.ToLower()}");
        }

        private static string getKategoryTree(string category1, string category2, string category3, string category4)
        {
            String category = category1;
            if (!String.IsNullOrEmpty(category2))
            {
                category += "/" + category2;
            }
            if (!String.IsNullOrEmpty(category3))
            {
                category += "/" + category3;
            }
            if (!String.IsNullOrEmpty(category4))
            {
                category += "/" + category4;
            }

            return category;
        }


        const string baseUri = "https://testapi.ekipdagitim.com/";
        const string siteUrl = "https://www.mikrokitap.com.tr";

        public void Execute(IJobExecutionContext context)
        {
            IMapper urunMapper = getMapper();

            Ticimax.Root ticimax = new Ticimax.Root
            {
                Urunler = new Ticimax.Urunler
                {
                    Urun = new List<Ticimax.Urun>()
                }
            };

            EkipDagitim.Root productObject = getEkipData();

            foreach (var item in productObject.items)
            {

                var urunItem = urunMapper.Map<EkipDagitim.Item, Ticimax.Urun>(item);

                urunItem.UrunSecenek = new Ticimax.UrunSecenek
                {
                    Secenek = new List<Ticimax.Secenek>
                    {
                        urunMapper.Map<EkipDagitim.Item, Ticimax.Secenek>(item)
                    }
                };

                fillSecenek(item, urunItem);

                ticimax.Urunler.Urun.Add(urunItem);
                Serialize(ticimax);

            }
        }

        private static void fillSecenek(EkipDagitim.Item item, Ticimax.Urun urunItem)
        {
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik = new Ticimax.EkSecenekOzellik();
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik = new List<Ticimax.Ozellik>();
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "productCode", Deger = item.productCode, Text = item.productCode });
            //urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "productDescription", Deger = item.productDescription, Text = item.productDescription });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "productName", Deger = item.productName, Text = item.productName });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "publisher", Deger = item.publisher, Text = item.publisher });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "netDiscount", Deger = item.netDiscount, Text = item.netDiscount });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "impressionsStatus", Deger = item.impressionsStatus, Text = item.impressionsStatus });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "writer", Deger = item.writer, Text = item.writer });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "translator", Deger = item.translator, Text = item.translator });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "paperType", Deger = item.paperType, Text = item.paperType });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "coverCase", Deger = item.coverCase, Text = item.coverCase });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "yearOfPrinting", Deger = item.yearOfPrinting, Text = item.yearOfPrinting });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "numberOfPages", Deger = item.numberOfPages, Text = item.numberOfPages });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "numberOfImpressions", Deger = item.numberOfImpressions, Text = item.numberOfImpressions });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "category1", Deger = item.category1, Text = item.category1 });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "category1Code", Deger = item.category1Code, Text = item.category1Code });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "category2", Deger = item.category2, Text = item.category2 });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "category2Code", Deger = item.category2Code, Text = item.category2Code });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "category3", Deger = item.category3, Text = item.category3 });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "category3Code", Deger = item.category3Code, Text = item.category3Code });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "category4", Deger = item.category4, Text = item.category4 });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "category4Code", Deger = item.category4Code, Text = item.category4Code });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "warehouseStock", Deger = item.warehouseStock.ToString(), Text = item.warehouseStock.ToString() });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "salesPrice", Deger = item.salesPrice.ToString(), Text = item.salesPrice.ToString() });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "taxRate", Deger = item.taxRate.ToString(), Text = item.taxRate.ToString() });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "customsTariffCode", Deger = item.customsTariffCode, Text = item.customsTariffCode });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "images", Deger = item.images.ToString(), Text = item.images.ToString() });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "xmlCurrentCode", Deger = item.xmlCurrentCode, Text = item.xmlCurrentCode });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "barcode1", Deger = item.barcode1, Text = item.barcode1 });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "barcode2", Deger = item.barcode2, Text = item.barcode2 });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "barcode3", Deger = item.barcode3, Text = item.barcode3 });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "createdDate", Deger = item.createdDate.ToString(), Text = item.createdDate.ToString() });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "modifedDate", Deger = item.modifedDate.ToString(), Text = item.modifedDate.ToString() });
            urunItem.UrunSecenek.Secenek[0].EkSecenekOzellik.Ozellik.Add(new Ticimax.Ozellik { Tanim = "id", Deger = item.id, Text = item.id.ToString() });
        }

        private static EkipDagitim.Root getEkipData()
        {
            object data = new
            {
                username = "fuattek",
                password = "666666"
            };
            string jwtToken = POST("", "Account/login", data);

            string productsString = GET(jwtToken, "api/Product/GetProducts?page=1&size=1000");

            EkipDagitim.Root productObject = JsonConvert.DeserializeObject<EkipDagitim.Root>(productsString);
            return productObject;
        }

        private static IMapper getMapper()
        {
            var urunConfiguration = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<EkipDagitim.Item, Ticimax.Urun>()
                  .ForMember(d => d.UrunKartiID, opt => opt.MapFrom(c => c.productCode))
                  .ForMember(d => d.UrunAdi, opt => opt.MapFrom(c => c.productName + "-" + c.writer))
                  .ForMember(d => d.OnYazi, opt => opt.MapFrom(c => String.Format($"<![CDATA[{c.productDescription}]]>")))
                  .ForMember(d => d.Marka, opt => opt.MapFrom(c => c.publisher))
                  .ForMember(d => d.SatisBirimi, opt => opt.MapFrom(c => "ADET"))
                  .ForMember(d => d.KategoriID, opt => opt.MapFrom(c => c.category2Code))//değerlendiriekcek 
                  .ForMember(d => d.Kategori, opt => opt.MapFrom(c => c.category2))
                  .ForMember(d => d.KategoriTree, opt => opt.MapFrom(c => getKategoryTree(c.category1, c.category2, c.category3, c.category4)))
                  .ForMember(d => d.UrunUrl, opt => opt.MapFrom(c => getUrl(siteUrl, c.productName, c.writer)));

                    cfg.CreateMap<EkipDagitim.Item, Ticimax.Secenek>()
                      .ForMember(d => d.StokKodu, opt => opt.MapFrom(c => c.productCode))
                      .ForMember(d => d.Barkod, opt => opt.MapFrom(c => c.productCode))
                      .ForMember(d => d.StokAdedi, opt => opt.MapFrom(c => c.warehouseStock))
                      .ForMember(d => d.AlisFiyati, opt => opt.MapFrom(c => c.salesPrice))
                      .ForMember(d => d.SatisFiyati, opt => opt.MapFrom(c => c.salesPrice))
                      .ForMember(d => d.IndirimliFiyat, opt => opt.MapFrom(c => c.salesPrice))
                      .ForMember(d => d.KDVDahil, opt => opt.MapFrom(c => "true"))
                      .ForMember(d => d.KdvOrani, opt => opt.MapFrom(c => c.taxRate.ToString()))
                      .ForMember(d => d.ParaBirimi, opt => opt.MapFrom(c => "TL"))
                      .ForMember(d => d.ParaBirimiKodu, opt => opt.MapFrom(c => "TRY"))
                      .ForMember(d => d.Desi, opt => opt.MapFrom(c => "1"));

                    cfg.CreateMap<EkipDagitim.Item, Ticimax.Resimler>()
             .ForMember(d => d.Resim, opt => opt.MapFrom(c => new List<string>() { c.images.FirstOrDefault() }));
                }

            );

            return urunConfiguration.CreateMapper();
        }

        public static void Serialize<T>(T dataToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            var path = HostingEnvironment.MapPath("~/ekipdagitim.xml");
            // System.IO.FileStream file = System.IO.File.Create(@"D:\vhosts\revzenkitap.com\httpdocs\ekipdagitim\ekipdagitim.xml");
            System.IO.FileStream file = System.IO.File.Create(path);
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

        public class Login
        {
            public string username;
            public string password;

        }

    }
}