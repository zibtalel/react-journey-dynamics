namespace Crm.ErpExtension.BusinessObjects
{
	using System;

	public class InforFb : InforObject
	{
		//Properties
		public InforFirma Firma { get; set; }
		public InforSalesISvc SalesISvc { get; set; }
		public int RNr { get; set; }
		public int? SAint { get; set; }
		public string ANr { get; set; }
		public string MNr { get; set; }
		public string UTNr { get; set; }
		public string Bemerkung { get; set; }
		public string BelegNrRech { get; set; }
		public DateTime? BelegDatRech { get; set; }
		public string BelegNrLief { get; set; }
		public DateTime? BelegDatLief { get; set; }
		public string BelegNrAuft { get; set; }
		public DateTime? BelegDatAuft { get; set; }
		public string BelegNrAng { get; set; }
		public DateTime? BelegDatAng { get; set; }
		public string BelegNrBest { get; set; }
		public DateTime? BelegDatBest { get; set; }
		public string BelegNrAnfr { get; set; }
		public DateTime? BelegDatAnfr { get; set; }
		public DateTime? BelegDat11 { get; set; }
		public string ZDesc { get; set; }
		public int SoKnz1 { get; set; }
		public float? Brutto { get; set; }
		public float? Netto2 { get; set; }
		public string WE { get; set; }
		public int? Zust { get; set; }
		public DateTime? Segm1Term { get; set; }
		public DateTime? Segm2Term { get; set; }
		public DateTime? Segm3Term { get; set; }
		public DateTime? Segm4Term { get; set; }
		public DateTime? Segm5Term { get; set; }
		public DateTime? Segm6Term { get; set; }
		public int? Segm1ZArt { get; set; }
		public int? Segm2ZArt { get; set; }
		public int? Segm3ZArt { get; set; }
		public int? Segm4ZArt { get; set; }
		public int? Segm5ZArt { get; set; }
		public int? Segm6ZArt { get; set; }
		public string ISvcMainStatus { get; set; }
		public string Komm { get; set; }
	}

	public class InforFbA : InforFb
	{
	}

	public class InforFbR : InforFb
	{
	}

	public class InforFbL : InforFb
	{
	}

	public class InforFbB : InforFb
	{
	}

}
