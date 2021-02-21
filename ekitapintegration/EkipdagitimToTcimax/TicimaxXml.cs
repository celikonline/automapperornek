
using System.Xml.Serialization;
using System.Collections.Generic;
namespace ekitapintegration.EkipdagitimToTcimax.Ticimax
{

    [XmlRoot(ElementName = "Resimler")]
    public class Resimler
    {
        [XmlElement(ElementName = "Resim")]
        public List<string> Resim { get; set; }
    }

    [XmlRoot(ElementName = "Ozellik")]
    public class Ozellik
    {
        [XmlAttribute(AttributeName = "Tanim")]
        public string Tanim { get; set; }
        [XmlAttribute(AttributeName = "Deger")]
        public string Deger { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "EkSecenekOzellik")]
    public class EkSecenekOzellik
    {
        [XmlElement(ElementName = "Ozellik")]
        public List<Ozellik> Ozellik { get; set; }
    }

    [XmlRoot(ElementName = "Secenek")]
    public class Secenek
    {
        [XmlElement(ElementName = "VaryasyonID")]
        public string VaryasyonID { get; set; }
        [XmlElement(ElementName = "StokKodu")]
        public string StokKodu { get; set; }
        [XmlElement(ElementName = "Barkod")]
        public string Barkod { get; set; }
        [XmlElement(ElementName = "StokAdedi")]
        public string StokAdedi { get; set; }
        [XmlElement(ElementName = "AlisFiyati")]
        public string AlisFiyati { get; set; }
        [XmlElement(ElementName = "SatisFiyati")]
        public string SatisFiyati { get; set; }
        [XmlElement(ElementName = "IndirimliFiyat")]
        public string IndirimliFiyat { get; set; }
        [XmlElement(ElementName = "KDVDahil")]
        public string KDVDahil { get; set; }
        [XmlElement(ElementName = "KdvOrani")]
        public string KdvOrani { get; set; }
        [XmlElement(ElementName = "ParaBirimi")]
        public string ParaBirimi { get; set; }
        [XmlElement(ElementName = "ParaBirimiKodu")]
        public string ParaBirimiKodu { get; set; }
        [XmlElement(ElementName = "Desi")]
        public string Desi { get; set; }
        [XmlElement(ElementName = "EkSecenekOzellik")]
        public EkSecenekOzellik EkSecenekOzellik { get; set; }
    }

    [XmlRoot(ElementName = "UrunSecenek")]
    public class UrunSecenek
    {
        [XmlElement(ElementName = "Secenek")]
        public List<Secenek> Secenek { get; set; }
    }

    [XmlRoot(ElementName = "Urun")]
    public class Urun
    {
        [XmlElement(ElementName = "UrunKartiID")]
        public string UrunKartiID { get; set; }
        [XmlElement(ElementName = "UrunAdi")]
        public string UrunAdi { get; set; }
        [XmlElement(ElementName = "OnYazi")]
        public string OnYazi { get; set; }
        [XmlElement(ElementName = "Aciklama")]
        public string Aciklama { get; set; }
        [XmlElement(ElementName = "Marka")]
        public string Marka { get; set; }
        [XmlElement(ElementName = "SatisBirimi")]
        public string SatisBirimi { get; set; }
        [XmlElement(ElementName = "KategoriID")]
        public string KategoriID { get; set; }
        [XmlElement(ElementName = "Kategori")]
        public string Kategori { get; set; }
        [XmlElement(ElementName = "KategoriTree")]
        public string KategoriTree { get; set; }
        [XmlElement(ElementName = "UrunUrl")]
        public string UrunUrl { get; set; }
        [XmlElement(ElementName = "Resimler")]
        public Resimler Resimler { get; set; }
        [XmlElement(ElementName = "UrunSecenek")]
        public UrunSecenek UrunSecenek { get; set; }
        [XmlElement(ElementName = "TeknikDetaylar")]
        public string TeknikDetaylar { get; set; }
    }

    [XmlRoot(ElementName = "Urunler")]
    public class Urunler
    {
        [XmlElement(ElementName = "Urun")]
        public List<Urun> Urun { get; set; }
    }

    [XmlRoot(ElementName = "Marka")]
    public class Marka
    {
        [XmlElement(ElementName = "MarkaID")]
        public string MarkaID { get; set; }
        [XmlElement(ElementName = "Tanim")]
        public string Tanim { get; set; }
    }

    [XmlRoot(ElementName = "Markalar")]
    public class Markalar
    {
        [XmlElement(ElementName = "Marka")]
        public List<Marka> Marka { get; set; }
    }

    [XmlRoot(ElementName = "Root")]
    public class Root
    {
        [XmlElement(ElementName = "Urunler")]
        public Urunler Urunler { get; set; }
        [XmlElement(ElementName = "Markalar")]
        public Markalar Markalar { get; set; }
    }

}

