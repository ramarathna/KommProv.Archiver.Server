using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KommProv.Archiver.Server.Data.Models
{
    public class Provision
    {
        [Column("Id", Order = 0)]

        public long Id { get; set; }
        [Column("SALESIDORIG", Order = 1)]
        public string SALESIDORIG { get; set; }

        public decimal LINENUM { get; set; }
        [Column("SALESIDRETURN", Order = 2)]
        public string SALESIDRETURN { get; set; }
        [Column("MZSALESCONCLUSIONDATE", Order = 3)]
        public DateTime MZSALESCONCLUSIONDATE { get; set; }
        [Column("INVOICEDATE", Order = 4)]
        public DateTime INVOICEDATE { get; set; }

        [Column("CHANNELCREATED", Order = 5)]
        public long CHANNELCREATED { get; set; }
        [Column("OPERATINGUNTINUMBERSALE", Order = 6)]
        public string OPERATINGUNTINUMBERSALE { get; set; }
        [Column("STORENUMBERSALE", Order = 7)]
        public string STORENUMBERSALE { get; set; }
        [Column("STAFFIDCREATED", Order = 8)]
        public string STAFFIDCREATED { get; set; }
        [Column("STAFFIDSALES", Order = 9)]
        public string STAFFIDSALES { get; set; }
        [Column("FIRSTNAMECREATED", Order = 10)]
        public string FIRSTNAMECREATED { get; set; }
        [Column("LASTNAMECREATED", Order = 11)]
        public string LASTNAMECREATED { get; set; }
        [Column("FIRSTNAMECUSTOMER", Order = 12)]
        public string FIRSTNAMECUSTOMER { get; set; }
        [Column("LASTNAMECUSTOMER", Order = 13)]
        public string LASTNAMECUSTOMER { get; set; }
        [Column("MZEMPLTYPEID", Order = 14)]
        public string MZEMPLTYPEID { get; set; }
        [Column("MZEMPLTYPEIDSALES", Order = 15)]
        public string MZEMPLTYPEIDSALES { get; set; }
        [Column("FIRSTNAMESALES", Order = 16)]
        public string FIRSTNAMESALES { get; set; }
        [Column("LASTNAMESALES", Order = 17)]
        public string LASTNAMESALES { get; set; }
        [Column("CHANNELSALES", Order = 18)]
        public long CHANNELSALES { get; set; }
        [Column("MZABOTABLE", Order = 19)]
        public long MZABOTABLE { get; set; }
        [Column("ABOID", Order = 20)]
        public string ABOID { get; set; }
        [Column("QTY", Order = 21)]
        public decimal QTY { get; set; }
        [Column("ITEMID", Order = 22)]
        public string ITEMID { get; set; }
        [Column("INVENTSERIALID", Order = 23)]
        public string INVENTSERIALID { get; set; }
        [Column("ORDERACCOUNT", Order = 24)]
        public string ORDERACCOUNT { get; set; }
        [Column("SALESPRICE", Order = 25)]
        public decimal SALESPRICE { get; set; }
        [Column("MANAGERPERSONNELNUMBER", Order = 26)]
        public string MANAGERPERSONNELNUMBER { get; set; }

        [Column("MANAGERNAME", Order = 27)]
        public string MANAGERNAME { get; set; }
        [Column("ABONAME", Order = 28)]
        public string ABONAME { get; set; }
        [Column("ABOGROUPNAME", Order = 29)]
        public string ABOGROUPNAME { get; set; }
        [Column("ABOTYPEID", Order = 30)]
        public string ABOTYPEID { get; set; }
        [Column("PERIODOFVALIDITYMTH", Order = 31)]
        public int? PERIODOFVALIDITYMTH { get; set; }
        [Column("PROVIDERID", Order = 32)]
        public string PROVIDERID { get; set; }
        [Column("ISYOUTHCONTRACT", Order = 33)]
        public int? ISYOUTHCONTRACT { get; set; }
        [Column("HIGHMIDLOW", Order = 34)]
        public int? HIGHMIDLOW { get; set; }
        [Column("SALESPOOLID", Order = 35)]
        public string SALESPOOLID { get; set; }
        [Column("MANUFACTURERCODE", Order = 36)]
        public string MANUFACTURERCODE { get; set; }
        [Column("DIMUMSATZART", Order = 37)]
        public string DIMUMSATZART { get; set; }
        [Column("MZAVGCOSTPRICEPCS_MAX", Order = 38)]
        public decimal? MZAVGCOSTPRICEPCS_MAX { get; set; }
        [Column("MZDISCAMOUNT", Order = 39)]
        public decimal? MZDISCAMOUNT { get; set; }
        [Column("MZDISCOUNTCODE", Order = 40)]
        public string MZDISCOUNTCODE { get; set; }
        [Column("ORGANIZATIONNAME", Order = 41)]
        public string ORGANIZATIONNAME { get; set; }
        [Column("CUSTOMERACCOUNT", Order = 42)]
        public string CUSTOMERACCOUNT { get; set; }
        [Column("SALESORDERPOOLID", Order = 43)]
        public string SALESORDERPOOLID { get; set; }
        [Column("MSISDN", Order = 44)]
        public string MSISDN { get; set; }
        //[Column("CUSTABOCONTRACTID", Order = 44)]
        // public string CUSTABOCONTRACTID { get; set; }
        [Column("INVOICEID", Order = 45)]
        public string INVOICEID { get; set; }
        [Column("RECEIPTID", Order = 46)]
        public string RECEIPTID { get; set; }
        [Column("INVENTTRANSID", Order = 47)]
        public string INVENTTRANSID { get; set; }
        [Column("ECORESCATEGORY_NAME", Order = 48)]
        public string ECORESCATEGORY_NAME { get; set; }
        [Column("VornameInfofeld", Order = 49)]
        public string VornameInfofeld { get; set; }
        [Column("NachnameInfofeld", Order = 50)]
        public string NachnameInfofeld { get; set; }
        [Column("MZSTATUS", Order = 51)]
        public string MZSTATUS { get; set; }
        [Column("AboDetailsRecID", Order = 52)]
        public long AboDetailsRecID { get; set; }
        //Kommprov fields
        [Column("Value", Order = 53)]
        public double Value { get; set; }
        [Column("CustomerName", Order = 54)]
        public string CustomerName { get; set; }
        [Column("ProvisionType", Order = 55)]
        public int ProvisionType { get; set; }
        [Column("Custabocontractid", Order = 56)]
        public string Custabocontractid { get; set; }



        [Column("DeviceName", Order = 57)]
        public string DeviceName { get; set; }
        [Column("ProvisionDirection", Order = 58)]
        public string ProvisionDirection { get; set; }
        [Column("KommisionstraegerId", Order = 59)]
        public string KommisionstraegerId { get; set; }
        [Column("KommisionstraegerName", Order = 60)]
        public string KommisionstraegerName { get; set; }
        [Column("RecievingUnit", Order = 61)]
        public string RecievingUnit { get; set; }
        [Column("RecievingUnitNumber", Order = 62)]
        public string RecievingUnitNumber { get; set; }
        [Column("DiscountReasonId", Order = 63)]
        public string DiscountReasonId { get; set; }
        [Column("DiscountReason", Order = 64)]
        public string DiscountReason { get; set; }
        [Column("RuleFound", Order = 65)]
        public bool RuleFound { get; set; }


        [Column("ProviderName", Order = 66)]

        public string ProviderName { get; set; }

        [Column("LastCalculated", Order = 67)]
        public DateTime LastCalculated { get; set; }


        [Column("ProvisionTypeName", Order = 68)]

        public string ProvisionTypeName { get; set; }
        [Column("Art", Order = 69)]

        public string Art { get; set; }
        [Column("CommsionClass", Order = 70)]

        public string CommsionClass { get; set; }

        [Column("ABOTypeName", Order = 71)]
        public string ABOTypeName { get; set; }
        [Column("OperatingUnitName", Order = 72)]
        public string OperatingUnitName { get; set; }

        [Column("RuleId", Order = 73)]
        public long? RuleId { get; set; }
        [Column("MATChannel", Order = 74)]
        public short? MATChannel { get; set; }


    }
}
