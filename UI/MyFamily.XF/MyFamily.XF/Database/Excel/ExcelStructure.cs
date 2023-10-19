using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;

namespace MyFamily.XF.Database.Excel
{

    public class ExcelStructure
    {
        public List<Cell> Headers { get; set; } = new List<Cell>();
        public List<List<Cell>> Values { get; set; } = new List<List<Cell>>();
    }

    public class TransactionSheetStructure : ExcelStructure
    {
        private TransactionSheetStructure() { }



        public static TransactionSheetStructure Get(Transaction transaction)
        {
            TransactionSheetStructure structure = new TransactionSheetStructure();
            structure.Headers = new List<Cell>()
            {
                new Cell() { CellValue = new CellValue("Guid"), DataType = CellValues.String },
                new Cell() { CellValue = new CellValue("Дата"), DataType = CellValues.String },
                new Cell() { CellValue = new CellValue("Категория"), DataType = CellValues.String },
                new Cell() { CellValue = new CellValue("Подкатегория"), DataType = CellValues.String },
                new Cell() { CellValue = new CellValue("Сумма транзакции"), DataType = CellValues.String },
                new Cell() { CellValue = new CellValue("Название"), DataType = CellValues.String },
                new Cell() { CellValue = new CellValue("Описание"), DataType = CellValues.String },
            };

            structure.Values = new List<List<Cell>>()
            { 
                new List<Cell>() {
                 new Cell() { CellValue = new CellValue(transaction.Date), DataType = CellValues.Date },
                 new Cell() { CellValue = new CellValue(transaction.Category), DataType = CellValues.String} ,
                 new Cell() { CellValue = new CellValue(transaction.SubCategory), DataType = CellValues.String  },
                 new Cell() { CellValue = new CellValue(transaction.Amount), DataType = CellValues.Number  },
                 new Cell() { CellValue = new CellValue(transaction.Name), DataType = CellValues.String} ,
                 new Cell() { CellValue = new CellValue(transaction.Description), DataType = CellValues.String } ,
                 new Cell() { CellValue = new CellValue(transaction.Guid.ToString()), DataType = CellValues.String } , }
            };
            return structure;
        }

    }

    public class Transaction
    {
        /// <summary>
        /// Id операции
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Дата и время транзакции.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Категория.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Подкатегория.
        /// </summary>
        public string SubCategory { get; set; }

        /// <summary>
        /// Сумма транзакции.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
    }
}
