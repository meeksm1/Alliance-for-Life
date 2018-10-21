using System;

namespace Alliance_for_Life.Models
{
    public class SurveyReport
    {
        public int SurveyId { get; set; }

        public int SubcontractorId { get; set; }

        public int MonthId { get; set; }

        public DateTime Date { get; set; }

        public int SurveysCompleted { get; set; }

        public string Orgname { get; set; }

        public string Month { get; set; }
    }
}