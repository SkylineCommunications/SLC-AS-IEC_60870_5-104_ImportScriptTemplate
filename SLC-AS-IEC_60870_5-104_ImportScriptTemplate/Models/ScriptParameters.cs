namespace SLC_AS_IEC_60870_5_104_ImportScriptTemplate.Models
{
	using System;

	public class ScriptParameters
	{
		public string ElementName { get; set; }

		public Guid RequestGuid { get; set; }

		public string Device { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }
	}
}