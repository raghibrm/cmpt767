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
    public partial class VisualizationController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult country_selections_che_oop_ext_multiple_countries_graph()
        {
            List<string> countriesList = get_country_list();
            ViewBag.countriesList = countriesList;
            return View();
        }

        public ActionResult country_selection_le_hale_single_country_graph()
        {
            List<string> countriesList = get_country_list();
            ViewBag.countriesList = countriesList;
            return View();
        }

        public ActionResult country_selections_che_le_relationship_graph()
        {
            List<string> countriesList = get_country_list();
            ViewBag.countriesList = countriesList;
            return View();
        }

        public ActionResult che_le_relationship_graph(string[] countries = null)
        {
            // fetch countries- using parameter received
            var totalCountries = countries?.Length;
            System.Diagnostics.Debug.WriteLine("Total countries selected: " + totalCountries);
            String countryListAsString = "";
            for (int i = 0; i < totalCountries; i++)
            {
                System.Diagnostics.Debug.WriteLine("country: " + countries[i]);
                countryListAsString = countryListAsString + countries[i] + ", ";
            }
            countryListAsString = countryListAsString.Remove(countryListAsString.Length - 2, 2);

            // fetch data
            int maxYear = 2016;
            int minYear = 2000;
            int dataTransitionSize = maxYear - minYear + 1;
            var che_datas = new Dictionary<string, string>[dataTransitionSize];
            var ole_datas = new Dictionary<string, string>[dataTransitionSize];
            var ohale_datas = new Dictionary<string, string>[dataTransitionSize];

            for (int i = maxYear; i >= minYear; i--)
            {
                che_datas[i - minYear] = get_che_oop_ext_per_capita_data(countries, i.ToString());
                System.Diagnostics.Debug.WriteLine(che_datas[i - minYear]);

                ole_datas[i - minYear] = get_le_hale_data(countries, i.ToString(), "le_both");
                System.Diagnostics.Debug.WriteLine(ole_datas[i - minYear]);

                ohale_datas[i - minYear] = get_le_hale_data(countries, i.ToString(), "hale_both");
                System.Diagnostics.Debug.WriteLine(ohale_datas[i - minYear]);

            }

            // prepare for view
            ViewBag.countryListAsString = countryListAsString;
            ViewBag.totalCountries = totalCountries;
            ViewBag.dataTransitionSize = dataTransitionSize;
            ViewBag.minYear = minYear;
            ViewBag.che_datas = che_datas;
            ViewBag.ole_datas = ole_datas;
            ViewBag.ohale_datas = ohale_datas;
            return View();
        }

        public ActionResult che_oop_ext_multiple_countries_graph(string[] countries = null) //type = 0 for percentage of gdp, type = 1 for per capita
        {
            // fetch countries- using parameter received
            var totalCountries = countries?.Length;
            System.Diagnostics.Debug.WriteLine("Total countries selected: " + totalCountries);
            String countryListAsString = "";
            for (int i = 0; i < totalCountries; i++)
            {
                System.Diagnostics.Debug.WriteLine("country: " + countries[i]);
                countryListAsString = countryListAsString + countries[i] + ", ";
            }
            countryListAsString = countryListAsString.Remove(countryListAsString.Length - 2, 2);

            // fetch data
            int maxYear = 2016;
            int minYear = 2000;
            int dataTransitionSize = maxYear - minYear + 1;
            var che_datas = new Dictionary<string, string>[dataTransitionSize];
            var oop_datas = new Dictionary<string, string>[dataTransitionSize];
            var ext_datas = new Dictionary<string, string>[dataTransitionSize];

            for (int i = maxYear; i >= minYear; i--)
            {
                che_datas[i - minYear] = get_che_oop_ext_per_capita_data(countries, i.ToString());
                System.Diagnostics.Debug.WriteLine(che_datas[i - minYear]);

                oop_datas[i - minYear] = get_che_oop_ext_per_capita_data(countries, i.ToString(), "oop");
                System.Diagnostics.Debug.WriteLine(oop_datas[i - minYear]);

                ext_datas[i - minYear] = get_che_oop_ext_per_capita_data(countries, i.ToString(), "ext");
                System.Diagnostics.Debug.WriteLine(ext_datas[i - minYear]);

            }

            // prepare for view
            ViewBag.countryListAsString = countryListAsString;
            ViewBag.totalCountries = totalCountries;
            ViewBag.dataTransitionSize = dataTransitionSize;
            ViewBag.minYear = minYear;
            ViewBag.che_datas = che_datas;
            ViewBag.oop_datas = oop_datas;
            ViewBag.ext_datas = ext_datas;
            return View();
        }

        public ActionResult le_hale_single_country_graph(string country)
        {
            // fetch countries- using parameter received
            System.Diagnostics.Debug.WriteLine("Country selected: " + country);

            // fetch data
            int maxYear = 2016;
            int minYear = 2000;
            int dataTransitionSize = maxYear - minYear + 1;
            var dataDict = new Dictionary<string, string>[dataTransitionSize];
            /*
            var le_male_datas = new Dictionary<string, string>[dataTransitionSize];
            var le_female_datas = new Dictionary<string, string>[dataTransitionSize];
            var hale_male_datas = new Dictionary<string, string>[dataTransitionSize];
            var hale_female_datas = new Dictionary<string, string>[dataTransitionSize];
            */
            for (int i = maxYear; i >= minYear; i--)
            {
                dataDict[i - minYear] = get_le_hale_data2(country, i.ToString());
                System.Diagnostics.Debug.WriteLine(dataDict[i - minYear]);
            }

            // prepare for view
            ViewBag.country = country;
            ViewBag.dataTransitionSize = dataTransitionSize;
            ViewBag.minYear = minYear;
            ViewBag.dataDict = dataDict;

            return View();
        }




        /* UNUSED: (but can be extended)


        public ActionResult country_selections_le_hale_multiple_countries_graph()
        {
            List<string> countriesList = get_country_list();
            ViewBag.countriesList = countriesList;
            return View();
        }

        public ActionResult le_hale_multiple_countries_graph(string[] countries = null) //type = 0 for percentage of gdp, type = 1 for per capita
        {
            // fetch countries- using parameter received
            var totalCountries = countries?.Length;
            System.Diagnostics.Debug.WriteLine("Total countries selected: " + totalCountries);
            String countryListAsString = "";
            for (int i = 0; i < totalCountries; i++)
            {
                System.Diagnostics.Debug.WriteLine("country: " + countries[i]);
                countryListAsString = countryListAsString + countries[i] + ", ";
            }
            countryListAsString = countryListAsString.Remove(countryListAsString.Length - 2, 2);

            // fetch data
            int maxYear = 2016;
            int minYear = 2000;
            int dataTransitionSize = maxYear - minYear + 1;
            var le_datas = new Dictionary<string, string>[dataTransitionSize];
            var hale_datas = new Dictionary<string, string>[dataTransitionSize];

            for (int i = maxYear; i >= minYear; i--)
            {
                le_datas[i - minYear] = get_le_hale_data(countries, i.ToString());
                System.Diagnostics.Debug.WriteLine(le_datas[i - minYear]);

                hale_datas[i - minYear] = get_le_hale_data(countries, i.ToString(), "hale_both");
                System.Diagnostics.Debug.WriteLine(hale_datas[i - minYear]);
            }

            // prepare for view
            ViewBag.countryListAsString = countryListAsString;
            ViewBag.totalCountries = totalCountries;
            ViewBag.dataTransitionSize = dataTransitionSize;
            ViewBag.minYear = minYear;
            ViewBag.le_datas = le_datas;
            ViewBag.hale_datas = hale_datas;
            return View();
        }
        */
    }
}