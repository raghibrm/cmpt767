using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace CMPT767_Project.Controllers
{
    /*
     Few Notes:
     Data is retrieved by using various filters. Even if you need to get the whole data, you can use all required filters appropriately. This might sound like a redundant logic but it has been
     implemented this way to keep the logic simple (Keep It Simple Stupid) so that same method is used across different kinds of visualization tasks. 
     * */

    public partial class VisualizationController
    {
        public List<string> get_country_list()
        {
            System.Diagnostics.Debug.WriteLine("Inside get_country_data method");
            List<string> data = new List<string>();

            var excelFile = new FileInfo(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, @"Content\DataFiles\che_per_capita.xlsx"));
            using (ExcelPackage package = new ExcelPackage(excelFile))
            {
                System.Diagnostics.Debug.WriteLine("Inside loop");
                var worksheet = package.Workbook.Worksheets[3];
                int rowCount = worksheet.Dimension.End.Row;

                for (int i = 2; i <= rowCount; i++)
                {
                    data.Add(worksheet.Cells[i, 5].Text.ToString().Trim());
                }
            }
            data = data.Distinct().ToList();
            data.Sort();
            return data;
        }

        public Dictionary<string, string> get_le_hale_data2(string country, string year)
        {
            System.Diagnostics.Debug.WriteLine("Inside get_le_hale_data2");
            System.Diagnostics.Debug.WriteLine("Getting data for country: " + country);
            var data = new Dictionary<string, string>();

            var path = @"Content\DataFiles\le_hale.xlsx";

            var excelFile = new FileInfo(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, path));
            using (ExcelPackage package = new ExcelPackage(excelFile))
            {
                System.Diagnostics.Debug.WriteLine("Inside loop");
                var worksheet = package.Workbook.Worksheets[3];
                int rowCount = worksheet.Dimension.End.Row;

                for (int i = 2; i <= rowCount; i++)
                {
                    if (worksheet.Cells[i, 4].Text.ToString().Trim() == country && worksheet.Cells[i, 2].Text.ToString().Trim()==year)
                    {
                        //{ "dataType": "Male Life Expectancy", "dataValue": 72.3, "year": "2000" },
                        if(worksheet.Cells[i, 6].Text.ToString().Trim() != "")
                        {
                            if (worksheet.Cells[i, 5].Text.ToString().Trim() == "Male")
                            {
                                data.Add("Male Life Expectancy", worksheet.Cells[i, 6].Text.ToString().Trim());
                            }
                            else if (worksheet.Cells[i, 5].Text.ToString().Trim() == "Female")
                            {
                                data.Add("Female Life Expectancy", worksheet.Cells[i, 6].Text.ToString().Trim());
                            }
                            else
                            {
                                data.Add("Overall Life Expectancy", worksheet.Cells[i, 6].Text.ToString().Trim());
                            }
                        }

                        if (worksheet.Cells[i, 9].Text.ToString().Trim() != "")
                        {
                            if (worksheet.Cells[i, 5].Text.ToString().Trim() == "Male")
                            {
                                data.Add("Healthy Male Life Expectancy", worksheet.Cells[i, 9].Text.ToString().Trim());
                            }
                            else if (worksheet.Cells[i, 5].Text.ToString().Trim() == "Female")
                            {
                                data.Add("Healthy Female Life Expectancy", worksheet.Cells[i, 9].Text.ToString().Trim());
                            }
                            else
                            {
                                data.Add("Overall Healthy Life Expectancy", worksheet.Cells[i, 9].Text.ToString().Trim());
                            }
                        }
                            

                    }
                }
            }

            return data;
        }

        public Dictionary<string, string> get_le_hale_data(string[] countries, string year, string dataType = "le_both")
        {
            System.Diagnostics.Debug.WriteLine("Inside get_le_hale_data");
            System.Diagnostics.Debug.WriteLine("Getting data for year: " + year);
            var data = new Dictionary<string, string>();

            var path = @"Content\DataFiles\le_hale.xlsx";// default
            /*
            if (dataType == "oop")
            {
                path = @"Content\DataFiles\le_hale.xlsx";
            }
            else if (dataType == "ext")
            {
                path = @"Content\DataFiles\ext_per_capita.xlsx";
            }
            */

            string sex_identifier = "Both sexes";
            if(dataType.Contains("female"))
            {
                sex_identifier = "Female";
            }
            else if (dataType.Contains("male"))
            {
                sex_identifier = "Male";
            }

            var excelFile = new FileInfo(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, path));
            using (ExcelPackage package = new ExcelPackage(excelFile))
            {
                System.Diagnostics.Debug.WriteLine("Inside loop");
                var worksheet = package.Workbook.Worksheets[3];
                int rowCount = worksheet.Dimension.End.Row;

                for (int i = 2; i <= rowCount; i++)
                {
                    if (worksheet.Cells[i, 2].Text.ToString().Trim() == year && countries.Contains(worksheet.Cells[i, 4].Text.ToString().Trim())
                        && worksheet.Cells[i, 5].Text.ToString().Trim() == sex_identifier)
                    {
                        String Country = worksheet.Cells[i, 4].Text.ToString().Trim();
                        String le = "";
                        if(dataType.Contains("hale"))
                        {
                            le = worksheet.Cells[i, 9].Text.ToString().Trim();
                        }
                        else
                        {
                            le = worksheet.Cells[i, 6].Text.ToString().Trim();
                        }
                        System.Diagnostics.Debug.WriteLine("Year: " + year + ", Country: " + Country + ", "+ dataType + ": " + le);
                        data.Add(Country, le);// update this
                    }
                }
            }

            return data;
        }
        

        public Dictionary<string, string> get_che_oop_ext_per_capita_data(string[] countries, string year, string dataType = "che")
        {
            System.Diagnostics.Debug.WriteLine("Inside get_che_oop_ext_per_capita_data");
            System.Diagnostics.Debug.WriteLine("Getting data for year: " + year);
            var data = new Dictionary<string, string>();

            var path = @"Content\DataFiles\che_per_capita.xlsx";// default (as for che)
            if(dataType=="oop")
            {
                path = @"Content\DataFiles\oop_per_capita.xlsx";
            }
            else if(dataType=="ext")
            {
                path = @"Content\DataFiles\ext_per_capita.xlsx";
            }

            var excelFile = new FileInfo(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, path));
            using (ExcelPackage package = new ExcelPackage(excelFile))
            {
                System.Diagnostics.Debug.WriteLine("Inside loop");
                var worksheet = package.Workbook.Worksheets[3];
                int rowCount = worksheet.Dimension.End.Row;

                for (int i = 2; i <= rowCount; i++)
                {
                    if (worksheet.Cells[i, 2].Text.ToString().Trim() == year && countries.Contains(worksheet.Cells[i, 5].Text.ToString().Trim()))
                    {
                        String country = worksheet.Cells[i, 5].Text.ToString().Trim();
                        String usd = worksheet.Cells[i, 6].Text.ToString().Trim();
                        System.Diagnostics.Debug.WriteLine("DataType: "+ dataType + ", Year: " + year + ", Country: " + country + ", usd: " + usd);
                        data.Add(country, usd);// update this
                    }
                }
            }

            return data;
        }

        

        // DISCARDED
        public Dictionary<string, Double> GetCHEData(string Year)
        {
            System.Diagnostics.Debug.WriteLine("inside func1");
            System.Diagnostics.Debug.WriteLine("Year: " + Year);
            var data = new Dictionary<string, Double>();

            var excelFile = new FileInfo(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, @"Content\DataFiles\che_per_capita.xlsx"));
            using (ExcelPackage package = new ExcelPackage(excelFile))
            {
                System.Diagnostics.Debug.WriteLine("inside func2");
                var worksheet = package.Workbook.Worksheets[3];
                int rowCount = worksheet.Dimension.End.Row;

                for (int i = 2; i <= rowCount; i++)
                {
                    if (worksheet.Cells[i, 2].Text.ToString().Trim() == Year)
                    {
                        String Country = worksheet.Cells[i, 5].Text.ToString().Trim();
                        Double Value = Convert.ToDouble(worksheet.Cells[i, 6].Text.ToString().Trim());
                        System.Diagnostics.Debug.WriteLine("Country: " + Country + ", Value: " + Value);
                        data.Add(Country, Value);// update this
                    }
                }
            }

            return data;
        }

        // combine le and hale into one function
        public Dictionary<string, string> get_le_data(string[] countries, string year)
        {
            System.Diagnostics.Debug.WriteLine("Inside get_le_data");
            System.Diagnostics.Debug.WriteLine("Getting data for year: " + year);
            var data = new Dictionary<string, string>();

            var excelFile = new FileInfo(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, @"Content\DataFiles\le_hale.xlsx"));
            using (ExcelPackage package = new ExcelPackage(excelFile))
            {
                System.Diagnostics.Debug.WriteLine("Inside loop");
                var worksheet = package.Workbook.Worksheets[3];
                int rowCount = worksheet.Dimension.End.Row;

                for (int i = 2; i <= rowCount; i++)
                {
                    if (worksheet.Cells[i, 2].Text.ToString().Trim() == year && countries.Contains(worksheet.Cells[i, 4].Text.ToString().Trim())
                        && worksheet.Cells[i, 5].Text.ToString().Trim() == "Both sexes")
                    {
                        String Country = worksheet.Cells[i, 4].Text.ToString().Trim();
                        String le = worksheet.Cells[i, 6].Text.ToString().Trim();
                        System.Diagnostics.Debug.WriteLine("Year: " + year + ", Country: " + Country + ", le: " + le);
                        data.Add(Country, le);// update this
                    }
                }
            }

            return data;
        }

        public Dictionary<string, string> get_hale_data(string[] countries, string year)
        {
            System.Diagnostics.Debug.WriteLine("Inside get_hale_data");
            System.Diagnostics.Debug.WriteLine("Getting data for year: " + year);
            var data = new Dictionary<string, string>();

            var excelFile = new FileInfo(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, @"Content\DataFiles\le_hale.xlsx"));
            using (ExcelPackage package = new ExcelPackage(excelFile))
            {
                System.Diagnostics.Debug.WriteLine("Inside loop");
                var worksheet = package.Workbook.Worksheets[3];
                int rowCount = worksheet.Dimension.End.Row;

                for (int i = 2; i <= rowCount; i++)
                {
                    if (worksheet.Cells[i, 2].Text.ToString().Trim() == year && countries.Contains(worksheet.Cells[i, 4].Text.ToString().Trim())
                        && worksheet.Cells[i, 5].Text.ToString().Trim() == "Both sexes")
                    {
                        String Country = worksheet.Cells[i, 4].Text.ToString().Trim();
                        String hale = worksheet.Cells[i, 9].Text.ToString().Trim();
                        System.Diagnostics.Debug.WriteLine("Year: " + year + ", Country: " + Country + ", hale: " + hale);
                        data.Add(Country, hale);// update this
                    }
                }
            }

            return data;
        }
    }
}