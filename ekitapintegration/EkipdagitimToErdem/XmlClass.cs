
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace ekitapintegration.EkipdagitimToErdem.quartz
{
	[XmlRoot(ElementName = "kitap")]
	public class Kitap
	{
		[XmlElement(ElementName = "barkod")]
		public string Barkod { get; set; }
		[XmlElement(ElementName = "eseradi")]
		public string Eseradi { get; set; }
		[XmlElement(ElementName = "fiyat")]
		public string Fiyat { get; set; }
		[XmlElement(ElementName = "stokdurum")]
		public string Stokdurum { get; set; }
		[XmlElement(ElementName = "yasgrubu")]
		public string Yasgrubu { get; set; }
		[XmlElement(ElementName = "sinif")]
		public string Sinif { get; set; }
		[XmlElement(ElementName = "kategori")]
		public string Kategori { get; set; }
		[XmlElement(ElementName = "yayinci")]
		public string Yayinci { get; set; }
		[XmlElement(ElementName = "yazari")]
		public string Yazari { get; set; }
		[XmlElement(ElementName = "sayfa")]
		public string Sayfa { get; set; }
		[XmlElement(ElementName = "basimtarihi")]
		public string Basimtarihi { get; set; }
		[XmlElement(ElementName = "dil")]
		public string Dil { get; set; }
		[XmlElement(ElementName = "ebat")]
		public string Ebat { get; set; }
		[XmlElement(ElementName = "kapakres")]
		public string Kapakres { get; set; }
		[XmlElement(ElementName = "ozet")]
		public string Ozet { get; set; }
		[XmlAttribute(AttributeName = "s.no")]
		public string Sno { get; set; }
	}

	[XmlRoot(ElementName = "Erdem_Sonuc")]
	public class Erdem_Sonuc
	{
		[XmlElement(ElementName = "kitap")]
		public List<Kitap> Kitap { get; set; }
		[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Xsi { get; set; }
	}

}