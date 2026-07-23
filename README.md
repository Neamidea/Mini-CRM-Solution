# Mini CRM Solution

A Microsoft Power Platform mini CRM application built to demonstrate Dataverse development, model-driven apps, Power Automate, PCF controls, and custom C# plugins.

---

## Project Overview

This solution allows users to manage customer leads through a simple CRM workflow.

Features include:

- Lead management
- Custom Dataverse tables
- Model-driven application
- Business process automation with Power Automate
- Custom Power Apps Component Framework (PCF) controls
- Custom Dataverse plugins written in C#

---

## Technologies Used

- Microsoft Power Platform
- Dataverse
- Power Automate
- Power Apps Component Framework (PCF)
- TypeScript
- JavaScript
- C#
- .NET
- Visual Studio & Visual Studio Code
- Power Platform CLI (PAC)
- PlugIn Registration Tool
- CSS
- HTML
- ChatGPT
- Microsoft Copilot

---

## Repository Structure

```
Mini-CRM-Solution/
│
├── CrmPlugins/
│   └── dea_customer_plugin/
│   └── dea_lead_plugin/
│
├── EditablePCF/
│
├── MiniCrmUnpackaged/
│   └── Unpacked solution containing client scripts under Web Resources folder
│
├── Screenshots/
│
└── README.md
```

---

## Custom Components

### Editable PCF (TypeScript)

A custom PCF control replacing the standard text field with an editable interface featuring:

- View mode
- Edit mode
- Save functionality
- Dataverse data binding

### dea_customer_plugin Dataverse Plugin (C#, .NET)

A custom Dataverse C# plugin that prevents users from modifying a customer's credit limit when the customer account is marked as Frozen.
The plugin executes before the record is saved and validates the requested change against the previous state of the record using a Pre-Image.

When a customer record is updated, the plugin:

- Retrieves the record being updated (Target)
- Retrieves the configured Pre-Image
- Reads the customer's current status
- Compares the previous credit limit with the new value
- Blocks the save operation if the customer status is Frozen and the credit limit has been modified.

If the validation fails, the user receives the message:

"This customer is frozen. You cannot change credit limit."

### dea_lead_plugin Dataverse Plugin (C#, .NET)

A custom Dataverse C# plugin that automatically formats lead names using a consistent naming convention during record creation and updates.
The plugin executes before the record is saved and ensures every lead follows the organization's required naming format.

When a lead is created or updated, the plugin:

- Retrieves the target lead record
- Reads the Lead Name field
- Verifies that a value exists
- Checks whether the name already starts with LEAD-
- If not, automatically prefixes the lead name using the current year
- Saves the updated value before the record is written to Dataverse

### dea_autoFillOpportunityName Client Script (JavaScript)

When creating a new opportunity and customer topic are selected, set Opportunity Name automatically to a standardized format. Script runs OnSave and OnChange.

### dea_calculateTotalAmount Client Script (JavaScript)

Set Total Amount field to calculated result based on quantity and unit price. Script runs OnChange.

### dea_checkEstimatedRevenue Client Script (JavaScript)

If Estimated Revenue field is set above 10.000, show form notification "High value opportunity - manager review may be required.". Script runs OnChange.

---

## Skills Demonstrated

- Dataverse customization
- Solution management
- Power Platform ALM
- Custom PCF development
- TypeScript development
- C# plugin development
- Git & GitHub

---

## Notes

This repository contains the unpacked Power Platform solution for source control together with the original PCF and plugin source code.
