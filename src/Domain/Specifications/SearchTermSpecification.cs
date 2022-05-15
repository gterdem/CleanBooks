using Ardalis.Specification;
using CleanBooks.Domain.Entities;

namespace CleanBooks.Domain.Specifications;

public class SearchTermSpecification : Specification<Book>
{
    public SearchTermSpecification(string queryTerm, string inTitle, string inAuthor, string inPublisher, string subject)
    {
        var query = Query
            .Where(q => q.VolumeInfo.Title.Contains(queryTerm) || q.VolumeInfo.Subtitle.Contains(queryTerm) || q.VolumeInfo.Description.Contains(queryTerm));
        
        if (!string.IsNullOrEmpty(inTitle))
        {
            query = query.Where(q => q.VolumeInfo.Title.Contains(inTitle));
        }

        if (!string.IsNullOrEmpty(inAuthor))
        {
            query = query.Where(q => q.VolumeInfo.Authors.Contains(inAuthor));
        }

        if (!string.IsNullOrEmpty(inPublisher))
        {
            query = query.Where(q => q.VolumeInfo.Publisher.Contains(inPublisher));
        }

        if (!string.IsNullOrEmpty(subject))
        {
            query = query.Where(q => q.VolumeInfo.Categories.Contains(subject));
        }
    }
}