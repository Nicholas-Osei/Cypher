using System;
namespace Cypher.Application.Features.Questions.Queries.GetAllQuestions
{
    public class GetAllQuestionsResponse
    {
        public int Id { get; set; }
        public string question { get; set; }
        public string Answer { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
    }
}
