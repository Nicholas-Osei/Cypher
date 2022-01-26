using System;
using AutoMapper;
using Cypher.Application.Features.Questions.Commands.Create;
using Cypher.Domain.Entities.Cypher;

namespace Cypher.Application.Mappings
{
    public class QuestionProfile: Profile
    {
        public QuestionProfile()
        {
            CreateMap<CreateQuestionsCommand, Question>().ReverseMap();
        }
    }
}
