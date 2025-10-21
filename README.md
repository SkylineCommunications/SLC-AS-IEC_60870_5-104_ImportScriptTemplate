# SLC-AS-IEC_60870_5-104_ImportScriptTemplate

## About

This repository contains a DataMiner Automation script template designed to import metadata into a DataMiner element using the IEC 60870-5-104 protocol. The script template validates input parameters, retrieves metadata, and sends it to the specified DataMiner element.

### About DataMiner

DataMiner is a transformational platform that provides vendor-independent control and monitoring of devices and services. Out of the box and by design, it addresses key challenges such as security, complexity, multi-cloud, and much more. It has a pronounced open architecture and powerful capabilities enabling users to evolve easily and continuously.

The foundation of DataMiner is its powerful and versatile data acquisition and control layer. With DataMiner, there are no restrictions to what data users can access. Data sources may reside on premises, in the cloud, or in a hybrid setup.

A unique catalog of 7000+ connectors already exists. In addition, you can leverage DataMiner Development Packages to build your own connectors (also known as "protocols" or "drivers").

> **Note**
> See also: [About DataMiner](https://aka.dataminer.services/about-dataminer).

### About Skyline Communications

At Skyline Communications, we deal with world-class solutions that are deployed by leading companies around the globe. Check out [our proven track record](https://aka.dataminer.services/about-skyline) and see how we make our customers' lives easier by empowering them to take their operations to the next level.

## Prerequisites

- **DataMiner Environment**: This script is compatible with DataMiner environments running version 10.3 or higher.

## How It Works

1. **Script Entry Point**: The `Run` method serves as the entry point for the script. It handles exceptions and ensures safe execution.
2. **Parameter Validation**: The `GetScriptParameters` method retrieves and validates the script parameters, ensuring the target element is active.
3. **Metadata Retrieval**: The `GetMetadataRows` method retrieves metadata rows to be sent to the target element. **(To be implemented)**
4. **Metadata Import**: The `SendMetadataToElement` method sends the metadata to the specified DataMiner element using InterApp calls.

The `ScriptParameters` class encapsulates the parameters required for the script:
- **`Device`**: The name of the device linked to the DataMiner element.
- **`ElementName`**: The name of the DataMiner element to which metadata will be sent.
- **`RequestGuid`**: A GUID representing the request identifier.
- **`Username`**: The username used to authenticate the access to the data source.
- **`Password`**: The password used to authenticate the access to the data source.

## To-Do

- Implement the **`GetMetadataRows`** method to retrieve metadata rows dynamically.

## License

This project is licensed under the [MIT License](LICENSE).

---

For more information about DataMiner and its Automation API, visit the [official documentation](https://docs.dataminer.services/).