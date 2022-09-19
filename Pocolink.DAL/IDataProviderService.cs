using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Pocolink.Models.Models;

namespace Pocolink.DAL
{
    public interface IDataProviderService
    {
        public List<ShortenedUrl> ListDocuments(Expression<Func<ShortenedUrl, bool>> predicate);

        public Task AddDocumentToCollection (ShortenedUrl document);
    }
}