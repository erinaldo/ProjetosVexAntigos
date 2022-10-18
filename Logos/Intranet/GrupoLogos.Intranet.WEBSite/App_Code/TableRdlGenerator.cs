using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
//using Telerik.Charting.Styles;

namespace DynamicTable
{
    class TableRdlGenerator
    {
        private List<string> m_fields;

        public List<string> Fields
        {
            get { return m_fields; }
            set { m_fields = value; }
        }

        public Rdl.TableType CreateTable()
        {
            Rdl.TableType table = new Rdl.TableType();
            table.Name = "Table1";
             table.Items = new object[]
                {
                    CreateTableColumns(),
                    CreateHeader(),
                    CreateDetails(),
                };
            table.ItemsElementName = new Rdl.ItemsChoiceType21[]
                {
                    Rdl.ItemsChoiceType21.TableColumns,
                    Rdl.ItemsChoiceType21.Header,
                    Rdl.ItemsChoiceType21.Details,                    
                };
            return table;
        }

        private Rdl.HeaderType CreateHeader()
        {
            Rdl.HeaderType header = new Rdl.HeaderType();
            header.Items = new object[]
                {
                    CreateHeaderTableRows(),
                };
            header.ItemsElementName = new Rdl.ItemsChoiceType20[]
                {
                    Rdl.ItemsChoiceType20.TableRows,
                };
            return header;
        }

        private Rdl.TableRowsType CreateHeaderTableRows()
        {
            Rdl.TableRowsType headerTableRows = new Rdl.TableRowsType();
            headerTableRows.TableRow = new Rdl.TableRowType[] { CreateHeaderTableRow() };
            return headerTableRows;
        }

        private Rdl.TableRowType CreateHeaderTableRow()
        {
            Rdl.TableRowType headerTableRow = new Rdl.TableRowType();
            headerTableRow.Items = new object[] { CreateHeaderTableCells(), "0.2cm" };
            return headerTableRow;
        }

        private Rdl.TableCellsType CreateHeaderTableCells()
        {
            Rdl.TableCellsType headerTableCells = new Rdl.TableCellsType();
            headerTableCells.TableCell = new Rdl.TableCellType[m_fields.Count];
            for (int i = 0; i < m_fields.Count; i++)
            {
                headerTableCells.TableCell[i] = CreateHeaderTableCell(m_fields[i]);
            }
            return headerTableCells;
        }

        private Rdl.TableCellType CreateHeaderTableCell(string fieldName)
        {
            Rdl.TableCellType headerTableCell = new Rdl.TableCellType();
            headerTableCell.Items = new object[] { CreateHeaderTableCellReportItems(fieldName) };
            return headerTableCell;
        }

        private Rdl.ReportItemsType CreateHeaderTableCellReportItems(string fieldName)
        {
            Rdl.ReportItemsType headerTableCellReportItems = new Rdl.ReportItemsType();
            headerTableCellReportItems.Items = new object[] { CreateHeaderTableCellTextbox(fieldName) };
            return headerTableCellReportItems;
        }

        private Rdl.TextboxType CreateHeaderTableCellTextbox(string fieldName)
        {
            Rdl.TextboxType headerTableCellTextbox = new Rdl.TextboxType();
            headerTableCellTextbox.Name = fieldName + "_Header";

            if (fieldName != "Filial")
            {
                fieldName = fieldName.Substring(0, fieldName.Length - 1);

                if (fieldName.Substring(fieldName.Length - 1) == "1" || fieldName.Substring(fieldName.Length - 1) == "2" || fieldName.Substring(fieldName.Length - 1) == "3")
                {
                    fieldName = fieldName.Substring(0, fieldName.Length - 1);
                }

                if (fieldName == "TRANSITTIME")
                    fieldName = "TRANSIT TIME";

                if (fieldName == "QTNF")
                    fieldName = "NF ENTREGUES";

                if (fieldName == "NF")
                    fieldName = "% NF";

                if (fieldName == "NF" || fieldName == "PNFNAOENTREGUE")
                    fieldName = "% NF";

                if (fieldName == "TOTALENTREGUE")
                    fieldName = "TOTAL NF ENTREGUES";

                if (fieldName == "NFNAOENTREGUE")
                    fieldName = " NF NÃO ENTREGUES";

                if (fieldName == "TOTALGERA")
                    fieldName = "TOTAL DE NOTAS";

                

            }

            headerTableCellTextbox.Items = new object[] 
                {
                    fieldName,
                    CreateHeaderTableCellTextboxStyle(headerTableCellTextbox.Name),
                    true,
                    "1cm",                    
                    "0.1in",                    
                };
            headerTableCellTextbox.ItemsElementName = new Rdl.ItemsChoiceType14[] 
                {
                    Rdl.ItemsChoiceType14.Value,
                    Rdl.ItemsChoiceType14.Style,
                    Rdl.ItemsChoiceType14.CanGrow,
                    Rdl.ItemsChoiceType14.Width,
                    Rdl.ItemsChoiceType14.Height,

                   
                };
            return headerTableCellTextbox;
        }

        private Rdl.StyleType CreateHeaderTableCellTextboxStyle(string h)
        {
            Rdl.StyleType headerTableCellTextboxStyle = new Rdl.StyleType();

            if (h == "Filial_Header")
            {

                headerTableCellTextboxStyle.Items = new object[]
                {
                    "700",
                    "9pt",      
                    CreateBorderColorStyleWidth("black"),
                    CreateBorderColorStyleWidth("Solid"),
                    CreateBorderColorStyleWidth("1pt"),
                };
                headerTableCellTextboxStyle.ItemsElementName = new Rdl.ItemsChoiceType5[]
                {
                    Rdl.ItemsChoiceType5.FontWeight,
                    Rdl.ItemsChoiceType5.FontSize,
                    Rdl.ItemsChoiceType5.BorderColor,
                    Rdl.ItemsChoiceType5.BorderStyle,
                    Rdl.ItemsChoiceType5.BorderWidth,
                   
                                
                };
            }
            else
            {
                headerTableCellTextboxStyle.Items = new object[]
                {
                    "700",
                    "9pt",
                    "Right",
                    CreateBorderColorStyleWidth("black"),
                    CreateBorderColorStyleWidth("Solid"),
                    CreateBorderColorStyleWidth("0.5pt"),
                };
                headerTableCellTextboxStyle.ItemsElementName = new Rdl.ItemsChoiceType5[]
                {
                    Rdl.ItemsChoiceType5.FontWeight,
                    Rdl.ItemsChoiceType5.FontSize,
                    Rdl.ItemsChoiceType5.TextAlign,
                    Rdl.ItemsChoiceType5.BorderColor,
                    Rdl.ItemsChoiceType5.BorderStyle,
                    Rdl.ItemsChoiceType5.BorderWidth,                    

                };
            }

            return headerTableCellTextboxStyle;
        }

        private Rdl.DetailsType CreateDetails()
        {
            Rdl.DetailsType details = new Rdl.DetailsType();
            details.Items = new object[] { CreateTableRows() };
            return details;
        }

        private Rdl.TableRowsType CreateTableRows()
        {
            Rdl.TableRowsType tableRows = new Rdl.TableRowsType();
            tableRows.TableRow = new Rdl.TableRowType[] { CreateTableRow() };
            return tableRows;
        }

        private Rdl.TableRowType CreateTableRow()
        {
            Rdl.TableRowType tableRow = new Rdl.TableRowType();
            tableRow.Items = new object[] { CreateTableCells(), "0.25in" };
            return tableRow;
        }

        private Rdl.TableCellsType CreateTableCells()
        {
            Rdl.TableCellsType tableCells = new Rdl.TableCellsType();
            tableCells.TableCell = new Rdl.TableCellType[m_fields.Count];
            for (int i = 0; i < m_fields.Count; i++)
            {
                tableCells.TableCell[i] = CreateTableCell(m_fields[i]);
            }
            return tableCells;
        }

        private Rdl.TableCellType CreateTableCell(string fieldName)
        {
            Rdl.TableCellType tableCell = new Rdl.TableCellType();
            tableCell.Items = new object[] { CreateTableCellReportItems(fieldName) };
            return tableCell;
        }

        private Rdl.ReportItemsType CreateTableCellReportItems(string fieldName)
        {
            Rdl.ReportItemsType reportItems = new Rdl.ReportItemsType();
            reportItems.Items = new object[] { CreateTableCellTextbox(fieldName)};
            return reportItems;
        }

        private Rdl.TextboxType CreateTableCellTextbox(string fieldName)
        {
            Rdl.TextboxType textbox = new Rdl.TextboxType();
            textbox.Name = fieldName;
            textbox.Items = new object[] 
                {
                    "=Fields!" + fieldName + ".Value",
                    CreateTableCellTextboxStyle(fieldName),
                    true,
                    "0.1cm",
                 
                };
            textbox.ItemsElementName = new Rdl.ItemsChoiceType14[] 
                {
                    Rdl.ItemsChoiceType14.Value,
                    Rdl.ItemsChoiceType14.Style,
                    Rdl.ItemsChoiceType14.CanGrow,
                    Rdl.ItemsChoiceType14.Height,
                };

            
            return textbox;
        }

        private Rdl.StyleType CreateTableCellTextboxStyle(string fieldName)
        {
            Rdl.StyleType style = new Rdl.StyleType();


            if (fieldName == "Filial")
            {
                style.Items = new object[]
                {
                    "=iif(RowNumber(Nothing) mod 2, \"White\", \"White\")", "Left", "200","7pt","Verdana",
                    CreateBorderColorStyleWidth("black"),
                    CreateBorderColorStyleWidth("Solid"),
                    CreateBorderColorStyleWidth("0.5pt"),
                };

                style.ItemsElementName = new Rdl.ItemsChoiceType5[]
                {
                    Rdl.ItemsChoiceType5.BackgroundColor,
                    Rdl.ItemsChoiceType5.TextAlign,
                    Rdl.ItemsChoiceType5.FontWeight,
                    Rdl.ItemsChoiceType5.FontSize,    
                    Rdl.ItemsChoiceType5.FontFamily,
                    Rdl.ItemsChoiceType5.BorderColor,
                    Rdl.ItemsChoiceType5.BorderStyle,
                    Rdl.ItemsChoiceType5.BorderWidth,
                    
                };
            }
            else
            {
                style.Items = new object[]
                {
                    "=iif(RowNumber(Nothing) mod 2, \"White\", \"White\")", 
                    "Right",  "200","7pt", "Verdana",
                    CreateBorderColorStyleWidth("black"),
                    CreateBorderColorStyleWidth("Solid"),
                    CreateBorderColorStyleWidth("0.5pt"),

                };
                style.ItemsElementName = new Rdl.ItemsChoiceType5[]
                {
                    Rdl.ItemsChoiceType5.BackgroundColor,
                    Rdl.ItemsChoiceType5.TextAlign,
                    Rdl.ItemsChoiceType5.FontWeight,
                    Rdl.ItemsChoiceType5.FontSize,                    
                    Rdl.ItemsChoiceType5.FontFamily,
                    Rdl.ItemsChoiceType5.BorderColor,
                    Rdl.ItemsChoiceType5.BorderStyle,
                    Rdl.ItemsChoiceType5.BorderWidth,
       

                    
                };
            }
            return style;
        }

        private Rdl.TableColumnsType CreateTableColumns()
        {
            Rdl.TableColumnsType tableColumns = new Rdl.TableColumnsType();            
            tableColumns.TableColumn = new Rdl.TableColumnType[m_fields.Count];
            for (int i = 0; i < m_fields.Count; i++)
            {
                tableColumns.TableColumn[i] = CreateTableColumn(m_fields[i].ToString());
            }
            return tableColumns;
        }

        private Rdl.TableColumnType CreateTableColumn(string nomeColuna)
        {
            //Rdl.StyleType style = new Rdl.StyleType();
            //style.Items = new object[]
            //    {
            //        "Solid","1pt",
            //    };

            //style.ItemsElementName = new Rdl.ItemsChoiceType5[]
            //    {
            //        Rdl.ItemsChoiceType5.BorderStyle,
            //        Rdl.ItemsChoiceType5.BorderWidth,
            //    };


            
            Rdl.TableColumnType tableColumn = new Rdl.TableColumnType();
            if (nomeColuna == "Filial")
                tableColumn.Items = new object[] {"5cm"};
            else
                tableColumn.Items = new object[] { "2cm"};

            
                
                return tableColumn;
        }

        Rdl.BorderColorStyleWidthType CreateBorderColorStyleWidth(string s)
        {
            Rdl.BorderColorStyleWidthType b = new Rdl.BorderColorStyleWidthType();
            b.Items = new object[]
                 {
                     s,
                 };
            b.ItemsElementName = new Rdl.ItemsChoiceType3[]
                 {
                     Rdl.ItemsChoiceType3.Default,
                 };
            return b;
        }


    }
}
