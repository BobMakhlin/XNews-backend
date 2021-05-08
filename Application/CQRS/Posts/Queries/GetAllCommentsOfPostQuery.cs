using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.CQRS.Comments.Models;
using Application.Persistence.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Posts.Queries
{
    public class GetAllCommentsOfPostQuery : IRequest<IEnumerable<CommentDto>>
    {
        #region Properties

        public Guid PostId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetAllCommentsOfPostQuery, IEnumerable<CommentDto>>
        {
            #region Fields

            private readonly IXNewsDbContext _context;
            private readonly IMapper _mapper;

            #endregion

            #region Constructors

            public Handler(IXNewsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            #endregion

            #region IRequestHandler<GetAllCommentsOfPostQuery, IEnumerable<CommentDto>>

            public async Task<IEnumerable<CommentDto>> Handle(GetAllCommentsOfPostQuery request,
                CancellationToken cancellationToken)
            {
                List<CommentDto> commentsOfPost = await _context.Comment
                    .Where(c => c.PostId == request.PostId)
                    .ProjectToListAsync<CommentDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);

                return CreateCommentHierarchyFromFlatList(commentsOfPost);
            }

            #endregion

            #region Methods

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