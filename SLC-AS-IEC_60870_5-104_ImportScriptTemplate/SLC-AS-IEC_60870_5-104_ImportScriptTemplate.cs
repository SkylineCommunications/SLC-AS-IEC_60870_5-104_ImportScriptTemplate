namespace SLCASIEC608705104ImportScriptTemplate
{
	using System;
	using System.Collections.Generic;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.ConnectorAPI.IEC_60870_5_104.InterApp;
	using Skyline.DataMiner.ConnectorAPI.IEC_60870_5_104.InterApp.Messages.Requests;
	using Skyline.DataMiner.ConnectorAPI.IEC_60870_5_104.Models;

	using SLC_AS_IEC_60870_5_104_ImportScriptTemplate.Enums;
	using SLC_AS_IEC_60870_5_104_ImportScriptTemplate.Models;

	/// <summary>
	/// Represents a DataMiner Automation script.
	/// </summary>
	public class Script
	{
		/// <summary>
		/// The script entry point.
		/// </summary>
		/// <param name="engine">Link with SLAutomation process.</param>
		public static void Run(IEngine engine)
		{
			try
			{
				RunSafe(engine);
			}
			catch (ScriptAbortException)
			{
				// Catch normal abort exceptions (engine.ExitFail or engine.ExitSuccess)
				throw; // Comment if it should be treated as a normal exit of the script.
			}
			catch (ScriptForceAbortException)
			{
				// Catch forced abort exceptions, caused via external maintenance messages.
				throw;
			}
			catch (ScriptTimeoutException)
			{
				// Catch timeout exceptions for when a script has been running for too long.
				throw;
			}
			catch (InteractiveUserDetachedException)
			{
				// Catch a user detaching from the interactive script by closing the window.
				// Only applicable for interactive scripts, can be removed for non-interactive scripts.
				throw;
			}
			catch (Exception e)
			{
				engine.ExitFail("Run|Something went wrong: " + e);
			}
		}

		private static void RunSafe(IEngine engine)
		{
			ScriptParameters parameters = GetScriptParameters(engine);

			List<MetaDataRow> metaDataRows = GetMetadataRows(engine, parameters);

			SendMetadataToElement(engine, parameters, metaDataRows);
		}

		private static List<MetaDataRow> GetMetadataRows(IEngine engine, ScriptParameters parameters)
		{
			// TODO: Implement metadata retrieval logic here.
			throw new NotImplementedException();
		}

		private static void SendMetadataToElement(IEngine engine, ScriptParameters parameters, List<MetaDataRow> metaDataRows)
		{
			var interAppCalls = new Iec608705104InterAppCalls(engine.GetUserConnection(), parameters.ElementName);

			var importRequest = new ImportMetaDataRequest
			{
				RequestGuid = parameters.RequestGuid,
				MetaDataRows = metaDataRows,
			};

			var response = interAppCalls.SendSingleResponseMessage(importRequest);

			if (response.SuccessStatus)
			{
				engine.ExitSuccess("Data successfully imported.");
			}
			else
			{
				engine.ExitFail($"Import failed: {response.StatusMessage}");
			}
		}

		private static ScriptParameters GetScriptParameters(IEngine engine)
		{
			var parameters = new ScriptParameters
			{
				ElementName = engine.GetScriptParam("ImportElementName").Value,
				RequestGuid = Guid.TryParse(engine.GetScriptParam("RequestGuid").Value, out Guid parsedGuid) ? parsedGuid : Guid.Empty,
			};

			var element = engine.FindElement(parameters.ElementName);

			if (element == null)
			{
				engine.ExitFail($"GetScriptParameters|Element '{parameters.ElementName}' not found.");
			}

			if (!element.IsActive)
			{
				engine.ExitFail($"GetScriptParameters|Element '{parameters.ElementName}' is not running.");
			}

			parameters.Device = Convert.ToString(element.GetParameter((int)ParameterPids.Device));
			parameters.Username = Convert.ToString(element.GetParameter((int)ParameterPids.Username));
			parameters.Password = Convert.ToString(element.GetParameter((int)ParameterPids.Password));

			return parameters;
		}
	}
}