﻿using System.Text.RegularExpressions;
using Contracts;
using Typesense;

namespace SearchService.MessageHandlers;

public class QuestionUpdatedHandler(ITypesenseClient client)
{
    public async Task HandleAsync(QuestionUpdated message)
    {
        await client.UpdateDocument("questions", message.QuestionId, new
        {
            message.Title,
            Content = StripHtml(message.Content),
            message.Tags
        });
    }
    
    private static string StripHtml(string content)
    {
        return Regex.Replace(content, "<.*?>", string.Empty);
    }
}