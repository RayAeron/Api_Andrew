using Api_Andrew.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Andrew.Models
{
    public class ResponseMark
    {
        public ResponseMark(Rating rating)
        {
            id_Rating = rating.id_Rating;
            Teacher_s_Assessment = rating.Teacher_s_Assessment;
            Manager_s_Assessment = rating.Manager_s_Assessment;
            Final_Assessment = rating.Final_Assessment;
            id_Student = (int)rating.id_Student;
            id_Practice = (int)rating.id_Practice;
            id_Teacher = (int)rating.id_Teacher;
            id_Director = (int)rating.id_Director;
        }

        public int id_Rating { get; set; }
        public int Teacher_s_Assessment { get; set; }
        public int Manager_s_Assessment { get; set; }
        public decimal Final_Assessment { get; set; }
        public int id_Student { get; set; }
        public int id_Practice { get; set; }
        public int id_Teacher { get; set; }
        public decimal id_Director { get; set; }
    }
}