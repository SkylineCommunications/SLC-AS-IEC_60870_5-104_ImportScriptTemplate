# Getting Started with Skyline DataMiner DevOps

Welcome to the Skyline DataMiner DevOps environment!
This quick-start guide will help you get up and running. It was auto-generated based on the initial project setup during creation.
For more details and comprehensive instructions, please visit [DataMiner Docs](https://docs.dataminer.services/).

## Creating a DataMiner application package

This project is configured to create a .dmapp file every time you build the project.

When you compile or build the project, you will find the generated .dmapp in the standard output folder, typically the *bin* folder of your project.

When you publish the project, a corresponding item will be created in the online DataMiner Catalog.

## Migrating to a multi-artifact DataMiner application package

If you need to combine additional components in your .dmapp file, you should:

1. Open the `SLC-AS-IEC_60870_5-104_ImportScriptTemplate.csproj` file and ensure the `<GenerateDataminerPackage>` property is set to `False`.

1. Right-click your solution and select *Add* > *New Project*.

1. Select the *Skyline DataMiner Package Project* template.

Follow the provided **Getting Started** guide in the new project for further instructions.

## Publishing to the Catalog

This project was created with support for publishing to the DataMiner Catalog.
You can publish your artifact either manually via the Visual Studio IDE or by setting up a CI/CD workflow.
## Publishing to the Catalog with Complete CI/CD Workflow

This project includes a complete GitHub workflow with quality gates and code analysis for Catalog publishing.

Follow these steps to set it up:

1. Create a GitHub repository by going to *Git* > *Create Git Repository* in Visual Studio, selecting GitHub, and filling in the wizard before clicking *Create and Push*.

1. Create a SonarCloud project:

   - Visit [https://sonarcloud.io/projects/create](https://sonarcloud.io/projects/create)
   - Create a new project and note its project ID

1. Add the SonarCloud project ID as a repository variable:

   - Navigate to your repository's [Settings > Secrets and variables > Actions > Variables tab](https://github.com/SkylineCommunications/SLC-AS-IEC_60870_5-104_ImportScriptTemplate/settings/secrets/actions)
   - Click *New repository variable*
   - Create a variable named `SONAR_NAME` with the value `SkylineCommunications_SLC-AS-IEC_60870_5-104_ImportScriptTemplate` (format: `[organization]_[repository-name]`)

1. In GitHub, go to the *Actions* tab and check the workflow status.

1. If the workflow reports missing DATAMINER_TOKEN:

   - Obtain an **organization key** from [admin.dataminer.services](https://admin.dataminer.services/) with the following scopes:
     - *Register catalog items*
     - *Read catalog items*
     - *Download catalog versions*
   - Add the key as a secret in your GitHub repository by navigating to *Settings* > *Secrets and variables* > *Actions* and creating a secret named `DATAMINER_TOKEN`
   - Re-run the workflow

With this setup, any push to any branch will trigger the quality workflow with code analysis. Pushes to the main/master branch will generate a new pre-release version, using the latest commit message as the version description.

### Releasing a specific version

1. Navigate to the *<> Code* tab in your GitHub repository.

1. In the menu on the right, select *Releases*.

1. Create a new release, select the desired version as a **tag**, and provide a title and description.

> [!NOTE]
> The description will be visible in the DataMiner Catalog.
