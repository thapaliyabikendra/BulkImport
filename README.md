# BulkImport

## About this solution
BulkImport is a dynamic and reusable bulk import service that allows bulk data import operations to be seamlessly integrated into different projects. It supports CSV and Excel (.xlsx) file formats, providing validation, unique filtering, and repository persistence features. All the fundamental ABP modules have already been installed, making it highly compatible with ABP-based projects. 

## Features

* Supports CSV and Excel (.xlsx) file formats.

* Configurable file size limit and allowed file extensions.

* Data Validation using FluentValidation.

* Unique identifier filtering to prevent duplicate data entries.

* Asynchronous processing for optimized performance.

* Exception handling for user-friendly error messages.

* Fully compatible with ABP Framework for Domain-Driven Design.

## Technologies Used

* .NET Core (ASP.NET Core Application Services)

* CsvHelper (For CSV parsing)

* FluentValidation (For data validation)

* Ganss.Excel (For Excel file handling)

* Volo.Abp (for domain-driven development and repository management)

* OfficeOpenXml (for Excel processing)

* Blob Storing (ABP Blob Storage & File System Blob Provider)

## Installation

1. Clone the repository:

   git clone https://github.com/yourusername/bulkimport.git

2. Navigate to the project directory:

   cd bulkimport

## Configuration

The BulkImportOptions class allows customization of the import settings. You can configure:

* Allowed file extensions

* Maximum file size

* BlobContainer name

Example:

{
  "AllowedExtensions": [".csv", ".xlsx"],
  "FileSizeLimit": 10485760  // 10 MB,
  "BlobContainerName": "bulk-import-files"
}

## Usage

1. Inject the Service

public class YourService : BulkImportService<MyEntity, Guid, MyDto, MyValidator>
{
    public YourService(IRepository<MyEntity, Guid> repository, IObjectMapper objectMapper, IOptions<BulkImportOptions> options, IMappingProvider<MyDto> mappingProvider,
    IBlobContainerFactory blobContainerFactory) : base(repository, objectMapper, options, mappingProvider, blobContainerFactory)
    {
    }
}

## Validation

The service validates each row before processing. If validation fails, an AbpValidationException is thrown with detailed error messages.

## Exception Handling

* Invalid File Extension: Ensures only allowed file formats are used.

* File Size Exceeded: Restricts oversized files.

* Empty File Handling: Detects if the uploaded file has no data.

* Validation Errors: Reports validation issues per row.

## License

This project is licensed under the MIT License.

## Contact

For any issues, please open an issue on GitHub or contact the maintainer at bikendra.thapayliya@amniltech.com.
