

using System.Xml.Serialization;
using System.Collections.Generic;
using System;

namespace ekitapintegration.EkipdagitimToTcimax.EkipDagitim
{
    public class Item
    {
        public string productCode { get; set; }
        public string productDescription { get; set; }
        public string productName { get; set; }
        public string publisher { get; set; }
        public string netDiscount { get; set; }
        public string impressionsStatus { get; set; }
        public string writer { get; set; }
        public string translator { get; set; }
        public string paperType { get; set; }
        public string coverCase { get; set; }
        public string yearOfPrinting { get; set; }
        public string numberOfPages { get; set; }
        public string numberOfImpressions { get; set; }
        public string category1 { get; set; }
        public string category1Code { get; set; }
        public string category2 { get; set; }
        public string category2Code { get; set; }
        public string category3 { get; set; }
        public string category3Code { get; set; }
        public string category4 { get; set; }
        public string category4Code { get; set; }
        public double warehouseStock { get; set; }
        public double salesPrice { get; set; }
        public double taxRate { get; set; }
        public string customsTariffCode { get; set; }
        public List<string> images { get; set; }
        public string xmlCurrentCode { get; set; }
        public string barcode1 { get; set; }
        public string barcode2 { get; set; }
        public string barcode3 { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime modifedDate { get; set; }
        public string id { get; set; }
    }

    public class Root
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int totalCount { get; set; }
        public int totalPages { get; set; }
        public List<Item> items { get; set; }
    }
}

