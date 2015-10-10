namespace MiniOrchard.Setting
{
	public class SiteSettings : Settings<SiteSettings>, IAfterLoadActions
	{
		public double RctRat { get; set; }
		public string ImpactPath { get; set; }
		public string SpecCustPath { get; set; }
		public string MajorPath { get; set; }
		public string DemoPath { get; set; }
		public string MajorDemoFile { get; set; }
		public string[] RecordUsers { get; set; }
		public bool ShowMask { get; set; }
		public bool ShowDailyMonitor { get; set; }
		public string[] HighRiskSheetNames { get; set; }
		public string[] LowRiskSheetNames { get; set; }

		public override bool Enabled
		{
			get { return true; }
		}

		public void AfterLoad()
		{

		}
	}
}
