namespace SLC_AS_IEC_60870_5_104_ImportScriptTemplate.Models
{
	using System;

	/// <summary>
	/// Encapsulates the parameters required for the script execution.
	/// </summary>
	public class ScriptParameters
	{
		/// <summary>
		/// Gets or sets the device name associated with the target element.
		/// </summary>
		/// <value>The name of the device linked to the DataMiner element.</value>
		public string Device { get; set; }

		/// <summary>
		/// Gets or sets the name of the DataMiner element.
		/// </summary>
		/// <value>The name of the DataMiner element to which metadata will be sent.</value>
		public string ElementName { get; set; }

		/// <summary>
		/// Gets or sets the password required for authentication with the data source.
		/// </summary>
		/// <value>The password used to authenticate the access to the data source.</value>
		public string Password { get; set; }

		/// <summary>
		/// Gets or sets the unique identifier for the import request.
		/// </summary>
		/// <value>A <see cref="Guid"/> representing the request identifier.</value>
		public Guid RequestGuid { get; set; }

		/// <summary>
		/// Gets or sets the username required for authentication with the data source.
		/// </summary>
		/// <value>The username used to authenticate the access to the data source.</value>
		public string Username { get; set; }
	}
}