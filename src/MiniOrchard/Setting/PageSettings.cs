namespace MiniOrchard.Setting
{
	public class PageSettings : Settings<PageSettings>, IAfterLoadActions
	{
		public int PageSize { get; set; }

		public override bool Enabled
		{
			get { return PageSize > 0; }
		}

		public void AfterLoad()
		{

		}
	}

}
