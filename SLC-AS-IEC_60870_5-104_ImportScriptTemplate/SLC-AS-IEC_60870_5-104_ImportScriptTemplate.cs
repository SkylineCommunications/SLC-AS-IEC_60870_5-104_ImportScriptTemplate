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
				engine.ExitFail($"Run|Something went wrong: {e}");
			}
		}

		private static void RunSafe(IEngine engine)
		{
			// Retrieve and validate script parameters.
			ScriptParameters parameters = GetScriptParameters(engine);

			// Retrieve metadata rows to be sent to the target element.
			List<MetaDataRow> metaDataRows = GetMetadataRows(engine, parameters);

			// Send metadata to the specified DataMiner element.
			SendMetadataToElement(engine, parameters, metaDataRows);
		}

		/// <summary>
		/// Retrieves a collection of metadata rows.
		/// </summary>
		/// <param name="engine">Link with SLAutomation process. Cannot be <see langword="null"/>.</param>
		/// <param name="parameters">The parameters that define the script context and influence metadata retrieval. Cannot be <see langword="null"/>.</param>
		/// <returns>A list of <see cref="MetaDataRow"/> objects representing the retrieved metadata rows.</returns>
		/// <exception cref="NotImplementedException">Thrown if the method is not yet implemented.</exception>
		private static List<MetaDataRow> GetMetadataRows(IEngine engine, ScriptParameters parameters)
		{
			// TODO: Implement metadata retrieval logic here.
			throw new NotImplementedException();
		}

		/// <summary>
		/// Sends metadata to the specified DataMiner element.
		/// </summary>
		/// <remarks>This method sends metadata to a DataMiner element by creating an import request and invoking the
		/// appropriate InterApp call. If the operation succeeds, the engine exits with a success message. Otherwise,
		/// it exits with a failure message containing the reason for the failure.</remarks>
		/// <param name="engine">Link with SLAutomation process. Cannot be <see langword="null"/>.</param>
		/// <param name="parameters">The parameters containing the request details, including the target element name and request identifier. Cannot be <see langword="null"/>.</param>
		/// <param name="metaDataRows">A list of metadata rows to be sent to the target element. This list must not be null. This list must not be <see langword="null"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="parameters"/> or <paramref name="metaDataRows"/> is <see langword="null"/>.</exception>
		/// <exception cref="Exception">Thrown if the import operation fails due to an error in the InterApp call.</exception>
		private static void SendMetadataToElement(IEngine engine, ScriptParameters parameters, List<MetaDataRow> metaDataRows)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}

			if (metaDataRows == null)
			{
				throw new ArgumentNullException("metaDataRows");
			}

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

		/// <summary>
		/// Retrieves and validates script parameters.
		/// </summary>
		/// <remarks>This method retrieves script parameters, validates the associated element,
		/// and ensures that the element is active and running. If the element is not found or is inactive, the
		/// method terminates the operation with a failure message.</remarks>
		/// <param name="engine">Link with SLAutomation process. Cannot be <see langword="null"/>.</param>
		/// <returns>A <see cref="ScriptParameters"/> object containing the retrieved parameters, including the element name, request
		/// GUID, device, username, and password.</returns>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="engine"/> is <see langword="null"/>.</exception>
		/// <exception cref="Exception">Thrown if the target element is not found or is inactive.</exception>
		private static ScriptParameters GetScriptParameters(IEngine engine)
		{
			if (engine == null)
			{
				throw new ArgumentNullException("engine");
			}

			var parameters = new ScriptParameters
			{
				ElementName = engine.GetScriptParam("ImportElementName").Value,
				RequestGuid = Guid.TryParse(engine.GetScriptParam("RequestGuid").Value, out Guid parsedGuid) ? parsedGuid : Guid.Empty,
				Device = engine.GetScriptParam("Device").Value,
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

			parameters.Username = Convert.ToString(element.GetParameter((int)ParameterPids.Username));
			parameters.Password = Convert.ToString(element.GetParameter((int)ParameterPids.Password));

			return parameters;
		}
	}
}