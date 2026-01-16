using Contracts;
using Microsoft.EntityFrameworkCore;
using QuestionService.Data;

namespace QuestionService.MessageHandlers;

public class VoteCastedHandler(QuestionDbContext db)
{
    public async Task Handle(VoteCasted message)
    {
        switch (message.TargetType)
        {
            case "Question":
                await db.Questions
                    .Where(x => x.Id == message.TargetId)
                    .ExecuteUpdateAsync(setters =>
                        setters.SetProperty(x => x.Votes, x => x.Votes + message.VoteValue));
                break;
            case "Answer":
                await db.Answers
                    .Where(x => x.Id == message.TargetId)
                    .ExecuteUpdateAsync(setters =>
                        setters.SetProperty(x => x.Votes, x => x.Votes + message.VoteValue));
                break;
        }
    }
}