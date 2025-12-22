# SLC-AS-IEC_60870_5-104_ImportScriptTemplate

## Summary

This automation script serves as a template for integrating external data sources with the IEC 60870-5-104 connector. It retrieves metadata from external systems and imports it into a DataMiner element using the connector's Import Script feature. The script validates input parameters, retrieves metadata rows, and sends them to the target element via InterApp communication.

## Input Arguments

The script accepts the following input parameters:

- **ImportElementName** (text): The name of the DataMiner element that will receive the imported metadata. This element must be active and running with the IEC 60870-5-104 connector.

- **RequestGuid** (text): A unique identifier (GUID) for the import request. This is used to track and correlate the import operation with the connector.

- **Device** (text): The device identifier or configuration data that specifies the source or target of the metadata retrieval.

## Project Type

**Automation Script** - This is a DataMiner Automation script as indicated by the DMSScript XML file structure.
