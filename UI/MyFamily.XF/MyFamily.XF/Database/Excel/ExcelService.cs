using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MyFamily.XF.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Cell = DocumentFormat.OpenXml.Spreadsheet.Cell;

namespace MyFamily.XF.Database.Excel
{
    /// <summary>
    /// Сервис для работы с экселем
    /// </summary>
    internal class ExcelService
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ExcelService()
        {
            var externalStoragePath = DependencyService.Get<IPathService>().PublicExternalFolder;
            FolderPath = Path.Combine(externalStoragePath, "ExcelBudget");
            FilePath = Path.Combine(FolderPath, "Transactions.xlsx");
            if (!File.Exists(FilePath))
                GenerateExcel();
        }

        /// <summary>
        /// Путь к папке
        /// </summary>
        private string FolderPath { get; }
        /// <summary>
        /// Путь к файлу
        /// </summary>
        private string FilePath { get; }

        #region methods
        private string GenerateExcel()
        {
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            Environment.SetEnvironmentVariable("MONO_URI_DOTNETRELATIVEORABSOLUTE", "true");

            // Creating the SpreadsheetDocument in the indicated FilePath
            var filePath = FilePath;
            using (var document = SpreadsheetDocument.Create(FilePath, SpreadsheetDocumentType.Workbook))
            {
                var wbPart = document.AddWorkbookPart();
                wbPart.Workbook = new Workbook();

                var part = wbPart.AddNewPart<WorksheetPart>();
                part.Worksheet = new Worksheet(new SheetData());
                var part2 = wbPart.AddNewPart<WorksheetPart>();
                part2.Worksheet = new Worksheet(new SheetData());

                //  Here are created the sheets, you can add all the child sheets that you need.
                wbPart.Workbook.AppendChild
                    (
                       new Sheets(
                                new Sheet()
                                {
                                    Id = wbPart.GetIdOfPart(part),
                                    SheetId = 1,
                                    Name = "Transactions"
                                },
                                new Sheet()
                                {
                                    Id = wbPart.GetIdOfPart(part2),
                                    SheetId = 2,
                                    Name = "Categories"
                                }
                    )
                );
                var sheet = part.Worksheet.Elements<SheetData>().First();
                var headers = new List<Cell>()
                {
                    new Cell() { CellValue = new CellValue("Дата"), DataType = CellValues.String },
                    new Cell() { CellValue = new CellValue("Категория"), DataType = CellValues.String },
                    new Cell() { CellValue = new CellValue("Подкатегория"), DataType = CellValues.String },
                    new Cell() { CellValue = new CellValue("Сумма транзакции"), DataType = CellValues.String },
                    new Cell() { CellValue = new CellValue("Название"), DataType = CellValues.String },
                    new Cell() { CellValue = new CellValue("Описание"), DataType = CellValues.String },
                    new Cell() { CellValue = new CellValue("Guid"), DataType = CellValues.String },
                };
                var row = new Row();
                foreach (var header in headers)
                {
                    row.Append(header);
                }
                sheet.InsertAt(row, 0);


                // Just save and close you Excel file
                wbPart.Workbook.Save();
            }

            // Dont't forget return the filePath
            return filePath;
        }

        public void InsertDataIntoTransactionsSheet(ExcelStructure data)
        {
            Environment.SetEnvironmentVariable("MONO_URI_DOTNETRELATIVEORABSOLUTE", "true");

            using (var document = SpreadsheetDocument.Open(FilePath, true))
            {
                var wbPart = document.WorkbookPart;
                var part = wbPart.WorksheetParts.ToList()[0];
                var sheetData = part.Worksheet.Elements<SheetData>().First();

                foreach (var value in data.Values)
                {
                    var dataRow = sheetData.InsertAt(new Row(), 1);

                    foreach (var dataElement in value)
                    {
                        dataRow.Append(dataElement);
                    }
                }
                wbPart.Workbook.Save();
            }
        }

        public List<(string, List<string>)> GetCategories()
        {
            //Environment.SetEnvironmentVariable("MONO_URI_DOTNETRELATIVEORABSOLUTE", "true");

            using (var document = SpreadsheetDocument.Open(FilePath, true))
            {
                var wbPart = document.WorkbookPart;
                var part = wbPart.WorksheetParts.ToList()[1];
                var sheetData = part.Worksheet.Elements<SheetData>().First();

                var categories = new List<(string, List<string>)>();
                foreach (var column in sheetData.Elements<Row>())
                {
                    var cells = column.Elements<Cell>();
                    var category = cells.First().CellValue.Text;
                    var subCategories = cells.Skip(1).Select(p => p.CellValue.Text).ToList();
                    categories.Add((category, subCategories));
                }
                return categories;
            }
        }

        public void AddCategory(string category)
        {
            Environment.SetEnvironmentVariable("MONO_URI_DOTNETRELATIVEORABSOLUTE", "true");

            using (var document = SpreadsheetDocument.Open(FilePath, true))
            {
                var wbPart = document.WorkbookPart;
                var part = wbPart.WorksheetParts.ToList()[1];
                var sheetData = part.Worksheet.Elements<SheetData>().First();

                var row = new Row();
                row = sheetData.InsertAt(row, 0);
                row.Append(new Cell() { CellValue = new CellValue(category), DataType = CellValues.String });
                wbPart.Workbook.Save();
            }
        }

        public void AddSubCategory(string categoryOwner, string subcategory)
        {
            Environment.SetEnvironmentVariable("MONO_URI_DOTNETRELATIVEORABSOLUTE", "true");

            using (var document = SpreadsheetDocument.Open(FilePath, true))
            {
                var wbPart = document.WorkbookPart;
                var part = wbPart.WorksheetParts.ToList()[1];
                var sheetData = part.Worksheet.Elements<SheetData>().First();

                foreach (var column in sheetData.Elements<Row>())
                {
                    var cells = column.Elements<Cell>();
                    var category = cells.First().CellValue.Text;
                    if (category == categoryOwner)
                    {
                        column.Append(new Cell() { CellValue = new CellValue(subcategory), DataType = CellValues.String });
                        break;
                    }
                }
                wbPart.Workbook.Save();
            }
        } 
        #endregion
    }
}
