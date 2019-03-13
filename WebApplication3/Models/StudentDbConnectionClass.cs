using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication3.HelperClasses;

namespace WebApplication3.Models
{

    public class StudentDbConnectionClass
    {
        public static String selectStudents(String queryString)
        {
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String html = "<ul>";

            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using ( var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        html += "<li> Name: " + reader.GetString(0) + " " + reader.GetString(1) + " </li>";
                    }
                }
                connection.Close();
            }
            html += "</ul>";
            return html;
        }

        public static List<Section> searchSections(String searchType, String searchParameter)
        {
            List<Section> sections = new List<Section>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = "SELECT [section_id],[course_id],[course_name],[instructor],[days],[start_time],[end_time],[semster],[year],[capactiy],[seats_remaining],[type],[building_full_name],[room_number] FROM [HarborViewUniversity].[dbo].[section_view] WHERE instructor LIKE '%"+ searchParameter + "%' order by course_id";
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sections.Add(new Section(reader.GetByte(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7)
                            , reader.GetString(8), reader.GetString(9), reader.GetString(10), reader.GetString(11), reader.GetByte(12), reader.GetByte(13)));
                    }
                }
                connection.Close();
            }

            return sections;
        }
    }

}