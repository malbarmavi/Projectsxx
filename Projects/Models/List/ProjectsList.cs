using System;

namespace Projects.Models
{
    public class ProjectInfo : Project
    {


        public int TaskCount { get; set; }
        public int Undecided { get; set; }
        public int InProcess { get; set; }
        public int Faile { get; set; }
        public int Success { get; set; }
        public Double Precent { get; set; }


    }
}