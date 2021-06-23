using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.CQRS.Comments.Models;
using Application.Pagination.Common.Models;
using Application.Pagination.Common.Models.PagedList;
using Application.Persistence.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Posts.Queries.PostComment
{
    public class GetPagedListOfPostCommentsQuery : IRequest<IPagedList<CommentDto>>, IPaginationRequest
    {
        #region Properties

        public Guid PostId { get; set; }

        #endregion

        #region IPaginationRequest

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetPagedListOfPostCommentsQuery, IPagedList<CommentDto>>
        {
            #region Fields

            private readonly IXNewsDbContextExtended _context;
            private readonly IMapper _mapper;

            #endregion

            #region Constructors

            public Handler(IXNewsDbContextExtended context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            #endregion

            #region IRequestHandler<GetPagedListOfPostCommentsQuery, IPagedList<CommentDto>>

            public async Task<IPagedList<CommentDto>> Handle(GetPagedListOfPostCommentsQuery request,
                CancellationToken cancellationToken)
            {
                int rootCommentsCount = await GetRootCommentsCountOfPostAsync(request.PostId, cancellationToken)
                    .ConfigureAwait(false);

                if (rootCommentsCount == 0)
                {
                    await _context.Post.ThrowIfDoesNotExistAsync(request.PostId)
                        .ConfigureAwait(false);
                    return PagedList<CommentDto>.CreateEmptyPagedList(request);
                }

                IEnumerable<CommentDto> commentHierarchy = await GetCommentHierarchyOfPostAsync(request.PostId,
                        request.PageNumber, request.PageSize, cancellationToken)
                    .ConfigureAwait(false);

                IPaginationRequest paginationRequest = request;
                return PagedList<CommentDto>.CreateFromExistingPage(commentHierarchy, rootCommentsCount,
                    paginationRequest);
            }

            #endregion

            #region Methods

            /// <summary>
            /// Returns count of root comments of a post with the specified <paramref name="postId"/>.
            /// Root comment is a comment, that doesn't have a parent comment
            /// (its property <see cref="CommentDto.ParentCommentId"/> is equal to <see langword="null"/>).
            /// </summary>
            /// <param name="postId"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            private async Task<int> GetRootCommentsCountOfPostAsync(Guid postId, CancellationToken cancellationToken)
            {
                return await _context.Comment
                    .Where(c => c.PostId == postId && c.ParentCommentId == null)
                    .CountAsync(cancellationToken)
                    .ConfigureAwait(false);
            }

            /// <summary>
            /// Returns the comment hierarchy of a post with the specified <paramref name="postId"/>,
            /// paginated according to <paramref name="pageNumber"/> and <paramref name="pageSize"/>.
            /// </summary>
            /// <param name="postId"></param>
            /// <param name="pageNumber"></param>
            /// <param name="pageSize"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            private async Task<IEnumerable<CommentDto>> GetCommentHierarchyOfPostAsync(Guid postId, int pageNumber,
                int pageSize, CancellationToken cancellationToken)
            {
                List<CommentDto> commentFlatList = await _context
                    .GetPostComments(postId, pageNumber, pageSize)
                    .AsNoTracking()
                    .ProjectToListAsync<CommentDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);

                IEnumerable<CommentDto> commentHierarchy = CreateCommentHierarchyFromFlatList(commentFlatList);
                return commentHierarchy;
            }

            /// <summary>
            /// Creates hierarchy of comments based on the given <paramref name="commentFlatList"/>.
            /// </summary>
            /// <param name="commentFlatList">
            /// A collection of comments, in which property <see cref="CommentDto.Children"/> of each item is empty.
            /// </param>
            /// <returns>
            /// A collection of root comments (property <see cref="CommentDto.ParentCommentId"/> of each of these comments
            /// is equal to <see langword="null"/>). Property <see cref="CommentDto.Children"/> of each item contains
            /// child-comments of this item and so on.
            /// </returns>
            private IEnumerable<CommentDto> CreateCommentHierarchyFromFlatList(List<CommentDto> commentFlatList)
            {
                Dictionary<Guid, CommentDto> commentDictionary = commentFlatList
                    .ToDictionary(c => c.CommentId);

                foreach (var comment in commentFlatList)
                {
                    if (comment.ParentCommentId == null)
                    {
                        continue;
                    }

                    if (commentDictionary.TryGetValue((Guid) comment.ParentCommentId, out CommentDto parent))
                    {
                        parent.Children.Add(comment);
                    }
                }

                IEnumerable<CommentDto> rootComments = commentFlatList
                    .Where(c => c.ParentCommentId == null);
                return rootComments;
            }

            #endregion
        }

        #endregion
    }
}